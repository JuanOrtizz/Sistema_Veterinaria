using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

public class AnimalConvertidor : JsonConverter<Animal>
{
    public override Animal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jsonDoc = JsonDocument.ParseValue(ref reader);
        var rootElement = jsonDoc.RootElement;

        if (rootElement.TryGetProperty("Tipo", out JsonElement tipoElement))
        {
            string tipo = tipoElement.GetString();

            switch (tipo)
            {
                case "Perro":
                    return JsonSerializer.Deserialize<Perro>(rootElement.GetRawText(), options);
                case "Gato":
                    return JsonSerializer.Deserialize<Gato>(rootElement.GetRawText(), options);
                case "Ave":
                    return JsonSerializer.Deserialize<Ave>(rootElement.GetRawText(), options);
                case "Roedor":
                    return JsonSerializer.Deserialize<Roedor>(rootElement.GetRawText(), options);
                default:
                    throw new JsonException($"Tipo de animal desconocido: {tipo}");
            }
        }
        else
        {
            throw new JsonException("No se encontró la propiedad 'Tipo' en el JSON.");
        }
    }

    public override void Write(Utf8JsonWriter writer, Animal value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}