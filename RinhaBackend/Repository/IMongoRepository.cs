using MongoDB.Bson;
using MongoDB.Driver;
using RinhaBackend.Models;

namespace RinhaBackend.Repository
{
    public interface IMongoRepository
    {
        Task DeleteByIdAsync(ObjectId id);
        Task<BsonPessoa> FindAndReplaceOneAsync(FilterDefinition<BsonPessoa> filter, BsonPessoa bson);
        Task<List<BsonPessoa>> GetByFilterAsync(FilterDefinition<BsonPessoa> filter);
        Task<BsonPessoa> GetByIdAsync(ObjectId id);
        Task<long> GetCountAsync();
        Task InsertDocumentAsync(BsonPessoa bson);
    }
}