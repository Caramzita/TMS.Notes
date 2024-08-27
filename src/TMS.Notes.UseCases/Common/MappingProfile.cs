using AutoMapper;
using TMS.Notes.Contracts;
using TMS.Notes.UseCases.Notes;

namespace TMS.Notes.UseCases.Common;

/// <summary>
/// Профиль маппинга для преобразования объектов между различными слоями приложения.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="MappingProfile"/> и задает конфигурации маппинга.
    /// </summary>
    public MappingProfile()
    {
        CreateMap<CreateNoteRequest, NoteInputModel>();

        CreateMap<UpdateNoteRequest, NoteInputModel>();
    }
}
