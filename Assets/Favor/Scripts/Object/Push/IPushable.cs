using System.Diagnostics;

public interface IPushable
{
    void Push();
    int MassLimit { get; set; }
}