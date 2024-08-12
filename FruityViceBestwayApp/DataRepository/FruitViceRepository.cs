using FruityViceBestwayApp.Data;
using FruityViceBestwayApp.Models.Entities;
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

            var result = new Fruit();

            try
            {
                result = await _context.Fruits.FirstOrDefaultAsync(x => x.Name.Equals(fruit));
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
            }

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
            _context.Fruits.Add(fruit);
            await _context.SaveChangesAsync();
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
