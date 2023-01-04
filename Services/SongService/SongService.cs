global using AutoMapper;
using projekt_programowanie.Dtos.Song;
using projekt_programowanie.Models;

namespace projekt_programowanie.Services.SongService
{
    public class SongService : ISongService
    {
        private static List<Song> songs = new List<Song> {
            new Song(),
            new Song { Id = 1, Name = "Poker face"}
        };
        private readonly IMapper _mapper;

        public SongService(IMapper mapper)
        {
            _mapper = mapper;
        }

       
        public async Task<ServiceResponse<List<GetSongDto>>> AddSong(Song newSong)
        {
            var serviceResponse = new ServiceResponse<List<GetSongDto>>();
            songs.Add(_mapper.Map<Song>(newSong));
            serviceResponse.Data= songs.Select(c => _mapper.Map<GetSongDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSongDto>>> GetAllSongs()
        {
            var serviceResponse = new ServiceResponse<List<GetSongDto>>();
            serviceResponse.Data= songs.Select(c => _mapper.Map<GetSongDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSongDto>> GetSongById(int id)
        {
            var serviceResponse = new ServiceResponse<GetSongDto>();
            var song = songs.FirstOrDefault(m => m.Id == id);
            serviceResponse.Data = _mapper.Map<GetSongDto>(song);
            return serviceResponse;
        }
    }
}
