
package stoplichtserver.timedActions;

import stoplichtserver.Crossroad;
import stoplichtserver.Lane;
import stoplichtserver.dataTypes.ELight;

/**
 *
 * @author Bauke
 */
public class TrafficLightRed implements TimedAction {

    private Crossroad c;
    private Lane l;
    
    public TrafficLightRed(Crossroad crossroad, Lane lane) {
        
        c = crossroad;
        l = lane;
        
    }
    
    @Override
    public void exe() {
        
       l.setLight(ELight.RED);
       
       Timer t = new Timer(Crossroad.TIME_TILL_RESET, new TrafficLightRed(c, l));
       t.run(); 
        
    }
    
}
