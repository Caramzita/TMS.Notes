namespace TMS.Notes.Contracts;

/// <summary>
/// Класс запроса на добавление заметки.
/// </summary>
public record CreateNoteRequest(string Title, string Description);
