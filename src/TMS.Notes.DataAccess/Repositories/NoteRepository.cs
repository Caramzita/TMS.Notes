using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TMS.Notes.Core;
using TMS.Notes.DataAccess.Dtos;
using TMS.Notes.UseCases.Abstractions;

namespace TMS.Notes.DataAccess.Repositories;

/// <summary>
/// Репозиторий заметок.
/// </summary>
public class NoteRepository : INoteRepository
{
    /// <summary>
    /// Контекст базы данных заметок.
    /// </summary>
    private readonly NoteDbContext _context;

    /// <summary>
    /// Маппер.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="NoteRepository"/>.
    /// </summary>
    /// <param name="context"> Контекст базы данных. </param>
    /// <param name="mapper"> Сервис для маппинга объектов. </param>
    public NoteRepository(NoteDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<Note> GetNotesAsync(Guid userId, string? searchTerm = null, string? sortBy = null)
    {
        var query = _context.Notes.AsNoTracking().Where(note => note.UserId == userId);

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(note => note.Title.Contains(searchTerm));
        }

        query = sortBy switch
        {
            "title" => query.OrderBy(note => note.Title),
            "creationDate" => query.OrderBy(note => note.CreationDate),
            "editDate" => query.OrderBy(note => note.EditDate),
            _ => query.OrderBy(note => note.CreationDate)
        };

        var entities = query.AsAsyncEnumerable();

        await foreach (var entity in entities)
        {
            yield return _mapper.Map<Note>(entity);
        }
    }

    /// <inheritdoc/>
    public async Task<Note?> GetNoteById(Guid id)
    {
        var entity = await _context.Notes.AsNoTracking()
                                   .FirstOrDefaultAsync(n => n.Id == id)
                                   .ConfigureAwait(false);

        return _mapper.Map<Note>(entity);
    }

    /// <inheritdoc/>
    public async Task Add(Note note)
    {
        var entity = _mapper.Map<NoteDto>(note);

        await _context.AddAsync(entity).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task Delete(Note note)
    {
        var entity = _mapper.Map<NoteDto>(note);

        _context.Notes.Remove(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task Update(Note note)
    {
        var entity = _mapper.Map<NoteDto>(note);

        _context.Notes.Update(entity);
        await _context.SaveChangesAsync().ConfigureAwait(false);
    }
}
