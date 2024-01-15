

// ABSTRACT FACTORY - returns abstract products
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Abstract Factory Pattern - provides an interface for creating families of related or dependent
/// objects without specifying their concrete classes
/// 
///  - Uses object composition instead of inheritence(like factory method does)
///  - Down side - if you need to add another product, the interface needs to change, along with 
///  all the implementations of that interface
/// </summary>
/// 
public interface PizzaIngredientFactory{
    public Dough createDough();
    public Sauce createSauce();
    public Cheese createCheese();
    public List<Veggies> createVeggies();
}

//ABSTRACT PRODUCTS
public interface Dough{
    public string GetName();
}

public interface Sauce{
    public string GetName();
}

public interface Cheese{
    public string GetName();
}

public interface Veggies{
    public string GetName();
}


//CONCRETE FACTORY - implements methods of the abstract factory
public class NYPizzaIngredientFactory{
    public Dough createDough(){
        return new ThinCrustDough();
    }

    public Sauce createSauce(){
        return new MarinaraSauce();
    }

    public Cheese createCheese(){
        return new ReggianoCheese();
    }

    public List<Veggies> createVeggies(){
        return new List<Veggies>{new Onion(), new Garlic()};
    }
}

//CONCRETE PRODUCTS - actual products that the concrete factory would return
public class ThinCrustDough : Dough{
    private string name;
    public ThinCrustDough(){
        this.name = "ThinCrustDough";
    }

    public string GetName()
    {
        return this.name;
    }
}

public class MarinaraSauce : Sauce{
    private string name;
    public MarinaraSauce(){
        name = "MarinaraSauce";
    }

    public string GetName()
    {
        return this.name;
    }
}

public class ReggianoCheese : Cheese{
    private string name;
    public ReggianoCheese(){
        name = "ReggianoCheese";
    }

    public string GetName()
    {
        return this.name;
    }
}

public class Garlic : Veggies{
    private string name;
    public Garlic(){
        name = "Garlic";
    }

    public string GetName()
    {
        return this.name;
    }
}

public class Onion : Veggies{
    private string name;
    public Onion(){
        name = "Onion";
    }

    public string GetName()
    {
        return this.name;
    }
}


// The different pizza's in the factory pattern will have abstract PizzaIngredientFactory 
// using object composition pattern. The pizza factories, will create correct abstract
// pizza ingredient factories and pass the ingredient factories to the pizza constructors
// where pizza's will hold the instances in the abstract ingredient factory reference.
// Pizza's will call createDough() etc methods in the prepare method. 