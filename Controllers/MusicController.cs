using Microsoft.AspNetCore.Mvc;
using projekt_programowanie.Models;
using projekt_programowanie.Services.SongService;
using Microsoft.AspNetCore.Authorization;

namespace projekt_programowanie.Controllers
{
    [Authorize]
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

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetSongDto>>>> UpdateSong(UpdateSongDto updatedSong)
        {
            var response = await _songService.UpdateSong(updatedSong);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetSongDto>>> DeleteSong(int id)
        {
            var response = await _songService.DeleteSong(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
