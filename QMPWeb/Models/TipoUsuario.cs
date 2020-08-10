using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{
    public abstract class TipoUsuario
    {
        private int tipo;

        public int Tipo { get => tipo; }
        public abstract Guardarropa crearGuardarropa(String nombreGuardarropa, Usuario user);
        public abstract int topePrendasPorGuardarropa();
    }
}