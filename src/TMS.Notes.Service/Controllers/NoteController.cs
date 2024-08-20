using MediatR;
using Microsoft.AspNetCore.Mvc;
using TMS.Notes.UseCases.Notes.Commands.CreateNote;
using TMS.Notes.UseCases.Notes.Commands.DeleteNote;
using TMS.Notes.UseCases.Notes.Commands.UpdateNote;
using TMS.Notes.UseCases.Notes.Queries.GetNotes;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Notes;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TMS.Notes.UseCases.Abstractions;
using TMS.Application.UseCases;
using TMS.Notes.Contracts;

namespace TMS.Notes.Service.Controllers;

/// <summary>
/// Контроллер предоставляющий Rest API для работы с заметками.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class NoteController : ControllerBase
{
    /// <summary>
    /// Медиатр.
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// Маппер.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Сервис для доступа к данным пользователя.
    /// </summary>
    private readonly IUserAccessor _userAccessor;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="NoteController"/>.
    /// </summary>
    /// <param name="mediator"> Медиатр. </param>
    /// <param name="mapper"> Маппер. </param>
    /// <param name="userAccessor"> Данные пользователя. </param>
    /// <exception cref="ArgumentNullException">Генерируется, 
    /// если значение медиатора, маппера или данные пользовалея равно null.</exception>
    public NoteController(IMediator mediator, IMapper mapper, IUserAccessor userAccessor)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
    }

    /// <summary>
    /// Получить все заметки пользователя.
    /// </summary>
    /// <returns> Асинхронный список заметок пользователя. </returns>
    /// <response code="200"> Успешно </response>
    [HttpGet]
    [ProducesResponseType(typeof(IAsyncEnumerable<Note>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IAsyncEnumerable<Note> GetAllNotes([FromQuery] string? searchTerm = null,
                                              [FromQuery] string? sortBy = null)
    {
        var userId = _userAccessor.GetUserId();

        return _mediator.CreateStream(new GetNotesQuery(userId));
    }

    /// <summary>
    /// Создать заметку.
    /// </summary>
    /// <param name="request"> Объект класса <see cref="CreateNoteRequest"/>. </param>
    /// <returns> Идентификатор заметки <see cref="Guid"/>. </returns>
    /// <response code="201"> Успешно </response>
    [HttpPost]
    [ProducesResponseType(typeof(Note), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateNoteRequest request)
    {
        var command = _mapper.Map<NoteInputModel>(request);
        command.UserId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new CreateNoteCommand(command));

        return result.ToActionResult();
    }

    /// <summary>
    /// Обновить заметку.
    /// </summary>
    /// <param name="request"> Объект класса <see cref="UpdateNoteRequest"/>. </param>
    /// <returns> Обновленная заметка. </returns>
    /// <response code="200"> Успешно </response>
    [HttpPut]
    [ProducesResponseType(typeof(Note), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateNoteRequest request)
    {
        var command = _mapper.Map<NoteInputModel>(request);
        command.UserId = _userAccessor.GetUserId();

        var result = await _mediator.Send(new UpdateNoteCommand(request.Id, command));

        return result.ToActionResult();
    }

    /// <summary>
    /// Удалить заметку.
    /// </summary>
    /// <param name="id"> Идентификатор заметки. </param>
    /// <returns> <see cref="NoContentResult"/>. </returns>
    /// <response code="204"> Успешно </response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = _userAccessor.GetUserId();
        var result = await _mediator.Send(new DeleteNoteCommand(id, userId));

        return result.ToActionResult();
    }
}
