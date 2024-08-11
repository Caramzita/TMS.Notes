namespace TMS.Notes.Core;

/// <summary>
/// Модель заметки.
/// </summary>
public class Note
{
    /// <summary>
    /// Максимальное значение длины заголовка.
    /// </summary>
    public const int MAX_LENGHT_TITLE = 50;

    /// <summary>
    /// Максимальное значение длины описания.
    /// </summary>
    public const int MAX_LENGHT_DESCRIPTION = 250;

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
    public DateTime EditDate { get; set; }
}
