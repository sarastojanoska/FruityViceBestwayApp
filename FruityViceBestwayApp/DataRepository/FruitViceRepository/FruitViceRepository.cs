using FruityViceBestwayApp.Data;
using FruityViceBestwayApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FruityViceBestwayApp.DataRepository
{
    public class FruitViceRepository : IFruitViceRepository
    {
        private readonly ApplicationDbContext _context;

        public FruitViceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Fruit> GetFruitByName(string fruit)
        {
            var result = await _context.Fruits
                .Include(f => f.Nutrition)
                .FirstOrDefaultAsync(x => x.Name.Equals(fruit));
            return result;
        }
        public async Task<Nutrition> GetNutritionByFruitId(int fruitId)
        {
            return await _context.Nutritions.FirstOrDefaultAsync(x => x.FruitId == fruitId);
        }
        public async Task<List<Metadata>> GetAllMetaDataByFruitId(int fruitId)
        {
            return await _context.Metadata.Where(x => x.FruitId == fruitId && !x.IsDeleted).ToListAsync();
        }
        public async Task<Metadata> GetMetaDataByKey(string key, int fruitId)
        {
            return await _context.Metadata.FirstOrDefaultAsync(x => x.Key == key && x.FruitId == fruitId && !x.IsDeleted);
        }
        public async Task AddFruit(Fruit fruit)
        {
            var fr = new Fruit();
            try
            {
                _context.Fruits.Add(fruit);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding fruit: " + ex.Message);
            }
        }

        public async Task AddMetadata(Metadata metadata)
        {
            _context.Metadata.Add(metadata);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMetadata(Metadata metadata)
        {
            _context.Metadata.Update(metadata);
            await _context.SaveChangesAsync();
        }
    }
}
