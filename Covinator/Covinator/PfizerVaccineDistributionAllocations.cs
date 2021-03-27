namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PfizerVaccineDistributionAllocations
    {
        [JsonProperty("jurisdiction")]
        public string Jurisdiction { get; set; }

        [JsonProperty("week_of_allocations")]
        public DateTimeOffset WeekOfAllocations { get; set; }

        [JsonProperty("_1st_dose_allocations")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long The1StDoseAllocations { get; set; }

        [JsonProperty("_2nd_dose_allocations")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long The2NdDoseAllocations { get; set; }
    }

    public partial class PfizerVaccineDistributionAllocations
    {
        public static PfizerVaccineDistributionAllocations[] FromJson(string json) => JsonConvert.DeserializeObject<PfizerVaccineDistributionAllocations[]>(json, QuickType.Converter.Settings);
    }

    public static class Serialize1
    {
        public static string ToJson(this PfizerVaccineDistributionAllocations[] self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter1
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter1 : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }
    }
}
