
public class MoveIncreaseHitPoints implements ISpecialMove
{

	private final int MIN_ADD = 25;
	private final int MAX_ADD = 50;
	
	@Override
	public void execute(DungeonCharacter source, DungeonCharacter target) {
		int hPoints;

		hPoints = (int)(Math.random() * (MAX_ADD - MIN_ADD + 1)) + MIN_ADD;
		source.addHitPoints(hPoints);
		System.out.println(source.getName() + " added [" + hPoints + "] points.\n"
							+ "Total hit points remaining are: "
							+ source.getHitPoints());
		 System.out.println();


	}

}
