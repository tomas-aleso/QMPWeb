using System;
using QueMePongo;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace queMePongo.Repositories
{
    public class TipoPrendaRepository
    {
        public void Insert(TipoPrenda tipoPrenda, DB context)
        {
            if (context.tipoprendas.Any(c => c.descripcion == tipoPrenda.descripcion))
            { }
            else
            {
                context.tipoprendas.Add(tipoPrenda);
                context.SaveChanges();
                int idPrenda = tipoPrenda.id_tipoPrenda;
                foreach (String s in tipoPrenda.tiposDeTelaPosibles)
                {
                    Tela t = new Tela();
                    t.descripcion = s;
                    TelaRepository tr = new TelaRepository();
                    telaXtipoPrendaRepository ttpr = new telaXtipoPrendaRepository();
                    ttpr.id_tela = tr.Insert(t, context);
                    ttpr.id_tipoprenda = idPrenda;
                    context.telaXtipoPrendaRepositories.Add(ttpr);
                    context.SaveChanges();
                }
            }
        }

        public List<TipoPrenda> TraerTiposDePrenda(){

            DB db = new DB();

            return db.tipoprendas.Where(tipoPrenda => !tipoPrenda.descripcion.StartsWith("Sin")).ToList();

        }

        public TipoPrenda TraerTipoDePrendaPorId(int idTipoPrenda){

            DB db = new DB();

            return db.tipoprendas.FromSqlRaw($"Select * from tipoprendas where id_tipoprenda = '{idTipoPrenda}'").FirstOrDefault();

        }


    }
}