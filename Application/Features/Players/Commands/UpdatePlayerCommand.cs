using Application.Common.Entities;
using Application.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Players.Commands
{
    public class UpdatePlayerCommand : PlayerDto, IRequest
    {
        
        public class UpdatePlayerCommandHandler: IRequestHandler<UpdatePlayerCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IGenericRepository<Player> _repository;
            private readonly IMapper _mapper;


            public UpdatePlayerCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Player> repository, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _repository = repository;
                _mapper = mapper;
            }

            public async Task Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
            {

                var player = _mapper.Map<Player>(request);

                if (player == null)
                {
                    throw new ArgumentException();
                }
                else
                {
                    await _repository.UpdateAsync(player);
                    await _unitOfWork.Save(cancellationToken);

                }

            }
        }
    }
}

