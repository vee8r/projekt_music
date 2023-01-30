using projekt_programowanie.Data;
using System.Security.Claims;

namespace projekt_programowanie.Services.OpinionService
{
    public class OpinionService : IOpinionService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public OpinionService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetSongDto>> AddOpinion(AddOpinionDto newOpinion)
        {
            var response = new ServiceResponse<GetSongDto>();
            try
            {
                var song = await _context.Songs
                    .FirstOrDefaultAsync(c => c.Id == newOpinion.SongId &&
                        c.User!.Id == int.Parse(_httpContextAccessor.HttpContext!.User
                            .FindFirstValue(ClaimTypes.NameIdentifier)!));

                if (song is null)
                {
                    response.Success = false;
                    response.Message = "Song not found.";
                    return response;
                }

                var opinion = new Opinion
                {
                    Description = newOpinion.Description,
                    
                    Song = song
                };

                _context.Opinions.Add(opinion);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetSongDto>(song);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
