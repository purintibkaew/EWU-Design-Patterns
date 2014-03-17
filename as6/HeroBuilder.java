public class HeroBuilder
{	
    public static enum HEROES
    { 
    	Warrior,
    	Sorceress,
    	Thief
    }
    
    public static enum SPECIAL_MOVE
    { 
    	CrushingBlow,
    	IncreaseHitPoints,
    	SurpriseAttack
    }
    
    private HEROES heroType;
    private String heroName;
    private int hitPoints;
    private int attackSpeed;
    private double chanceToHit;
    private int damageMin;
    private int damageMax;
    private double chanceToBlock;
    private SpecialMove specialMove;
    private String specialMoveName;
    
    
    public HeroBuilder()
    {
    	heroType = HEROES.Thief;
    	heroName = "Thief";
    	hitPoints = 75;
    	attackSpeed = 6;
    	chanceToHit = .8;
    	damageMin = 20;
    	damageMax = 40;
    	chanceToBlock = .5;
    	specialMove = new MoveSurpriseAttack();
    	specialMoveName = "Surprise Attack";
    }
    
    public void setHeroType(HEROES type)
    {
    	heroType = type;
    }
    
    public void setHeroName(String name)
    {
    	heroName = name;
    }
    
    public void setHitPoints(int hp)
    {
    	hitPoints = hp;
    }
    
    public void setAttackSpeed(int as)
    {
    	attackSpeed = as;
    }
    
    public void setChanceToHit(double cth)
    {
    	chanceToHit = cth;
    }
    
    public void setDamageMin(int dmin)
    {
    	damageMin = dmin;
    }
    
    public void setDamageMax(int dmax)
    {
    	damageMax = dmax;
    }
    
    public void setChanceToBlock(double ctb)
    {
    	chanceToBlock = ctb;
    }
    
    public void setSpecialMove(SPECIAL_MOVE type)
    {
    	switch(type)
    	{
	    	case CrushingBlow:
	    		specialMove = new MoveCrushingBlow();
	    	case IncreaseHitPoints:
	    		specialMove = new MoveIncreaseHitPoints();
	    	default:
	    	case SurpriseAttack:
	    		specialMove = new MoveSurpriseAttack();
    	}
    }

    public void setSpecialMoveName(String smn)
    {
    	specialMoveName = smn;
    }
    
	public Hero getHero()
	{
		Hero heroToBuild = null;		
		
		switch(heroType)
		{
			case Warrior:
				heroToBuild = new Warrior(heroName, hitPoints, attackSpeed, chanceToHit, damageMin, damageMax, chanceToBlock, specialMove, specialMoveName);
			case Sorceress:
				heroToBuild = new Sorceress(heroName, hitPoints, attackSpeed, chanceToHit, damageMin, damageMax, chanceToBlock, specialMove, specialMoveName);
			default:
			case Thief:
				heroToBuild = new Thief(heroName, hitPoints, attackSpeed, chanceToHit, damageMin, damageMax, chanceToBlock, specialMove, specialMoveName);
		}
		
		return heroToBuild;
	}
}