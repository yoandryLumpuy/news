
namespace news_api.DataTransferObjects
{
    public class UserForListDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string[] Roles { get; set; }
    }
}
