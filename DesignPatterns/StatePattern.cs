/// <summary>
/// Allows an object to change its behavior when it's internal state changes. The object appears to change it's
/// class, which it does. 
///     - It's similar to strategy in a way that the client has an object reference that changes the classs
///     its pointing to based on certain conditions
///     - State transition can be controlled by either state classes or the client
///     - This pattern results in a greater number of classes in your design.
///     - State classes can be shared among client instances.
///     - All state classes are dependent on each other. One way to introduce flexibility is to have getters 
///     and setters for the states in the client. That way we can build and return different states to the
///     state classes from the client as needed.
/// </summary>

// State interface for a gumball machine. Find state diagram picture in repository
public interface State{
    bool insertQuarter();
    void ejectQuarter();
    void turnCrank();
    void dispense();

}

// The client of the state machine
public class GumballMachine{
    private State noQuarter;
    private State hasQuarter;
    private State sold;
    private State soldOut;


    State currentState;
    int gumballCount;

    public GumballMachine(int count){
        this.gumballCount = count;
        noQuarter = new NoQuarterState(this);
        soldOut = new SoldOutState(this);
        sold = new SoldState(this);
        hasQuarter = new HasQuarterState(this);

        if(this.gumballCount > 0){
            this.currentState = noQuarter;
        }
        else{
            this.currentState = soldOut;
        }
    }

    public State getQuarterInsertedState(){
        return this.hasQuarter;
    }
    public State getNoMoreGumballsState(){
        return this.soldOut;
    }
    public State getCrankTurnedState(){
        return this.sold;
    }
    public State getResetState(){
        return this.noQuarter;
    }

    public void setState(State newState){
        this.currentState = newState;
    }

    public bool hasGumballs(){
        return this.gumballCount > 0;
    }
    public void dispenseGumball(){
        this.gumballCount -= 1;
    }

    //Methods that the client will expose for it's clients to  use
    public void insertQuarter(){
        this.currentState.insertQuarter();
    }

    public void ejectQuarter(){
        this.currentState.ejectQuarter();
    }

    public void turnCrank(){
        this.currentState.turnCrank();
        this.currentState.dispense();
    }

    public override string ToString(){
        return $"Currently, the machine has {this.gumballCount} gumballs. It's in state {this.currentState.ToString()}";
    }

}

public class NoQuarterState : State{
    GumballMachine client;

    public NoQuarterState(GumballMachine m){
        this.client = m;
    }

    public bool insertQuarter(){
        Console.WriteLine("You inserted a quarter");
        this.client.setState(client.getQuarterInsertedState());

        return true;
    }

    public void ejectQuarter(){
        Console.WriteLine("You haven't inserted a quarter");
    }

    public void turnCrank(){
        Console.WriteLine("You turned, but there's no quarter");
    }

    public void dispense(){
        Console.WriteLine("You need to pay first");
    }
}

public class HasQuarterState : State{
    GumballMachine client;

    public HasQuarterState(GumballMachine m){
        this.client = m;
    }

    public bool insertQuarter(){
        Console.WriteLine("You already inserted a quarter.");
        return false;
    }

    public void ejectQuarter(){
        Console.WriteLine("Quarter returned");
        this.client.setState(client.getResetState());
    }

    public void turnCrank(){
        Console.WriteLine("Turning the crank");
        this.client.setState(client.getCrankTurnedState());
    }

    public void dispense(){
        Console.WriteLine("You need to turn the crank first");
    }
}

public class SoldOutState : State{
    GumballMachine client;

    public SoldOutState(GumballMachine m){
        this.client = m;
    }

    public bool insertQuarter(){
        Console.WriteLine("Cannot accept quarter. No gumballs to dispense.");
        return false;
    }

    public void ejectQuarter(){
        Console.WriteLine("Cannot eject. You did not insert a quarter");
    }

    public void turnCrank(){
        Console.WriteLine("No gumballs to dispense");
    }

    public void dispense(){
        Console.WriteLine("No gumballs to dispense");
    }
}

public class SoldState : State{
    GumballMachine client;

    public SoldState(GumballMachine m){
        this.client = m;
    }

    public bool insertQuarter(){
        Console.WriteLine("Cannot accept another quarter.");
        return false;
    }

    public void ejectQuarter(){
        Console.WriteLine("Cannot eject. The transaction has been completed.");
    }

    public void turnCrank(){
        Console.WriteLine("Cannot dispense again. No more quarters.");
    }

    public void dispense(){
        Console.WriteLine("Dispensing gumball");
        this.client.dispenseGumball();

        if (this.client.hasGumballs()){
            this.client.setState(this.client.getResetState());
        }
        else{
            Console.WriteLine("Oops, out of gumballs");
            this.client.setState(this.client.getNoMoreGumballsState());
        }
    }
}