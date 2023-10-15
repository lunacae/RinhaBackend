using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RinhaBackend.Models
{
    public class BsonPessoa
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get ; set; }
        public string apelido { get; set; }
        public string nome { get; set; }
        public string nascimento { get; set; }

        public List<string>? stack { get; set; }
    }
}
