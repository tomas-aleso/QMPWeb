using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    [Table("telas")]
    public class Tela
    {
        [Key]
        [Column("id_tela")]
        public int id_tela { get; set; }

        [Column("descripcion")]
        public string descripcion { get; set; }

        public Tela(String descrip){
            descripcion = descrip;
        }

        public Tela() { }
    }
}
