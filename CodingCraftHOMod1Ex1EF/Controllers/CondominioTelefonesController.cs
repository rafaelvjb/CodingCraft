using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using CodingCraftHOMod1Ex1EF.Models;

namespace CodingCraftHOMod1Ex1EF.Controllers
{
    public class CondominioTelefonesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: CondominioTelefones
        public async Task<ActionResult> Index()
        {
            var condominioTelefones = db.CondominioTelefones.Include(c => c.Condominio);
            return View(await condominioTelefones.ToListAsync());
        }

        // GET: CondominioTelefones/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var condominioTelefone = await db.CondominioTelefones.FindAsync(id);
            if (condominioTelefone == null) return HttpNotFound();
            return View(condominioTelefone);
        }

        // GET: CondominioTelefones/Create
        public ActionResult Create()
        {
            ViewBag.CondominioId = new SelectList(db.Condominios, "CondominioId", "Nome");
            return View();
        }

        // POST: CondominioTelefones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include =
                "CondominioTelefoneId,CondominioId,Ddd,Numero,Referencia,UltimaModificacao,UsuarioModificacao,DataCriacao,UsuarioCriacao")]
            CondominioTelefone condominioTelefone)
        {
            if (ModelState.IsValid)
            {
                condominioTelefone.CondominioTelefoneId = Guid.NewGuid();
                db.CondominioTelefones.Add(condominioTelefone);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CondominioId =
                new SelectList(db.Condominios, "CondominioId", "Nome", condominioTelefone.CondominioId);
            return View(condominioTelefone);
        }

        // GET: CondominioTelefones/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var condominioTelefone = await db.CondominioTelefones.FindAsync(id);
            if (condominioTelefone == null) return HttpNotFound();
            ViewBag.CondominioId =
                new SelectList(db.Condominios, "CondominioId", "Nome", condominioTelefone.CondominioId);
            return View(condominioTelefone);
        }

        // POST: CondominioTelefones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include =
                "CondominioTelefoneId,CondominioId,Ddd,Numero,Referencia,UltimaModificacao,UsuarioModificacao,DataCriacao,UsuarioCriacao")]
            CondominioTelefone condominioTelefone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(condominioTelefone).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CondominioId =
                new SelectList(db.Condominios, "CondominioId", "Nome", condominioTelefone.CondominioId);
            return View(condominioTelefone);
        }

        // GET: CondominioTelefones/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var condominioTelefone = await db.CondominioTelefones.FindAsync(id);
            if (condominioTelefone == null) return HttpNotFound();
            return View(condominioTelefone);
        }

        // POST: CondominioTelefones/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var condominioTelefone = await db.CondominioTelefones.FindAsync(id);
            db.CondominioTelefones.Remove(condominioTelefone);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}