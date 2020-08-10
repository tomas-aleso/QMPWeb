using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    class GenerarSugerencias
    {
        public List<Atuendo> ejecutarGenerar(int temp, List<Prenda> p, Evento even)
        {
            return filtrarCombinaciones(combinar(filtrarPrenda(p, even)), temp);
        }

        public List<List<Prenda>> filtrarPrenda(List<Prenda> p, Evento even)
        {
            FiltroPrendas f = new FiltroPrendas();
            return f.filtrarPrendasPorCategoria(p, even);
        }

        public List<Atuendo> combinar(List<List<Prenda>> prendasFiltradas)
        {
            Combinaciones c = new Combinaciones();
            return c.generarCombinacion(prendasFiltradas);
        }

        public List<Atuendo> filtrarCombinaciones(List<Atuendo> a, int temp)
        {
            FiltroCombinaciones fc = new FiltroCombinaciones();
            List<Atuendo> primerFiltrado = fc.filtrarSuperpociciones(a);
            List<Atuendo> segundoFiltrado = fc.filtrarTemperatura(primerFiltrado, temp);
            return segundoFiltrado;
        }
    }
}