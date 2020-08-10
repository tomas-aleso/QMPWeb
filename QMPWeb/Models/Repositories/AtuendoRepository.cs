using System;
using System.Collections.Generic;
using QueMePongo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace queMePongo.Repositories
{
    public class AtuendoRepository
    {
        public void Insert(Atuendo atuendo, Evento even, DB context)
        {
            context.atuendos.Add(atuendo);
            context.SaveChanges();
            sugerenciaXeventoRepository gur = new sugerenciaXeventoRepository();
            prendaXatuendoRepository par;
            gur.id_atuendo = atuendo.id_atuendo;
            gur.id_evento = even.id_evento;
            context.sugerenciaXeventoRepositories.Add(gur);
            context.SaveChanges();
            foreach (Prenda p in atuendo.prendas)
            {
                par = new prendaXatuendoRepository();
                par.id_atuendo = atuendo.id_atuendo;
                par.id_prenda = p.id_prenda;
                context.prendaXatuendoRepositories.Add(par);
                context.SaveChanges();
            }
            
        }

        public void Delete(int atuendoId, DB context)
        {
            Atuendo g = new Atuendo();
            g = context.atuendos.Single(b => b.id_atuendo == atuendoId);
            List<prendaXatuendoRepository> gur = new List<prendaXatuendoRepository>();
            gur = context.prendaXatuendoRepositories.Where(u => u.id_atuendo == atuendoId).ToList();
            foreach (prendaXatuendoRepository gu in gur)
            {
                context.prendaXatuendoRepositories.Remove(gu);
            }
            context.atuendos.Remove(g);
            context.SaveChanges();
        }

        
    }
}