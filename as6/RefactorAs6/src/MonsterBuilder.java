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
    	monsterName = "Gnarltooth the Gremlin";
    	hitPoints = 70;
    	attackSpeed = 5;
    	chanceToHit = .8;
    	damageMin = 15;
    	damageMax = 30;
    	chanceToHeal = .4;
    	minHeal = 20;
    	maxHeal = 40;
    	attackMessage = " jabs his kris at ";
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