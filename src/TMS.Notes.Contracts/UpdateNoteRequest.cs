namespace TMS.Notes.Contracts;

/// <summary>
/// Класс запроса на обновление заметки.
/// </summary>
public record UpdateNoteRequest(Guid Id, string Title, string Description);
