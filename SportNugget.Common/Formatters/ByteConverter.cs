using System.Runtime.Serialization.Formatters.Binary;

namespace SportNugget.Common.Formatters
{
    public static class ByteConverter
    {
        public static T Convert<T>(byte[] data)
        {
            var result = default(T);
            using (var memoryStream = new MemoryStream(data))
            {
                var binaryFormatter = new BinaryFormatter();
                result = (T) binaryFormatter.Deserialize(memoryStream);
            }

            return result;
        }

        public static byte[] Convert<T>(T data)
        {
            byte[] bytes;

            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, data);
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }
    }
}
