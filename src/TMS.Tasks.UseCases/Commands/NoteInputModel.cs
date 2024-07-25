namespace TMS.Tasks.UseCases.Commands;

public class NoteInputModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public NoteInputModel(string title, string description)
    {
        Title = title;
        Description = description;
    }
}
