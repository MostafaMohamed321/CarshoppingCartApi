using CarshoppingCartApi.Models;

namespace CarshoppingCartApi.Repositories
{
    public interface IHomeRepo
    {
        Task<IEnumerable<Car>> GetCars();
        Task<IEnumerable<Car>> GetCarByAnyString(string N, int genreId);

    }
}
