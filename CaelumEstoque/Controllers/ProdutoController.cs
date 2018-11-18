using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();
            //ViewBag cria atributos dinamicamente
            ViewBag.Produtos = produtos;
            return View();
        }

        public ActionResult Form()
        {
            CategoriasDAO dao = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = dao.Lista();
            ViewBag.Categorias = categorias;
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Produto produto)
        {

            //Produto produto = new Produto()
            //{
            //    Nome = nome,
            //    Preco = preco,
            //    Descricao = descricao,
            //    Quantidade = quantidade,
            //    CategoriaId = categoria
            //};

            //A variável produto recebida pelo método da classe controladora é instanciada e 
            //preenchida por um componente do ASP.NET MVC conhecido como Model Binder.

            new ProdutosDAO().Adiciona(produto);

            return RedirectToAction("Index", "Produto");
        }
    }
}