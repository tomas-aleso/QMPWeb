
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    class FiltroPrendas
    {
        public List<List<Prenda>> filtrarPrendasPorCategoria(List<Prenda> prendas, Evento even)
        {
            List<Prenda> TorsoCapa0 = new List<Prenda>();
            List<Prenda> TorsoCapa1 = new List<Prenda>();
            List<Prenda> TorsoCapa2 = new List<Prenda>();
            List<Prenda> PiernasCapa0 = new List<Prenda>();
            List<Prenda> PiernasCapa1 = new List<Prenda>();
            List<Prenda> PiernasCapa2 = new List<Prenda>();
            List<Prenda> calzado = new List<Prenda>();
            List<Prenda> Accesorios = new List<Prenda>();
            foreach (Prenda p in prendas)
            {

                if (string.Equals(p.tipo.categoria, "torso") && p.tipo.capa == 0 && p.validarFechas(even))
                {
                    TorsoCapa0.Add(p);
                }

                if (string.Equals(p.tipo.categoria, "torso") && p.tipo.capa == 1 && p.validarFechas(even))
                {
                    TorsoCapa1.Add(p);
                }

                if (string.Equals(p.tipo.categoria, "torso") && p.tipo.capa == 2 && p.validarFechas(even))
                {
                    TorsoCapa2.Add(p);
                }

                if (string.Equals(p.tipo.categoria, "piernas") && p.tipo.capa == 0 && p.validarFechas(even))
                {
                    PiernasCapa0.Add(p);
                }

                if (string.Equals(p.tipo.categoria, "piernas") && p.tipo.capa == 1 && p.validarFechas(even))
                {
                    PiernasCapa1.Add(p);
                }

                if (string.Equals(p.tipo.categoria, "piernas") && p.tipo.capa == 2 && p.validarFechas(even))
                {
                    PiernasCapa2.Add(p);
                }

                if (string.Equals(p.tipo.categoria, "calzado") && p.validarFechas(even))
                {
                    calzado.Add(p);
                }

                if (string.Equals(p.tipo.categoria, "accesorio") && p.validarFechas(even))
                {
                    Accesorios.Add(p);
                }
            }

            List<List<Prenda>> prendasFiltradas = new List<List<Prenda>>();

            prendasFiltradas.Add(TorsoCapa0);
            prendasFiltradas.Add(TorsoCapa1);
            prendasFiltradas.Add(TorsoCapa2);
            prendasFiltradas.Add(PiernasCapa0);
            prendasFiltradas.Add(PiernasCapa1);
            prendasFiltradas.Add(PiernasCapa2);
            prendasFiltradas.Add(calzado);
            prendasFiltradas.Add(Accesorios);

            return prendasFiltradas;

        }
    }
}