using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Wpf.Ui.Demo.Mvvm.DTO
{
    public class CharacterDTO
    {
        public int id { get; set; }
        public DateTime createdDate { get; set; }
        public string name { get; set; }


        public int damage { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not CharacterDTO)
                return false;

            var otherCharacter = (CharacterDTO)obj;

            if (id != otherCharacter.id || createdDate != otherCharacter.createdDate || damage != otherCharacter.damage || name!=otherCharacter.name || Class!=otherCharacter.Class)
                return false;

            return true;
        }
    }
}
