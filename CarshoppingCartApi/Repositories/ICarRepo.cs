using CarshoppingCartApi.Models;

namespace CarshoppingCartApi.Repositories
{
    public interface ICarRepo
    {
        Task<Car> GetCarById(int id);
        Task AddCar(Car car);
        Task DeleteCar(Car car);
         Task UpdateCar(Car car);
    }
}