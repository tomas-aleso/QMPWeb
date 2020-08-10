using System;
using System.Collections.Generic;
using QueMePongo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace queMePongo.Repositories
{
    [Table("sugerenciasxevento")]
    public class sugerenciaXeventoRepository
    {
        [Key]
        [Column("id_sugerenciaxevento")]
        public int id_sugerenciaxevento { get; set; }

        [Column("id_atuendo")]
        public int id_atuendo { get; set; }

        [Column("id_evento")]
        public int id_evento { get; set; }
        public sugerenciaXeventoRepository() { }

        public List<sugerenciaXeventoRepository> BuscarSugerenciasPorEvento(int eventoId, DB db)
        {
            return db.sugerenciaXeventoRepositories.FromSqlRaw($"Select * From sugerenciasxevento Where id_evento = '{eventoId}'").ToList();
        }
    }

    
}
