using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using queMePongo.Repositories;
using Microsoft.EntityFrameworkCore;


namespace QueMePongo
{
    [Table("usuarios")]
    public class Usuario
    {

        [Key]
        [Column("id_usuario")]
        public int id_usuario { get; set; }

        [Column("usuario")]
        public string usuario { get; set; }

        [Column("contrasenia")]
        public string contrasenia { get; set; }

        [Column("tipodeusuario")]
        public int tipoDeUsuario { get; set; }

        [Column("mail")]
        public string mail { get; set; }

        [NotMapped]
        public List<Evento> eventosDeUsuario { get; set; }

        public List<Guardarropa> guardarropas = new List<Guardarropa>();

        public List<Evento> eventos = new List<Evento>();

        [NotMapped]
        public TipoUsuario tipoUsuario { get; set; }

        public Usuario(String user, TipoUsuario tu, String pass, String email)
        {
            usuario = user;
            tipoUsuario = tu;
            tipoDeUsuario = tu.Tipo;
            contrasenia = pass;
            mail = email;
        }

        public void modificarTipo(TipoUsuario tu)
        {
            this.tipoUsuario = tu;
            tipoDeUsuario = tu.Tipo;
        }

        public Usuario() { }

        public List<Atuendo> ObtenerSugerencias(Evento even)
        {
            Apis api = new Apis();
            int temperatura = 20;//api.solicitarClima(even.lugar);
            List<Atuendo> sugerencias = new List<Atuendo>();
            foreach (Guardarropa guardarropa in guardarropas)
            {
                foreach (Atuendo atuendo in guardarropa.generarSugerencias(temperatura, even))
                {
                    sugerencias.Add(atuendo);
                }

            }
            sugerencias = sugerencias.OrderBy(s1 => s1.getPuntuacion()).ToList();
            return sugerencias;
        }


        public void crearEvento(DateTime fechaIni, DateTime fechaFinP, DateTime fechaIniP, String lugar, String descripcion, int tipoEvento)
        {
            // Evento even = new Evento(lugar, descripcion, this, fechaIni, fechaIniP, fechaFinP, tipoEvento);
            // eventos.Add(even);
            // Console.WriteLine("Se ha creado el evento");
        }

        public void eliminarEvento(String lugar)
        {
            foreach (Evento a in eventos)
            {

                if (lugar == a.lugar)
                {
                    eventos.Remove(a);
                    Console.WriteLine("Evento eliminado");
                    break;
                }
            }
        }

        public bool puedeCompartirElGuardarropa(int tipoDeUsuarioACompartir){
            return tipoDeUsuario == tipoDeUsuarioACompartir || tipoDeUsuario == 0;
        }

        public int compartirGuardarropa(Guardarropa guardarropaOriginal, Usuario usuarioParaCompartir){

            guardarropaXusuarioRepository gxuDAO = new guardarropaXusuarioRepository(); 
            guardarropaXusuarioRepository gxuDAOParaConsulta = new guardarropaXusuarioRepository(); 

            if(usuarioParaCompartir != null){//Compruebo que exista el usuario al que se quiere compartir
                gxuDAOParaConsulta = gxuDAO.BuscarGuardarropaPorIdYIdDeUsuario(guardarropaOriginal.id_guardarropa, usuarioParaCompartir.id_usuario);
                if(gxuDAOParaConsulta == null){//Si fuese != null significa que ya le compartio el guardarropa a ese usuario
                    if(this.puedeCompartirElGuardarropa(usuarioParaCompartir.tipoDeUsuario)){

                        // usuarioDuenio.compartirGuardarropa(guardarropaOriginal, usuarioParaCompartir.id_usuario);
                        DB db = new DB();

                        gxuDAO.id_guardarropa = guardarropaOriginal.id_guardarropa;
                        gxuDAO.id_usuario = usuarioParaCompartir.id_usuario;
                        gxuDAO.nombreGuardarropa = guardarropaOriginal.nombreGuardarropas;

                        db.guardarropaXusuarioRepositories.Add(gxuDAO);

                        db.SaveChanges();

                        return 0;

                    } else {
                        
                        return 1;

                    }
                } else {//En caso de que ya le compartió el guardarropas

                    return 2;

                }
            } else { //Mensaje de error por si no existe el usuario

                return 3;

            }

        }

        public void elegirAtuendo(Evento even)
        {
            List<Atuendo> atuendos = new List<Atuendo>();
            atuendos = this.ObtenerSugerencias(even);


            /*
            Console.WriteLine("Indique el numero de sugerencia que quiere seleccionar:");

            String sugerenciaElegida = Console.ReadLine();

            int opcion = int.Parse(sugerenciaElegida);

            if (atuendos[opcion].validarAtuendo(even))
            {
                foreach (Prenda p in atuendos[opcion].prendas)
                {
                    p.eventos.Add(even);
                    Console.WriteLine("Ha elegido su atuendo Correctamente");
                    calificarAtuendo(atuendos[opcion]);
                }
            }
            else
            {
                Console.WriteLine("El atuendo que eligio ya esta en uso en ese periodo de tiempo, elija otro");
                this.elegirAtuendo(even);
            }*/

        }

        public void calificarAtuendo(Atuendo atuendo) // no se verifica datos ingresados ya que proximamente se hara con una interfaz
        {
            Console.WriteLine("Desea calificar el atuendo y/n");
            String str = Console.ReadLine();
            if (str == "y") {
                Console.WriteLine("Ingrese puntuacion del 1 al 5");
                String puntuacion = Console.ReadLine();

                int punt = int.Parse(puntuacion);
                atuendo.prendas.ForEach(p => p.calificar(punt));
            }
        }

    }

}