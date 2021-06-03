using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstrutoraViverSA.Controllers
{
    public class OrcamentosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
