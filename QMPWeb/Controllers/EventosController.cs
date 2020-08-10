using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueMePongo;
using queMePongo.Repositories;

namespace QMPWeb.Controllers
{
    public class EventosController : Controller
    {
        public IActionResult Index(int idUsuario)
        {
                
            if(idUsuario != 0){


                var userRepository = new UsuarioRepository();
                DB db = new DB();

                ViewBag.Eventos = db.eventos.FromSqlRaw($"Select * From eventos Where id_usuario = '{idUsuario}'").ToList();

                ViewBag.Id = idUsuario;

                return View("Eventos");
            } else {
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpPost]
        public IActionResult CrearEvento(IFormCollection form)
        {

            DB db = new DB();
            UsuarioRepository userDAO = new UsuarioRepository();
            Usuario usuario = new Usuario();

            int idUser = Convert.ToInt32(form["idUsuario"]);
            DateTime fechaInicioPrendas = Convert.ToDateTime(form["idFechaIni"]);
            DateTime fechaFinPrendas = Convert.ToDateTime(form["idFechaFin"]);
            DateTime fechaDeRecordatorio = fechaInicioPrendas.AddHours(-1 * Convert.ToInt32(form["idRecord"]));
            int tipoEvento = Convert.ToInt32(form["frecuencia"]);

            usuario = userDAO.BuscarUsuarioPorId(idUser);

            Evento evento = new Evento(form["idLugar"], form["despripcionEvento"], usuario, fechaDeRecordatorio, fechaInicioPrendas, fechaFinPrendas, tipoEvento);

            EventoRepository eventoDAO = new EventoRepository();
            eventoDAO.Insert(evento, db);

            TempData["SuccessMessage"] = "Evento creado con exito! :D";

            return RedirectToAction("Index", "Eventos", new { idUsuario = idUser });

        }

        public IActionResult EliminarEvento(IFormCollection form)
        {

            string idEventoString = form["idEvento"];
            string idUsuarioString = form["idUsuario"];
            int idEvento = Convert.ToInt32(idEventoString);
            int idUser = Convert.ToInt32(idUsuarioString);

            EventoRepository eventoDAO = new EventoRepository();

            eventoDAO.Delete(idEvento);

            TempData["SuccessMessage"] = "Evento eliminado! :D";
            return RedirectToAction("Index", "Eventos", new { idUsuario = idUser });

        }

        public IActionResult CargarEventoParaEditar(int idEvento, int idUsuario){

            EventoRepository eventoDAO = new EventoRepository();
            Evento evento = eventoDAO.BuscarEventoPorId(idEvento);

            ViewBag.Id = idUsuario;
            ViewBag.Evento = evento;
            switch(evento.tipoEvento){
                case 0:
                    ViewBag.TipoDeEvento = "Evento único";
                    break;
                case 1:
                    ViewBag.TipoDeEvento = "Todos los días";
                    break;
                case 2:
                    ViewBag.TipoDeEvento = "Una vez por semana";
                    break;
                case 3:
                    ViewBag.TipoDeEvento = "Una vez por mes";
                    break;
                case 4:
                    ViewBag.TipoDeEvento = "Una vez por año";
                    break;
                default:
                    ViewBag.TipoDeEvento = "No seleccionaste una opcion";
                    break;
            }

            return View("Evento");

        }

        public IActionResult ActualizarEvento(IFormCollection form){

            EventoRepository eventoDAO = new EventoRepository();

            string idUsuarioString = form["idUsuario"];
            int idUser = Convert.ToInt32(idUsuarioString);

            eventoDAO.Update(form);

            TempData["SuccessMessage"] = "Evento actualizado correctamente! :D";
            return RedirectToAction("Index", "Eventos", new {idUsuario = idUser});

        }

        public IActionResult CargarAtuendosParaElegir(int idEvento, int idUsuario){
            
            EventoRepository eventoDAO = new EventoRepository();

            Evento evento = eventoDAO.BuscarEventoPorId(idEvento);
            Atuendo at = new Atuendo();
            DB db = new DB();

            ViewBag.Sugerencias = at.getAtuendosPorEv(idEvento,db);
            ViewBag.Evento = evento;
            ViewBag.Id = idUsuario;

            return View("SugerenciasParaEvento");

        }

    }
}
