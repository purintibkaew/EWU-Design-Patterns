
public class MoveSurpriseAttack implements SpecialMove
{

	@Override
	public void execute(DungeonCharacter source, DungeonCharacter target)
	{
		double surprise = Math.random();
		if (surprise <= .4)
		{
			System.out.println("Surprise attack was successful!\n" +
								source.getName() + " gets an additional turn.");
			source.incrementNumTurns();
			source.attack(target);
		}//end surprise
		else if (surprise >= .9)
		{
			System.out.println("Uh oh! " + target.getName() + " saw you and" +
								" blocked your attack!");
		}
		else
		    source.attack(target);
	}

}
