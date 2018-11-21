public class MapInfo
{

    public int id;
    public int[] connections;//互相连通的地图
    public int xRange;
    public int yRange;
    public int[] cellList;
    public int groundType = 0;

    public MapInfo(int id,int[] connections,int x,int y, int[] cellList){
        this.id = id;
        this.connections = connections;
        this.xRange = x;
        this.yRange = y;
        this.cellList = cellList;
    }
}
