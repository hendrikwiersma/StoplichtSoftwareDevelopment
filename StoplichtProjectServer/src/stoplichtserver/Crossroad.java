/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package stoplichtserver;

import java.sql.Timestamp;
import stoplichtserver.dataTypes.ELightId;
import stoplichtserver.dataTypes.ELight;
import stoplichtserver.timedActions.Timer;
import stoplichtserver.timedActions.TrafficLightRed;

/**
 *
 * @author Bauke
 */
public class Crossroad {
    
    public static final int TIME_TILL_RED = 5000;
    public static final int TIME_TILL_RESET = 2000;
    public static final int TRAFFIC_LIGHTS = 50;
    
    private Lane[] lanes = new Lane[TRAFFIC_LIGHTS];
    public volatile boolean canSetLights = true;
    public Timer t = new Timer();
    public Thread thread = new Thread(t);
     
    public Crossroad(Port p) {
        
        for (int i = 0; i < lanes.length; i++) {
            
            lanes[i] = new Lane(new ELightId((byte)i), p);
            
        }

        thread.start();
        
    }
    
    public void addCar(ELightId id) {
        
        Lane l = lanes[id.value()];
        l.addCar();

        setLights();
        
    }
    
    public void removeCar(ELightId id) {

        Lane l = lanes[id.value()];
        l.removeCar();
        
    }
    
    public void setLights() {
        
        if (canSetLights) {
            
            for(int i = 0; i < lanes.length; i++) {
                
                Lane l = lanes[i];
                
                if (l.getCars() > 0) {
                    
                    activateLane(l);
                    
                }
                
            }

        }
        
    }
    
    private void activateLane(Lane l) {
        
        canSetLights = false;

        l.setLight(ELight.GREEN);
        Timestamp time = new Timestamp(System.currentTimeMillis() + TIME_TILL_RED);
        t.addAction(new TrafficLightRed(this, l, time));
       
    }
    
}
