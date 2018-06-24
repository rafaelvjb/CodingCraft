using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using CodingCraftHOMod1Ex1EF.Models;
using WebGrease.Css.Extensions;

namespace CodingCraftHOMod1Ex1EF.Controllers
{
    public class CondominiosController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Condominios
        public async Task<ActionResult> Index(Condominio filtrosCondominio)
        {
            var condominios = db.Condominios.Include(c => c.Cidade)
                                            .Include(c => c.CondominioTelefones);
            //                                .Where(c => c.Cidade.SiglaEstado.StartsWith("P"));

            //if (1 == 1)
            //{
            //    condominios = condominios.Where(c => !c.Nome.Contains("Condomínio"));
            //}
            if (!string.IsNullOrEmpty(filtrosCondominio.Nome))
                condominios = condominios.Where(condominio => condominio.Nome.Contains(filtrosCondominio.Nome));

            if (!string.IsNullOrEmpty(filtrosCondominio.RazaoSocial))
                condominios = condominios.Where(condominio =>
                    condominio.RazaoSocial.Contains(filtrosCondominio.RazaoSocial));

            if (!string.IsNullOrEmpty(filtrosCondominio.Cnpj))
                condominios = condominios.Where(condominio => condominio.Cnpj.Contains(filtrosCondominio.Cnpj));

            if (filtrosCondominio.CidadeId != default(Guid))
                condominios = condominios.Where(condominio => condominio.CidadeId == filtrosCondominio.CidadeId);

            var model = await condominios.AsNoTracking()
                //.Select(condominio => new
                //   {
                //       condominio.Cidade,
                //       condominio.Cnpj,
                //       condominio.RazaoSocial,
                //       condominio.CondominioId,
                //       condominio.Nome,
                //       condominio.Descricao
                //   } )
                .OrderBy(condominio => condominio.Cidade.Nome)
                .ThenBy(condominio => condominio.Nome)
                .ToListAsync();


            ViewBag.CidadeId = new SelectList(db.Cidades.Select(cidade => new {cidade.CidadeId, cidade.Nome}),
                "CidadeId", "Nome");
            return View(model);
        }

        // GET: Condominios/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var condominio = await db.Condominios.Include(c => c.Cidade).Include(c => c.CondominioTelefones).SingleAsync(c => c.CondominioId == id);

            return condominio == null ? HttpNotFound() : (ActionResult) View(condominio);
        }

        // GET: Condominios/Create
        public ActionResult Create()
        {
            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome");

            return View(new Condominio {CondominioTelefones = new List<CondominioTelefone>()});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "CondominioId,CidadeId,Nome,Descricao,RazaoSocial,Cnpj,CondominioTelefones")]
            Condominio condominio)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Condominios.Add(condominio);

                    await db.SaveChangesAsync();
                    scope.Complete();
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome", condominio.CidadeId);
            return View(condominio);
        }

        // GET: Condominios/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var condominio = await db.Condominios.Include(c => c.Cidade).Include(c=> c.CondominioTelefones).SingleAsync(c => c.CondominioId == id);
            
            if (condominio == null) return HttpNotFound();
            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome", condominio.CidadeId);
            return View(condominio);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "CondominioId,CidadeId,Nome,Descricao,RazaoSocial,Cnpj,CondominioTelefones")]
            Condominio condominio)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Expression<Func<CondominioTelefone, bool>> telefonesDeletadosFiltro = tel => tel.CondominioId == condominio.CondominioId;
                    var telefonesDeletados = await db.CondominioTelefones.AsNoTracking()
                        .Where(telefonesDeletadosFiltro).ToListAsync();
                    telefonesDeletados = telefonesDeletados.Where(tel =>
                        condominio.CondominioTelefones.All(t => t.CondominioTelefoneId != tel.CondominioTelefoneId)).ToList();

                    foreach (var telefone in telefonesDeletados)
                    {
                        db.Entry(telefone).State = EntityState.Deleted;
                    }

                    condominio.CondominioTelefones.ForEach(t =>
                    {
                        if (string.IsNullOrEmpty(t.CondominioId.ToString()) || t.CondominioId == default(Guid))
                        {
                            t.CondominioId = condominio.CondominioId;
                            db.Entry(t).State = EntityState.Added;
                        }
                        else 
                        {
                            db.Entry(t).State = EntityState.Modified;
                        }
                    });
                    db.Entry(condominio).State = EntityState.Modified;
                    
                    await db.SaveChangesAsync();
                    scope.Complete();
                }

                return RedirectToAction("Index");
            }

            ViewBag.CidadeId = new SelectList(db.Cidades, "CidadeId", "Nome", condominio.CidadeId);
            return View(condominio);
        }

       

        // GET: Condominios/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var condominio = await db.Condominios.Include(c => c.Cidade).Include(c => c.CondominioTelefones).SingleAsync(c => c.CondominioId == id);
            if (condominio == null) return HttpNotFound();
            return View(condominio);
        }

        // POST: Condominios/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var condominio = await db.Condominios.FindAsync(id);

                db.Condominios.Remove(condominio);
                await db.SaveChangesAsync();
                scope.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult NovaLinhaTelefone()//Condominio condominio)
        {
            var condominioTelefone = new CondominioTelefone
            {
               // CondominioId = condominio.CondominioId
            };
            return PartialView("_LinhaTelefone", condominioTelefone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}