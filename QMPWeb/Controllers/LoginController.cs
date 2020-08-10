using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueMePongo;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QMPWeb.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(int? error)
        {

            if(error != null){
                DB db = new DB();
                Error err = db.errores.FromSqlRaw($"Select * From errores Where id_error = '{error}'").FirstOrDefault();
                ViewData.Add("mensajeDeError", err.descripcion);
                return View("Login");
            } else {
                return View("Login");
            }

        }

        [HttpPost]
        public ActionResult login(IFormCollection form)
        {
            DB db = new DB();

            String nombreUsuario = form["usuario"];

            Usuario user = db.usuarios.FromSqlRaw($"Select * From Usuarios Where usuario ='{nombreUsuario}'").FirstOrDefault();

            if(user != null && user.contrasenia == form["contrasenia"]){

                return RedirectToAction("Index", "Home", new {idUsuario = user.id_usuario});

            } else {
            
                TempData["ErrorMessage"] = "Usuario o Contraseña incorrectos";

                return RedirectToAction("Index", "Login");

            }

        }

        public ActionResult Logout(){

            return RedirectToAction("Index", "Login");
            
        }
    }
}
