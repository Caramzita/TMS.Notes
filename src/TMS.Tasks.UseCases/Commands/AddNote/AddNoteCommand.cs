using MediatR;
using TMS.Tasks.Core.Models;

namespace TMS.Tasks.UseCases.Commands.AddNote;

public class AddNoteCommand : IRequest<Note>
{
    public NoteInputModel Model { get; }

    public AddNoteCommand(NoteInputModel model)
    {
        Model = model;
    }
}

