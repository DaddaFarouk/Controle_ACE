using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Controle_ACE_2.Models
{
    public class Departement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 DepartementId { get; set; }
        public String Description { get; set; }
        public String Location { get; set; }
        public ICollection<Salarie> Salaries { get; set; } 
    }
}
