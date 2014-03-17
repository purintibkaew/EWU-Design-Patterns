

public class MonsterBuilder
{    
    private String monsterName;
    private int hitPoints;
    private int attackSpeed;
    private double chanceToHit;
    private int damageMin;
    private int damageMax;
    private double chanceToHeal;
    private int minHeal;
    private int maxHeal;
    private String attackMessage;
    
    
    public MonsterBuilder()
    {
    	monsterName = "Sargath the Skeleton";
    	hitPoints = 100;
    	attackSpeed = 3;
    	chanceToHit = .8;
    	damageMin = 30;
    	damageMax = 50;
    	chanceToHeal = .3;
    	minHeal = 30;
    	maxHeal = 50;
    	attackMessage = " slices his rusty blade at ";
    }
    
    public void setmonsterName(String name)
    {
    	monsterName = name;
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
    
    public void setChanceToHeal(double cth)
    {
    	chanceToHeal = cth;
    }
    
    public void setMinHeal(int hmin)
    {
    	minHeal = hmin;
    }
    
    public void setMaxHeal(int hmax)
    {
    	maxHeal = hmax;
    }
    
    public void setAttackMessage(String am)
    {
    	attackMessage = am;
    }
    
	public Monster getMonster()
	{
		return new Monster(	monsterName, hitPoints, attackSpeed, 
							chanceToHit, chanceToHeal, damageMin, 
							damageMax, minHeal, maxHeal, 
							attackMessage);
	}
}