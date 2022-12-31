using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Controle_ACE_2.Models
{
    public class Salarie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 ID { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public String Fonction { get; set; }
        public Double Salaire { get; set; }

        [ForeignKey("Departement")]
        public Int32 DepartementId { get; set; }  
        public Departement Departement { get; set; }  
    }
}
