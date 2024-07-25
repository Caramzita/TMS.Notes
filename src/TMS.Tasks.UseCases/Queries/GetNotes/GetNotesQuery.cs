using MediatR;
using TMS.Tasks.Core.Models;

namespace TMS.Tasks.UseCases.Queries.GetNotes;

public sealed record GetNotesQuery : IStreamRequest<Note>;
