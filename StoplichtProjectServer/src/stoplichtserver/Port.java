package stoplichtserver;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketTimeoutException;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.JTextArea;
import stoplichtserver.dataTypes.*;

public class Port extends Thread {

    public static final int PORT_NUMER = 10000;
    public static final int MESSAGE_LENGTH = 4;

    private volatile boolean receiving = true;
    
    private ServerSocket server;
    private Socket socket;
    private JTextArea area;
    private Crossroad c;
    
    private boolean sendCar = true;

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

        System.err.println("data0:" + data[0]+ ", data1:" + data[1]+", data2:"+data[2]);
        
        switch ((int) data[2]) {

            case 1:
                signOn(new ELightId(data[1]));
                break;
            case 0:
                signOff(new ELightId(data[1]));
                break;

        }

    }

    private void signOn(ELightId id){

        System.out.println("add car on light: " + id.string());
        c.addCar(id);
        
    }
    
    private void signOff(ELightId id) {
        
        System.out.println("remove car on light: " + id.string());
        c.removeCar(id);
        
    }

    public void setCrossroad(Crossroad c) {
        
        this.c = c;
        
    }
    
    public void sendCar() {
        
        try {
            sendTestData();
        } catch (IOException ex) {
            Logger.getLogger(Port.class.getName()).log(Level.SEVERE, null, ex);
        }
    
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
