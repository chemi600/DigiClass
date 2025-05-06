using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Demo.Mvvm.DTO;

namespace Wpf.Ui.Demo.Mvvm.Interfaces
{
    public interface ICharacterProvider<T> where T : class
    {
        Task<bool> PostCharacter(CreateCharacterDTO user,string token);

        Task<List<T?>> Get(string token);
        Task<bool> PathCharacter(string url, CharacterDTO character, string token);
    }
}
