using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Empresa.Repositorio.SqlServer;
using Empresa.Dominio;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Empresa.Mvc.Controllers
{
    public class ContatosController : Controller
    {
        private readonly EmpresaDbContex _contexto;
        private IDataProtector _protectorProvider;

        public ContatosController(EmpresaDbContex contexto,IDataProtectionProvider protetionProvider,
            IConfiguration configuracao)
        {            
            _contexto = contexto;
            _protectorProvider = protetionProvider.CreateProtector(
                configuracao.GetSection("ChaveCriptografia").Value);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_contexto.Contatos.ToList());
        }

        public IActionResult Create()
        {
            if (User.HasClaim("Contatos", "Inserir"))
            {
                return View();
            }

            return RedirectToAction("AcessoNegado", "Home");
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            if (!ModelState.IsValid)
            {
                return View(contato);
            }

            contato.Senha = _protectorProvider.Protect(contato.Senha);
            _contexto.Add(contato);
            _contexto.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
