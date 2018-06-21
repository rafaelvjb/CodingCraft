using CodingCraftHOMod1Ex1EF.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex1EF.Models
{
    [Table("Condominios")]
    public class Condominio : Entidade
    {
        public Condominio()
        {
            CondominioId = Guid.NewGuid();
        }

        [Key]
        public Guid CondominioId { get; set; }
        public Guid CidadeId { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        [StringLength(1000)]
        public string Descricao { get; set; }

        [Display(Name = "Razão Social")]
        [StringLength(200)]
        public string RazaoSocial { get; set; }

        [Cnpj]
        [Index("IUQ_Condominio_Cnpj", IsUnique = true)]
        [StringLength(14)]
        public string Cnpj { get; set; }

        public virtual Cidade Cidade { get; set; }

        public virtual ICollection<CondominioTelefone> CondominioTelefones { get; set; }
    }
}