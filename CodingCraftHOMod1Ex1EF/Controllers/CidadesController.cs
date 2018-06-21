using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using CodingCraftHOMod1Ex1EF.Models;

namespace CodingCraftHOMod1Ex1EF.Controllers
{
    public class CidadesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cidades
        public async Task<ActionResult> Index()
        {
            return View(await db.Cidades.ToListAsync());
        }

        // GET: Cidades/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cidade = await db.Cidades.FindAsync(id);
            if (cidade == null) return HttpNotFound();
            return View(cidade);
        }

        // GET: Cidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CidadeId,Nome,SiglaEstado")]
            Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    cidade.CidadeId = Guid.NewGuid();
                    db.Cidades.Add(cidade);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            return View(cidade);
        }

        // GET: Cidades/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cidade = await db.Cidades.FindAsync(id);
            if (cidade == null) return HttpNotFound();
            return View(cidade);
        }

        // POST: Cidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CidadeId,Nome,SiglaEstado")]
            Cidade cidade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cidade).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cidade);
        }

        // GET: Cidades/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cidade = await db.Cidades.FindAsync(id);
            if (cidade == null) return HttpNotFound();
            return View(cidade);
        }

        // POST: Cidades/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var cidade = await db.Cidades.FindAsync(id);
            db.Cidades.Remove(cidade);
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