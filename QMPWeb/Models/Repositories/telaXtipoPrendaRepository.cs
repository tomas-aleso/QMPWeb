using System;
using QueMePongo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queMePongo.Repositories
{
    [Table("telaxtipo_prenda")]
    public class telaXtipoPrendaRepository
    {
        [Key]
        [Column("id_telaxtipo_prenda")]
        public int id_telaxtipo_prenda { get; set; }

        [Column("id_tela")]
        public int id_tela { get; set; }

        [Column("id_tipoprenda")]
        public int id_tipoprenda { get; set; }
        public telaXtipoPrendaRepository() { }
    }
}
