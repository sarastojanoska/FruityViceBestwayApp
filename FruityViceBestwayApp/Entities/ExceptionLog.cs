namespace FruityViceBestwayApp.Entities
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateOccurred { get; set; }
    }
}
