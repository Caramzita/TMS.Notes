using MediatR;

namespace TMS.Notes.UseCases.Notes.Commands.DeleteNote;

public class DeleteNoteCommand : IRequest<Unit>
{
    public Guid Id { get; }

    public Guid UserId { get; }

    public DeleteNoteCommand(Guid id, Guid userId)
    {
        UserId = userId;
        Id = id;
    }
}
