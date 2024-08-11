namespace TMS.Notes.UseCases.Abstractions;

/// <summary>
/// Интерфейс для доступа к данным пользователя.
/// </summary>
public interface IUserAccessor
{
    /// <summary>
    /// Получить идентификатор пользователя.
    /// </summary>
    /// <returns></returns>
    Guid GetUserId();
}
