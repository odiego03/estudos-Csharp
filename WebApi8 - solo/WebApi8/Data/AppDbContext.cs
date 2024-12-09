using Microsoft.EntityFrameworkCore;
using WebApi8.Models;

namespace WebApi8.Data
{
    public class AppDbContext : DbContext
    {
        // Configura a conexão ao banco com as opções fornecidas.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        //aqui eu crio a tabela de autores com as porpriedades do AutorModel
        public DbSet<AutorModel> Autores { get; set; }
        //faço a msm coisa aqui com livros
        public DbSet<LivroModel> Livros { get; set; }



    }
}
