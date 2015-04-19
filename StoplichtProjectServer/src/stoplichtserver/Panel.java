package stoplichtserver;

import java.awt.event.ActionEvent;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;

@SuppressWarnings("serial")
public class Panel extends JFrame {

    private final JPanel contentPanel;
    private final JTextArea area;
    private final JButton btnSendCar;
    private final JButton btnClose;
    private final Crossroad crossroad;

    public Panel() {

        // main panel
        contentPanel = new JPanel();
        this.setContentPane(contentPanel);

        // log
        area = new JTextArea(5, 20);
        JScrollPane pane = new JScrollPane(area);
        contentPanel.add(pane);

        // server
        Port port = new Port(area);
        port.start();
        
        // crossroad
        crossroad = new Crossroad(port);
        port.setCrossroad(crossroad);
        
        // button creating new cars
        btnSendCar = new JButton("send new car");
        btnSendCar.addActionListener((ActionEvent e) -> {
            
            port.sendCar();
            
        });
        contentPanel.add(btnSendCar);
        
        // button for closing the stream
        btnClose = new JButton("close stream");
        btnClose.addActionListener((ActionEvent e) -> {
            
            port.closeServer();
            
        });
        contentPanel.add(btnClose);

        // update and view panel content
        this.pack();
        this.setVisible(true);
    }

}
