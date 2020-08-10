using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueMePongo;
using queMePongo.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QMPWeb.Controllers
{
    public class PrendasController : Controller
    {
        public readonly IHostingEnvironment hostingEnviroment;
        public PrendasController(IHostingEnvironment ihe){
            hostingEnviroment = ihe;
        }

        public IActionResult Index(int idUsuario)
        {
            if(idUsuario != 0){

                guardarropaXusuarioRepository guardarropaDAO = new guardarropaXusuarioRepository();

                List<guardarropaXusuarioRepository> guardarropasParciales = guardarropaDAO.listarGuardarropasDeUsuario(idUsuario);

                guardarropaXprendaRepository prendasDAO = new guardarropaXprendaRepository();

                List<guardarropaXprendaRepository> prendasParciales = new List<guardarropaXprendaRepository>();

                foreach(guardarropaXusuarioRepository guarda in guardarropasParciales)
                {
                    prendasParciales.AddRange(prendasDAO.listarPrendasDeGuardarropa(guarda.id_guardarropa)); 
                }
            
                ViewBag.Prendas = prendasParciales;

                ViewBag.Id = idUsuario;

                ViewBag.PrendasDelUsuario = TraerPrendasDelUsuario(idUsuario);

                return View("Prendas");

            } else {

                return RedirectToAction("Index", "Login");

            }
        }

        [HttpPost]
        public IActionResult Create(IFormCollection form, IFormFile imagenDePrenda){

            DB db = new DB();

            Prenda prendaNueva = new Prenda();
            PrendaRepository prendaDAO = new PrendaRepository();

            prendaNueva.colorPrincipal = form["colorPrincipal"];
            prendaNueva.colorSecundario = form["colorSecundario"];

            string idTelaString = form["tipoDeTela"];
            string idGuardarropaString = form["idGuardarropa"];
            string idTipoPrendaString = form["tipoDePrenda"];
            string idUsuarioString = form["idUsuario"];

            int idGuardarropa = Convert.ToInt32(idGuardarropaString);
            int idTela = Convert.ToInt32(idTelaString);
            int idTipoPrenda = Convert.ToInt32(idTipoPrendaString);
            int idUsuario = Convert.ToInt32(idUsuarioString);
            
            prendaNueva.id_tela = idTela;
            prendaNueva.tipoPrenda = idTipoPrenda;
            prendaNueva.id_duenio = idUsuario;

            prendaNueva.urlImagen = prendaNueva.nombreUnicoImagen(imagenDePrenda, hostingEnviroment);

            prendaDAO.CrearPrenda(prendaNueva, db, idGuardarropa);

            TempData["SuccessMessage"] = "Prenda creada correctamente! :D ";

            return RedirectToAction("Index", "Prendas", new {idUsuario = idUsuario});
        }

        public IActionResult CargarPrendaParaEditar(int idPrenda, int idUsuario){

            PrendaRepository prendaDAO = new PrendaRepository();
            TelaRepository telaDAO = new TelaRepository();
            TipoPrendaRepository tipoPrendaDAO = new TipoPrendaRepository();

            Prenda prenda = prendaDAO.BuscarPrendaPorId(idPrenda);

            ViewBag.Id = idUsuario;
            ViewBag.Prenda = prenda;

            ViewBag.TipoDePrenda = tipoPrendaDAO.TraerTipoDePrendaPorId(prenda.tipoPrenda);
            ViewBag.TipoDeTelaDePrenda = telaDAO.TraerTelaPorId(prenda.id_tela);

            ViewBag.TiposDePrenda = tipoPrendaDAO.TraerTiposDePrenda();
            ViewBag.Telas = telaDAO.TraerTelas();

            return View("Prenda");

        }

        [HttpPost]
        public IActionResult ActualizarPrenda(IFormCollection form, IFormFile imagenNueva){

            PrendaRepository prendaDAO = new PrendaRepository();

            string idUsuarioString = form["idUSuario"];
            string idPrendaString = form["idPrenda"];
            string idTipoPrendaViejoString = form["tipoPrendaViejo"];
            string idTipoPrendaNuevoString = form["tipoPrendaNuevo"];
            string idTipoTelaViejoString = form["tipoTelaViejo"];
            string idTipoTelaNuevoString = form["tipoTelaNuevo"];
  
            int idUsuario = Convert.ToInt32(idUsuarioString);
            int idPrenda = Convert.ToInt32(idPrendaString);

            Prenda prenda = prendaDAO.BuscarPrendaPorId(idPrenda);

            prenda.id_prenda = idPrenda;
            prenda.colorPrincipal = form["colorPrincipal"];
            prenda.colorSecundario = form["colorSecundario"];

            if(imagenNueva != null){
                prenda.urlImagen = prenda.nombreUnicoImagen(imagenNueva, hostingEnviroment);
            }

            if(idTipoPrendaNuevoString != "-"){
                int idTipoPrendaNuevo = Convert.ToInt32(idTipoPrendaNuevoString);
                prenda.tipoPrenda = idTipoPrendaNuevo;
            } 

            if(idTipoTelaNuevoString != "-"){
                int idTipoTela = Convert.ToInt32(idTipoTelaNuevoString);
                prenda.id_tela = idTipoTela;
            }

            if(prendaDAO.EditarPrenda(prenda, idUsuario)){
                TempData["SuccessMessage"] = "Prenda editada correctamente! :D";
                return RedirectToAction("Index", "Prendas", new {idUsuario = idUsuario});
            } else {
                TempData["ErrorMessage"] = "No podes editar esta prenda porque no sos el dueño :(";
                return RedirectToAction("Index", "Prendas", new {idUsuario = idUsuario});
            }

        }

        [HttpPost]
        public IActionResult EliminarPrenda(IFormCollection form){

            PrendaRepository prendaDAO = new PrendaRepository();
            DB db = new DB();

            string idPrendaString = form["idPrenda"];
            string idUsuarioString = form["idUsuario"];

            int idPrenda = Convert.ToInt32(idPrendaString);
            int idUser = Convert.ToInt32(idUsuarioString);

            if(prendaDAO.EliminarPrenda(idPrenda, idUser, db)){

                TempData["SuccessMessage"] = "Prenda eliminada! :D";

                return RedirectToAction("Index", "Prendas", new {idUsuario = idUser});

            } else {

                TempData["ErrorMessage"] = "No podes eliminar esta prenda porque no sos su dueño!";

                return RedirectToAction("Index", "Prendas", new {idUsuario = idUser});

            }

        }
        public List<Prenda> TraerPrendasDelUsuario(int idUsuario){
            
            PrendaRepository prendaDAO = new PrendaRepository();

            List<Prenda> prendasDelUsuario = prendaDAO.PrendasDelUsuario(idUsuario);

            return prendasDelUsuario;

        }

        [HttpPost]
        public IActionResult AgregarPrendaAGuardarropa(IFormCollection form){

            PrendaRepository prendaDAO = new PrendaRepository();

            string idUsuarioString = form["idUsuario"];
            string idPrendaString = form["idPrenda"];
            string idGuardarropaString = form["idGuardarropa"];

            int idUser = Convert.ToInt32(idUsuarioString);
            int idPrenda = Convert.ToInt32(idPrendaString);
            int idGuardarropa = Convert.ToInt32(idGuardarropaString);

            if(prendaDAO.agregarPrendaAGuardarropa(idPrenda, idGuardarropa, idUser)){
                TempData["SuccessMessage"] = "Prenda agregada al guardarropa! :D";
            }else{
                TempData["ErrorMessage"] = "Esa prenda ya está agregada en ese guardarropa!";
            }
            
            return RedirectToAction("Index", "Prendas", new {idUsuario = idUser});

        }

    }
}
