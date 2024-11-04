namespace school.Models
{
    public record CreateStudentDto(string Name, int Age, string Email); //students
    //
    public record CreateMarkDto(string Marknumber, string Marktext, string Description, Guid StudentId);//marks

}
