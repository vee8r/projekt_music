using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projekt_programowanie.Services.OpinionService;

namespace projekt_programowanie.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OpinionController : ControllerBase
    {
        private readonly IOpinionService _opinionService;
        public OpinionController(IOpinionService weaponService)
        {
            _opinionService = weaponService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetSongDto>>> AddOpinion(AddOpinionDto newOpinion)
        {
            return Ok(await _opinionService.AddOpinion(newOpinion));
        }
    }
}
