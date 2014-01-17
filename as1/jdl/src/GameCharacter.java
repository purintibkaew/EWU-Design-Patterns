
public class GameCharacter 
{

	private IGuitar guitar;
	private ISolo solo;
	

	public void playGuitar() 
	{
		guitar.play();
	}

	public void playSolo() 
	{
		solo.play();
	}

	public void setGuitar(IGuitar g)
	{
		guitar = g;
	}
	
	public void setSolo(ISolo s)
	{
		solo = s;
	}
}
