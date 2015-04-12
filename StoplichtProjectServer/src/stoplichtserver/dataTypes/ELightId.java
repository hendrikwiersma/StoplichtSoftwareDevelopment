package stoplichtserver.dataTypes;


public class ELightId implements EData{
	
	private byte b;
	
	public ELightId(byte b) {
		
		this.b = b;
		
	}
	
	@Override
	public byte value() {
		
		return b;
		
	}
	
	@Override
	public String string() {
		
		return "ID:" + b;
		
	}

}
