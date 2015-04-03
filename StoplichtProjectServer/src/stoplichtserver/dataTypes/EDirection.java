package stoplichtserver.dataTypes;

public enum EDirection implements EData {

	NOORD((byte)0x00, "Noord"), OOST((byte)0x01, "Oost"), ZUID((byte)0x02, "Zuid"), WEST((byte)0x03, "West"), VENTWEG((byte)0x04, "Ventweg");
	
	private byte b;
	private String s;
	
	private EDirection(byte b, String s) {
		
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
	
	public static EDirection create(byte type) {
		
		switch ((int)type) {
			
			case 0: return NOORD;
			case 1: return OOST;
			case 2: return ZUID;
			case 3: return WEST;
			case 4: return VENTWEG;
			
		}
		
		return null;
		
	}
	
}
