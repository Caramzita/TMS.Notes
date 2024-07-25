using MediatR;
using TMS.Tasks.Core.Models;

namespace TMS.Tasks.UseCases.Commands.UpdateNote;

public class UpdateNoteCommand : IRequest<Note>
{
    public Guid Id { get; }

    public NoteInputModel Model { get; }

    public UpdateNoteCommand(Guid id, NoteInputModel model)
    {
        Id = id;
        Model = model;
    }
}
