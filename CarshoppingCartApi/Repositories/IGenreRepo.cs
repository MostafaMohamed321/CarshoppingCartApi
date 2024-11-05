using CarshoppingCartApi.Models;

namespace CarshoppingCartApi.Repositories
{
    public interface IGenreRepo
    {
        Task<IEnumerable<Genre>> GetGenres();
        Task<Genre> GetGenreById(int id);


        Task AddGenre(Genre genre);
        Task DeleteGenre(Genre genre);


    }
}