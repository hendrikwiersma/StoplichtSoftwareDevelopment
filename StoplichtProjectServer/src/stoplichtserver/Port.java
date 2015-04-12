package stoplichtserver;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketTimeoutException;
import javax.swing.JTextArea;
import stoplichtserver.dataTypes.*;

public class Port extends Thread {

    public static final int PORT_NUMER = 1000;
    public static final int MESSAGE_LENGTH = 4;

    private volatile boolean receiving = true;
    
    private ServerSocket server;
    private Socket socket;
    private JTextArea area;
    private Crossroad c;
    
    private boolean sendCar = false;

    DataOutputStream out;

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

                        if (sendCar) {
                            
                            sendTestData();
                            sendCar = false;
                        
                        }

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

    private void sendTestData() throws IOException {

        sendVehicle(EDirection.ZUID, EDirection.WEST, EType.AUTO);
        area.append("sending car");
        //sendTrafficlight(new EId(0), ELight.ROOD);
        //sendTrafficlight(new EId(200), ELight.GROEN);
        //sendTrafficlight(new EId(255), ELight.ORANJE);

    }

    public void sendVehicle(EDirection start, EDirection dest, EType vehicle) throws IOException {

        sendPacket(EMode.VEHICLE_MODE, start, dest, vehicle);

    }

    public void sendTrafficlight(ELightId id, ELight light) throws IOException {

        sendPacket(EMode.TRAFFICLIGHT_MODE, id, light, new ELightId((byte)0x00));

    }

    private void sendPacket(EData a, EData b, EData c, EData d) throws IOException {

        byte[] vehiclePacket = {a.value(), b.value(), c.value(), d.value()};
        DataOutputStream out = new DataOutputStream(socket.getOutputStream());
        System.out.println("Sending: " + a.string() + ", " + b.string() + ", " + c.string() + ", " + d.string());
        out.write(vehiclePacket);

    }

    private void receivePacket(byte[] data) {

        switch ((int) data[0]) {

            case 0:
                signOn(new ELightId(data[1]), new EDist(data[2]));
                break;
            case 1:
                signOff(new ELightId(data[1]));
                break;

        }

    }

    private void signOn(ELightId id, EDist distance){

        c.addCar(id, distance);
        
    }
    
    private void signOff(ELightId id) {
        
        c.removeCar(id);
        
    }

    public void setCrossroad(Crossroad c) {
        
        this.c = c;
        
    }
    
    public void sendCar() {
        
        sendCar = true;
    
    }
    
    public void closeServer() {

        area.append("Terminated" + '\n');
        receiving = false;
        try {
            server.close();
        } catch (IOException e) {
        }
        
    }

}
