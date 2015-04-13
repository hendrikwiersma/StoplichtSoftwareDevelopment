
package stoplichtserver.timedActions;

import java.sql.Timestamp;
import stoplichtserver.Crossroad;
import static stoplichtserver.Crossroad.TIME_TILL_RESET;
import stoplichtserver.Lane;
import stoplichtserver.dataTypes.ELight;

/**
 *
 * @author Bauke
 */
public class TrafficLightRed implements TimedAction {

    private Crossroad c;
    private Lane l;
    private Timestamp t;
    
    public TrafficLightRed(Crossroad crossroad, Lane lane, Timestamp timestamp) {
        
        c = crossroad;
        l = lane;
        t = timestamp;
        
    }
    
    @Override
    public void exe() {
       
       System.err.println("to red");
       l.setLight(ELight.RED);
       
        Timestamp time = new Timestamp(System.currentTimeMillis() + TIME_TILL_RESET);
        c.t.addAction(new TrafficLightInterval(c, time));
        
    }

    @Override
    public Timestamp getTimestamp() {
        
        return t;
        
    }
    
}
