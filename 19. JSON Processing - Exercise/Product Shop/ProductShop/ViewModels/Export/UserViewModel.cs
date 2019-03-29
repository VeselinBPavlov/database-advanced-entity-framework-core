namespace ProductShop.ViewModels.Export
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class UserViewModel
    {
        [JsonProperty(PropertyName = ("firstName"))]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = ("lastName"))]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = ("soldProducts"))]
        public List<SoldProductViewModel> SoldProducts { get; set; }
    }
}
