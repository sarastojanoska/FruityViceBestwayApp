using AutoMapper;
using FruityViceBestwayApp.DataRepository;
using FruityViceBestwayApp.Entities;
using FruityViceBestwayApp.Models.Helper;
using FruityViceBestwayApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Xml.Linq;


namespace FruityViceBestwayApp.Services
{
    public class FruitViceService : IFruitViceService
    {
        private readonly IExceptionLoggerService _exceptionLogger;
        private readonly IFruitViceRepository _fruitViceRepository;
        private readonly IAPIFruityService _apiFruityService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public FruitViceService(IExceptionLoggerService exceptionLogger, IFruitViceRepository fruitViceRepository, IMapper mapper, IAPIFruityService apiFruityService, IMemoryCache cache)
        {
            _exceptionLogger = exceptionLogger;
            _fruitViceRepository = fruitViceRepository;
            _mapper = mapper;
            _apiFruityService = apiFruityService;
            _cache = cache;
        }

        public async Task<FruitViewModel> GetFruitByName(string fruit)
        {
            if (_cache.TryGetValue(fruit, out FruitViewModel cachedFruit))
            {
                return cachedFruit;
            }
            else
            {
                var response = new FruitViewModel()
                {
                    Nutritions = new NutritionsViewModel(),
                    Metadata = new List<MetadataViewModel>()
                };
                var fruitDb = await _fruitViceRepository.GetFruitByName(fruit);
                if (fruitDb != null)
                {
                    response = _mapper.Map<FruitViewModel>(fruitDb);
                }
                else
                {
                    response = await _apiFruityService.GetFruitFromApi(fruit);
                    if (response != null)
                    {
                        var newfruitDb = _mapper.Map<Fruit>(response);
                        await _fruitViceRepository.AddFruit(newfruitDb);
                    }
                }
                _cache.Set(fruit, response, TimeSpan.FromMinutes(30));
                return response;
            }
        }

        public async Task<FruitViewModel> AddMetadata(string name, string key, string value)
        {
            var response = new FruitViewModel()
            {
                Nutritions = new NutritionsViewModel(),
                Metadata = new List<MetadataViewModel>()
            };
            var fruitDb = await _fruitViceRepository.GetFruitByName(name);
            if (fruitDb != null)
            {
                response = _mapper.Map<FruitViewModel>(fruitDb);
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
                response = _mapper.Map<FruitViewModel>(fruitDb);
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
                response = _mapper.Map<FruitViewModel>(fruitDb);
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
            }
            return response;
        }
    }
}
