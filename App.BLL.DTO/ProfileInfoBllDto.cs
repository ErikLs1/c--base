namespace App.BLL.DTO;

public class ProfileInfoBllDto
{
    public string PersonFirstName { get; set; } = default!;
    public string PersonLastName { get; set; } = default!;
    public string PersonPhoneNumber { get; set; } = default!;
    public string PersonAddress { get; set; } = default!;
    public string PersonGender { get; set; } = default!;
    public DateOnly? PersonBirthDate { get; set; }
}