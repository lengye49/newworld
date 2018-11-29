public class MapInfo
{

    public int Id;
    public int Type;
    public bool IsDesigned;

    public int xRange;
    public int yRange;
    public int BlockCount;
    public int[] DegisnList;

    public MapInfo(int id, int type, int rows, int columns, int blocks, int designType, int[] designList)
    {
        this.Id = id;
        this.Type = type;
        this.IsDesigned = designType > 0;
        this.xRange = rows;
        this.yRange = columns;
        this.BlockCount = blocks;
        this.DegisnList = designList;
    }
}

