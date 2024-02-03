using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Entities
{
    public class Todo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
     public string Id { get; set; }    
     public string Title { get; set; }    
     public bool IsCompleted { get; set; }    
    }
}