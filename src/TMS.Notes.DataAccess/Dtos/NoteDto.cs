namespace TMS.Notes.DataAccess.Dtos;

/// <summary>
/// Класс заметки для обращения в бд.
/// </summary>
public class NoteDto
{
    /// <summary>
    /// Идентификатор заметки.
    /// </summary>
    public Guid Id { get; set; }

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

    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Дата изменения.
    /// </summary>
    public DateTime? EditDate { get; set; }
}
