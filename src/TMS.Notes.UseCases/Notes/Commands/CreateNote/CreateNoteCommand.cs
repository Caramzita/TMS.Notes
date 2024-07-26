using MediatR;
using TMS.Notes.Core;

namespace TMS.Notes.UseCases.Notes.Commands.CreateNote;

public class CreateNoteCommand : IRequest<Note>
{
    public NoteInputModel Model { get; }

    public CreateNoteCommand(NoteInputModel model)
    {
        Model = model;
    }
}

