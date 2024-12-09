using System.Text.Json.Serialization;

namespace WebApi8.Models
{
    public class AutorModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }


        [JsonIgnore] // vai ignorar essa parte abaixo na hora de cadastrar o autor nao vai pedir pra cadastras o livro
        public ICollection<LivroModel> Livros { get; set; }

    }
}
