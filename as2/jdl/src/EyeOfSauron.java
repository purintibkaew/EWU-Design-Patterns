/*
 * Team Eagle Plaid
 * 
 * Jonathan Lewis
 * Jacob Schwartz
 * Ian Davies
 * 
 */


import java.util.Observable;


public class EyeOfSauron extends Observable {

	private int hobbits;
	private int elves;
	private int dwarves;
	private int men;
	
	public void setEnemies(int h, int e, int d, int m) {
		hobbits = h;
		elves = e;
		dwarves = d;
		men = m;
		
		Object[] o = new Object[2];
		
		o[0] = new int[] {	 hobbits,
							 elves,
							 dwarves,
							 men };
		
		o[1] = new String[] { 	"hobbits", 
								"elves", 
								"dwarves", 
								"men" };
		
		this.setChanged();
		this.notifyObservers(o);
		//this.notifyObservers();
	}
	
	/*
	public int getHobbits()
	{
		return hobbits;
	}
	
	public int getElves()
	{
		return elves;
	}
	
	public int getDwarves()
	{
		return dwarves;
	}
	
	public int getMen()
	{
		return men;
	}
	*/

}
