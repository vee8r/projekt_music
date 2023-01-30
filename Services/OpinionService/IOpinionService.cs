namespace projekt_programowanie.Services.OpinionService
{
    public interface IOpinionService
    {
        Task<ServiceResponse<GetSongDto>> AddOpinion(AddOpinionDto newOpinion);
    }
}
