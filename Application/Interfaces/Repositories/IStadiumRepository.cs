using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IStadiumRepository
    {
        Task<List<Stadium>> GetStadiumByCityAsync(string cityName);
    }
}
