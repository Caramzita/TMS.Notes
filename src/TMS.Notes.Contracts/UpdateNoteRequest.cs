namespace TMS.Notes.Contracts;

/// <summary>
/// Модель запроса на обновление заметки.
/// </summary>
public record UpdateNoteRequest(Guid Id, string Title, string Description);
