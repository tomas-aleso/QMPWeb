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
    public class GuardarropasController : Controller
    {

        public IActionResult Index(int idUsuario)
        {
            if(idUsuario != 0){
                var userRepository = new UsuarioRepository();
                guardarropaXusuarioRepository guardarropaDAO = new guardarropaXusuarioRepository();

                List<guardarropaXusuarioRepository> guardarropasParciales = guardarropaDAO.listarGuardarropasDeUsuario(idUsuario);
                ViewBag.Guardarropas = guardarropasParciales;

                ViewBag.Id = idUsuario;

                return View("Guardarropas");
            } else {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public IActionResult CrearGuardarropa(IFormCollection form){

            DB db = new DB();
            Guardarropa guardarropa = new Guardarropa();

            int idUser = Convert.ToInt32(form["idUsuario"]);

            if(form["nombreGuardarropa"] != ""){
                guardarropa.nombreGuardarropas = form["nombreGuardarropa"];
                guardarropa.id_duenio = idUser;

                GuardarropaRepository guardarropaRepo = new GuardarropaRepository();
                guardarropaRepo.Create(guardarropa, db, idUser);

                Prenda p = new Prenda();
                p.crearPrendasVacias(guardarropa.id_guardarropa,idUser);

                TempData["SuccessMessage"] = "Guardarropa "+guardarropa.nombreGuardarropas+" creado con exito!";
                return RedirectToAction("Index", "Guardarropas", new {idUsuario = idUser});
            } else {

                TempData["ErrorMessage"] = "Tenes que ingresar un nombre válido para crear un guardarropa";
                return RedirectToAction("Index", "Guardarropas", new {idUsuario = idUser});

            }


        }

        public IActionResult EliminarGuardarropa(int idGuardarropa, int idUsuario)
        {

            GuardarropaRepository guardarropaDAO = new GuardarropaRepository();

            var mensaje = guardarropaDAO.Delete(idGuardarropa, idUsuario);

            TempData["SuccessMessage"] = mensaje;
            return RedirectToAction("Index", "Guardarropas", new {idUsuario = idUsuario});

        }

        [HttpPost]
        public IActionResult CompartirGuardarropa(IFormCollection form){

            //Hago estos pasamanos asquerosos porque no me deja parsear directamente
            String nombreDeUsuarioACompartir = form["nombreUsuarioACompartir"];
            String idUsuarioDuenioString = form["idUsuarioDuenio"];
            String idGuardarropaACompartirString = form["idGuardarropaACompartir"];
            int idUsuarioDuenio = Convert.ToInt32(idUsuarioDuenioString);
            int idGuardarropaACompartir = Convert.ToInt32(idGuardarropaACompartirString);

            UsuarioRepository userDAO = new UsuarioRepository();
            GuardarropaRepository guardarropaDAO = new GuardarropaRepository();

            Usuario usuarioDuenio = userDAO.BuscarUsuarioPorId(idUsuarioDuenio);
            Usuario usuarioParaCompartir = userDAO.BuscarUsuarioPorUsername(nombreDeUsuarioACompartir);

            Guardarropa guardarropaParaCompartir = guardarropaDAO.buscarGuardarropaPorIdYPorDuenio(idGuardarropaACompartir, idUsuarioDuenio);
            
            int respuesta = usuarioDuenio.compartirGuardarropa(guardarropaParaCompartir, usuarioParaCompartir);

            switch(respuesta){
                case 0:

                    TempData["SuccessMessage"] = "Guardarropa compartido con "+ usuarioParaCompartir.usuario +" :D !";

                    return RedirectToAction("Index", "Guardarropas", new {idUsuario = idUsuarioDuenio});

                case 1:

                    TempData["ErrorMessage"] = "No se puede compartir el guardarropas con el usuario " + nombreDeUsuarioACompartir + " porque es de un tipo de usuario inferior al tuyo!";

                    return RedirectToAction("Index", "Guardarropas", new {idUsuario = idUsuarioDuenio});

                case 2:

                    TempData["ErrorMessage"] = "Ya compartiste el guardarropa "+ guardarropaParaCompartir.nombreGuardarropas +" con el usuario " + nombreDeUsuarioACompartir + "!";

                    return RedirectToAction("Index", "Guardarropas", new {idUsuario = idUsuarioDuenio});

                case 3:

                    TempData["ErrorMessage"] = "El usuario " + nombreDeUsuarioACompartir + " no existe!";

                    return RedirectToAction("Index", "Guardarropas", new {idUsuario = idUsuarioDuenio});

                default:

                    return RedirectToAction("Index", "Login");

            }

        }
        
        [HttpPost]
        public IActionResult EditarGuardarropa(IFormCollection form){

            GuardarropaRepository guardarropaDAO = new GuardarropaRepository();

            String idGuardarropaString = form["idGuardarropa"];
            String idUsuarioString = form["idUsuario"];
            int idGuardarropa = Convert.ToInt32(idGuardarropaString);
            int idUsuario = Convert.ToInt32(idUsuarioString);

            String nuevoNombreGuardarropa = form["nuevoNombreGuardarropa"];
            String nombreViejoGuardarropa = form["nombreViejoGuardarropa"];

            
            if(guardarropaDAO.TryUpdate(idGuardarropa, idUsuario, nuevoNombreGuardarropa)){//Try update devuelve un true en caso de que pudo editar el guardarropa

                TempData["SuccessMessage"] = "Modificaste el nombre del guardarropa '" + nombreViejoGuardarropa + "' a '" + nuevoNombreGuardarropa + "' con exito :D !";

                return RedirectToAction("Index", "Guardarropas", new {idUsuario = idUsuario});

            } else {

                TempData["ErrorMessage"] = "No podes editar el guardarropa " + nombreViejoGuardarropa + " porque no sos el dueño";

                return RedirectToAction("Index", "Guardarropas", new {idUsuario = idUsuario});

            }

        }

        public IActionResult VerPrendasDelGuardarropas(int idGuardarropa, int idUsuario){

            DB db = new DB();
            GuardarropaRepository guardarropaDAO = new GuardarropaRepository();
            Guardarropa guardarropa = guardarropaDAO.buscarGuardarropaPorId(idGuardarropa);

            List<Prenda> prendas = guardarropaDAO.PrendasDelGuardarropas(idGuardarropa, db);

            ViewBag.Prendas = prendas;
            ViewBag.IdGuardarropa = idGuardarropa;
            ViewBag.nombreGuardarropa = guardarropa.nombreGuardarropas;
            ViewBag.Id = idUsuario;

            return View("PrendasDelGuardarropa");

        }

        public IActionResult EliminarPrenda(IFormCollection form){

            PrendaRepository prendaDAO = new PrendaRepository();
            DB db = new DB();
            
            string idPrendaString = form["idPrenda"];
            string idUsuarioString = form["idUsuario"];
            string idGuardarropaString = form["idGuardarropa"];

            int idPrenda = Convert.ToInt32(idPrendaString);
            int idUser = Convert.ToInt32(idUsuarioString);
            int idGuardarropa = Convert.ToInt32(idGuardarropaString);

            if(prendaDAO.EliminarPrenda(idPrenda, idUser, db)){

                TempData["SuccessMessage"] = "Prenda eliminada! :D";

                return RedirectToAction("VerPrendasDelGuardarropas", "Guardarropas", new {idGuardarropa = idGuardarropa ,idUsuario = idUser});

            } else {

                TempData["ErrorMessage"] = "No podes eliminar esta prenda porque no sos su dueño!";

                return RedirectToAction("VerPrendasDelGuardarropas", "Guardarropas", new {idGuardarropa = idGuardarropa ,idUsuario = idUser});

            }

        }

    }
}
