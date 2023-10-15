using MongoDB.Bson;
using MongoDB.Driver;
using RinhaBackend.Models;
using System;

namespace RinhaBackend.Repository
{
    public class MongoRepository : IMongoRepository
    {
        private IMongoCollection<BsonPessoa> pessoaCollection;
        private IMongoClient client;

        public MongoRepository(IMongoClient client)
        {
            this.client = client;
            this.pessoaCollection = client.GetDatabase("Rinha").GetCollection<BsonPessoa>("Pessoas");
        }


        public async Task<BsonPessoa> GetByIdAsync(ObjectId id)
        {
            var documents = await pessoaCollection.FindAsync(a => a.Id.Equals(id));
            return await documents.FirstOrDefaultAsync();
        }

        public async Task InsertDocumentAsync(BsonPessoa bson)
        {
            await pessoaCollection.InsertOneAsync(bson);
        }

        public async Task DeleteByIdAsync(ObjectId id)
        {
            await pessoaCollection.DeleteOneAsync<BsonPessoa>(_ => _.Id == id);
        }

        public async Task<List<BsonPessoa>> GetByFilterAsync(FilterDefinition<BsonPessoa> filter)
        {
            var options = new FindOptions<BsonPessoa>
            {
                Skip = 0,
                Limit = 50
            };
            var documents = await pessoaCollection.FindAsync(filter, options);
            return await documents.ToListAsync();
        }

        public async Task<BsonPessoa> FindAndReplaceOneAsync(FilterDefinition<BsonPessoa> filter, BsonPessoa bson)
        {
            return await pessoaCollection.FindOneAndReplaceAsync(filter, bson);
        }

        public async Task<long> GetCountAsync()
        {
            return await pessoaCollection.CountDocumentsAsync(_ => true);
        }
    }
}
