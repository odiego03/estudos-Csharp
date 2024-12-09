using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi8.Data;
using WebApi8.Dto.Livro;
using WebApi8.Models;

namespace WebApi8.Services.Livros
{
    public class LivroService : ILivroInterface
    {
        // Injetor de dependência para o contexto do banco de dados
        private readonly AppDbContext _context;

        // Construtor que recebe o contexto do banco
        public LivroService(AppDbContext context)
        {
            _context = context;
        }

        // Método para buscar um livro pelo ID
        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro)
        {
            // Instancia um modelo de resposta
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                // Busca o livro no banco de dados pelo ID informado
                var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(livrobanco => livrobanco.Id == idLivro);

                // Verifica se o livro foi encontrado
                if (livro == null)
                {
                    resposta.Mensagem = "Nao existe esse livro"; // Mensagem caso o livro não exista
                    return resposta;
                }

                // Define o livro encontrado e a mensagem de sucesso na resposta
                resposta.Dados = livro;
                resposta.Mensagem = "O livro foi encontrado";
                return resposta;
            }
            catch (Exception ex)
            {
                // Captura erros e define a mensagem de erro na resposta
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        // Método para buscar livros pelo ID do autor
        public async Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            // Instancia um modelo de resposta
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                // Busca livros no banco relacionados a um autor específico
                // Inclui as informações do autor para cada livro
                var livros = await _context.Livros
                    .Include(a => a.Autor)
                    .Where(livroBanco => livroBanco.Autor.Id == idAutor)
                    .ToListAsync(); // Executa a consulta e converte o resultado para uma lista

                // Verifica se nenhum livro foi encontrado
                if (livros == null)
                {
                    resposta.Mensagem = "Nao foi encontrado esse livro"; // Mensagem caso a lista esteja vazia
                    return resposta;
                }

                // Define a lista de livros encontrados e a mensagem de sucesso
                resposta.Dados = livros;
                resposta.Mensagem = "O livro foi encontrado";
                return resposta;
            }
            catch (Exception ex)
            {
                // Captura erros e define a mensagem de erro na resposta
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto)
        {
            ResponseModel < List < LivroModel >> resposta = new ResponseModel<List<LivroModel>>();

            try 
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroCriacaoDto.Autor.Id);
                    if (autor == null)
                    {
                    resposta.Mensagem = "nenuhum registro ncotrado";
                    return resposta;
                }
                var livro = new LivroModel()
                { 
                    Titulo = livroCriacaoDto.Titulo ,
                    Autor = autor
                };

                _context.Add( livro );
                await _context.SaveChangesAsync();

                resposta.Dados = await  _context.Livros.Include(a=> a.Autor).ToListAsync();
                resposta.Mensagem = "os livros foram encontrados";
                return resposta;

            }
            catch (Exception ex) 
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try {
                var livro = await _context.Livros
                     .Include(a => a.Autor)
                     .FirstOrDefaultAsync(livroBanco => livroBanco.Id == livroEdicaoDto.Id);

                var autor = await _context.Autores
                     .FirstOrDefaultAsync(autorBanco => autorBanco.Id == livroEdicaoDto.Autor.Id);



                if (autor == null)
                {
                    resposta.Mensagem = "Nenhum registro de autor localizado!";
                    return resposta;
                }

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum registro de livro localizado!";
                    return resposta;
                }

                livro.Titulo = livroEdicaoDto.Titulo;
                livro.Autor = autor;


                _context.Update(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.ToListAsync();
                return resposta;

            }
            catch (Exception ex) 
            {
                resposta.Mensagem= ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try 
            {
                var livro = await _context.Livros.Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroBanco => livroBanco.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Nenhum livro localizado!";
                    return resposta;
                }

                _context.Remove(livro);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Livros.ToListAsync();
                resposta.Mensagem = "Livro Removido com sucesso!";

                return resposta;

            } catch (Exception ex) 
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivro()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();

            try {
                var livros = await _context.Livros.Include(a=> a.Autor).ToListAsync();

                resposta.Dados = livros;
                resposta.Mensagem = "Todos os livros foram listados";
                return resposta;
            } catch (Exception ex) {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        
    }
}
