
using WebApi8.Dto.Livro;
using WebApi8.Models;

namespace WebApi8.Services.Livros
{
    public interface ILivroInterface
    {

        Task<ResponseModel<List<LivroModel>>> ListarLivro();
        Task<ResponseModel<LivroModel>> BuscarLivroPorId(int idLivro);
        Task<ResponseModel<List<LivroModel>>> BuscarLivroPorIdAutor(int idAutor);
        Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroCriacaoDto);

        Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int idLivro);

        Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroEdicaoDto);
    }
}
