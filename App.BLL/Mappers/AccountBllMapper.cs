using App.BLL.DTO.Identity;
using App.DAL.DTO;

namespace App.BLL.Mappers;

public class AccountBllMapper
{
    public PersonDalDto? Map(RegisterBllDto? entity)
    {
        if (entity == null) return null;

        var dto = new PersonDalDto()
        {
            PersonFirstName   = entity.FirstName,
            PersonLastName    = entity.LastName,
            PersonAddress     = entity.Address,
            PersonPhoneNumber = entity.PhoneNumber,
            PersonGender      = entity.Gender,
            PersonDateOfBirth = entity.DateOfBirth,
        };

        return dto;
    }
}