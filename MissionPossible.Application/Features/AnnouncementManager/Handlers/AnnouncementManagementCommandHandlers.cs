using MediatR;
using MissionPossible.Application.Common.Interfaces.Repositories;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.AnnouncementManager.Commands;
using MissionPossible.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.AnnouncementManager.Handlers
{
    public class AnnouncementManagementCommandHandlers :
        IRequestHandler<AddNewAnnouncementCommand, Result>,
        IRequestHandler<DeleteAnnouncementCommand, Result>
    {
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementManagementCommandHandlers(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<Result> Handle(AddNewAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var newAnn = new Announcement(Guid.NewGuid())
            {
                Content = request.Content,
                Title = request.Title,
                Url = request.Url,
            };

            await _announcementRepository.AddAsync(newAnn);
            return Result.Success();
        }

        public async Task<Result> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
        {
            var ann = await _announcementRepository.GetAsync(request.Id);
            ann.SetDelete(true);
            await _announcementRepository.UpdateAsync(ann);
            return Result.Success();
        }
    }
}
