namespace TMS.Notes.UseCases.Dtos;

/// <summary>
/// Dto добавление заметки.
/// </summary>
public class CreateNoteDto
{
    /// <summary>
    /// Заголовок.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
