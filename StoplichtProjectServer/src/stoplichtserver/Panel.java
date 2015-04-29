package stoplichtserver;

import java.awt.Component;
import java.awt.Dimension;
import java.awt.GridBagLayout;
import java.awt.Image;
import java.awt.TextField;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.net.Inet4Address;
import java.net.UnknownHostException;
import java.util.ArrayList;
import javax.imageio.ImageIO;
import javax.swing.Box;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JComponent;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;
import javax.swing.UIManager;
import stoplichtserver.dataTypes.EDirection;

@SuppressWarnings("serial")
public class Panel extends JFrame {

    private final int DEFAULT_PORT = 10000;
    
    private final JPanel[] jc = new JPanel[6];
    private final JTextArea textArea;
    private TextField tf;
    private final Crossroad crossroad;
    private Port port;

    public Panel() {

        this.setTitle("SDM Controller");

        try {
            
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
            
        } catch (Exception e) {}
        
        for (int i = 0; i < jc.length; i++) {

            jc[i] = new JPanel();
            jc[i].setLayout(new GridBagLayout());
            //jc[i].setBorder(BorderFactory.createLineBorder(Color.yellow));

        }

        // log
        textArea = new JTextArea(20, 40);

        JScrollPane pane = new JScrollPane(textArea);
        jc[1].setMinimumSize(new Dimension(500, 500));
        writeID();
        
        // server
        port = new Port(this);
        port.start();
        
        // crossroad
        crossroad = new Crossroad(port);
        port.setCrossroad(crossroad);
        
        tf = new TextField(Integer.toString(DEFAULT_PORT));
        jc[5].add(tf);

        this.setContentPane(jc[0]);

        // content panel
        jc[0].add(jc[1], new GBC(0, 0).setInsets(10, 10, 10, 5));
        jc[0].add(jc[2], new GBC(1, 0).setAnchor(GBC.NORTH).setInsets(10, 0, 10, 10));
        
        // text panel
        jc[1].add(pane);

        // controls panel
        jc[2].add(jc[3], new GBC(0,0).setAnchor(GBC.WEST));
        jc[2].add(jc[4], new GBC(0,1));
        jc[2].add(jc[5], new GBC(0,2).setAnchor(GBC.WEST).setInsets(0, 2, 0, 0));

        startStopPanel(jc[3]);
        buttonPanel(jc[4]);
        
        this.pack();
        this.setVisible(true);

        port.startServer();

    }

    private void startStopPanel(JPanel p) {

        // button for opening the stream
        JButton btnOpen = new JButton();
        btnOpen.setIcon(new ImageIcon(loadImage("start").getScaledInstance(20, 20, Image.SCALE_AREA_AVERAGING)));
        btnOpen.setPreferredSize(new Dimension(20, 20));
        btnOpen.setBorder(null);
        btnOpen.setContentAreaFilled(false);
        btnOpen.addActionListener(new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent e) {

                port.startServer();

            }

        });

        // button for closing the stream
        JButton btnClose = new JButton();
        btnClose.setIcon(new ImageIcon(loadImage("stop").getScaledInstance(20, 20, Image.SCALE_AREA_AVERAGING)));
        btnClose.setPreferredSize(new Dimension(20, 20));
        btnClose.setBorder(null);
        btnClose.setContentAreaFilled(false);
        btnClose.addActionListener(new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent e) {

                port.closeServer();

            }

        });

        p.add(btnOpen);
        p.add(Box.createRigidArea(new Dimension(2,2)));
        p.add(btnClose);

    }
    
    private void buttonPanel(JPanel p) {
        
        ArrayList<Component> components = new ArrayList<>();
        //components.add(Box.createRigidArea(new Dimension(1, 1)));
        
        // button creating new cars
        JButton bt1 = new JButton("Send car");
        bt1.addActionListener(new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent e) {

                port.sendCar();
                //port.sendCar(EDirection.NOORD, EDirection.OOST);

            }

        });
        components.add(bt1);

        EDirection[] directions = {EDirection.NOORD, EDirection.OOST, EDirection.ZUID, EDirection.WEST, EDirection.VENTWEG};
        JComboBox<EDirection> from = new JComboBox<EDirection>(directions);
        components.add(from);
        JComboBox<EDirection> to = new JComboBox<EDirection>(directions);
        components.add(to);
        
        JButton bt2 = new JButton("Get id");
        bt2.addActionListener(new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent e) {

                writeID();

            }

        });
        components.add(bt2);
        
        for (int i = 0; i < components.size(); i++) {
        
            Component c = components.get(i);
            c.setPreferredSize(new Dimension(80, 30));
            p.add(Box.createRigidArea(new Dimension(2, 2)), new GBC(0, i * 2));
            p.add(c, new GBC(0, i * 2 + 1));
        
        }
        
    }

    public BufferedImage loadImage(String imageName) {

        try {

            return ImageIO.read(new File("assets/" + imageName + ".png"));

        } catch (IOException e) {

            e.printStackTrace();

        }

        return null;

    }

    public void writeID() {
        
        try {
            
            writeText(Inet4Address.getLocalHost().getHostAddress());
            
        } catch (UnknownHostException ex) {
            
            System.err.println(" -- failed requesting ip adress -- ");
            
        }
        
    }

    public int getPortNummber() {
        
        String value = tf.getText();
        
        if (value.matches("[0-9]+")) {
        
            return Integer.parseInt(value);
            
        }
         
        System.err.println("error!!");
        return DEFAULT_PORT;
        
    }
    
    public void writeText(String output) {
        
        textArea.append(output + '\n');
        
    }
    
}
