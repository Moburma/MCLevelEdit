namespace MCLevelEdit.DataModel;

public class UAxis2d
{
    public Axis2d Axis2D { get; set; }
    public ushort Word { get; set; }

    public UAxis2d()
    {
        Axis2D = new Axis2d();
    }
}

