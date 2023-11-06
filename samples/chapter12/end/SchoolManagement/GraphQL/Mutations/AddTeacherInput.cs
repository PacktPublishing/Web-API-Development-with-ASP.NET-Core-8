namespace SchoolManagement.GraphQL.Mutations;

//public class AddTeacherInput
//{
//    public string FirstName { get; set; } = string.Empty;
//    public string LastName { get; set; } = string.Empty;
//    public string Email { get; set; } = string.Empty;
//    public string Phone { get; set; } = string.Empty;
//    public string? Bio { get; set; } = string.Empty;
//}

public record AddTeacherInput(
    string FirstName,
    string LastName,
    string Email,
    string? Phone,
    string? Bio);