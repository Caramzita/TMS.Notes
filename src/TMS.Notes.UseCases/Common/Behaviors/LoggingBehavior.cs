using MediatR;
using Serilog;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.UseCases.Common.Behaviors;

/// <summary>
/// Поведение для логирования запросов и ответов в конвейере обработки запросов Mediator.
/// </summary>
/// <typeparam name="TRequest">Тип запроса, который обрабатывается.</typeparam>
/// <typeparam name="TResponse">Тип ответа, который возвращается после обработки запроса.</typeparam>
public class LoggingBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse> where TRequest 
    : IRequest<TResponse>
{
    /// <summary>
    /// Интерфейс для доступа к информации о текущем пользователе.
    /// </summary>
    private readonly IUserAccessor _userAccessor;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LoggingBehavior{TRequest, TResponse}"/>.
    /// </summary>
    /// <param name="userAccessor"> Интерфейс для доступа к информации о текущем пользователе. </param>
    public LoggingBehavior(IUserAccessor userAccessor)
    {
        _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
    }

    /// <summary>
    /// Обрабатывает запрос, логируя информацию о запросе, пользователе и передаваемых данных.
    /// </summary>
    /// <param name="request"> Запрос, который нужно обработать. </param>
    /// <param name="next"> Делегат, представляющий следующий шаг в конвейере обработки запросов. </param>
    /// <param name="cancellationToken"> Токен отмены для отмены операции. </param>
    /// <returns> Операция, возвращающая ответ на запрос. </returns>
    public async Task<TResponse> Handle(TRequest request, 
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _userAccessor.GetUserId();

        Log.Information("Notes Request: {Name} {@UserId} {@Request}",
            requestName, userId, request);

        var response = await next();

        return response;
    }
}