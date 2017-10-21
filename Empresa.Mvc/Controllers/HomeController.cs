using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empresa.Mvc.ViewModels;
using Empresa.Repositorio.SqlServer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Empresa.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmpresaDbContex _contexto;
        private IDataProtector _protectorProvider;

        public HomeController(EmpresaDbContex contexto, IDataProtectionProvider protetionProvider,
            IConfiguration configuracao)
        {
            _contexto = contexto;
            _protectorProvider = protetionProvider.CreateProtector(
                configuracao.GetSection("ChaveCriptografia").Value);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var contato = _contexto.Contatos.Where(c => c.Email == viewModel.Email &&
            _protectorProvider.Unprotect(c.Senha) == viewModel.Senha).SingleOrDefault();

            if (contato == null)
            {
                ModelState.AddModelError("","Usuario ou senha Invalida");
                return View(viewModel);
            }

            var claimns = new List<Claim>
            {
                new Claim(ClaimTypes.Name, contato.Nome),
                new Claim(ClaimTypes.Email,contato.Email) 
            };

            return RedirectToAction("Index","Home");
        }
    }
}
