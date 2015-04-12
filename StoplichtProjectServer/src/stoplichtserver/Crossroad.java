/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package stoplichtserver;

import stoplichtserver.dataTypes.EDist;
import stoplichtserver.dataTypes.ELightId;
import stoplichtserver.dataTypes.ELight;
import stoplichtserver.timedActions.Timer;
import stoplichtserver.timedActions.TrafficLightRed;

/**
 *
 * @author Bauke
 */
public class Crossroad {
    
    public static final int TIME_TILL_RED = 100;
    public static final int TIME_TILL_RESET = 20;
    public static final int TRAFFIC_LIGHTS = 10;
    
    private Lane[] lanes = new Lane[TRAFFIC_LIGHTS];
    public boolean canSetLights = true;
       
    public void addCar(ELightId id, EDist distance) {
        
        Lane l = lanes[id.value()];
        l.addCar();

        setLights();
        
    }
    
    public Crossroad(Port p) {
        
        for (int i = 0; i < lanes.length; i++) {
            
            lanes[i] = new Lane(new ELightId((byte)i), p);
            
        }
        
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
        Timer t = new Timer(TIME_TILL_RED, new TrafficLightRed(this, l));
        t.run();
       
    }
    
}
