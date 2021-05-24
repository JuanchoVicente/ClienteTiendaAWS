using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteTienda.Models
{
    [Table("Hamburguesa")]
    public class Hamburguesa
    {
        [Key]
        [Column("Id")]
        public int IdHamburguesa {get; set;} 

        [Column("Nombre")]
        public string Nombre { get; set; }
        
        [Column("Imagen")]
        public string Imagen { get; set; }
        
        [Column("Precio")]
        public int Precio { get; set; }
        
        [Column("Ingredientes")]
        public string Ingredientes { get; set; }

    }
}
