using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Notes.Core;

namespace TMS.Notes.DataAccess.Configurations;

/// <summary>
/// Конфигурация класса заметок.
/// </summary>
public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
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
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );
    }
}
