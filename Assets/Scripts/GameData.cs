public sealed class GameData 
{
    private static GameData instance;
    private int coins;
     private int score;

    private GameData(){
            if(instance != null)
                return ;
            instance = this;
    }

    public static GameData Instance{
            get
            {
                if(instance == null)
                {
                    instance = new GameData();
                }
                return instance;

            }

    }

    public int Coins{

        get
        {
            return coins;
        }
        set
        {
            coins = value;
        }
    
    }

    public int Score{
        
        get
        {
            return score;
        }
        
        set
        {
            score = value;
        }
        
    
    }
}
