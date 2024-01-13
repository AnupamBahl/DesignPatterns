/// <summary>
/// Decorator pattern allows' you to attach additional responsiblity to objects dynamically. It is a 
/// flexible alternative to subclassing(inheritence) for extending functionality. 
/// 
/// When inheritence leads to a large number of subclasses (coffe shop example, each customized order 
/// is a sublcass), decorator pattern is helpful
/// 
/// Downsides \
///     - if code relies on type checking it will fail, the main type hides inside decorators
///     - Difficult to understand. Add's a large number of small sized classes to design.
///     - If you need to peak at various levels in the decorator chain, this pattern is not suitable (eg - 
///     wanting to check which base was used for the coffee)
/// 
/// </summary>
/// 


# region Main Objects and their interfaces
public abstract class Coffee{
    private double cost;
    private string description;

    public double Cost { get => cost; private protected set => cost = value; }
    public string Description { get => description ; private protected set => description = value; }

    public abstract double getCost();
    public abstract string getDescription();
}

public class DarkRoast : Coffee{

    public DarkRoast(double cost = 2.5, string desc = "Dark Roast"){
        this.Cost = cost;
        this.Description = desc;
    }

    public override double getCost(){
        return this.Cost;
    }

    public override string getDescription(){
        return this.Description;
    }
}

#endregion



#region Decorators

// The decorator interface inherits from the main object type. That allows it to store the main object
// and other decorators themselves, because all are the same super type.
public abstract class CoffeeDecorator : Coffee{
    protected private Coffee coffeeObject;
}

public class SoyMilk : CoffeeDecorator{
    public SoyMilk(Coffee obj, double cost = 0.5, string desc = "Soy Milk")
    {
        this.Cost = cost;
        this.Description = desc;
        this.coffeeObject = obj;
    }

    public override double getCost(){
        return this.Cost + this.coffeeObject.getCost();
    }

    public override string getDescription()
    {
        return $"{this.coffeeObject.getDescription()}, {this.Description}";
    }
}

public class WhipCream : CoffeeDecorator{
    public WhipCream(Coffee obj, double cost = 0.8, string desc = "Whip Cream")
    {
        this.Cost = cost;
        this.Description = desc;
        this.coffeeObject = obj;
    }

    public override double getCost(){
        return this.Cost + this.coffeeObject.getCost();
    }

    public override string getDescription()
    {
        return $"{this.coffeeObject.getDescription()}, {this.Description}";
    }
}

#endregion

