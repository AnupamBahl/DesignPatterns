/// <summary>
/// Definining behaviors first. Duck class and the behaviors have a Has-A relationship, instead of Is-A.
/// Design principles in action:
///  - Separate what varies. Encapsulate it.
///  - Design to an interface, not an implementation (if fly method was declared in the duck supercalss
///  or was being declared in the subclasses that inherit from duck class, each fly method would be 
///  tied to the objects of those subclasses.)
/// </summary>


# region Behaviors

public interface FlyBehavior{
    public void fly();
}

public class FlyWithWings : FlyBehavior{
    public void fly(){
        Console.WriteLine("I'm flying!!");
    }
}

public class FlyNoWay : FlyBehavior{
    public void fly(){
        Console.WriteLine("I can't fly");
    }
}

// STAGE 2 - testing dynamic allocation
public class FlyRocketPowered : FlyBehavior{
    public void fly(){
        Console.WriteLine("I am flying with a rocket");
    }
}


public interface QuackBehavior{
    public void quack();
}

public class Quack : QuackBehavior{
    public void quack(){
        Console.WriteLine("Quack!");
    }
}

public class MuteQuack : QuackBehavior{
    public void quack(){
        Console.WriteLine("<< Silence >>");
    }
}

public class Squeak : QuackBehavior{
    public void quack(){
        Console.WriteLine("Squeak!!");
    }
}

#endregion




#region DuckClasses

public abstract class Duck{
    public FlyBehavior flyBehavior;
    public QuackBehavior quackBehavior;

    public abstract void display();

    public void performFly(){
        flyBehavior.fly();
    }

    public void performQuack(){
        quackBehavior.quack();
    }

    public void swim(){
        Console.WriteLine("All ducks float, even decoys!");
    }

    // STAGE 2
    // Allowing runtime modification(dynamic allocation) of duck behaviors
    public void setFlyBehavior(FlyBehavior behavior){
        flyBehavior = behavior;
    }

    public void setQuackBehavior(QuackBehavior behavior){
        quackBehavior = behavior;
    }

}

public class MallardDuck : Duck{
    public MallardDuck(){
        quackBehavior = new Quack();
        flyBehavior = new FlyWithWings();
    }

    public override void display(){
        Console.WriteLine("I am a Mallard Duck");
    }
}

// STAGE 2
// Testing Dynamic allocation 
public class ModelDuck : Duck{
    public ModelDuck(){
        flyBehavior = new FlyNoWay();
        quackBehavior = new Quack();
    }

    public override void display()
    {
        Console.WriteLine("I am a model duck");
    }
}

#endregion