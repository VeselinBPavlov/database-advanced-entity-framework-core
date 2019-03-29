namespace ProductShop.ViewModels.Export
{
    using Newtonsoft.Json;

    public class UserSoldProductsViewModel
    {
        [JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "age", NullValueHandling = NullValueHandling.Ignore)]       
        public int? Age { get; set; }

        [JsonProperty(PropertyName = "soldProducts")]
        public SoldProductUserViewModel SoldProducts { get; set; }
    }
}
