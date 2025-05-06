using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Wpf.Ui.Demo.Mvvm.DTO
{
    public class Token
    {
        [JsonPropertyName("token")]
        public string token {  get; set; }
    }
}
