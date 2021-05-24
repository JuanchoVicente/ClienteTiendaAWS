using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteTienda.Models
{
    [Table("Aperitivo")]
    public class Aperitivo
    {
        [Key]
        [Column("IdAperitivo")]
        public int Id { get; set; }

        [Column("Nombre")]
        public String Nombre { get; set; }
        
        [Column("Descripcion")]
        public String Descripcion { get; set; }
        
        [Column("Precio")]
        public int Precio { get; set; }

        [Column("Categoria")]
        public String Categoria { get; set; }

        [Column("Imagen")]
        public String Imagen { get; set; }

        [Column("IdHamburguesa")]
        public int IdHambur { get; set; }

        [Column("Ingredientes")]
        public String Ingredientes { get; set; }
    }
}
