namespace TMS.Notes.UseCases.Dtos;

/// <summary>
/// Dto обновление заметки.
/// </summary>
public class UpdateNoteDto
{
    /// <summary>
    /// Идентификатор заметки.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Заголовок.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
