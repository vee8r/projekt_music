using Microsoft.AspNetCore.Mvc;
using projekt_programowanie.Models;

namespace projekt_programowanie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : ControllerBase 
    {
        private static List<Song> songs = new List<Song> {
            new Song(),
            new Song { Id = 1, Name = "Poker face"}
        };

        [HttpGet("GetAll")]
        public ActionResult<List<Song>> Get()
        {
            return Ok(songs);
        }

        [HttpGet("{id}")]
        public ActionResult<Song> GetSingle(int id)
        {
            return Ok(songs.FirstOrDefault( m => m.Id == id));
        }

        [HttpPost]
        public ActionResult<List<Song>> AddSong(Song newSong)
        {
            songs.Add(newSong);
            return Ok(songs);
        }

    }
}
