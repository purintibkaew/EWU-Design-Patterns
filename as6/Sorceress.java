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
    public Sorceress(String sorceressName, int hitPoints, int attackSpeed, 
    			 double chanceToHit, int damageMin, int damageMax, 
    			 double chanceToBlock, SpecialMove specialMove, String specialMoveName)
	{
		super(sorceressName, hitPoints, attackSpeed, chanceToHit, 
		      damageMin, damageMax, chanceToBlock, specialMove, specialMoveName);
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