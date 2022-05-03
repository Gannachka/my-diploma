
namespace Application.DTOs.ChatDTO
{
    public class MessageDeleteModelDTO
    {
        public int Id { get; set; }
        public string DeleteType { get; set; }
        public int DeletedUserId { get; set; }
    }
}
