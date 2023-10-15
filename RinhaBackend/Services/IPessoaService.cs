using MongoDB.Bson;
using RinhaBackend.Models;

namespace RinhaBackend.Services
{
    public interface IPessoaService
    {
        Task CreatePessoaAsync(Pessoa bson);
        Task<List<Pessoa>> GetByFilder(string filter);
        Task<Pessoa> GetByIdAsync(string id);
        Task<long> GetCountAsync();
    }
}