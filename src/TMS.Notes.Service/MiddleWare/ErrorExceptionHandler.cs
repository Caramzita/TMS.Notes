using System.Net;
using System.Text.Json;
using FluentValidation;
using TMS.Notes.UseCases.Exceptions;

namespace TMS.Notes.Service.MiddleWare;

/// <summary>
/// Обработчик исключений, который перехватывает ошибки, возникающие в ходе обработки HTTP-запросов,
/// и возвращает соответствующий HTTP-код и сообщение об ошибке.
/// </summary>
public class ErrorExceptionHandler
{
    /// <summary>
    /// Делегат запроса, представляющий следующий шаг в конвейере обработки запросов.
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="ErrorExceptionHandler"/>.
    /// </summary>
    /// <param name="next"> Делегат запроса, представляющий следующий шаг в конвейере обработки запросов. </param>
    /// <exception cref="ArgumentNullException"> Выбрасывается, если <paramref name="next"/> равен <c>null</c>. </exception>
    public ErrorExceptionHandler(RequestDelegate next) => 
        _next = next ?? throw new ArgumentNullException(nameof(next));

    /// <summary>
    /// Выполняет обработку HTTP-запроса и перехватывает исключения, возникшие в процессе выполнения.
    /// </summary>
    /// <param name="context"> Контекст текущего HTTP-запроса. </param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    /// <summary>
    /// Обрабатывает исключение, устанавливая соответствующий код состояния HTTP и сообщение об ошибке.
    /// </summary>
    /// <param name="context"> Контекст текущего HTTP-запроса. </param>
    /// <param name="exception"> Исключение, которое нужно обработать. </param>
    /// <returns> Операция записи результата в ответ. </returns>
    public Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        
        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}