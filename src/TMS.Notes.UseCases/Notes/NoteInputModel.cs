namespace TMS.Notes.UseCases.Notes;

/// <summary>
/// Модель заметки.
/// </summary>
public class NoteInputModel
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Заголовок.
    /// </summary>
    public string Title { get; set; } = string.Empty;


    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
