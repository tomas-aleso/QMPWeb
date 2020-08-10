using System;
using System.Collections.Generic;
using QueMePongo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace queMePongo.Repositories
{
    [Table("guardarropaxprenda")]
    public class guardarropaXprendaRepository
    {
        [Key]
        [Column("id_guardarropaxprenda")]
        public int id_guardarropaXprenda { get; set; }

        [Column("id_prenda")]
        public int id_prenda { get; set; }

        [Column("id_guardarropa")]
        public int id_guardarropa { get; set; }
        public guardarropaXprendaRepository() { }

        public List<guardarropaXprendaRepository> listarPrendasDeGuardarropa(int idguardarropa)
        {

            DB db = new DB();

            return db.guardarropaXprendaRepositories.FromSqlRaw($"Select * From guardarropaxprenda Where id_guardarropa = '{idguardarropa}'").ToList();
        }
    }
}