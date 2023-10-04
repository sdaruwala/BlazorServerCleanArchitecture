using Application.Common.Entities;
using Application.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Players.Queries.GetAllPlayers
{
    public class GetAllPlayersQuery : IRequest<List<PlayerDto>>
    {
        public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPlayersQuery, List<PlayerDto>>
        {

            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetAllPlayersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<List<PlayerDto>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.Repository<Player>().Entities
                    .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken);
            }          
        }
    }
}
