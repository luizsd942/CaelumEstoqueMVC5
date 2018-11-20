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
        //Gerando rota Padrão para a lista de Produtos
        [Route("produtos", Name = "ListaProdutos")]
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();
            //ViewBag cria atributos dinamicamente
            //ViewBag.Produtos = produtos;
            //Passa a utilizar View Fortemente Tipada
            return View(produtos);
        }

        public ActionResult Form()
        {
            ViewBag.Categorias = new CategoriasDAO().Lista();
            ViewBag.Produto = new Produto();
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

            var idCategoria = 1;
            if (produto.CategoriaId.Equals(idCategoria) && produto.Preco < 100)
            {
                ModelState.AddModelError("Produto.ValidaInformatica","Produto de Informática abaixo de R$100");
            }

            if (ModelState.IsValid) {
                new ProdutosDAO().Adiciona(produto);
                return RedirectToAction("Index", "Produto");
            }
            else
            {
                // produto inválido então guarda na ViewBag para manter os dados digitados
                ViewBag.Produto = produto;                
                ViewBag.Categorias = new CategoriasDAO().Lista();
                return View("Form");
            }
        }

        //Gerando rota Padrão para detalhes do produto
        [Route("produtos/{id}",Name = "DetalhesProduto")]
        public ActionResult VisualizaProduto(int id)
        {
            ViewBag.Produto = new ProdutosDAO().BuscaPorId(id);
            return View();
        }
    }
}