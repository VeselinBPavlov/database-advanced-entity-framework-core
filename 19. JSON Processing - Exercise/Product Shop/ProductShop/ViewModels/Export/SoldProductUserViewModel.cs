namespace ProductShop.ViewModels.Export
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class SoldProductUserViewModel
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "products")]
        public List<ProductSoldUserViewModel> Products { get; set; }
    }
}