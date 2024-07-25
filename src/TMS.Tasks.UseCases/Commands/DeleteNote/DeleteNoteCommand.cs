using MediatR;

namespace TMS.Tasks.UseCases.Commands.DeleteNote;

public class DeleteNoteCommand : IRequest
{
    public Guid Id { get; }

    public DeleteNoteCommand(Guid id)
    {
        Id = id;
    }
}
