using FruityViceBestwayApp.Models.Entities;

namespace FruityViceBestwayApp.DataRepository
{
    public interface IFruitViceRepository
    {
        Task<Fruit> GetFruitByName(string fruit);
        Task<Nutrition> GetNutritionByFruitId(int fruitId);
        Task<List<Metadata>> GetAllMetaDataByFruitId(int fruitId);
        Task<Metadata> GetMetaDataByKey(string key, int fruitId);
        Task AddFruit(Fruit fruit);
        Task AddMetadata(Metadata metadata);
        Task UpdateMetadata(Metadata metadata);
    }
}
