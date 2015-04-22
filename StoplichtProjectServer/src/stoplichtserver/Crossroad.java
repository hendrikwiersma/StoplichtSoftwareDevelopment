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
import stoplichtserver.timedActions.TrafficLightOrange;

/**
 * Crossroad consisting out of multiple Lanes
 * 
 * @author Bauke
 */
public class Crossroad {
    
    public static final int TIME_TILL_ORANGE = 7000;
    public static final int TIME_TILL_RED = 3000;
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
    
    /**
     * add a new car on specific lane
     * 
     * @param id 
     */
    public void addCar(ELightId id) {
        
        Lane l = lanes[id.value()];
        l.addCar();

        setLights();
        
    }
    
    /**
     * remove a car from lane
     * 
     * @param id 
     */
    public void removeCar(ELightId id) {

        Lane l = lanes[id.value()];
        l.removeCar();
        
    }
    
    /**
     * cycle through all lanes
     * and activate corresponding lights
     * 
     */
    public void setLights() {
        
        if (canSetLights) {
            
            for(int i = 0; i < lanes.length; i++) {
                
                Lane l = lanes[i];
                
                if (l.getCarAmount() > 0) {
                    
                    activateLane(l);
                    
                }
                
            }

        }
        
    }
    
    /**
     * set lane to active
     * @param l 
     */
    private void activateLane(Lane l) {
        
        canSetLights = false;

        l.setLightState(ELight.GREEN);
        Timestamp time = new Timestamp(System.currentTimeMillis() + TIME_TILL_RED);
        t.addAction(new TrafficLightOrange(this, l, time));
       
    }
    
}
