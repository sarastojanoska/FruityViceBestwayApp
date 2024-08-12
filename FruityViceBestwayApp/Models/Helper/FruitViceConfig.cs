namespace FruityViceBestwayApp.Models.Helper
{
    public class FruitViceConfig
    {
        public string BaseUrl { get; set; }
        
        public class ApiEndpoints
        {
            public static readonly string GetFruit = "api/fruit";
            public static readonly string GetAll = "api/fruit/all";
            public static readonly string PutFruit = "api/fruit";
        }
    }
}
