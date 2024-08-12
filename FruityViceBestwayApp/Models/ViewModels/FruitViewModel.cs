namespace FruityViceBestwayApp.Models.ViewModels
{
    public class FruitViewModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Family { get; set; }
        public string Genus { get; set; }
        public string Order { get; set; }
        public NutritionsViewModel Nutritions { get; set; }
        public List<MetadataViewModel> Metadata { get; set; }

    }
}
