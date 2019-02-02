
namespace _5051.Models.Enums
{
    /// <summary>
    /// The School day is full, ends early, starts late etc.
    /// </summary>
    public enum SchoolCalendarDismissalEnum
    {
        // Not specified
        Unknown = 0,

        // Regular start or stop
        Normal = 1,

        // Early start, or early dismissal (example Wed early dismissal)
        Early = 2,

        // Late start or dismissal.  (example school delay start)
        Late = 3,
    }
}