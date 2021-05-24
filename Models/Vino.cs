using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClienteTienda.Models
{
    [Table("Vino")]
    public class Vino
    {
        [Key]
        [Column("Id")]
        public int IdVino { get; set; }
        [Column("Nombre")]
        public String Nombre { get; set; }
        [Column("Precio")]
        public int Precio { get; set; }
        [Column("Descripcion")]
        public String Descripcion { get; set; }
        [Column("Capacidad")]
        public int Capacidad { get; set; }
        [Column("Cantidad")]
        public int Cantidad { get; set; }
        [Column("Imagen")]
        public String Imagen { get; set; }
        [Column("Conservacion")]
        public String Conservacion { get; set; }
        [Column("Enlace")]
        public String Enlace { get; set; }
        [Column("Valoracion")]
        public int Valoracion { get; set; }

    }
}
