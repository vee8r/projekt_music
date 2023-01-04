
namespace projekt_programowanie.Dtos.Song
{
    public class GetSongDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Physical";
        public string Artist { get; set; } = "Dua Lipa";

        public DateTime ReleaseDate { get; set; } = new DateTime(2020, 10, 24);
        public Models.Type Class { get; set; } = Models.Type.Pop;
    }
}
