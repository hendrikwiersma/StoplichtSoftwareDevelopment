/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package stoplichtserver.dataTypes;

public class EDist implements EData{
	
	private byte b;
	
	public EDist(byte b) {
		
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