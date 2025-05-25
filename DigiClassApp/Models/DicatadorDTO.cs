using System.Text.Json.Serialization;


namespace DigiClass.Models
{
    public class DicatadorDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("pais")]
        public string Pais { get; set; }
    
        }
    }

  
  





