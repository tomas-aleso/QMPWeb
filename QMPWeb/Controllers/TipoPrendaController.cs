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
    public class TipoPrendaController : Controller
    {

        public List<TipoPrenda> TraerTiposDePrenda(){

            TipoPrendaRepository tipoPrendaDAO = new TipoPrendaRepository();

            return tipoPrendaDAO.TraerTiposDePrenda();
        }

    }
}
