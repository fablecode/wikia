namespace wikia.Models.Article.Details
{
    public class Revision
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int User_Id { get; set; }
        public int Timestamp { get; set; }
    }
}