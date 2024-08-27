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
    public Guid Id { get; private set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; private set; }

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
    public DateTime CreationDate { get; }

    /// <summary>
    /// Дата изменения.
    /// </summary>
    public DateTime? EditDate { get; private set; }
    
    /// <summary>
    /// Создает экземпляр класса <see cref="Note"/>.
    /// </summary>
    /// <param name="title"> Заголовок. </param>
    /// <param name="description"> Описание. </param>
    /// <param name="userId"> Идентификатор пользователя. </param>
    public Note(string title, string description, Guid userId)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        UserId = userId;
        CreationDate = DateTime.UtcNow;
    }

    /// <summary>
    /// Конструктор для инициализации модели уже существующей заметки.
    /// </summary>
    /// <param name="id"> Идентификатор. </param>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <param name="title"> Заголовок. </param>
    /// <param name="description"> Описание. </param>
    /// <param name="creationDate"> Дата создания заметки. </param>
    /// <param name="editDate"> Дата изменения заметки. </param>
    public Note(Guid id, Guid userId, string title, string description, DateTime creationDate, DateTime? editDate)
    {
        Id = id;
        UserId = userId;
        Title = title;
        Description = description;
        CreationDate = creationDate;
        EditDate = editDate;
    }

    /// <summary>
    /// Обновляет данные заметки.
    /// </summary>
    /// <param name="title"> Заголовок. </param>
    /// <param name="description"> Описание. </param>
    public void Update(string title, string description)
    {
        bool isUpdated = false;

        if (!string.IsNullOrWhiteSpace(title) && !Title.Equals(title))
        {
            Title = title;
            isUpdated = true;
        }

        if (!string.IsNullOrWhiteSpace(description) && !Description.Equals(description))
        {
            Description = description;
            isUpdated = true;
        }

        if (isUpdated)
        {
            EditDate = DateTime.UtcNow;
        }
    }
}