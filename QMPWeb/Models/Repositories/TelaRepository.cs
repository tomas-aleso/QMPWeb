using System;
using QueMePongo;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace queMePongo.Repositories
{
    public class TelaRepository
    {
        public int Insert(Tela tela, DB context)
        {
            if (context.telas.Any(c => c.descripcion == tela.descripcion))
            { }
            else
            {
                context.telas.Add(tela);
                context.SaveChanges();
            }
            return (context.telas.Single(b => b.descripcion == tela.descripcion)).id_tela;
        }

        public List<Tela> TraerTelas(){
            DB db = new DB();

            return db.telas.ToList();
        }

        public Tela TraerTelaPorId(int idTela){

            DB db = new DB();

            return db.telas.FromSqlRaw($"Select * from telas where id_tela = '{idTela}'").FirstOrDefault();
        }
    }
}
