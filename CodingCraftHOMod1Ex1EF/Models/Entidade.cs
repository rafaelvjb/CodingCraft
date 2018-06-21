using CodingCraftHOMod1Ex1EF.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex1EF.Models
{
    public abstract class Entidade : EntidadeNaoEditavel, IEntidade
    {
        [Display(Name = "Data da Última Modificação")]
        public DateTime? UltimaModificacao { get; set; }
        [Display(Name = "Usuário da Última Modificação")]
        public string UsuarioModificacao { get; set; }
    }
}