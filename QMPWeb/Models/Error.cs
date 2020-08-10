using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    [Table("errores")]
    public class Error
    {
        [Key]
        [Column("id_error")]
        public int id_atuendo { get; set; }

        [Column("nombreerror")]
        public string nombreError {get; set;}

        [Column("descripcion")]
        public string descripcion {get; set;}
    }
}