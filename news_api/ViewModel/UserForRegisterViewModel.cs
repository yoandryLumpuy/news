using NewsApiClientClasses.Constants;

namespace news_api.ViewModel
{
    public class UserForRegisterViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Countries Country { get; set; }
        public Languages Language { get; set; }
    }
}
