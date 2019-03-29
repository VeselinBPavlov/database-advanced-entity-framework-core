namespace ProductShop.ViewModels.Export
{
    using Newtonsoft.Json;

    public class ProductSoldUserViewModel
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
    }
}
