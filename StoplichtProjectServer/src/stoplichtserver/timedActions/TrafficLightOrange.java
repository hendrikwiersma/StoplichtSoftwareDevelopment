/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package stoplichtserver.timedActions;

import java.sql.Timestamp;
import stoplichtserver.Crossroad;
import static stoplichtserver.Crossroad.TIME_TILL_RED;
import stoplichtserver.Lane;
import stoplichtserver.dataTypes.ELight;

/**
 *
 * @author Bauke
 */
public class TrafficLightOrange implements TimedAction {

    private Crossroad c;
    private Lane l;
    private Timestamp t;
    
    public TrafficLightOrange(Crossroad crossroad, Lane lane, Timestamp timestamp) {
        
        c = crossroad;
        l = lane;
        t = timestamp;
        
    }
    
    @Override
    public void exe() {
        
       System.err.println("to orange");
       l.setLightState(ELight.ORANGE);
       
        Timestamp time = new Timestamp(System.currentTimeMillis() + TIME_TILL_RED);
        c.t.addAction(new TrafficLightRed(c, l, time));
        
    }

    @Override
    public Timestamp getTimestamp() {
       
        return t;
        
    }
    
}
