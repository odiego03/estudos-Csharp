using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WebApi8.Data;
using WebApi8.Dto.Autor;
using WebApi8.Models;

namespace WebApi8.Services.Autor
{
    public class AutorService : IAutorInterface
    {

        private readonly AppDbContext _context;
        public AutorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int idAutor)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();//variavel de respota
            try 
            {  var autor = await _context.Autores.FirstOrDefaultAsync(autorbanco => autorbanco.Id == idAutor);

                if (autor == null) 
                {
                    resposta.Mensagem = "Nao exite esse autor";
                    return resposta;
                }

                resposta.Dados = autor;
                resposta.Mensagem = "o Autor foi encontrado";
                return resposta;
                
            }
            catch(Exception ex) 
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;

            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try 
            {
                var livros = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livrobanco => livrobanco.Id == idLivro);
                if(livros == null)
                {
                    resposta.Mensagem = "Nao foi encontrado esse livro";
                    return resposta;
                }

                resposta.Dados = livros.Autor;
                resposta.Mensagem = "O livro foi encontrado";
                return resposta;
            }
            catch(Exception ex) 
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorCriacaoDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();


            try 
            {
                var autor = new AutorModel()
                {
                   Nome = autorCriacaoDto.Nome,
                   Sobrenome = autorCriacaoDto.Sobrenome
                };
                _context.Add(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "O Autor foi cadastrado";
                return resposta;
            }
            catch(Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorEdicaoDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(autorbanco => autorbanco.Id == autorEdicaoDto.Id);

                if (autor == null)
                {
                    resposta.Mensagem = "Nao exite esse autor";
                    return resposta;
                }

                autor.Nome = autorEdicaoDto.Nome;
                autor.Sobrenome = autorEdicaoDto.Sobrenome;

                _context.Update(autor);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();

                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int idAutor)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();

            try
            { 
                //aqui vai percorrer a tabela do dtb e achar o id procurado
                var autorExcluido = await _context.Autores.FirstOrDefaultAsync(autorBanco => autorBanco.Id == idAutor);
                //verifica se exite esse id
                if( autorExcluido == null)
                {
                    resposta.Mensagem = "Nao exite esse autor";
                    return resposta;
                }

                // excluira do dtb o autorExcluido
                _context.Remove(autorExcluido);

                //sava as informaçoes no banco
                await _context.SaveChangesAsync();
                //lista os autores com a exclusao ja feita
                resposta.Dados = await _context.Autores.ToListAsync();
                return resposta;





            }
            catch (Exception ex)
            {

                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                var autores = await _context.Autores.ToListAsync();

                resposta.Dados = autores;
                resposta.Mensagem = "Todos autores foram coletados";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}