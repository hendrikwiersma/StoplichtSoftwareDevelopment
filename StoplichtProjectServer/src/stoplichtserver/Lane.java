package stoplichtserver;

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
        p.sendTrafficlight(id, lightState);
        
    }
    
    public ELight getLight() {
        
        return lightState;
        
    }
    
}
