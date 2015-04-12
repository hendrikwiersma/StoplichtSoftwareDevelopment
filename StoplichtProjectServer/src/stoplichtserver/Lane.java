package stoplichtserver;

import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;
import stoplichtserver.dataTypes.ELight;
import stoplichtserver.dataTypes.ELightId;

/**
 *
 * @author Bauke
 */
public class Lane {
  
    private ELightId id;
    private ELight lightState = ELight.RED;
    private int cars = 0;
    private Port p;
    
    public Lane(ELightId id, Port p) {
        
        this.id = id;
        this.p = p;
        
    }
    
    public void addCar() {
        
        cars ++;
        
    }
    
    public void removeCar() {
        
        cars --;
        
    }
    
    public int getCars() {
        
        return cars;
        
    }
    
    public void setLight(ELight lightState) {
        
        this.lightState = lightState;
        try {
            p.sendTrafficlight(id, lightState);
        } catch (IOException ex) {
            Logger.getLogger(Lane.class.getName()).log(Level.SEVERE, null, ex);
        }
        
    }
    
    public ELight getLight() {
        
        return lightState;
        
    }
    
}
