using CodingCraftHOMod1Ex1EF.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex1EF.Models
{
    public abstract class EntidadeNaoEditavel : IEntidadeNaoEditavel
    {
        [Display(Name = "Data da Criação")]
        public DateTime DataCriacao { get; set; }
        [Display(Name = "Usuário da Criação")]
        public string UsuarioCriacao { get; set; }
    }
}