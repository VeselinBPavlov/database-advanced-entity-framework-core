namespace JSONProcessing
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Person
    {
        // You can use every name.
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public List<string> Interests { get; set; }
    }
}
