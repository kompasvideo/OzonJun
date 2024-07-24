using System.Text.Json;
using Confluent.Kafka;

namespace Kafka.Common;

public sealed class JsonValueSerializer<T> :ISerializer<T>, IDeserializer<T>
{
    public byte[] Serialize(T data, SerializationContext context) 
        => JsonSerializer.SerializeToUtf8Bytes(data);

    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
       if (isNull)
           throw new ArgumentNullException(nameof(data), "Null data encountered");
       return JsonSerializer.Deserialize<T>(data) 
           ?? throw new ArgumentNullException(nameof(data), "Null data encountered");
    }
}