namespace App.DTO.V1.DTO;

public class ProfileInfoDto
{
    public Guid UserId { get; set; }
    public string PersonFirstName { get; set; } = default!;
    public string PersonLastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PersonPhoneNumber { get; set; } = default!;
    public string PersonAddress { get; set; } = default!; 
    public string PersonGender { get; set; } = default!;
    public DateOnly? PersonDateOfBirth { get; set; }
}