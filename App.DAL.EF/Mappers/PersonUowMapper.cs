using App.DAL.DTO;
using App.Domain;
using Base.Contracts;

namespace App.DAL.EF.Mappers;

public class PersonUowMapper : IMapper<PersonDalDto, Person>
{
    public PersonDalDto? Map(Person? entity)
    {
        if (entity == null) return null;

        var dto = new PersonDalDto()
        {
            Id = entity.Id,
            PersonFirstName = entity.PersonFirstName,
            PersonLastName = entity.PersonLastName,
            PersonPhoneNumber = entity.PersonPhoneNumber,
            PersonAddress = entity.PersonAddress,
            PersonGender = entity.PersonGender,
            PersonDateOfBirth = entity.PersonDateOfBirth,
        };

        return dto;
    }

    public Person? Map(PersonDalDto? dto)
    {
        if (dto == null) return null;

        var entity = new Person()
        {
            Id = dto.Id,
            UserId = dto.UserId,
            PersonFirstName = dto.PersonFirstName,
            PersonLastName = dto.PersonLastName,
            PersonPhoneNumber = dto.PersonPhoneNumber,
            PersonAddress = dto.PersonAddress,
            PersonGender = dto.PersonGender,
            PersonDateOfBirth = dto.PersonDateOfBirth,
        };

        return entity;
    }
}