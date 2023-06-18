namespace AnnoDemoWebApiEF.Models
{
    public class Annotation
    {
        public int Id { get; set; }
        public int BeginTime { get; set; }
        public string? Description { get; set; }
        public int? EndTime { get; set; }
        public string? HashTags { get; set; }
        public string? ImdbId { get; set; }
        public int? Likes { get; set; }
        public string? Title { get; set; }
        public string? User { get; set; }
    }

}
