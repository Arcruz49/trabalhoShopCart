﻿namespace CarrinhoDeCompra.Models.Resource
{
    public class ResourceCarrinho
    {
        public string NmProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal? Preco {  get; set; }
        public string Caminho { get; set; }
    }
}
