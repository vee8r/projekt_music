global using AutoMapper;
using projekt_programowanie.Data;
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
        private readonly DataContext _context;

        public SongService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

       
        public async Task<ServiceResponse<List<GetSongDto>>> AddSong(AddSongDto newSong)
        {
            var serviceResponse = new ServiceResponse<List<GetSongDto>>();
            var song = _mapper.Map<Song>(newSong);
            song.Id = songs.Max(c => c.Id) + 1;
            songs.Add(song);
            serviceResponse.Data= songs.Select(c => _mapper.Map<GetSongDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSongDto>>> DeleteSong(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetSongDto>>();

            try
            {
                var song = songs.FirstOrDefault(c => c.Id == id);
                if (song is null)
                    throw new Exception($"Song with Id '{id}' not found.");

                songs.Remove(song);


                serviceResponse.Data = songs.Select(c => _mapper.Map<GetSongDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetSongDto>>> GetAllSongs()
        {
            var serviceResponse = new ServiceResponse<List<GetSongDto>>();
            var dbSongs = await _context.Songs.ToListAsync();
            serviceResponse.Data= dbSongs.Select(c => _mapper.Map<GetSongDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSongDto>> GetSongById(int id)
        {
            var serviceResponse = new ServiceResponse<GetSongDto>();
            var dbSong = await _context.Songs.FirstOrDefaultAsync(m => m.Id == id);
            serviceResponse.Data = _mapper.Map<GetSongDto>(dbSong);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSongDto>> UpdateSong(UpdateSongDto updatedSong)
        {
            var serviceResponse = new ServiceResponse<GetSongDto>();

            try
            {
                var song = songs.FirstOrDefault(c => c.Id == updatedSong.Id);
                if (song is null)
                    throw new Exception($"Song with Id '{updatedSong.Id}' not found.");

               
                song.Name = updatedSong.Name;
                song.Artist = updatedSong.Artist;
                song.ReleaseDate = updatedSong.ReleaseDate;
                song.Class = updatedSong.Class;


                serviceResponse.Data = _mapper.Map<GetSongDto>(song);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }
    }
}
