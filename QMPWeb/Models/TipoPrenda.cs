using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    [Table("tipoprendas")]
    public class TipoPrenda
    {
        [Key]
        [Column("id_tipoprenda")]
        public int id_tipoPrenda { get; set; }

        [Column("descripcion")]
        public String descripcion { get; set; }

        [Column("categoria")]
        public String categoria { get; set; }

        [Column("niveldeabrigo")]
        public int nivelDeAbrigo { get; set; }

        [Column("capa")]
        public int capa { get; set; }

        public List<String> tiposDeTelaPosibles = new List<String>();

        [NotMapped]
        public virtual ICollection<Prenda> Prendas { get; set; }

        public TipoPrenda() { }
    }
}