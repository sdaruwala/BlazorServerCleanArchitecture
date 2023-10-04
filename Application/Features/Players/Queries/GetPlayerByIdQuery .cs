using Application.Common.Entities;
using Application.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Players.Queries.GetPlayerById
{
    public class GetPlayerByIdQuery : IRequest<PlayerDto>
    {
        public int Id { get; set; }
        public class GetAllPlayersQueryHandler : IRequestHandler<GetPlayerByIdQuery, PlayerDto>
        {

            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public GetAllPlayersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<PlayerDto> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
            {
                var query = await _unitOfWork.Repository<Player>().Entities
                    .Where(x => x.Id == request.Id)
                    .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

                if (query == null)
                {
                    throw new ArgumentException();
                }
                else
                {
                    return query;
                }                    
            }          
        }
    }
}
