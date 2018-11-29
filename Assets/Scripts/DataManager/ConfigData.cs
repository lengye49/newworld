
public class ConfigData 
{
    public static int DefParam = 60;
    public static int[,] LandFormsList = new int[8, 14]{
        {0,1,0,0,1,0,1,0,0,1,0,0,1,1},
        {1,1,0,0,0,1,0,0,1,1,0,0,1,1},
        {1,1,1,0,0,1,0,0,0,1,1,0,1,0},
        {1,1,1,0,0,1,0,0,1,1,1,0,1,0},
        {0,1,0,0,0,1,0,0,0,1,0,0,1,1},
        {0,0,1,0,0,1,0,0,1,1,0,1,1,1},
        {0,0,0,1,0,1,0,1,0,1,0,0,1,1},
        {1,0,0,0,0,0,0,0,1,1,0,0,1,1},
    };
    public static int[] LandFormsProp = new int[8] { 4, 6, 10, 8, 8, 6, 8, 4 };

    public static int MaxLandFormCount = 20;
}
