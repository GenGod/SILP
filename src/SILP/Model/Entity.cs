using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SILP.Model
{
    public class Entity
    {
        [BsonId(IdGenerator = typeof(MongoDB.Bson.Serialization.IdGenerators.StringObjectIdGenerator))]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
    }
}
