﻿using Loja.Mvc.Helpers;
using Loja.Repositorios.SqlServer.EF;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Loja.Mvc.Areas.Vendas.Controllers.Api
{
    public class LeiloesController : ApiController
    {
        private LojaDbContext _db = new LojaDbContext();

        public IHttpActionResult Get()
        {
            return Ok(Mapeamento.Mapear(_db.Produtos.Where(p => p.EmLeilao).ToList()));
        }

        public IHttpActionResult Post(FormDataCollection form)
        {
            //Todo: var clase leilao e tabela


            return CreatedAtRoute("VendasDefaultApi",
                new {id = form["lote"] }, form);
        }
    }
}
