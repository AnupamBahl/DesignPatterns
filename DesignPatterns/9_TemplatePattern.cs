/// <summary>
/// Template Pattern - defines the skeleton of an algorithm and allows subclasses to define certain steps of the alorithm.
/// Subcasses can redefine certain steps, but cannot alter the structure of the alorithm itself.
/// </summary>
/// 

public abstract class CaffeineBeverage{

    // in C# all methods not declared 'virtual' are 'final' and cannot be overridden by subclasses
    /// <summary>
    /// This method is the template method. It cannot be overridden since it's not marked virtual.
    /// boilWater() and pourInACup() are the same for all beverages. brew() and addCondiments() are decalred
    /// virtual to allow subclasses to define their own implementations of it.
    /// 
    /// customerWantsCondiments() - is a hook. Hooks have default behavior in the template class. The subclasses
    /// may or may not redefine the hook. This gives the subclasses the ability to 'hook into' the algorigthm
    /// at various points if they wish. They are free to ignore it.
    /// </summary>
    public void prepareRecipe(){
        boilWater();
        brew();
        pourInCup();
        
        if (customerWantsCondiments()){
            addCondiments();
        }
    }

    public void boilWater(){
        Console.WriteLine("The water is boiling");
    }

    public void pourInCup(){
        Console.WriteLine("Pouring the drink in the cup");
    }

    /// <summary>
    /// Abstract methods must be overridden by subclasses
    /// </summary>
    public abstract void brew();
    public abstract void addCondiments();

    /// <summary>
    /// The hook is defined virtual. Subclasses may or may not override it.
    /// </summary>
    public virtual bool customerWantsCondiments(){
        return true;
    }

}


public class CoffeeBeverage : CaffeineBeverage{
    public override void brew(){
        Console.WriteLine("Dripping Coffee through filter");   
    }

    public override void addCondiments(){
        Console.WriteLine("Adding sugar and milk");
    }
}

public class TeaBeverage : CaffeineBeverage{
    public override void brew(){
        Console.WriteLine("Steeping the tea");
    }

    public override void addCondiments(){
        Console.WriteLine("Adding lemon");
    }

    public override bool customerWantsCondiments(){
        Console.WriteLine("Do you like lemon in your tea? (y/n)");
        string? input = Console.ReadLine();

        if(input != null && input.ToLower().StartsWith('y')){
            return true;
        }

        return false;
    }
}