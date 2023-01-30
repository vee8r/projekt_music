namespace projekt_programowanie.Models

{
    public class Opinion
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Song? Song { get; set; }
        public int SongId { get; set; }
    }
}
