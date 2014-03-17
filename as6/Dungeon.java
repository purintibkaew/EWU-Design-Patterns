import java.util.Scanner;

/**
 * Title: Dungeon.java
 *
 * Description: Driver file for Heroes and Monsters project
 *
 * Copyright:    Copyright (c) 2001
 * Company: Code Dogs Inc.
 * I.M. Knurdy
 *
 * History:
 *  11/4/2001: Wrote program
 *    --created DungeonCharacter class
 *    --created Hero class
 *    --created Monster class
 *    --had Hero battle Monster
 *    --fixed attack quirks (dead monster can no longer attack)
 *    --made Hero and Monster abstract
 *    --created Warrior
 *    --created Ogre
 *    --made Warrior and Ogre battle
 *    --added battleChoices to Hero
 *    --added special skill to Warrior
 *    --made Warrior and Ogre battle
 *    --created Sorceress
 *    --created Thief
 *    --created Skeleton
 *    --created Gremlin
 *    --added game play features to Dungeon.java (this file)
 *  11/27/2001: Finished documenting program
 * version 1.0
 */



/*
  This class is the driver file for the Heroes and Monsters project.  It will
  do the following:

  1.  Allow the user to choose a hero
  2.  Randomly select a monster
  3.  Allow the hero to battle the monster

  Once a battle concludes, the user has the option of repeating the above

*/
public class Dungeon
{
	private static Scanner keyboard = new Scanner(System.in);
	
    public static void main(String[] args)
	{

		Hero theHero;
		Monster theMonster;

		do
		{
			theHero = chooseHero();
			
		    theMonster = generateMonster();
			battle(theHero, theMonster);

		} while (playAgain());

    }//end main method

/*-------------------------------------------------------------------
chooseHero allows the user to select a hero, creates that hero, and
returns it.  It utilizes a polymorphic reference (Hero) to accomplish
this task
---------------------------------------------------------------------*/
	private static Hero chooseHero()
	{
		int choice;
		String name;
		HeroBuilder heroBuilder = new HeroBuilder();

		System.out.println("Choose a hero:\n" +
					       "1. Warrior\n" +
						   "2. Sorceress\n" +
						   "3. Thief");
		choice = keyboard.nextInt();
		keyboard.nextLine(); // clear buffer


		switch(choice)
		{
			case 1: 
				buildWarrior(heroBuilder);
				break;

			case 2: 
				buildSorceress(heroBuilder);
				break;

			default: System.out.println("invalid choice, returning Thief");
			case 3: 
				break;
		}//end switch
		
		System.out.println("Enter character name: ");
		
		//the older code didn't allow \n to be submitted
		while((name = keyboard.nextLine()).compareTo("") == 0);
		
		heroBuilder.setHeroName(name);
		
		return heroBuilder.getHero();
	}//end chooseHero method
	
	private static void buildWarrior(HeroBuilder heroBuilder)
	{
		heroBuilder.setAttackSpeed(4);
		heroBuilder.setChanceToBlock(.2);
		heroBuilder.setChanceToHit(.8);
		heroBuilder.setDamageMax(60);
		heroBuilder.setDamageMin(35);
		heroBuilder.setHitPoints(125);
		heroBuilder.setSpecialMove(HeroBuilder.SPECIAL_MOVE.CrushingBlow);
		heroBuilder.setSpecialMoveName("Crushing Blow");
		heroBuilder.setAttackMessage(" swings a mighty sword at ");
	}

	private static void buildSorceress(HeroBuilder heroBuilder)
	{
		heroBuilder.setAttackSpeed(5);
		heroBuilder.setChanceToBlock(.3);
		heroBuilder.setChanceToHit(.7);
		heroBuilder.setDamageMax(25);
		heroBuilder.setDamageMin(50);
		heroBuilder.setHitPoints(75);
		heroBuilder.setSpecialMove(HeroBuilder.SPECIAL_MOVE.IncreaseHitPoints);
		heroBuilder.setSpecialMoveName("Increase Hit Points");
		heroBuilder.setAttackMessage(" casts a spell of fireball at ");
	}
/*-------------------------------------------------------------------
generateMonster randomly selects a Monster and returns it.  It utilizes
a polymorphic reference (Monster) to accomplish this task.
---------------------------------------------------------------------*/
	private static Monster generateMonster()
	{
		int choice;
		MonsterBuilder monsterBuilder = new MonsterBuilder();

		choice = (int)(Math.random() * 3) + 1;

		switch(choice)
		{
			case 1: 
				buildOgre(monsterBuilder);
				break;

			case 2: 
				buildGremlin(monsterBuilder);
				break;

			default: System.out.println("invalid choice, returning Skeleton");
			case 3: 
				break;

		}//end switch

		return monsterBuilder.getMonster();
	}//end generateMonster method
	
