using CarshoppingCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarshoppingCartApi.Repositories
{
    public class GenreRepo:IGenreRepo
    {
        private readonly CarShoppingDb _db;
        public GenreRepo(CarShoppingDb db)
        {
            _db = db;

        }
        public async Task<IEnumerable<Genre>> GetGenres()
        {
            return await _db.Genres.ToListAsync();
        }
        public async Task<Genre> GetGenreById(int id)
        {
            var genre = await _db.Genres.FirstOrDefaultAsync(x => x.Id == id);
            return genre;

        }

        public async Task AddGenre(Genre genre)
        {
             _db.Genres.Add(genre);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteGenre(Genre genre)
        {
            var dGenre = _db.Genres.Remove(genre);
           await _db.SaveChangesAsync();
        }
    }
}
