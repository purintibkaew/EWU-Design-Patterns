/*
 * Team Eagle Plaid
 * 
 * Jonathan Lewis
 * Jacob Schwartz
 * Ian Davies
 * 
 */


import java.util.Observable;
import java.util.Observer;


public class BadGuy implements Observer {

	private String ID;
	private EyeOfSauron eye;
	
	public BadGuy(EyeOfSauron eye, String ID) {
		this.eye = eye;
		this.eye.addObserver(this);
		this.ID = ID;
	}

	public void defeated() {
		this.eye.deleteObserver(this);
		System.out.println(ID + ": I have been defeated. No!!!");
	}

	@Override
	public void update(Observable o, Object arg) {
		/*
		System.out.println(ID + ": I have been notified by the Eye of Sauron that there are " +
						 eye.getHobbits() + " hobbits, " +
						 eye.getElves() + " elves, " +
						 eye.getDwarves() + " dwarves, " + 
						 eye.getMen() + " men.");
		*/
		
		System.out.print(ID + ": I have been notified by the Eye of Sauron that there are ");
		
		Object[] array = (Object[]) arg;
		int[] enemiesCount = (int[]) array[0];
		String[] enemiesNames = (String[]) array[1];
		
		for(int i = 0; i < enemiesCount.length; i++)
		{
			System.out.print(enemiesCount[i] + " " + enemiesNames[i]);
			
			if(i < enemiesCount.length-1)
				System.out.print(", ");
			else
				System.out.print(".\n");
		}
	}

}
