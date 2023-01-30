global using AutoMapper;
using System.Security.Claims;
using projekt_programowanie.Data;
using projekt_programowanie.Dtos.Song;
using projekt_programowanie.Models;

namespace projekt_programowanie.Services.SongService
{
    public class SongService : ISongService
    {
        
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SongService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);


        public async Task<ServiceResponse<List<GetSongDto>>> AddSong(AddSongDto newSong)
        {
            var serviceResponse = new ServiceResponse<List<GetSongDto>>();
            var song = _mapper.Map<Song>(newSong);
            song.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            serviceResponse.Data=
               await _context.Songs
                    .Where(c => c.User!.Id == GetUserId())
                    .Select(c => _mapper.Map<GetSongDto>(c))
                    .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetSongDto>>> DeleteSong(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetSongDto>>();

            try
            {
                var song = await _context.Songs
                    .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
                if (song is null)
                    throw new Exception($"Song with Id '{id}' not found.");

               _context.Songs.Remove(song);

                await _context.SaveChangesAsync();

                serviceResponse.Data = 
                    await _context.Songs
                    .Where(c => c.User!.Id == GetUserId())
                        .Select(c => _mapper.Map<GetSongDto>(c)).ToListAsync();
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
            var dbSongs = await _context.Songs
             .Include(g => g.Opinion)
             .Where(g => g.User!.Id == GetUserId())
             .ToListAsync();
            serviceResponse.Data = dbSongs.Select(g => _mapper.Map<GetSongDto>(g)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSongDto>> GetSongById(int id)
        {
            var serviceResponse = new ServiceResponse<GetSongDto>();
            var dbSong = await _context.Songs
                .Include(c => c.Opinion)
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetSongDto>(dbSong);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSongDto>> UpdateSong(UpdateSongDto updatedSong)
        {
            var serviceResponse = new ServiceResponse<GetSongDto>();

            try
            {
                var song =
                   await _context.Songs
                        .Include(c => c.User)
                        .FirstOrDefaultAsync(c => c.Id == updatedSong.Id);
                if (song is null || song.User!.Id != GetUserId())
                    throw new Exception($"Song with Id '{updatedSong.Id}' not found.");

               
                song.Name = updatedSong.Name;
                song.Artist = updatedSong.Artist;
                song.ReleaseDate = updatedSong.ReleaseDate;
                song.Class = updatedSong.Class;

                await _context.SaveChangesAsync();
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
