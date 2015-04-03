package stoplichtserver.dataTypes;

public enum EType implements EData{
	
	AUTO((byte)0x00, "Auto"), FIETS((byte)0x01, "Fiets"), BUS((byte)0x02, "Bus"), VOETGANGER((byte)0x03, "Voetganger");
	
	private byte b;
	private String s;
	
	private EType(byte b, String s) {
		
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
	
	public static EType create(byte type) {
		
		switch ((int)type) {
			
			case 0: return AUTO;
			case 1: return FIETS;
			case 2: return BUS;
			case 3: return VOETGANGER;
			
		}
		
		return null;
		
	}
	
}
