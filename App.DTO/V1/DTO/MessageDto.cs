namespace App.DTO.V1.DTO;

public class MessageDto
{
    public MessageDto()
    {
    }

    public MessageDto(params string[] messages)
    {
        Messages = messages;
    }

    public ICollection<string> Messages { get; set; } = new List<string>();
}