using FruityViceBestwayApp.DataRepository;
using FruityViceBestwayApp.Models.Entities;
using FruityViceBestwayApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace FruityViceBestwayApp.Services
{
    public class FruitViceService : IFruitViceService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly IFruitViceRepository _fruitViceRepository;

        public FruitViceService(IFruitViceRepository fruitViceRepository)
        {
            _fruitViceRepository = fruitViceRepository;
        }

        public async Task<FruitViewModel> GetFruitByName(string fruit)
        {
            var response = new FruitViewModel()
            {
                Nutritions = new NutritionsViewModel()
            };
            var fruitDb = await _fruitViceRepository.GetFruitByName(fruit);
            if (fruitDb != null)
            {
                var nutrition = await _fruitViceRepository.GetNutritionByFruitId(fruitDb.Id);
                response.Name = fruitDb.Name;
                response.Genus = fruitDb.Genus;
                response.Family = fruitDb.Family;
                response.Order = fruitDb.Order;
                response.Nutritions.Sugar = nutrition.Sugar;
                response.Nutritions.Protein = nutrition.Protein;
                response.Nutritions.Fat = nutrition.Fat;
                response.Nutritions.Carbohydrates = nutrition.Carbohydrates;
                response.Nutritions.Calories = nutrition.Calories;
                return response;
            }
            else
            {
                response = await GetFruitFromApi(fruit);
                if (response != null)
                {
                    var newfruitDb = new Fruit
                    {
                        Name = response.Name,
                        Genus = response.Genus,
                        Family = response.Family,
                        Order = response.Order,
                        Nutrition = new Nutrition
                        {
                            Sugar = response.Nutritions.Sugar,
                            Protein = response.Nutritions.Protein,
                            Fat = response.Nutritions.Fat,
                            Carbohydrates = response.Nutritions.Carbohydrates,
                            Calories = response.Nutritions.Calories
                        }
                    };
                    await _fruitViceRepository.AddFruit(newfruitDb);
                }
            }
            return response;
        }
        public async Task<FruitViewModel> GetFruitFromApi(string fruit)
        {
            var response = new FruitViewModel();
            try
            {
                var uri = new Uri($"https://www.fruityvice.com/api/fruit/{fruit}");
                response = await client.GetFromJsonAsync<FruitViewModel>(uri);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return response;
        }

        public async Task<FruitViewModel> AddMetadata(string name,string key, string value)
        {
            var response = new FruitViewModel()
            {
                Nutritions = new NutritionsViewModel(),
                Metadata = new List<MetadataViewModel>()
            };
            var fruitDb = await _fruitViceRepository.GetFruitByName(name);
            if (fruitDb != null)
            {
                var nutrition = await _fruitViceRepository.GetNutritionByFruitId(fruitDb.Id);
                var newMeta = new Metadata
                {
                    FruitId = fruitDb.Id,
                    Key = key,
                    Value = value,
                    IsDeleted = false
                };
                await _fruitViceRepository.AddMetadata(newMeta);
                var metadata = await _fruitViceRepository.GetAllMetaDataByFruitId(fruitDb.Id);
                foreach (var item in metadata)
                {
                    response.Metadata.Add(new MetadataViewModel { Key = item.Key, Value = item.Value });
                }
                response.Name = fruitDb.Name;
                response.Genus = fruitDb.Genus;
                response.Family = fruitDb.Family;
                response.Order = fruitDb.Order;
                response.Nutritions.Sugar = nutrition.Sugar;
                response.Nutritions.Protein = nutrition.Protein;
                response.Nutritions.Fat = nutrition.Fat;
                response.Nutritions.Carbohydrates = nutrition.Carbohydrates;
                response.Nutritions.Calories = nutrition.Calories;
            }
            return response;
        }

        public async Task<FruitViewModel> RemoveMetadata(string name, string key, string value)
        {
            var response = new FruitViewModel()
            {
                Nutritions = new NutritionsViewModel(),
                Metadata = new List<MetadataViewModel>()
            };
            var fruitDb = await _fruitViceRepository.GetFruitByName(name);
            if (fruitDb != null)
            {
                var nutrition = await _fruitViceRepository.GetNutritionByFruitId(fruitDb.Id);
                var meta = await _fruitViceRepository.GetMetaDataByKey(key, fruitDb.Id);
                if (meta != null)
                {
                    meta.IsDeleted = true;
                    await _fruitViceRepository.UpdateMetadata(meta);
                }
                var metadata = await _fruitViceRepository.GetAllMetaDataByFruitId(fruitDb.Id);
                foreach (var item in metadata)
                {
                    response.Metadata.Add(new MetadataViewModel { Key = item.Key, Value = item.Value });
                }
                response.Name = fruitDb.Name;
                response.Genus = fruitDb.Genus;
                response.Family = fruitDb.Family;
                response.Order = fruitDb.Order;
                response.Nutritions.Sugar = nutrition.Sugar;
                response.Nutritions.Protein = nutrition.Protein;
                response.Nutritions.Fat = nutrition.Fat;
                response.Nutritions.Carbohydrates = nutrition.Carbohydrates;
                response.Nutritions.Calories = nutrition.Calories;
            }
            return response;
        }

        public async Task<FruitViewModel> UpdateMetadata(string name, string key, string value)
        {
            var response = new FruitViewModel()
            {
                Nutritions = new NutritionsViewModel(),
                Metadata = new List<MetadataViewModel>()
            };
            var fruitDb = await _fruitViceRepository.GetFruitByName(name);
            if (fruitDb != null)
            {
                var nutrition = await _fruitViceRepository.GetNutritionByFruitId(fruitDb.Id);
                var meta = await _fruitViceRepository.GetMetaDataByKey(key, fruitDb.Id);
                if (meta != null)
                {
                    meta.Value = value;
                    await _fruitViceRepository.UpdateMetadata(meta);
                }
                var metadata = await _fruitViceRepository.GetAllMetaDataByFruitId(fruitDb.Id);
                foreach (var item in metadata)
                {
                    response.Metadata.Add(new MetadataViewModel { Key = item.Key, Value = item.Value });
                }
                response.Name = fruitDb.Name;
                response.Genus = fruitDb.Genus;
                response.Family = fruitDb.Family;
                response.Order = fruitDb.Order;
                response.Nutritions.Sugar = nutrition.Sugar;
                response.Nutritions.Protein = nutrition.Protein;
                response.Nutritions.Fat = nutrition.Fat;
                response.Nutritions.Carbohydrates = nutrition.Carbohydrates;
                response.Nutritions.Calories = nutrition.Calories;
            }
            return response;
        }
    }
}
