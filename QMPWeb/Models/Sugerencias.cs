using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    class Combinaciones
    {
        public List<Atuendo> generarCombinacion(List<List<Prenda>> prendasFiltradas)
        {
            List<Atuendo> sugerencias = new List<Atuendo>();
            for (int c = 0; c < prendasFiltradas[7].Count; c++)
            {
                for (int d = 0; d < prendasFiltradas[6].Count; d++)
                {
                    for (int e = 0; e < prendasFiltradas[5].Count; e++)
                    {
                        for (int f = 0; f < prendasFiltradas[4].Count; f++)
                        {
                            for (int g = 0; g < prendasFiltradas[3].Count; g++)
                            {
                                for (int h = 0; h < prendasFiltradas[2].Count; h++)
                                {
                                    for (int i = 0; i < prendasFiltradas[1].Count; i++)
                                    {
                                        for (int j = 0; j < prendasFiltradas[0].Count; j++)
                                        {
                                            Atuendo temp = new Atuendo();
                                            temp.prendas.Add(prendasFiltradas[7][c]);
                                            temp.prendas.Add(prendasFiltradas[6][d]);
                                            temp.prendas.Add(prendasFiltradas[5][e]);
                                            temp.prendas.Add(prendasFiltradas[4][f]);
                                            temp.prendas.Add(prendasFiltradas[3][g]);
                                            temp.prendas.Add(prendasFiltradas[2][h]);
                                            temp.prendas.Add(prendasFiltradas[1][i]);
                                            temp.prendas.Add(prendasFiltradas[0][j]);
                                            sugerencias.Add(temp);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return sugerencias;
        }
    }
}