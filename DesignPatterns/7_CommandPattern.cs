using System.ComponentModel.Design;

/// <summary>
/// Command Pattern - encapsualtes a request as an object, allowing the user to parameterize clients with different
/// requests, queue or log requests and support operations like undo and restore state
/// 
/// Features:
///     - Command pattern decouples the invoker(the object making the request) of the command and the reciever(the object
///     that knows how to execute the command) of the command.
/// 
///     - Sometimes the Command objects can themselves implement the execution(receiver functionality), but this makes
///     composing certain commands difficut.
///         - Eg - a macro command, that has a list of different receivers and executes them all. 
/// 
/// Terminology
/// Receiver - the class that knows the details of executing a request
/// Invoker - the class that manages all request invocations and stores various types of requests (eg remote control). 
/// Invokers can be parameterized with commands even at runtime.
/// Command - the interface defines behavior. Each concrete command object knows how to execute a particular command (the client 
/// defines this by composing various commands as needed).
/// </summary>
/// 



//RECEIVERS
#region Receivers
public class LightSwitch{
    private string name;
    public LightSwitch(string name){
        this.name = name;
    }
    public void on(){
        Console.WriteLine($"{this.name} switched on.");
    }

    public void off(){
        Console.WriteLine($"{this.name} switched off.");
    }
}

public class HotTub{
    public void on(){
        Console.WriteLine("HotTub switched on.");
    }

    public void off(){
        Console.WriteLine("HotTub switched off.");
    }
}

#endregion


//COMMANDS INTERFACE & CONCRETE CLASSES
#region Commands
public interface Command{
    public void Execute();
    public void Undo();
}

public class EmptyCommand : Command{
    public void Execute(){}
    public void Undo(){}
}

// Macro command is a combination of multiple commands
public class MacroCommand : Command{
    List<Command> commands;

    public MacroCommand(List<Command> commands){
        this.commands = commands;
    }

    public void Execute(){
        this.commands.ForEach(c => c.Execute());
    }

    public void Undo(){
        this.commands.ForEach(c => c.Undo());
    }
}

public class LightSwitchOnCommand : Command{
    LightSwitch LSwitch;

    public LightSwitchOnCommand(LightSwitch Switch){
        this.LSwitch = Switch;
    }

    public void Execute(){
        LSwitch.on();
    }

    public void Undo(){
        LSwitch.off();
    }
}

public class LightSwitchOffCommand : Command{
    LightSwitch LSwitch;

    public LightSwitchOffCommand(LightSwitch Switch){
        this.LSwitch = Switch;
    }

    public void Execute(){
        LSwitch.off();
    }

    public void Undo(){
        LSwitch.on();
    }
}

public class HotTubOffCommand : Command{
    HotTub hottub;

    public HotTubOffCommand(HotTub hottub){
        this.hottub = hottub;
    }

    public void Execute(){
        hottub.off();
    }

    public void Undo(){
        hottub.on();
    }
}

public class HotTubOnCommand : Command{
    HotTub hottub;

    public HotTubOnCommand(HotTub hottub){
        this.hottub = hottub;
    }

    public void Execute(){
        hottub.on();
    }

    public void Undo(){
        hottub.off();
    }
}

#endregion


//INVOKER (REMOTE CONTROL)
#region Invoker

public class RemoteControl{
    private List<Command> onCommands = new List<Command>();
    private List<Command> offCommands = new List<Command>();
    private Command lastCommand;

    public RemoteControl(){
        for (int i=0; i<5; i++){
            this.onCommands.Add(new EmptyCommand());
            this.offCommands.Add(new EmptyCommand());    
        }

        this.lastCommand = new EmptyCommand();
    }

    public void SetCommand(Command newOnCommand, Command newOffCommand, int buttonNumber){
        if (buttonNumber > 10 || buttonNumber < 1){
            Console.WriteLine($"Button {buttonNumber} does not exists.");
        }
        else{
            this.onCommands[buttonNumber - 1] = newOnCommand;
            this.offCommands[buttonNumber - 1] = newOffCommand;
        }
    }

    public void ButtonOn(int buttonNumber){
        if (buttonNumber > 10 || buttonNumber < 1){
            Console.WriteLine($"Button {buttonNumber} does not exists.");
        }
        else{
            this.onCommands[buttonNumber - 1].Execute();
            this.lastCommand = this.onCommands[buttonNumber - 1];
        }
    }

    public void ButtonOff(int buttonNumber){
        if (buttonNumber > 10 || buttonNumber < 1){
            Console.WriteLine($"Button {buttonNumber} does not exists.");
        }
        else{
            this.offCommands[buttonNumber - 1].Execute();
            this.lastCommand = this.onCommands[buttonNumber - 1];
        }
    }

    public void Undo(){
        this.lastCommand.Undo();
    }

    public String toString(){
        String resultString = String.Empty;

        for (int i = 0; i < this.onCommands.Count(); i++){
            resultString += $"{this.onCommands[i].GetType().Name.ToString()} : {this.offCommands[i].GetType().Name.ToString()}\n";
        }

        return resultString;
    }
}

#endregion
