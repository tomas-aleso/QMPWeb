using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using queMePongo.Repositories;
using QueMePongo;

namespace QMPWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int? idUsuario)
        {

            if(idUsuario != null){ //Valida que el usuario se haya logeado, como una especie de middleware
                DB db = new DB();

                UsuarioRepository userDAO = new UsuarioRepository();
                Usuario usuario = userDAO.BuscarUsuarioPorId(idUsuario);

                ViewBag.Id = usuario.id_usuario;
                ViewBag.NombreUsuario = usuario.usuario;

                return View();

            } else {

                return RedirectToAction("Index", "Login");

            }

        }
    }
}
