namespace TMS.Notes.UseCases.Dtos;

public class UpdateNoteDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}
