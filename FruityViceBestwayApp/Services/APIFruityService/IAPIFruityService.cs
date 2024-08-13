using FruityViceBestwayApp.Models.ViewModels;

namespace FruityViceBestwayApp.Services
{
    public interface IAPIFruityService
    {
        Task<FruitViewModel> GetFruitFromApi(string fruit);
    }
}
