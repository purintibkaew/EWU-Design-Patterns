/**
 * Title:
 * Description:
 * Copyright:    Copyright (c) 2001
 * Company:
 * @author
 * @version 1.0
 */

public class Thief extends Hero
{
    public Thief(String theifName, int hitPoints, int attackSpeed, 
    			 double chanceToHit, int damageMin, int damageMax, 
    			 double chanceToBlock, SpecialMove specialMove, String specialMoveName)
	{
		super(theifName, hitPoints, attackSpeed, chanceToHit, 
		      damageMin, damageMax, chanceToBlock, specialMove, specialMoveName);
    }//end constructor

	public void specialMove(DungeonCharacter opponent){
		specialMove.execute(this, opponent);
		numTurns--;
	}
}