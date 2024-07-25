﻿namespace TMS.Tasks.Core.Models;

public class Note
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }
}
