package stoplichtserver;

import stoplichtserver.dataTypes.ELight;
import stoplichtserver.dataTypes.ELightId;

/**
 * single lane, with its own trafficlight and waiting list for cars
 * 
 * @author Bauke
 */
public class Lane {

    private ELightId id;
    private ELight lightState = ELight.RED;
    private int cars = 0;
    private Port p;

    /**
     * create a new lane
     * 
     * @param id light id (lane id)
     * @param p port for sending light updates
     */
    public Lane(ELightId id, Port p) {

        this.id = id;
        this.p = p;

    }

    public void addCar() {

        cars++;

    }

    public void removeCar() {

        cars--;

    }

    public int getCarAmount() {

        return cars;

    }

    /**
     * set the new light state
     * also sends update to the client
     * 
     * @param lightState 
     */
    public void setLightState(ELight lightState) {

        this.lightState = lightState;
        p.sendTrafficlight(id, lightState);

    }

    public ELight getLightState() {

        return lightState;

    }

}
