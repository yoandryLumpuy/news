namespace news_api.Extensions
{
    public class Constants
    {        
        //roles
        public const string RoleNameAdmin = "Admin";    

        public const string RoleNameNormalUser = "NormalUser" ;

        //policies
        public const string PolicyNameAdmin = "PolicyAdmin"; 
        public const string PolicyNameNormalAccess = "PolicyNormalAccess";   



        //constants for sorting query traces...
        public const string SortByCreatedByUser = "CreatedByUser";
        public const string SortByCreatedAt = "CreatedAt";
        public const string SortByFromDatetime = "From";
        public const string SortByToDatetime = "To";

        public const string SortByLanguage = "Language";

        public const string SortByQ = "Q";

        public const string SortByCategory = "Category";
        public const string SortByCountry = "Country";
        // ----------------------------------------------
        
    }

}
