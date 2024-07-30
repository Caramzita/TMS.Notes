using MediatR;
using TMS.Notes.Core;

namespace TMS.Notes.UseCases.Notes.Queries.GetNotes;

public sealed class GetNotesQuery : IStreamRequest<Note>
{
    public Guid UserId { get; }

    public GetNotesQuery(Guid userId)
    {
        UserId = userId;
    }
}
