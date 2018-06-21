using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex1EF.Models
{
    public class Cidade : Entidade
    {
        public Cidade()
        {
            CidadeId = Guid.NewGuid();
        }

        [Key]
        public Guid CidadeId { get; set; }

        [Required]
        [Index("IUQ_Cidades_Nome", IsUnique = true)]
        [StringLength(150)]
        public string Nome { get; set; }

        [Display(Name = "Sigla do Estado")]
        public string SiglaEstado { get; set; }

        public virtual ICollection<Condominio> Condominios { get; set; }
    }
}