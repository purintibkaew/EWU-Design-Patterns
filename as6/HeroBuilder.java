public class HeroBuilder
{    
    public static enum SPECIAL_MOVE
    { 
    	CrushingBlow,
    	IncreaseHitPoints,
    	SurpriseAttack
    }
    
    private String heroName;
    private int hitPoints;
    private int attackSpeed;
    private double chanceToHit;
    private int damageMin;
    private int damageMax;
    private double chanceToBlock;
    private SpecialMove specialMove;
    private String specialMoveName;
    private String attackMessage;
    
    
    public HeroBuilder()
    {
    	heroName = "Thief";
    	hitPoints = 75;
    	attackSpeed = 6;
    	chanceToHit = .8;
    	damageMin = 20;
    	damageMax = 40;
    	chanceToBlock = .5;
    	specialMove = new MoveSurpriseAttack();
    	specialMoveName = "Surprise Attack";
    	attackMessage = " slices his rusty blade at ";
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
    
    public void setAttackMessage(String am)
    {
    	attackMessage = am;
    }
    
	public Hero getHero()
	{
		return new Hero(heroName, hitPoints, attackSpeed, 
						chanceToHit, damageMin, damageMax, 
						chanceToBlock, specialMove, 
						specialMoveName, attackMessage);
	}
}