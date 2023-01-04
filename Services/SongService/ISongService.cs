using projekt_programowanie.Dtos.Song;

namespace projekt_programowanie.Services.SongService
{
    public interface ISongService
    {
        Task<ServiceResponse<List<GetSongDto>>> GetAllSongs();
        Task<ServiceResponse<GetSongDto>> GetSongById(int id);
        Task<ServiceResponse<List<GetSongDto>>> AddSong(AddSongDto newSong);
    }
}
