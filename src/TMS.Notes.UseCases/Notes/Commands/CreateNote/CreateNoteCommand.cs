﻿using MediatR;

namespace TMS.Notes.UseCases.Notes.Commands.CreateNote;

/// <summary>
/// Команда добавления заметки.
/// </summary>
public class CreateNoteCommand : IRequest<Guid>
{
    /// <summary>
    /// Модель заметки.
    /// </summary>
    public NoteInputModel Model { get; }

    public CreateNoteCommand(NoteInputModel model)
    {
        Model = model;
    }
}

