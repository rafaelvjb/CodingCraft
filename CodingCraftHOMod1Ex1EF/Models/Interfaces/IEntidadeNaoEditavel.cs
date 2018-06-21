using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCraftHOMod1Ex1EF.Models.Interfaces
{
    public interface IEntidadeNaoEditavel
    {
        DateTime DataCriacao { get; set; }
        string UsuarioCriacao { get; set; }
    }
}