	private static void buildOgre(MonsterBuilder monsterBuilder)
	{
		monsterBuilder.setAttackMessage(" slowly swings a club toward's ");
		monsterBuilder.setAttackSpeed(2);
		monsterBuilder.setChanceToHeal(.1);
		monsterBuilder.setChanceToHit(.6);
		monsterBuilder.setDamageMax(50);
		monsterBuilder.setDamageMin(30);
		monsterBuilder.setHitPoints(200);
		monsterBuilder.setMaxHeal(50);
		monsterBuilder.setMinHeal(30);
		monsterBuilder.setmonsterName("Oscar the Ogre");
	}
	
	private static void buildGremlin(MonsterBuilder monsterBuilder)
	{
		monsterBuilder.setAttackMessage(" jabs his kris at ");
		monsterBuilder.setAttackSpeed(5);
		monsterBuilder.setChanceToHeal(.4);
		monsterBuilder.setChanceToHit(.8);
		monsterBuilder.setDamageMax(30);
		monsterBuilder.setDamageMin(15);
		monsterBuilder.setHitPoints(70);
		monsterBuilder.setMaxHeal(40);
		monsterBuilder.setMinHeal(20);
		monsterBuilder.setmonsterName("Gnarltooth the Gremlin");
	}

/*-------------------------------------------------------------------
playAgain allows gets choice from user to play another game.  It returns
true if the user chooses to continue, false otherwise.
---------------------------------------------------------------------*/
	private static boolean playAgain()
	{
		String again;

		System.out.println("Play again (y/n)?");
		again = keyboard.nextLine();

		return (again.toLowerCase().equals("y"));
	}//end playAgain method


/*-------------------------------------------------------------------
battle is the actual combat portion of the game.  It requires a Hero
and a Monster to be passed in.  Battle occurs in rounds.  The Hero
goes first, then the Monster.  At the conclusion of each round, the
user has the option of quitting.
---------------------------------------------------------------------*/
	private static void battle(Hero theHero, Monster theMonster)
	{
		String continuePlaying = "play";
		System.out.println(theHero.getName() + " battles " +
							theMonster.getName());
		System.out.println("---------------------------------------------");

		//do battle
		while (theHero.isAlive() && theMonster.isAlive() && !continuePlaying.toLowerCase().equals("q"))
		{
		    //hero goes first
			boolean cont = heroAttackMenu(theHero, theMonster);

			//user chose to exit
			if(!cont){
				break;
			}
			
			//monster's turn (provided it's still alive!)
			if (theMonster.isAlive())
			    theMonster.attack(theHero);

			//let the player bail out if desired
			System.out.print("\n-->q to quit, anything else to continue: ");
			continuePlaying = keyboard.nextLine();

		}//end battle loop

		if (!theMonster.isAlive())
		    System.out.println(theHero.getName() + " was victorious!");
		else if (!theHero.isAlive())
			System.out.println(theHero.getName() + " was defeated :-(");
		else//both are alive so user quit the game
			System.out.println("Quitters never win ;-)");

	}//end battle method


	private static boolean heroAttackMenu(Hero theHero, Monster theMonster){
		theHero.setNumberOfTurns(theMonster);
		
		do{
			String choice;
			System.out.println("\n1) Attack Opponent");
			System.out.println("2) Use " + theHero.getSpecialMoveName());
		    System.out.print("Choose an option: ");
		    
		    choice = keyboard.nextLine();
		    
		    switch(choice.toLowerCase()){
		    case "1":
		    	theHero.attack(theMonster);
		    	break;
		    case "2":
		    	theHero.specialMove(theMonster);
		    	break;
		    case "q":
		    	return false;
		    default:
		    	System.out.println("Invalid choice!");	
		    }
		    
		    
		}while(theHero.getNumberOfTurns() > 0 && theHero.isAlive() && theMonster.isAlive());
		return true;
	}
}//end Dungeon class