using System.Runtime.CompilerServices;

/// <summary>
/// Defines an interface for creating an object, but lets subclasses decide which class
/// to instantiate. Factory method lets a class defer instantiation to subclasses.
/// 
///  - You can have multiple creator subclasses that create different types of the same object
///  - Even with jsut one creator subclass it's useful - you are decoupling imnplementation of
///  the product from it's use. If you add additional products/change it's implementation, doens't
///  affect creator code
///  - Parameterized factory - parameters can be objects, enums, or static constants instead of
///  string to ensure type safety
/// </summary>
/// 

#region Product

// Abstact product class
public abstract class Pizza {
    private String name;
    private String dough;
    private String sauce;
    private List<String> toppings = new List<String>();

    public string Name {  get => name; protected set => name = value; }
    public string Dough { get => dough; protected set => dough = value; }
    public string Sauce { get => sauce; protected set => sauce = value; }
    public List<string> Toppings { get => toppings; set => toppings = value; }

    public void Prepare(){
        Console.WriteLine($"Preparing \t\t\t: {Name}");
        Console.WriteLine($"Tossing dough \t\t\t: {Dough} ...");
        Console.WriteLine($"Adding sauce \t\t\t: {Sauce} ...");
        Console.WriteLine($"Adding toppings \t\t: {string.Join(",", Toppings)}");
    }

    public void Bake(){
        Console.WriteLine("Bake \t\t\t: For 25 minutes at 350");
    }

    public virtual void Cut(){
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Cutting \t\t: Diagonal slices");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void Box(){
        Console.WriteLine("Box \t\t\t: Officail PizzaStore box");
    }
}

// Concrete Class Type 1
public class NYStyleCheesePizza : Pizza {
    public NYStyleCheesePizza(){
        Name = "NY Style Cheese Pizza";
        Dough = "Thin Crust Dough";
        Sauce = "Marinara Sauce";

        Toppings.Add("Grated Reggiano Cheese");
    }
}

// Concrete Class Type 1
public class NYStylePeperroniPizza : Pizza {
    public NYStylePeperroniPizza() {
        Name = "NY Style Peperroni Pizza";
        Dough = "Thin Crust Dough";
        Sauce = "Marinara Sauce";

        Toppings.Add("Grated Reggiano Cheese");
        Toppings.Add("Peperroni");
    }

}

// Concrete Class Type 2
public class ChicagoStyleCheesePizza : Pizza {
    public ChicagoStyleCheesePizza() {
        Name = "Chicago Style Deep Dish Cheese Pizza";
        Dough = "Extra Thick Crust Dough";
        Sauce = "Plum Tomato Sauce";

        Toppings.Add("Shredded Mozarella Cheese");
    }

    public override void Cut(){
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Cutting \t\t: Square slices");
        Console.ForegroundColor = ConsoleColor.White;
    }
}

// Concrete Class Type 2
public class ChicagoStylePeperroniPizza : Pizza {
    public ChicagoStylePeperroniPizza() {
        Name = "Chicago Style Deep Dish Peperroni Pizza";
        Dough = "Extra Thick Crust Dough";
        Sauce = "Plum Tomato Sauce";

        Toppings.Add("Shredded Mozarella Cheese");
        Toppings.Add("Peperroni");
    }

    public override void Cut(){
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Cutting \t\t: Square slices");
        Console.ForegroundColor = ConsoleColor.White;
    }
}

#endregion


#region Creator Classes

public abstract class PizzaStore {
    private Pizza pizza;

    public Pizza Pizza { get => pizza; private set => pizza = value; }

    public void OrderPizza(String type) {
        this.Pizza = CreatePizza(type);

        this.Pizza.Prepare();
        this.Pizza.Bake();
        this.Pizza.Cut();
        this.Pizza.Box();
    }

    public abstract Pizza CreatePizza(string type);
}

public class NYPizzaStore : PizzaStore {
    public override Pizza CreatePizza(string type)
    {
        if (type == "Cheese"){
            return new NYStyleCheesePizza();
        }
        else if(type == "Peperroni"){
            return new NYStylePeperroniPizza();
        }
        else{
            throw new ArgumentOutOfRangeException();
        }
    }
}

public class ChicagoPizzaStore : PizzaStore {
    public override Pizza CreatePizza(string type)
    {
        if (type == "Cheese"){
            return new ChicagoStyleCheesePizza();
        }
        else if(type == "Peperroni"){
            return new ChicagoStylePeperroniPizza();
        }
        else{
            throw new ArgumentOutOfRangeException();
        }
    }
}

#endregion