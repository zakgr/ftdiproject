namespace Lib.Glue
{
    /// <summary>
    /// Hello World
    /// </summary>
    public interface IRelay
    {
        bool IsOpen { get; set; }
        int Index { get; }
        bool SetStatus(bool status);
    }
}
