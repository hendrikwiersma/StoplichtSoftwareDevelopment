package stoplichtserver;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.BufferedReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;

@SuppressWarnings("serial")
public class Panel extends JFrame {

    JPanel contentPanel;
    JTextArea area;
    JButton btnSendCar;
    JButton btnClose;
    Crossroad crossroad;

    ServerSocket server;
    Socket client;
    BufferedReader in;
    PrintWriter out;
    String line;

    public Panel() {

        contentPanel = new JPanel();
        this.setContentPane(contentPanel);

        area = new JTextArea(5, 20);
        JScrollPane pane = new JScrollPane(area);
        contentPanel.add(pane);

        Port port = new Port(area);
        port.start();
        
        crossroad = new Crossroad(port);
        
        port.setCrossroad(crossroad);
        
        btnSendCar = new JButton("send new car");
        btnSendCar.addActionListener(new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent e) {

                port.sendCar();

            }

        });
        contentPanel.add(btnSendCar);
        
        btnClose = new JButton("close stream");
        btnClose.addActionListener(new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent e) {

                port.closeServer();

            }

        });
        contentPanel.add(btnClose);

        this.pack();
        this.setVisible(true);
    }

}
