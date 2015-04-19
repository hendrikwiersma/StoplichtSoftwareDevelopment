package stoplichtserver;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketTimeoutException;
import javax.swing.JTextArea;
import stoplichtserver.dataTypes.*;

/**
 * interaction between server and client all sending / receiving happens here
 *
 * @author Bauke
 */
public class Port extends Thread {

    public static final int PORT_NUMER = 10000;
    public static final int MESSAGE_LENGTH = 4;

    private volatile boolean receiving = true;

    private ServerSocket server;
    private Socket socket;
    private JTextArea area;
    private Crossroad c;

    DataOutputStream out;

    /**
     * create new server
     *
     * @param area log
     */
    public Port(JTextArea area) {

        this.area = area;

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

            server = new ServerSocket(PORT_NUMER);

            while (receiving) {

                if (socket != null && socket.isConnected()) {
                    try {

                        DataInputStream in = new DataInputStream(socket.getInputStream());
                        byte[] data = new byte[MESSAGE_LENGTH];
                        in.read(data);

                        receivePacket(data);

                    } catch (Exception e) {

                        break;

                    }

                } else {

                    try {

                        area.append("Waiting for client on port " + server.getLocalPort() + "..." + '\n');
                        socket = server.accept();
                        area.append("Just connected to " + socket.getRemoteSocketAddress());
                        out = new DataOutputStream(socket.getOutputStream());

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
            area.append("Could not send vehicle" + '\n');

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
            area.append("Could not send trafficlight state" + '\n');

        }

    }

    private void sendPacket(EData a, EData b, EData c, EData d) throws IOException {

        byte[] vehiclePacket = {a.value(), b.value(), c.value(), d.value()};
        DataOutputStream out = new DataOutputStream(socket.getOutputStream());
        System.out.println("Sending: " + a.string() + ", " + b.string() + ", " + c.string() + ", " + d.string());
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

    /**
     * exit server
     */
    public void closeServer() {

        receiving = false;

        try {

            server.close();
            area.append("Server closed" + '\n');

        } catch (IOException e) {

            area.append("Failed closing server" + '\n');

        }

    }

}
