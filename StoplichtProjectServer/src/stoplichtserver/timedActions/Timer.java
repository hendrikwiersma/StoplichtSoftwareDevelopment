package stoplichtserver.timedActions;

import java.sql.Timestamp;
import java.util.ArrayList;

/**
 *
 * @author Bauke
 */
public class Timer implements Runnable{

    private ArrayList<TimedAction> ta;
    
    public Timer() {
        
        ta = new ArrayList<>();
        
    }
    
    @Override
    public void run() {
        
        while(true) {
            
            try {

                Thread.sleep(1000);

            } catch (InterruptedException e) {}

            Timestamp ts = new Timestamp(System.currentTimeMillis());
            
            for (int i = ta.size() - 1; i >= 0; i--) {

                TimedAction t = ta.get(i);
                
                if (ts.after(t.getTimestamp())) {
                    
                    System.err.println("exe");
                    t.exe();
                    ta.remove(t);
                    
                }
                    
            }
        
        }
        
    }
    
    public void addAction(TimedAction action) {
        
        System.err.println("add");
        ta.add(action);
        
    }
    
}
