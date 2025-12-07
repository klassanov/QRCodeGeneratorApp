using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QRCodeGeneratorApp.Persistence.Orders
{
    public class OrderDocument
    {
        public OrderDocument(string text, string customerName, string customerEmail)
        {
            Text = text;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CreatedAt = DateTime.Now;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        public string? Text { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerEmail { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
