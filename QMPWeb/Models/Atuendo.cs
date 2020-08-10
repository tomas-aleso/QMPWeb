using queMePongo.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    [Table("atuendos")]
    public class Atuendo
    {
        [Key]
        [Column("id_atuendo")]
        public int id_atuendo { get; set; }

        public List<Prenda> prendas = new List<Prenda>();

        [NotMapped]
        public virtual ICollection<Prenda> Prendas { get; set; }

        public Atuendo() { }

        public bool Igual(Atuendo atuendo)
        {
            if (prendas.Count != atuendo.prendas.Count)
                return false;
            for (int i = 0; i < prendas.Count; i++)
                if (!prendas[i].Igual(atuendo.prendas[i]))
                    return false;
            return true;
        }

        public float getPuntuacion()
        {
            float puntuacion = 0;
            prendas.ForEach(p => puntuacion += p.getCalif());
            return puntuacion/ (float)(prendas.Count);
        }

        public bool validarAtuendo(Evento even)
        {
            foreach (Prenda p in prendas)
            {
                if (p.validarFechas(even) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Atuendo> getAtuendosPorEv(int idEvento, DB context)
        {
            Atuendo a;
            List<Atuendo> atuendos = new List<Atuendo>();
            PrendaRepository pren = new PrendaRepository();
            TipoPrendaRepository t = new TipoPrendaRepository();
            sugerenciaXeventoRepository sxe = new sugerenciaXeventoRepository();
            prendaXatuendoRepository pxa = new prendaXatuendoRepository();
            List<sugerenciaXeventoRepository> listsxe = sxe.BuscarSugerenciasPorEvento(idEvento,context);
            foreach (sugerenciaXeventoRepository s in listsxe) 
            {
                a = new Atuendo();
                a.id_atuendo = s.id_atuendo;

                List<prendaXatuendoRepository> listpxa = pxa.BuscarPrendasPorSugerencias(a.id_atuendo, context);
                foreach (prendaXatuendoRepository p in listpxa) 
                {
                    Prenda prenda = pren.BuscarPrendaPorId(p.id_prenda);
                    prenda.tipo = t.TraerTipoDePrendaPorId(prenda.tipoPrenda);
                    a.prendas.Add(prenda);
                }

                atuendos.Add(a);
            }

            return atuendos;
        }
    }
}