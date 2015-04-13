package stoplichtserver.timedActions;

import java.sql.Timestamp;

/**
 *
 * @author Bauke
 */
public interface TimedAction {
    
    public void exe();
    public Timestamp getTimestamp();
    
}
