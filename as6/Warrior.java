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
    public Warrior()
	{

		super("Warrior", 125, 4, .8, 35, 60, .2, new MoveCrushingBlow(), "Crushing Blow");


    }//end constructor

    public Warrior(String name)
	{

		super(name, 125, 4, .8, 35, 60, .2, new MoveCrushingBlow(), "Crushing Blow");


    }//end constructor

	public void crushingBlow(DungeonCharacter opponent)
	{
		if (Math.random() <= .4)
		{
			int blowPoints = (int)(Math.random() * 76) + 100;
			System.out.println(name + " lands a CRUSHING BLOW for " + blowPoints
								+ " damage!");
			opponent.subtractHitPoints(blowPoints);
		}//end blow succeeded
		else
		{
			System.out.println(name + " failed to land a crushing blow");
			System.out.println();
		}//blow failed

	}//end crushingBlow method

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