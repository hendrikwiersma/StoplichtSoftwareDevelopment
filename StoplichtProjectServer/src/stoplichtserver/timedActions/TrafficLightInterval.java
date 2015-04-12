/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package stoplichtserver.timedActions;

import stoplichtserver.Crossroad;
import stoplichtserver.dataTypes.ELightId;

/**
 *
 * @author Bauke
 */
public class TrafficLightInterval implements TimedAction{

    private Crossroad k;
    private ELightId id;
    
    public TrafficLightInterval(Crossroad kruispunt, ELightId lightId) {
        
        k = kruispunt;
        id = lightId;
        
    }
    
    @Override
    public void exe() {
        
        k.canSetLights = true;
        k.setLights();
        
    }
    
}
