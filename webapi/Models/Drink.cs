using LiteDB;

namespace webapi.Models;

public class Drink
{
    private decimal _CurrentPrice;

    public Drink()
    {
        Id = ObjectId.NewObjectId();
    }

    public int AmountPurchased { get; set; }

    public decimal CurrentPrice
    {
        get => _CurrentPrice;
        set => _CurrentPrice = value < MinimumPrice ? MinimumPrice : value;
    }

    public ObjectId Id { get; set; }

    public decimal MinimumPrice { get; set; }

    public string Name { get; set; }

    public decimal OriginalPrice { get; set; }

    public static bool operator !=(Drink a, Drink b)
    {
        return !a.Equals(b);
    }

    public static bool operator ==(Drink a, Drink b)
    {
        return a.Equals(b);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Drink)obj);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public void ResetPrice()
    {
        CurrentPrice = OriginalPrice;
    }

    protected bool Equals(Drink other)
    {
        return Name == other.Name;
    }
}