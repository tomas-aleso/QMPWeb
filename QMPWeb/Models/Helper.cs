using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using queMePongo.Repositories;

namespace QueMePongo
{
    public class Helper
    {
        List<Usuario> usuarios = new List<Usuario>();
        public List<TipoPrenda> tipoDePrenda = new List<TipoPrenda>();
        public int capacidadMaxGratuito = 200;
        public int capacidadMaxPremium = -1;
        Gratuito gratuito = new Gratuito();
        Premium premium = new Premium();

        public Usuario crearUsuario(String nombre, String contrasenia, String mail)
        {
            DB context = new DB();
            UsuarioRepository usr = new UsuarioRepository();
            Usuario value = new Usuario(nombre, gratuito, contrasenia, mail);
            usr.Insert(value, context);
            usuarios.Add(value);
            return value;
        }

        public void upgradeUsuario(Usuario usr)
        {
            usr.modificarTipo(premium);
        }

        public void downgradeUsuario(Usuario usr)
        {
            usr.modificarTipo(gratuito);
        }

        public List<TipoPrenda> levantarJSon()
        {
            try
            {
                var json = System.IO.File.ReadAllText(@"../../datos.json");
                List<TipoPrenda> listaPrendas = JsonConvert.DeserializeObject<JObject>(json).Value<JArray>("tipoDePrenda").ToObject<List<TipoPrenda>>();
                TipoPrendaRepository tpr = new TipoPrendaRepository();
                DB context = new DB();
                foreach (TipoPrenda p in listaPrendas)
                {
                    tpr.Insert(p, context);
                }
                return listaPrendas;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR AL ABRIR Y CARGAR JSON");
                return null;
            }

        }
    }
}