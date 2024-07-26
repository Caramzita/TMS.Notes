using MediatR;
using TMS.Notes.Core;

namespace TMS.Notes.UseCases.Notes.Commands.UpdateNote;

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
