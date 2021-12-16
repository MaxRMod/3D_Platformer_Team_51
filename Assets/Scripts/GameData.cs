public sealed class GameData 
{
    private static GameData instance;
    private int score=0;
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

    public int Lives{

        get;
        set;
    
    }
}
