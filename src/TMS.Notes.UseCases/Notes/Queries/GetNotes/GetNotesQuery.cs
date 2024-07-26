using MediatR;
using TMS.Notes.Core;

namespace TMS.Notes.UseCases.Notes.Queries.GetNotes;

public sealed record GetNotesQuery : IStreamRequest<Note>;
