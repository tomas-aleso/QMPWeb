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
    public class TelasController : Controller
    {

        public List<Tela> TraerTelas(){

            TelaRepository telaDAO = new TelaRepository();

            return telaDAO.TraerTelas();

        }

    }
}
