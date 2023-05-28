using AutoMapper;
using MediatR;
using MissionPossible.Application.Common.Interfaces.Repositories;
using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.RequestManagement.Dtos;
using MissionPossible.Application.Features.RequestManagement.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.RequestManagement.Handlers
{
    public class RequestManagementQueryHandlers : 
        IRequestHandler<GetAllApplicationFormRequestQuery, Result>,
        IRequestHandler<GetAllStudentApplicationFormRequestQuery, Result>
    {
        private readonly IApplicationFormRepository _applicationFormRepository;
        private readonly IMapper _mapper;

        public RequestManagementQueryHandlers(IApplicationFormRepository applicationFormRepository, IMapper mapper)
        {
            _applicationFormRepository = applicationFormRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetAllApplicationFormRequestQuery request, CancellationToken cancellationToken)
        {
            var requests = await _applicationFormRepository.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<GetApplicationFormRequestDto>>(requests);

            return Result.Success(dto);
        }

        public async Task<Result> Handle(GetAllStudentApplicationFormRequestQuery request, CancellationToken cancellationToken)
        {
            var requests = await _applicationFormRepository.GetAllByStudentAsync(request.StudentId);
            var dto = _mapper.Map<IEnumerable<GetApplicationFormRequestDto>>(requests);
            return Result.Success(dto);
        }
    }
}
