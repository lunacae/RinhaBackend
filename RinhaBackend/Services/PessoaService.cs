using MongoDB.Bson;
using MongoDB.Driver;
using RinhaBackend.Models;
using RinhaBackend.Repository;
using System;
using System.Security.Cryptography;

namespace RinhaBackend.Services
{
    public class PessoaService : IPessoaService
    {
        private IMongoRepository mongoRepository;

        public PessoaService(IMongoRepository repository)
        {
            this.mongoRepository = repository;
        }
        public async Task<Pessoa> GetByIdAsync(string id)
        {
            ObjectId idObject = new ObjectId(id);
            var pessoa = await mongoRepository.GetByIdAsync(idObject);
            Pessoa p = MapBsonToPessoa(pessoa);
            return p;
        }

        public async Task CreatePessoaAsync(Pessoa p)
        {
            await ValidateBody(p);
            var bson = MaPessoaToBson(p);
            await mongoRepository.InsertDocumentAsync(bson);
        }

        private async Task ValidateBody(Pessoa p)
        {
            if (p == null)
                throw new Exception("Não foi possível ler os campos do body, verifique os tipos e tente novamente");

            if(string.IsNullOrEmpty(p.nome) || string.IsNullOrEmpty(p.apelido))
            {
                throw new ArgumentNullException("Nome ou apelido inválidos!");
            }

            FilterDefinition<BsonPessoa> filter = Builders<BsonPessoa>.Filter.Where(x => x.nome.ToLower() == p.nome.ToLower());

            var alreadyExists = await mongoRepository.GetByFilterAsync(filter);
            if(alreadyExists != null && alreadyExists.Count > 0)
            {
                throw new ArgumentNullException("Nome já existe na base de dados!");
            }
        }

        public async Task<List<Pessoa>> GetByFilder(string filter)
        {
            filter = filter.ToLower();
            FilterDefinition<BsonPessoa> nomeFilter = Builders<BsonPessoa>.Filter.Where(x => x.nome.ToLower() == filter);
            var pNome = await mongoRepository.GetByFilterAsync(nomeFilter);
            if (pNome.Count > 0)
            {
                return MapBsonToPessoa(pNome);
            }
                

            FilterDefinition<BsonPessoa> apelidoFilter = Builders<BsonPessoa>.Filter.Where(x => x.apelido.ToLower() == filter);
            var pApelido = await mongoRepository.GetByFilterAsync(nomeFilter);
            if (pApelido.Count > 0)
            {
                return MapBsonToPessoa(pApelido);
            }
                

            //FilterDefinition<BsonPessoa> stackFilter = Builders<BsonPessoa>.Filter.Where(x => x.stack.Where(a => a.ToLower() == filter).Contains(filter));
            FilterDefinition<BsonPessoa> stackFilter = Builders<BsonPessoa>.Filter.Where(x => x.stack.Contains(filter));
            var pStack = await mongoRepository.GetByFilterAsync(stackFilter);
            if (pStack.Count > 0)
            {
                return MapBsonToPessoa(pStack);
            }
                

            return new List<Pessoa>();
        }

        public async Task<long> GetCountAsync()
        {
            return await mongoRepository.GetCountAsync();
        }

        private Pessoa MapBsonToPessoa(BsonPessoa bson)
        {
            Pessoa p = new()
            {
                apelido = bson.apelido,
                nascimento = bson.nascimento,
                nome = bson.nome,
                stack = bson.stack,
                id = bson.Id.ToString()
            };

            return p;
        }

        private List<Pessoa> MapBsonToPessoa(List<BsonPessoa> bsons)
        {
            List<Pessoa> listToReturn = new();
            foreach (BsonPessoa bson in bsons)
            {
                Pessoa p = new()
                {
                    apelido = bson.apelido,
                    nascimento = bson.nascimento,
                    nome = bson.nome,
                    stack = bson.stack,
                    id = bson.Id.ToString()
                };
                listToReturn.Add(p);
            }
            

            return listToReturn;
        }

        private BsonPessoa MaPessoaToBson(Pessoa p)
        {
            if(string.IsNullOrEmpty(p.id))
            {
                return new BsonPessoa()
                {
                    apelido = p.apelido,
                    nascimento = p.nascimento,
                    nome = p.nome,
                    stack = p.stack == null ? p.stack : p.stack.ConvertAll(a => a.ToLower())
                };
            }

            BsonPessoa bson = new()
            {
                apelido = p.apelido,
                nascimento = p.nascimento,
                nome = p.nome,
                stack = p.stack,
                Id = new ObjectId(p.id)
            };

            return bson;
        }
    }
}
