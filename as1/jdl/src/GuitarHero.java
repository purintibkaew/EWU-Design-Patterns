public class GuitarHero 
{
	
    public static void main(String[] args) 
    {
        GameCharacter player1 = new GameCharacterSlash();
        GameCharacter player2 = new GameCharacterHendrix();
        GameCharacter player3 = new GameCharacterYoung();
        
        System.out.print("Slash has started playing the guitar: ");
        player1.playGuitar();
        
        player1.setGuitar(new GuitarGibsonSG());
        System.out.print("Slash has switched guitars to: ");
        player1.playGuitar();
        
        System.out.print("Hendrix has started playing the guitar: ");
        player2.playGuitar();
        
        System.out.print("Slash has started playing his solo: ");
        player1.playSolo();
        
        System.out.print("Hendrix has started playing his solo: ");
        player2.playSolo();
        
        System.out.print("Young has started playing his solo: ");
        player3.playSolo();
        
        player3.setSolo(new SoloSmashGuitar());
        System.out.print("Young has switched his solo style to: ");
        player3.playSolo();
        
        System.out.print("Young has started playing his guitar: ");
        player3.playGuitar();
    }
    
}