namespace _5051.Models.Enums
{
    /// <summary>
    /// Track the Status of the Student
    /// </summary>
    public enum AttendanceStatusEnum
    {
        // Invalid
        Unknown = 0,

        // Attended school
        Present = 1,

        //Absent excused
        AbsentExcused = 2,

        //Absent unexcused
        AbsentUnexcused = 3,
    }
}