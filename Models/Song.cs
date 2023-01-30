namespace projekt_programowanie.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Physical";
        public string Artist { get; set; } = "Dua Lipa";

        public DateTime ReleaseDate { get; set; } = new DateTime(2020, 10, 24);
        public Type Class { get; set; } = Type.Pop;
        public User? User { get; set; }
        public Opinion? Opinion { get; set; }
    }
}
