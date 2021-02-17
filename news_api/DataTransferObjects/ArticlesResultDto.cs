using System.Collections.Generic;

namespace news_api.DataTransferObjects
{
    public class ArticlesResultDto
    {        
        public string Status { get; set; }
        public string Error { get; set; }
        public int TotalResults { get; set; }
        public List<ArticleDto> Articles { get; set; }
    }
}

