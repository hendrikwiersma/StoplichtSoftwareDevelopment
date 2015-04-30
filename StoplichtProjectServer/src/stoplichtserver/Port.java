package stoplichtserver;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import stoplichtserver.dataTypes.*;

/**
 * interaction between server and client all sending / receiving happens here
 *
 * @author Bauke
 */
public class Port extends Thread {

    public static final int MESSAGE_LENGTH = 4;

    private volatile boolean running = true;
    private volatile boolean receiving = false;

    private ServerSocket server;
    private Socket socket;
    private Panel panel;
    private Crossroad c;

    DataOutputStream out;

    /**
     * create new server
     *
     * @param area log
     */
    public Port(Panel panel) {

        this.panel = panel;

    }

    /**
     * set crossroad (post init)
     *
     * @param c
     */
    public void setCrossroad(Crossroad c) {

        this.c = c;

    }

    @Override
    public void run() {

        //create the socket server object
        try {

            while (running) {

                if (receiving) {
                    
                    if (socket != null && socket.isConnected()) {

                        DataInputStream in = new DataInputStream(socket.getInputStream());
                        byte[] data = new byte[MESSAGE_LENGTH];
                        in.read(data);

                        receivePacket(data);

                    } else {

                        try{
                            System.out.println(server.getInetAddress().getHostAddress());
                        panel.writeText("Waiting for client on port " + server.getLocalPort() + "...");
                        socket = server.accept();
                        panel.writeText("Connected to " + socket.getRemoteSocketAddress());
                        out = new DataOutputStream(socket.getOutputStream());
                        }catch(Exception e) {
                            
                            System.out.println(e.getStackTrace());
                            
                        }

                    }
                }

                Thread.sleep(10);

            }

        } catch (Exception e) {

            e.printStackTrace();
            running = false;

        } finally {

            closeServer();

        }

    }

    /**
     * send new vehicle
     *
     * @param start
     * @param dest
     * @param vehicle
     */
    public void sendVehicle(EDirection start, EDirection dest, EType vehicle) {

        try {

            sendPacket(EMode.VEHICLE_MODE, start, dest, vehicle);

        } catch (IOException e) {

            System.err.println("Failed sending vehicle state:" + e.getMessage());
            panel.writeText("Could not send vehicle");

        }

    }

    /**
     * send trafficlight state
     *
     * @param id
     * @param light
     */
    public void sendTrafficlight(ELightId id, ELight light) {

        try {

            sendPacket(EMode.TRAFFICLIGHT_MODE, id, light, new ELightId((byte) 0x00));

        } catch (IOException e) {

            System.err.println("Failed sending trafficlight state:" + e.getMessage());
            panel.writeText("Could not send trafficlight state");

        }

    }

    private void sendPacket(EData a, EData b, EData c, EData d) throws IOException {

        byte[] vehiclePacket = {a.value(), b.value(), c.value(), d.value()};
        DataOutputStream out = new DataOutputStream(socket.getOutputStream());
        panel.writeText("Sending: " + a.string() + ", " + b.string() + ", " + c.string() + ", " + d.string());
        out.write(vehiclePacket);

    }

    private void receivePacket(byte[] data) {

        System.err.println("data0:" + data[0] + ", data1:" + data[1] + ", data2:" + data[2]);

        switch ((int) data[2]) {

            case 1:
                addWaitingCar(new ELightId(data[1]));
                break;
            case 0:
                removeWaitingCar(new ELightId(data[1]));
                break;

        }

    }

    private void addWaitingCar(ELightId id) {

        System.out.println("add car on light: " + id.string());
        c.addCar(id);

    }

    private void removeWaitingCar(ELightId id) {

        System.out.println("remove car on light: " + id.string());
        c.removeCar(id);

    }

    /**
     * send a new car
     */
    public void sendCar() {

        sendVehicle(EDirection.ZUID, EDirection.WEST, EType.AUTO);

    }

    public void startServer() {

        if (!receiving) {

            panel.writeText("Server started");
            
            try {

                server = new ServerSocket(panel.getPortNummber());

            } catch (IOException ex) {

                panel.writeText("Could not create server");
                closeServer();

            }

            receiving = true;

        }

    }
    
     /**
     * exit server
     */
    public void closeServer() {

        if (receiving) {

            receiving = false;

            try {

                server.close();
                panel.writeText("Server closed" + '\n');

            } catch (IOException e) {

                panel.writeText("Failed closing server");

            }

        }

    }
    
    public void exit() {
        
        running = false;
        
    }

}
