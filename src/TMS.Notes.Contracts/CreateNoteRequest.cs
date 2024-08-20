namespace TMS.Notes.Contracts;

/// <summary>
/// Модель запроса на добавление заметки.
/// </summary>
public record CreateNoteRequest(string Title, string Description);
