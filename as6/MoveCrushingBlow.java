
public class MoveCrushingBlow implements SpecialMove
{

	@Override
	public void execute(DungeonCharacter source, DungeonCharacter target)
	{
		if (Math.random() <= .4)
		{
			int blowPoints = (int)(Math.random() * 76) + 100;
			System.out.println(source.getName() + " lands a CRUSHING BLOW for " + blowPoints
								+ " damage!");
			target.subtractHitPoints(blowPoints);
		}//end blow succeeded
		else
		{
			System.out.println(source.getName() + " failed to land a crushing blow");
			System.out.println();
		}//blow failed
	}

}
