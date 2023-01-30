namespace projekt_programowanie
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Song,GetSongDto>();
            CreateMap<AddSongDto, Song>();
            CreateMap<UpdateSongDto, Song>();
            CreateMap<Opinion, GetOpinionDto>();
            CreateMap<AddOpinionDto, Opinion>();
        }
    }
}
