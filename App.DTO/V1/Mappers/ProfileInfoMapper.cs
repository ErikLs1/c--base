using App.BLL.DTO;
using App.DTO.V1.DTO;

namespace App.DTO.V1.Mappers;

public class ProfileInfoMapper
{
    public ProfileInfoDto? Map(ProfileInfoBllDto? entity, Guid userId, string email)
    {
        if (entity == null) return null;

        var res = new ProfileInfoDto()
        {
            UserId = userId,
            Email = email,
            PersonFirstName = entity.PersonFirstName,
            PersonLastName = entity.PersonLastName,
            PersonPhoneNumber = entity.PersonPhoneNumber,
            PersonAddress = entity.PersonAddress,
            PersonGender = entity.PersonGender,
            PersonDateOfBirth = entity.PersonBirthDate,
        };
        return res;
    }
}