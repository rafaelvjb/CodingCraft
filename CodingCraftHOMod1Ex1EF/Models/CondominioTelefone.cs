using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CodingCraftHOMod1Ex1EF.Models
{
    [Table("CondominioTelefones")]
    public class CondominioTelefone : Entidade
    {
        public CondominioTelefone()
        {
            CondominioTelefoneId = Guid.NewGuid();
        }

        [Key]
        public Guid CondominioTelefoneId { get; set; }
        [Index("IUQ_CondominioTelefone_Referencia_CondominioId", IsUnique = true, Order = 1)]
        public Guid CondominioId { get; set; }

        [Display(Name = "DDD")]
        [Required]
        [StringLength(2)]
        public string Ddd { get; set; }

        [Display(Name = "Número")]
        [Required]
        [StringLength(9)]
        public string Numero { get; set; }

        [Display(Name = "Referência")]
        [Index("IUQ_CondominioTelefone_Referencia_CondominioId", IsUnique = true, Order = 2)]
        [StringLength(50)]
        public string Referencia { get; set; }

        public virtual Condominio Condominio { get; set; }

        //Propriedade para tratamento de lista BCI
        [NotMapped]
        public bool ItemDeleted{ get; set; }


        public override string ToString()
        {
            return $"({Ddd}) {Numero} ";
        }
    }
}