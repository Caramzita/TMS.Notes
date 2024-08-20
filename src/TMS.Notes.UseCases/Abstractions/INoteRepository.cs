using TMS.Notes.Core;

namespace TMS.Notes.UseCases.Abstractions;

/// <summary>
/// Интерфейс репозитория для доступа к заметкам.
/// </summary>
public interface INoteRepository
{
    /// <summary>
    /// Получить список всех заметок пользователя.
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя. </param>
    /// <returns> Асинхронный список заметок. </returns>
    IAsyncEnumerable<Note> GetNotesAsync(Guid userId, string? searchTerm = null, string? sortBy = null);

    /// <summary>
    /// Получить заметку по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор заметки. </param>
    /// <returns> Запрошенная заметка. </returns>
    Task<Note?> GetNoteById(Guid id);

    /// <summary>
    /// Добавить заметку.
    /// </summary>
    /// <param name="note"> Заметка. </param>
    Task Add(Note note);


    /// <summary>
    /// Удалить заметку.
    /// </summary>
    /// <param name="note"> Заметка. </param>
    Task Delete(Note note);

    /// <summary>
    /// Обновить заметку.
    /// </summary>
    /// <param name="note"> Заметка. </param>
    Task Update(Note note);
}