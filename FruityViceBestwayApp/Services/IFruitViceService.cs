using FruityViceBestwayApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FruityViceBestwayApp.Services
{
    public interface IFruitViceService
    {
        Task<FruitViewModel> GetFruitByName(string fruit);
        \task
        Task<FruitViewModel> GetFruitFromApi(string fruit);
        Task<FruitViewModel> AddMetadata(string name, string key, string value);
        Task<FruitViewModel> RemoveMetadata(string name, string key, string value);
        Task<FruitViewModel> UpdateMetadata(string name, string key, string value);
    }
}
