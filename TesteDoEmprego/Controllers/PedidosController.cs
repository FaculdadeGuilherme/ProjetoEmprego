using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TesteDoEmprego.Data;
using TesteDoEmprego.Models;

namespace TesteDoEmprego.Controllers
{
    public class PedidosController : Controller
    {
        private readonly LojaContext _context;
        private ItemPedido itemPedido;

        public int PedidoId { get; private set; }

        public PedidosController(LojaContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var lojaContext = _context.Pedidos.Include(p => p.Usuario);
            return View(await lojaContext.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nome");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido, int[] produtos)
        {
            var produtosComprados = new List<Produto>();
            foreach (var produtoId in produtos)
            {
                var prod = _context.Produtos.Find(produtoId);
                produtosComprados.Add(prod);
            }


            var pedidoNovo = new Pedido
            {
                UsuarioId = pedido.UsuarioId,
                Data = DateTime.Now
        };
            _context.Pedidos.Add(pedidoNovo);
            _context.SaveChanges();

            foreach(var produto in produtosComprados)
            {
                var itemPedido = new ItemPedido();
            }
            _context.ItemPedidos.Add(itemPedido); 
            _context.SaveChanges();

            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.SingleOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,UsuarioId")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Usuario)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.SingleOrDefaultAsync(m => m.Id == id);
            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
