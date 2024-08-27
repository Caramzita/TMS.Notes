using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Notes.Core;
using TMS.Notes.DataAccess.Dtos;

namespace TMS.Notes.DataAccess.Configurations;

/// <summary>
/// Конфигурация класса заметок.
/// </summary>
public class NoteConfiguration : IEntityTypeConfiguration<NoteDto>
{
    /// <summary>
    /// Выполняет настройку конфигурации для сущности <see cref="NoteDto"/>.
    /// </summary>
    /// <param name="builder">Строитель конфигурации сущности <see cref="NoteDto"/>.</param>
    public void Configure(EntityTypeBuilder<NoteDto> builder)
    {
        builder.HasKey(note => note.Id);
        builder.Property(note => note.Title).HasMaxLength(Note.MAX_LENGHT_TITLE);
        builder.Property(note => note.Description).HasMaxLength(Note.MAX_LENGHT_DESCRIPTION);

        builder.Property(e => e.CreationDate)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

        builder.Property(e => e.EditDate)
            .HasConversion(
                v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null
            );
    }
}
