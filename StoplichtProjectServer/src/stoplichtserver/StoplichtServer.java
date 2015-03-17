package stoplichtserver;

import java.net.*;
import java.io.*;

public class StoplichtServer extends Thread {

    public int serverport = 10000;
    private ServerSocket serverSocket;
    DataOutputStream out;
    private Socket server;

    private StoplichtServer() throws IOException {
        serverSocket = new ServerSocket(serverport);
        serverSocket.setSoTimeout(10000);
    }

    @Override
    public void run() {
        while (true) {
            if (server != null && server.isConnected()) {
                try {
                    DataInputStream in = new DataInputStream(server.getInputStream());
                    byte[] b = new byte[NORM_PRIORITY];
                    in.read(b);
                    if (b[0] == 3) {
                        ReceiveVehicleSignal(b[1], b[2]);
                    }
                } catch (SocketTimeoutException s) {
                    System.out.println("Socket timed out!");
                    break;
                } catch (IOException e) {
                    break;
                }
            } else {
                try {
                    System.out.println("Waiting for client on port " + serverSocket.getLocalPort() + "...");
                    server = serverSocket.accept();
                    System.out.println("Just connected to " + server.getRemoteSocketAddress());
                    out = new DataOutputStream(server.getOutputStream());

                    sendVehiclePacket("Noord", "Ventweg", "Voetganger");
                    sendVehiclePacket("Oost", "Noord", "Fiets");
                    sendVehiclePacket("Zuid", "Oost", "Bus");
                    sendVehiclePacket("West", "Zuid", "Auto");

                    sendTrafficlightPacket(0, "Rood");
                    sendTrafficlightPacket(200, "Groen");
                    sendTrafficlightPacket(255, "Oranje");
                } catch (SocketTimeoutException s) {
                    System.out.println("Socket timed out!");
                    break;
                } catch (IOException e) {
                    break;
                }
            }
        }
    }

    public static void main(String[] args) {
        try {
            Thread t = new StoplichtServer();
            t.start();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void sendVehiclePacket(String StartingPoint, String EndPoint, String vehicleType) throws IOException {
        byte sendstartingpoint = 0;
        switch (StartingPoint) {
            case "Noord":
                sendstartingpoint = 0x00;
                break;
            case "Oost":
                sendstartingpoint = 0x01;
                break;
            case "Zuid":
                sendstartingpoint = 0x02;
                break;
            case "West":
                sendstartingpoint = 0x03;
                break;
            case "Ventweg":
                sendstartingpoint = 0x04;
                break;
            default:
                System.out.println("Invalid startingpoint.");
                break;
        }

        byte sendendpoint = 0;
        switch (EndPoint) {
            case "Noord":
                sendendpoint = 0x00;
                break;
            case "Oost":
                sendendpoint = 0x01;
                break;
            case "Zuid":
                sendendpoint = 0x02;
                break;
            case "West":
                sendendpoint = 0x03;
                break;
            case "Ventweg":
                sendendpoint = 0x04;
                break;
            default:
                System.out.println("Invalid endpoint.");
                break;
        }

        byte sendtype = 0;
        switch (vehicleType) {
            case "Auto":
                sendtype = 0x00;
                break;
            case "Fiets":
                sendtype = 0x01;
                break;
            case "Bus":
                sendtype = 0x02;
                break;
            case "Voetganger":
                sendtype = 0x03;
                break;
            default:
                System.out.println("Invalid VehicleType.");
                break;
        }

        byte[] vehiclePacket = {0x01, sendstartingpoint, sendendpoint, sendtype};
        DataOutputStream out = new DataOutputStream(server.getOutputStream());
        System.out.println("Sending a packet: " + vehicleType + " is going from " + StartingPoint + " to " + EndPoint);
        out.write(vehiclePacket);
    }

    public void sendTrafficlightPacket(int id, String state) throws IOException {
        byte lightstate = 0;
        switch (state) {
            case "Rood":
                lightstate = 0x00;
                break;
            case "Oranje":
                lightstate = 0x01;
                break;
            case "Groen":
                lightstate = 0x02;
                break;
            default:
                System.out.println("Invalid TrafficLight state.");
                break;
        }
        byte[] TrafficlightPacket = {0x02, (byte) id, lightstate, 0x00};

        System.out.println("Sending a packet: trafficlight with id " + id + " is turning " + state);
        out.write(TrafficlightPacket);
    }

    public void ReceiveVehicleSignal(byte trafficlightid, byte state) {
        if (state == 0) {
            System.out.println("Received a packet: A car is logging in at trafficlight id " + trafficlightid);
        } else {
            System.out.println("Received a packet: A car is logging out at trafficlight id " + trafficlightid);
        }
    }
}
