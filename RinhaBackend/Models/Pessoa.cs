using System.Security;

namespace RinhaBackend.Models
{
    public class Pessoa
    {
        public string? id {  get; set; }
        public string apelido {  get; set; }
        public string nome { get; set; }
        public string nascimento { get; set; }

        public List<string>? stack {  get; set; }
    }
}
