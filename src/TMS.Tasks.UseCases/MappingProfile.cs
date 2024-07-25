using AutoMapper;
using TMS.Tasks.Core.Models;
using TMS.Tasks.UseCases.Commands;

namespace TMS.Tasks.UseCases;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<NoteInputModel, Note>()
        .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
    }
}
