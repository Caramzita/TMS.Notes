using MediatR;
using Microsoft.AspNetCore.Mvc;
using TMS.Notes.UseCases.Notes.Commands.CreateNote;
using TMS.Notes.UseCases.Notes.Commands.DeleteNote;
using TMS.Notes.UseCases.Notes.Commands.UpdateNote;
using TMS.Notes.UseCases.Dtos;
using TMS.Notes.UseCases.Notes.Queries.GetNotes;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Notes;
using AutoMapper;

namespace TMS.Notes.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly IMapper _mapper;

    public NoteController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IAsyncEnumerable<Note>), 200)]
    public IAsyncEnumerable<Note> GetAllNotes()
    {
        var userId = Guid.Parse("07ab923c-3253-4dc1-85ab-31e4543b9ea9");
        return _mediator.CreateStream(new GetNotesQuery(userId));
    }

    [HttpPost]
    [ProducesResponseType(typeof(Note), 201)]
    public async Task<IActionResult> Create([FromBody] CreateNoteDto request)
    {
        var userId = Guid.Parse("07ab923c-3253-4dc1-85ab-31e4543b9ea9");
        var command = _mapper.Map<NoteInputModel>(request);
        command.UserId = userId;

        await _mediator.Send(new CreateNoteCommand(command));

        return Created();
    }

    [HttpPut]
    [ProducesResponseType(typeof(Note), 200)]
    public async Task<IActionResult> Update([FromBody] UpdateNoteDto request)
    {
        var userId = Guid.Parse("07ab923c-3253-4dc1-85ab-31e4543b9ea9");
        var command = _mapper.Map<NoteInputModel>(request);
        command.UserId = userId;

        var result = await _mediator.Send(new UpdateNoteCommand(request.Id, command));

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = Guid.Parse("07ab923c-3253-4dc1-85ab-31e4543b9ea9");

        await _mediator.Send(new DeleteNoteCommand(id, userId));

        return NoContent();
    }
}
