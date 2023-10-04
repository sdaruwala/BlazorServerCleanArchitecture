using Application.Common.Entities;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Features.Players.Commands
{
    public class CreatePlayerCommand : PlayerDto, IRequest
    {
   
        public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IGenericRepository<Player> _repository;
            private readonly IMapper _mapper;

            public CreatePlayerCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Player> repository, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _repository = repository;
                _mapper = mapper;
            }

            public async Task Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
            {                
                var player = _mapper.Map<Player>(request);

                if (player == null)
                {
                    throw new ArgumentException();
                }
                else { 
                    await _repository.AddAsync(player);
                    await _unitOfWork.Save(cancellationToken);                                     
                        
                }               

            }          
        }
    }
}
