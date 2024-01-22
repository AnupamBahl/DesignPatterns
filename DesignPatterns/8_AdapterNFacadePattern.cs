using System.Reflection.Metadata;

/// <summary>
/// Adapter Pattern - converts the interface of a class to another interface, one that clients expect.
/// Lets classes work together, that couldn't before, because of incompatible interfaces.
/// 
/// Facade Pattern - Provides a unified interface to a set of interfaces in a subsystem(complicated 
/// subsystems). Makes subsystems easier to use
/// 
/// - Either of these patterns don't extend the operations of a class/object, does NOT add new 
/// functionality like Decorator Pattern does./// 
/// </summary>
 
#region AdapterPattern
public interface Dog{
    public void Bark();
    public void Walk();
}

public interface Wolf{
    public void Howl();
    public void Run();
}

public class Doberman : Dog{
    public void Bark(){
        Console.WriteLine("Woof Woof!");
    }
    public void Walk(){
        Console.WriteLine("Tip Tap..");
    }
}

public class BlackWolf : Wolf{
    public void Howl(){
        Console.WriteLine("Ah-woooooo!");
    }
    public void Run(){
        Console.WriteLine("Tap Tap Tap..");
    }
}

/// <summary>
/// The client expects a wolf interface but needs to call dog object methods. Name the adapter
/// after the class the adapter calls, not the interface it wraps and presents to the client.
/// </summary>
public class DogAdapter : Wolf{
    private Dog dog;
    public DogAdapter(Dog dog){
        this.dog = dog;
    }

    public void Howl(){
        // calling bark multiple times to make up for howl
        for (int i=0; i<2; i++){
            this.dog.Bark();
        }
    }

    public void Run(){
        // calling walk multiple times to make up for run
        for (int i=0; i<2; i++){
            this.dog.Walk();
        }
    }
}

#endregion
