using MediatR;
using Microsoft.AspNetCore.Mvc;
using TMS.Tasks.Core.Models;
using TMS.Tasks.UseCases.Abstractions;
using TMS.Tasks.UseCases.Commands;
using TMS.Tasks.UseCases.Commands.AddNote;
using TMS.Tasks.UseCases.Commands.DeleteNote;
using TMS.Tasks.UseCases.Commands.UpdateNote;
using TMS.Tasks.UseCases.Queries.GetNotes;

namespace TMS.Tasks.Service.Controllers;

[Route("api/notes")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(ITaskRepository taskRepository, IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IAsyncEnumerable<Note>), 200)]
    public IAsyncEnumerable<Note> GetAllNotes()
    {
        return _mediator.CreateStream(new GetNotesQuery());
    }

    [HttpGet("{id:guid}")]

    [HttpPost]
    [ProducesResponseType(typeof(Note), 201)]
    public async Task<IActionResult> Add([FromBody] NoteInputModel note)
    {
        var result = await _mediator.Send(new AddNoteCommand(note));

        return Created();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Note), 200)]
    public async Task<IActionResult> Update(Guid id, NoteInputModel note)
    {
        var result = await _mediator.Send(new UpdateNoteCommand(id, note));

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteNoteCommand(id));

        return NoContent();
    }
}
