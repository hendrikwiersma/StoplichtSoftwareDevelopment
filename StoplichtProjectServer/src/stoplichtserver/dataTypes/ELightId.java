package stoplichtserver.dataTypes;


public class EId implements EData{
	
	private byte b;
	
	public EId(int b) {
		
		this.b = (byte)b;
		
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
