package stoplichtserver;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketTimeoutException;
import javax.swing.JTextArea;
import dataTypes.EData;
import dataTypes.EDirection;
import dataTypes.EId;
import dataTypes.ELight;
import dataTypes.EMode;
import dataTypes.EType;

public class Port extends Thread {
	
	public static final int		PORT_NUMER		= 1000;
	public static final int		MESSAGE_LENGTH	= 4;
	
	private volatile boolean	receiving		= true;
	
	private ServerSocket		server;
	private Socket				socket;
	private JTextArea			area;
	
	DataOutputStream			out;
	
	public Port(JTextArea area) {
	
		this.area = area;
		
	}
	
	@Override
	public void run() {
	
		//create the socket server object
		try {
			
			server = new ServerSocket(PORT_NUMER);
			
			while (receiving) {
				
				if (socket != null && socket.isConnected()) {
					try {
						
						DataInputStream in = new DataInputStream(socket.getInputStream());
						byte[] data = new byte[MESSAGE_LENGTH];
						in.read(data);
						
						receivePacket(data);
						
					} catch (SocketTimeoutException s) {
						
						area.append("Socket timed out!" + '\n');
						break;
						
					} catch (IOException e) {
						
						break;
					}
					
				} else {
					
					try {
						
						area.append("Waiting for client on port " + server.getLocalPort() + "..." + '\n');
						socket = server.accept();
						area.append("Just connected to " + socket.getRemoteSocketAddress());
						out = new DataOutputStream(socket.getOutputStream());
						
						sendTestData();
						
					} catch (SocketTimeoutException s) {
						
						area.append("Socket timed out!");
						break;
						
					} catch (IOException e) {
						
						break;
						
					}
				}
				
			}
			
		} catch (Exception e) {
			
			e.printStackTrace();
			
		} finally {
			
			try {
				server.close();
			} catch (IOException e) {
				e.printStackTrace();
			}
			
		}
		
	}
	
	private void sendTestData() throws IOException{ 
		
		sendVehicle(EDirection.NOORD, EDirection.VENTWEG, EType.VOETGANGER);
		sendVehicle(EDirection.OOST, EDirection.NOORD, EType.FIETS);
		sendVehicle(EDirection.ZUID, EDirection.OOST, EType.BUS);
		sendVehicle(EDirection.WEST, EDirection.ZUID, EType.AUTO);
		
		sendTrafficlight(new EId(0), ELight.ROOD);
		sendTrafficlight(new EId(200), ELight.GROEN);
		sendTrafficlight(new EId(255), ELight.ORANJE);
	
	}
	
	public void sendVehicle(EDirection start, EDirection dest, EType vehicle) throws IOException {
	
		sendPacket(EMode.TRAFFICLIGHT_MODE, start, dest, vehicle);
		
	}
	
	public void sendTrafficlight(EId id, ELight light) throws IOException {
	
		sendPacket(EMode.VEHICLE_MODE, id, light, new EId(0));
		
	}
	
	private void sendPacket(EData a, EData b, EData c, EData d) throws IOException {
	
		byte[] vehiclePacket = { a.value(), b.value(), c.value(), d.value() };
		DataOutputStream out = new DataOutputStream(socket.getOutputStream());
		System.out.println("Sending: " + a.string() + ", " + b.string() + ", " + c.string() + ", " + d.string());
		out.write(vehiclePacket);
		
	}
	
	private void receivePacket(byte[] data) {
	
		switch ((int) data[0]) {
		
			case 0:
				receiveVehicle(EDirection.create(data[1]), EDirection.create(data[2]), EType.create(data[3]));
			break;
			case 1:
				receiveTrafficlight(new EId(data[1]), ELight.create(data[2]));
			break;
		
		}
		
	}
	
	private void receiveVehicle(EDirection start, EDirection dest, EType vehicle) {
	
		System.out.println(start.string() + ", " + dest.string() + ", " + vehicle.string());
		
	}
	
	private void receiveTrafficlight(EId id, ELight light) {
	
		System.out.println(id.string() + ", " + light.string());
		
	}
	
	public void closeServer() {
		
		area.append("Terminated" + '\n');
		receiving = false;
		try {
			server.close();
		} catch (IOException e) {}
		
	}
	
}
