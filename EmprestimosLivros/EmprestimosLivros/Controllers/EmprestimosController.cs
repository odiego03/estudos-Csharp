using EmprestimosLivros.Data;
using EmprestimosLivros.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosLivros.Controllers
{
    public class EmprestimosController : Controller
    {


        readonly private ApplicationDbContext _db;
        public EmprestimosController(ApplicationDbContext db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<EmprestimosModel> emprestimos = _db.Emprestimos;

            return View(emprestimos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            EmprestimosModel emprestimos = _db.Emprestimos.FirstOrDefault(x => x.Id == id);


            if (emprestimos == null) 
            {
                return NotFound();
            }

            return View(emprestimos);
        }


        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimosModel emprestimos = _db.Emprestimos.FirstOrDefault(x => x.Id == id);
            if (emprestimos == null)
            {
                return NotFound();
            }

            return View(emprestimos);
        }




        [HttpPost]
        public IActionResult Cadastrar(EmprestimosModel emprestimos)
        {
            
            if (ModelState.IsValid)
            {
                _db.Emprestimos.Add(emprestimos);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro feito com sucesso!";

                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Editar(EmprestimosModel emprestimos)
        {
            if (ModelState.IsValid)
            {
                _db.Emprestimos.Update(emprestimos);
                _db.SaveChanges();


                TempData["MensagemSucesso"] = "Edição feita com sucesso!";

                return RedirectToAction("Index");

            }

            TempData["MensagemErro"] = "Algum erro ocorreu ao realizar a edição!";

            return View(emprestimos);
        }

        [HttpPost]
        public IActionResult Excluir(EmprestimosModel emprestimos)
        {
            if (emprestimos == null)
            {
                return NotFound();
            }

           _db.Emprestimos.Remove(emprestimos);
           
            _db.SaveChanges();

            TempData["MensagemSucesso"] = "Exclusão feita com sucesso!";

            return RedirectToAction("Index");
        }








    }
}
