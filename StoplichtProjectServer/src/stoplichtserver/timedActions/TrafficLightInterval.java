/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package stoplichtserver.timedActions;

import java.sql.Timestamp;
import stoplichtserver.Crossroad;
import stoplichtserver.dataTypes.ELightId;

/**
 *
 * @author Bauke
 */
public class TrafficLightInterval implements TimedAction{

    private Crossroad k;
    private Timestamp t;
    
    public TrafficLightInterval(Crossroad kruispunt, Timestamp timestamp) {
        
        k = kruispunt;
        t = timestamp;
        
    }
    
    @Override
    public void exe() {
        
        k.canSetLights = true;
        k.setLights();
        
    }

    @Override
    public Timestamp getTimestamp() {
        
        return t;
        
    }
    
}
