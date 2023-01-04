using Microsoft.AspNetCore.Mvc;
using projekt_programowanie.Models;
using projekt_programowanie.Services.SongService;

namespace projekt_programowanie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : ControllerBase 
    {
        
        private readonly ISongService _songService;

        public MusicController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetSongDto>>>> Get()
        {
            return Ok(await _songService.GetAllSongs());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetSongDto>>> GetSingle(int id)
        {
            return Ok(await _songService.GetSongById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetSongDto>>>> AddSong(AddSongDto newSong)
        {
            return Ok(await _songService.AddSong(newSong));
        }
    }
}
