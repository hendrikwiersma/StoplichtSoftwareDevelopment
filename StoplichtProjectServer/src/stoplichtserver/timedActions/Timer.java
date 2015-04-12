/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package stoplichtserver.timedActions;

import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Bauke
 */
public class Timer implements Runnable{

    private int ms;
    private TimedAction ta;
    
    public Timer(int miliseconds, TimedAction timedAction) {
        
        ms = miliseconds;
        ta = timedAction;
        
    }
    
    @Override
    public void run() {
        
        try {
            
            Thread.sleep(ms);
            
        } catch (InterruptedException ex) {
            Logger.getLogger(Timer.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        ta.exe();
        
    }
    
}
