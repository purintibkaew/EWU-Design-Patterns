/**
 * Title:
 * Description:
 * Copyright:    Copyright (c) 2001
 * Company:
 * @author
 * @version 1.0
 */



public class Sorceress extends Hero
{
	
//-----------------------------------------------------------------
    public Sorceress()
	{
		super("Sorceress", 75, 5, .7, 25, 50, .3, new MoveIncreaseHitPoints(), "Heal");
		
    }//end constructor
    
    public Sorceress(String name)
	{
		super(name, 75, 5, .7, 25, 50, .3, new MoveIncreaseHitPoints(), "Heal");


    }//end constructor

//-----------------------------------------------------------------
	public void attack(DungeonCharacter opponent)
	{
		System.out.println(name + " casts a spell of fireball at " +
							opponent.getName() + ":");
		super.attack(opponent);
	}//end override of attack method
	
	public void specialMove(DungeonCharacter opponent){
		specialMove.execute(this, this);
		numTurns--;
	}

}//end class