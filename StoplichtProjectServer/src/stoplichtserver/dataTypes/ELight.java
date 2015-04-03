package stoplichtserver.dataTypes;


public enum ELight implements EData {
	
	ROOD((byte)0x00, "Rood"), ORANJE((byte)0x01, "Oranje"), GROEN((byte)0x02, "Groen");
	
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
			
			case 0: return ROOD;
			case 1: return ORANJE;
			case 2: return GROEN;
			
		}
		
		return null;
		
	}
	
}
