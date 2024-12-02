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

    }
}
