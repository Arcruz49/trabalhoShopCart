using CarrinhoDeCompra.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarrinhoDeCompra.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var produtos = _context.CadProdutos.ToList();
            return View(produtos);
        }

        [HttpPost]
        public JsonResult AdicionaProdutoCarrinho(int cdProduto = 0)
        {
            try
            {
                if (cdProduto == 0) return Json(new {success = false, message = "Código inválido" });

                CadCarrinhoProduto carrinhoProduto = new CadCarrinhoProduto()
                {
                    CdProduto = cdProduto
                };

                _context.CadCarrinhoProdutos.Add(carrinhoProduto);
                _context.SaveChanges();
                return Json(new { success = true, message = "adicionado com sucesso" });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = "erro: " + ex.Message });
            }

        }
    }
}
