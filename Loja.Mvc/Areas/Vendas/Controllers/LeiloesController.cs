using Loja.Dominio;
using Loja.Mvc.Helpers;
using Loja.Repositorios.SqlServer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Loja.Mvc.Areas.Vendas.Controllers
{
    using System.Security.Claims;
    using System.Web.Mvc;
    //[Authorize(Roles = "Master")]
    //[Authorize(Roles = "Admin,Leiloeiro,Comprador")]

    public class LeiloesController : Controller
    {
        private LojaDbContext _db = new LojaDbContext();
        // GET: Vendas/Leiloes
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(Mapeamento.Mapear(_db.Produtos.Where(p => p.EmLeilao).ToList()));
        }        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ususario = (ClaimsIdentity)User.Identity;

            if (!ususario.HasClaim(Modulo.Leilao.ToString(),
                Acao.Detalhar.ToString()))
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            Produto produto = _db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(Mapeamento.Mapear(produto));
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}