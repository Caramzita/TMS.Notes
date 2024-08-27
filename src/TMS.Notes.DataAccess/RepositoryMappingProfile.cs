using AutoMapper;
using TMS.Notes.Core;
using TMS.Notes.DataAccess.Dtos;

namespace TMS.Notes.DataAccess;

/// <summary>
/// Профиль маппинга для AutoMapper, определяющий преобразования между сущностями домена и DTO.
/// </summary>
public class RepositoryMappingProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="RepositoryMappingProfile"/> и задает конфигурации маппинга.
    /// </summary>
    public RepositoryMappingProfile()
    {
        CreateMap<NoteDto, Note>().ReverseMap();
    }
}
