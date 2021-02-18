using System.Collections.Generic;

namespace news_api.ViewModel
{
    public class ArticlesResultViewModel
    {        
        public string Status { get; set; }
        public string Error { get; set; }
        public int TotalResults { get; set; }
        public List<ArticleViewModel> Articles { get; set; }
    }
}

