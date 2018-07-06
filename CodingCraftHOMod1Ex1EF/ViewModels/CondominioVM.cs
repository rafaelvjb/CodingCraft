using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodingCraftHOMod1Ex1EF.Attributes;
using CodingCraftHOMod1Ex1EF.Models;

namespace CodingCraftHOMod1Ex1EF.ViewModels
{
    public class CondominioVM
    {
        [Display(Name = "Cidade")]
        public Guid CidadeId { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; }

        [Display(Name = "Razão Social")]
        [StringLength(200)]
        public string RazaoSocial { get; set; }

        [Cnpj]
        [StringLength(14)]
        public string Cnpj { get; set; }

        public ICollection<Condominio> Result { get; set; }
    }
}