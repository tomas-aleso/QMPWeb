using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QueMePongo;
using queMePongo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace QMPWeb.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IConfiguration config;

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormCollection form)
        {
            var helper = new Helper();

            helper.crearUsuario(form["usuario"], form["contrasenia"], form["mail"]);

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public List<Guardarropa> TraerGuardarropasDelUsuario(int idUsuario){

            DB db = new DB();

            List<Guardarropa> guardarropas = db.guardarropas.FromSqlRaw($"Select * From guardarropas Where id_duenio = '{idUsuario}'").ToList();

            return guardarropas;

        }

    }
}
