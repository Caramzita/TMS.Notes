using AutoMapper;
using TMS.Notes.Contracts;
using TMS.Notes.UseCases.Notes;

namespace TMS.Notes.UseCases.Common;

/// <summary>
/// Конфигурация автомаппера
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateNoteRequest, NoteInputModel>();

        CreateMap<UpdateNoteRequest, NoteInputModel>();
    }
}
