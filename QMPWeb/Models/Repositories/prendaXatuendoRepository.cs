using System;
using System.Collections.Generic;
using QueMePongo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace queMePongo.Repositories
{
    [Table("prendaxatuendo")]
    public class prendaXatuendoRepository
    {
        [Key]
        [Column("id_prendaxatuendo")]
        public int id_prendaXatuendo { get; set; }

        [Column("id_atuendo")]
        public int id_atuendo { get; set; }

        [Column("id_prenda")]
        public int id_prenda { get; set; }
        public prendaXatuendoRepository() { }

        public List<prendaXatuendoRepository> BuscarPrendasPorSugerencias(int atuendoId, DB db)
        {
            return db.prendaXatuendoRepositories.FromSqlRaw($"Select * From prendaXatuendo Where id_atuendo = '{atuendoId}'").ToList();
        }
    }


}
