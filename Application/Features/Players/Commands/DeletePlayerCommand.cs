using Application.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Players.Commands
{
    public class DeletePlayerCommand : IRequest
    {
        public int Id { get; set; }
        public class DeletePlayerCommandHandler: IRequestHandler<DeletePlayerCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IGenericRepository<Player> _repository;


            public DeletePlayerCommandHandler(IUnitOfWork unitOfWork, IGenericRepository<Player> repository)
            {
                _unitOfWork = unitOfWork;
                _repository = repository;
               
            }

            public async Task Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
            {
                var player = await _repository.GetByIdAsync(request.Id);

                if (player == null)
                {
                    throw new ArgumentException();
                }
                else
                {
                    await _repository.DeleteAsync(player);
                    await _unitOfWork.Save(cancellationToken);
                }               
                
            }
        }
    }
}

