/**
 * Title:
 * Description:
 * Copyright:    Copyright (c) 2001
 * Company:
 * @author
 * @version 1.0
 */




public class Warrior extends Hero
{
    public Warrior(String warriorName, int hitPoints, int attackSpeed, 
    			 double chanceToHit, int damageMin, int damageMax, 
    			 double chanceToBlock, SpecialMove specialMove, String specialMoveName)
	{
		super(warriorName, hitPoints, attackSpeed, chanceToHit, 
		      damageMin, damageMax, chanceToBlock, specialMove, specialMoveName);
    }//end constructor

	public void attack(DungeonCharacter opponent)
	{
		System.out.println(name + " swings a mighty sword at " +
							opponent.getName() + ":");
		super.attack(opponent);
	}//end override of attack method

	public void specialMove(DungeonCharacter opponent){
		specialMove.execute(this, opponent);
		numTurns--;
	}

}//end Hero class