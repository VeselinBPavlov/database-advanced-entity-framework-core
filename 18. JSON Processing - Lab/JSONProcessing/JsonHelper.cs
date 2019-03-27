namespace JSONProcessing
{

    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public static class JsonHelper
    {
        public static string SelializeObject<T>(T obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            string result = string.Empty;

            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                result = Encoding.UTF8.GetString(stream.ToArray());

                return result;
            }
        }

        public static T DeserializeObject<T>(string json)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            var jsonBytes = Encoding.UTF8.GetBytes(json);
            T result = default(T);

            using (var stream = new MemoryStream(jsonBytes))
            {
                result = (T)serializer.ReadObject(stream);

                return result;
            }
        }
    }
}
