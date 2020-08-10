using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    class FiltroCombinaciones
    {
        public int totalDeAbrigo(Atuendo a)
        {
            int total = 0;
            for (int i = 0; i < a.prendas.Count; i++)
            {
                total += a.prendas[i].tipo.nivelDeAbrigo;
            }
            return total;
        }

        public List<Atuendo> filtrarSuperpociciones(List<Atuendo> SinFiltrar)
        {
            List<Atuendo> filtrados = new List<Atuendo>();
            foreach (Atuendo atuendo in SinFiltrar)
            {
                List<Prenda> Torso = atuendo.prendas.FindAll(p => string.Equals(p.tipo.categoria, "torso"));
                List<Prenda> piernas = atuendo.prendas.FindAll(p => string.Equals(p.tipo.categoria, "piernas"));
                List<Prenda> calzado = atuendo.prendas.FindAll(p => string.Equals(p.tipo.categoria, "calzado"));
                if (ComprobarTodo(Torso) && ComprobarTodo(piernas) && ComprobarTodo(calzado))
                    filtrados.Add(atuendo);
            }
            return filtrados;
        }
        private bool ComprobarTodo(List<Prenda> prendas)
        {
            int a = prendas.FindAll(p => p.tipo.capa == 1).Count;
            return prendas.Count <= 3 && a == 1;
        }

        public List<Atuendo> filtrarTemperatura(List<Atuendo> atuendos, int temperatura)
        {
            int nivelAbrigo = queNivelDeAbrigoNecesito(temperatura);
            List<Atuendo> atuendosAbrigados = new List<Atuendo>();
            for (int i = 0; i < atuendos.Count; i++)
            {
                if (cumpleAbrigo(totalDeAbrigo(atuendos[i]), nivelAbrigo))
                    atuendosAbrigados.Add(atuendos[i]);
            }
            return atuendosAbrigados;
        }

        private bool cumpleAbrigo(int totalAbrigo, int nivelAbrigo)
        {
            return (totalAbrigo >= nivelAbrigo - 5 || totalAbrigo <= nivelAbrigo + 5);
        }

        private int queNivelDeAbrigoNecesito(int temp)
        {
            if (temp < 0)
            {
                return 400;
            }
            if (temp >= 0 && temp < 5)
            {
                return 300;
            }
            if (temp >= 5 && temp < 10)
            {
                return 240;
            }
            if (temp >= 10 && temp < 15)
            {
                return 180;
            }
            if (temp >= 15 && temp < 20)
            {
                return 120;
            }
            if (temp >= 20 && temp < 30)
            {
                return 60;
            }
            if (temp >= 30)
            {
                return 40;
            }
            return 0;
        }
    }
}