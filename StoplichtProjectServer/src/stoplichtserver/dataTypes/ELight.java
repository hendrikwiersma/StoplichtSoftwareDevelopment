package stoplichtserver.dataTypes;


public enum ELight implements EData {
	
	RED((byte)0x00, "Rood"), ORANGE((byte)0x01, "Oranje"), GREEN((byte)0x02, "Groen");
	
	private byte b;
	private String s;
	
	private ELight(byte b, String s) {
		
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
	
	public static ELight create(byte type) {
		
		switch ((int)type) {
			
			case 0: return RED;
			case 1: return ORANGE;
			case 2: return GREEN;
			
		}
		
		return null;
		
	}
	
}
