namespace _5051.Models
{
    /// <summary>
    /// The Mock Status
    /// </summary>
    public enum DataSourceEnum
    {
        // Not specified
        Unknown = 0,

        // Mock Dataset
        Mock = 1,

        //TODO, remove
        SQL = 2,

        // Data Storage
        Local = 10,
        ServerTest = 11,
        ServerLive = 12
    }
}