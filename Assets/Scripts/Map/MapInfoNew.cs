public class MapInfoNew
{

    public int id;
    public int type;
    public bool isDesigned;

    public int xRange;
    public int yRange;
    public int blockCount;
    public int[] cellList;
}

public enum TerrainType{
    Desert,//沙漠
    Gobi,//戈壁
    Land,//土地
    Grassland,//绿草地
    Snowfield,//雪地
    Underwater,//水下
    Cave,//山洞
}
