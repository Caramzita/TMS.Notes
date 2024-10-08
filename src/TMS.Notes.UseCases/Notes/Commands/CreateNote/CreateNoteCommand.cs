﻿using MediatR;
using TMS.Application.UseCases;

namespace TMS.Notes.UseCases.Notes.Commands.CreateNote;

/// <summary>
/// Команда добавления заметки.
/// </summary>
public class CreateNoteCommand : IRequest<Result<Guid>>
{
    /// <summary>
    /// Модель заметки.
    /// </summary>
    public NoteInputModel Model { get; }

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="CreateNoteCommand"/>.
    /// </summary>
    /// <param name="model"> Модель заметки. </param>
    public CreateNoteCommand(NoteInputModel model)
    {
        Model = model;
    }
}

