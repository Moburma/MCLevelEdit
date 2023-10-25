namespace MCLevelEdit.DataModel;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
public struct UAxis2d
{
    [FieldOffset(0)]
    public byte X;
    [FieldOffset(1)]
    public byte Y;
    [FieldOffset(0)]
    public ushort Word;
}

