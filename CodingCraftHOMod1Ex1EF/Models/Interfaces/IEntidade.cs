using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCraftHOMod1Ex1EF.Models.Interfaces
{
    public interface IEntidade : IEntidadeNaoEditavel
    {
        DateTime? UltimaModificacao { get; set; }
        string UsuarioModificacao { get; set; }
    }
}
