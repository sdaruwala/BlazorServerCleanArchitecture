using Application.Common.Entities;
using Application.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Players.Queries.GetPositions
{
    public class GetPositionsQuery : IRequest<List<PositionDto>>
    {
        public class GetPositionsQueryHandler : IRequestHandler<GetPositionsQuery, List<PositionDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetPositionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<List<PositionDto>> Handle(GetPositionsQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Repository<Position>().Entities
                     .ProjectTo<PositionDto>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken);
            }
        }
    }
}
