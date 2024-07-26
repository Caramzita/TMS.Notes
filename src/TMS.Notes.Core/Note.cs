namespace TMS.Notes.Core;

public class Note
{
    public const int MAX_LENGHT_TITLE = 50;

    public const int MAX_LENGHT_DESCRIPTION = 250;

    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime EditDate { get; set; }
}
