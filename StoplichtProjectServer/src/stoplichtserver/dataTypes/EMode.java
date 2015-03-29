package dataTypes;

public enum EMode implements EData {

	VEHICLE_MODE((byte)0x01, "Vehicle"), TRAFFICLIGHT_MODE((byte)0x02, "TrafficLight");
	
	private byte b;
	private String s;
	
	private EMode(byte b, String s) {
		
		this.b = b;
		this.s = s;
		
	}
	
	@Override
	public byte value() {
		
		return b;
		
	}
	
	@Override
	public String string() {
		
		return s;
		
	}
	
	public static EMode create(byte type) {
		
		switch ((int)type) {
			
			case 0: return VEHICLE_MODE;
			case 1: return TRAFFICLIGHT_MODE;
			
		}
		
		return null;
		
	}
	
}
