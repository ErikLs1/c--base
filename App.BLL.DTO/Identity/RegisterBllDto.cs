namespace App.BLL.DTO.Identity;

public class RegisterBllDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Gender { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; } = default!;
}