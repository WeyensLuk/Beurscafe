using LiteDB;

namespace webapi.Models;

public class Drink
{
    public Drink()
    {
        Id = ObjectId.NewObjectId();
    }

    public int AmountPurchased { get; set; }
    public decimal CurrentPrice { get; set; }
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public decimal OriginalPrice { get; set; }
}