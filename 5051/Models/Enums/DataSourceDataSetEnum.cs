namespace _5051.Models
{
    /// <summary>
    /// The Type of Data running.
    /// </summary>
    public enum DataSourceDataSetEnum
    {
        // Not specified
        Default = 0,

        // Mock Dataset
        Demo = 1,

        // SQL Dataset
        UnitTest = 2
    }
}