namespace school.Models
{
    public record CreateStudentDto(string Name, int Age, string Email); //students
    //
    public record CreateMarkDto(string Marknumber, string Marktext, string Description, Guid StudentId);//marks
    //
    public record UpdateStudentDto(string Name, int Age, string Email); //student
    //
    public record UpdateMarkDto(string Marknumber, string Marktext, string Description, DateTime UpdatedTime);//marks
}
