
public class MapPortal : MapUnit
{
    public int destination;
    public int costType;//0no,1stone
    public int costParam;

    public MapPortal(){
        Id = 1;
        Desc = "下面是一个山洞，进去看看吗？";
        Image = 1;
        destination = 1;
        costType = 0;
        costParam = 0;
    }
}
