
public class MapPortal : MapUnit
{
    public int destination;
    public int costType;//0no,1stone
    public int costParam;

    public MapPortal(){
        Id = 1;
        Image = 1;
        destination = 1;
        costType = 0;
        costParam = 0;
    }
}
