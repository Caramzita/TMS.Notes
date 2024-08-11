﻿using AutoMapper;
using MediatR;
using TMS.Notes.Core;
using TMS.Notes.UseCases.Abstractions;
using TMS.Notes.UseCases.Exceptions;

namespace TMS.Notes.UseCases.Notes.Commands.UpdateNote;

/// <summary>
/// Обработчик команды обновления заметки.
/// </summary>
public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Note>
{
    private readonly INoteRepository _taskRepository;

    private readonly IMapper _mapper;

    public UpdateNoteCommandHandler(INoteRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Note> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _taskRepository.GetNoteById(request.Id);

        if (entity == null || entity.UserId != request.Model.UserId)
        {
            throw new NotFoundException(nameof(Note), request.Id);
        }

        entity.Title = request.Model.Title;
        entity.Description = request.Model.Description;
        entity.EditDate = DateTime.UtcNow;    

        await _taskRepository.Update(entity).ConfigureAwait(false);

        return entity;
    }
}
