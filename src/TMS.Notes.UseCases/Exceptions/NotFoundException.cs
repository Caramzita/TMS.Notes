namespace TMS.Notes.UseCases.Exceptions;

/// <summary>
/// Исключение, когда объект не найден.
/// </summary>
/// <remarks>
/// Это исключение обычно используется в сценариях, 
/// когда конкретный объект (например, запись в базе данных) 
/// запрашивается по его имени и ключу, но не может быть найден.
/// </remarks>
public class NotFoundException : Exception
{
    /// <summary>
    /// Представляет собой исключение, которое генерируется, 
    /// когда запрашиваемый объект не найден в системе.
    /// </summary>
    /// <param name="name"> Имя объекта, который не был найден. </param>
    /// <param name="key"> Ключевое значение, которое однозначно идентифицирует объект. </param>
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) not found") 
    { }
}
