namespace ProductShop.ViewModels.Export
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class UserProductsViewModel
    {
        [JsonProperty(PropertyName = "usersCount")]
        public int UserCount { get; set; }

        [JsonProperty(PropertyName = "users")]
        public List<UserSoldProductsViewModel> Users { get; set; }
    }
}
