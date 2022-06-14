using System.Text.Json;

namespace SportNugget.Common.Formatters
{
    public static class StringConverter
    {
        public static string Convert<T>(T data)
        {
            var json = JsonSerializer.Serialize(data);
            return json;
        }

        public static T Convert<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }
    }
}
