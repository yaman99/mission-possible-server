using AutoMapper;
using MediatR;
using MissionPossible.Application.Common.Interfaces.Repositories;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.AnnouncementManager.Commands;
using MissionPossible.Application.Features.AnnouncementManager.Dtos;
using MissionPossible.Application.Features.AnnouncementManager.Queries;
using MissionPossible.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.AnnouncementManager.Handlers
{
    public class AnnouncementManagementQueryHandlers :
        IRequestHandler<GetAllAnnouncementsQuery, Result>
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IMapper _mapper;

        public AnnouncementManagementQueryHandlers(IAnnouncementRepository announcementRepository, IMapper mapper)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetAllAnnouncementsQuery request, CancellationToken cancellationToken)
        {
            var ann = await _announcementRepository.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<GetAllAnnouncementsDto>>(ann);
            return Result.Success(dto);
        }
    }
}
