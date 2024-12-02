using CarrinhoDeCompra.Models;
using CarrinhoDeCompra.Models.Resource;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarrinhoDeCompra.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly ApplicationDbContext db;

        public CarrinhoController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var carrinho = (from a in db.CadCarrinhoProdutos
                            join p in db.CadProdutos on a.CdProduto equals p.CdProduto
                            group new { a, p } by a.CdProduto into grouped
                            select new ResourceCarrinho
                            {
                                CdProduto = grouped.FirstOrDefault().p.CdProduto,
                                NmProduto = grouped.FirstOrDefault().p.NmProduto, 
                                Quantidade = grouped.Count(),                    
                                Preco = grouped.FirstOrDefault().p.Preco,
                                Caminho = grouped.FirstOrDefault().p.Caminho
                            }).ToList();

            ViewBag.QtdTotalProdutos = carrinho.Count;
            ViewBag.PrecoTotal = carrinho.Sum(item => item.Preco * item.Quantidade);
            ViewBag.PrecoTotalFinal = carrinho.Sum(item => item.Preco * item.Quantidade) + 20;
            return View(carrinho);
        }

        public JsonResult RemoverProduto(int CdProduto = 0)
        {
            try
            {
                if (CdProduto == 0) return Json(new { success = false, message = "Código inválido" });

                var carrinhoProduto = (from a in db.CadCarrinhoProdutos
                                       where a.CdProduto == CdProduto
                                       select a).FirstOrDefault();

                db.CadCarrinhoProdutos.Remove(carrinhoProduto);
                db.SaveChanges();
                return Json(new { success = true, message = "Removido com sucesso" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "erro: " + ex.Message });
            }
        }

    }
}
