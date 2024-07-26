using AutoMapper;
using TMS.Notes.UseCases.Dtos;
using TMS.Notes.UseCases.Notes;

namespace TMS.Notes.UseCases;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateNoteDto, NoteInputModel>();

        CreateMap<UpdateNoteDto, NoteInputModel>();
    }
}
