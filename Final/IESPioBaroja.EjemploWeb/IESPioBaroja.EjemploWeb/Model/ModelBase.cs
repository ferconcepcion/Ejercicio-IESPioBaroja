using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace IESPioBaroja.EjemploWeb.Model
{
    public class ModelBase
    {
        [JsonPropertyName("id")]
        [JsonProperty("id")]
        public virtual string Id { get; set; }
    }
}
