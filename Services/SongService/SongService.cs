using projekt_programowanie.Models;

namespace projekt_programowanie.Services.SongService
{
    public class SongService : ISongService
    {
        private static List<Song> songs = new List<Song> {
            new Song(),
            new Song { Id = 1, Name = "Poker face"}
        };

        

        public List<Song> AddSong(Song newSong)
        {
            songs.Add(newSong);
            return songs;
        }

        public List<Song> GetAllSongs()
        {
            return songs;
        }

        public Song GetSongById(int id)
        {
            return songs.FirstOrDefault(m => m.Id == id);
        }
    }
}
