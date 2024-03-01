using System.Runtime.CompilerServices;
using System.Threading;

public class TestFile{
    public void SetupAndExecute(){
        TestExecutor testExecutor = new TestExecutor();

        var strategyPatternTests = new StrategyPatternSimulator(testExecutor);
        var observerPatternTests = new ObserverPatternSimulator(testExecutor);
        var decoratorPatternTests = new DecoratorPatternSimulator(testExecutor);
        var factoryPatternTests = new FactoryMethodPatternSimulator(testExecutor);
        //var singletonPatternTests = new SingletonPatternSimulator(testExecutor);
        var commandPatternTests = new CommandPatternSimulator(testExecutor);
        var adapterPatternTests = new AdapterPatternSimulator(testExecutor);
        var templatePatternTests = new TemplatePatternSimulator(testExecutor);
        var iteratorPatternTests = new IteratorPatternSimulator(testExecutor);

        testExecutor.Run();
    }
}



/// <summary>
/// Test Subjects
/// Following observer pattern. All tests subscribe to the subject below. Subject executes all tests.
/// </summary>
public abstract class TestExecutionSubject{
    public List<DesignPatternTest> testClasses = new List<DesignPatternTest>();

    public abstract void registerTests(DesignPatternTest tests);
    public abstract void unregisterTests(DesignPatternTest tests);
}

public class TestExecutor : TestExecutionSubject{

    public override void registerTests(DesignPatternTest tests){
        this.testClasses.Add(tests);
    }
    public override void unregisterTests(DesignPatternTest tests){
        this.testClasses.Remove(tests);
    }

    public void Run(){
        foreach(var tests in this.testClasses){
            this.StartBlock(tests.Description);
            tests.executeTests();
            this.EndBlock();
        }
    }

    private void StartBlock(String str){
        Console.WriteLine("-------------------------------------");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Testing:{str}\n");
        Console.ForegroundColor = ConsoleColor.White;
    }

    private void EndBlock(){
        Console.WriteLine("\nEnd Section");
        Console.WriteLine("-------------------------------------\n\n");
    }
}

/// <summary>
/// Test Classes (Observers) that subscribe to test subjec. Test subject executes all tests.
/// </summary>

public abstract class DesignPatternTest{
    private string description = "Not Defined";

    public string Description { get => description; private protected set => description = value; }

    public abstract void executeTests();

    protected void TypeHeading(string s){
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(s);
        Console.ForegroundColor = ConsoleColor.White;
    }
}

public class StrategyPatternSimulator : DesignPatternTest {

    public StrategyPatternSimulator(TestExecutionSubject executionSubject){
        this.Description = "StrategyPattern";
        executionSubject.registerTests(this);
    }

    public override void executeTests(){
        Duck mallard = new MallardDuck();
        mallard.performFly();
        mallard.performQuack();

        Console.ForegroundColor = ConsoleColor.Blue;
        TypeHeading("\nTesting dynamic behavior allocation:");
        Console.ForegroundColor = ConsoleColor.White;
        Duck model = new ModelDuck();
        model.performFly();
        model.setFlyBehavior(new FlyRocketPowered());
        model.performFly();
    }
}

public class ObserverPatternSimulator : DesignPatternTest {
    public ObserverPatternSimulator(TestExecutionSubject executionSubject){
        this.Description = "ObserverPattern";
        executionSubject.registerTests(this);
    }

    public override void executeTests(){
        WeatherObject obj = new WeatherObject();
        obj.updateWeatherData(75, 50);

        GeneralDisplay generalDisplay = new GeneralDisplay(obj);
        DeltaDisplay deltaDisplay = new DeltaDisplay(obj);
        obj.updateWeatherData(77, 42);
        obj.updateWeatherData(70, 80);
        obj.updateWeatherData(73, 65);
    }
}

public class DecoratorPatternSimulator : DesignPatternTest {
    public DecoratorPatternSimulator(TestExecutionSubject executionSubject){
        this.Description = "Decorator Pattern";
        executionSubject.registerTests(this);
    }

    public override void executeTests()
    {
        Coffee coffeeObject = new DarkRoast();

        printData(coffeeObject);
        coffeeObject = new SoyMilk(coffeeObject);
        printData(coffeeObject);
        coffeeObject = new WhipCream(coffeeObject);
        printData(coffeeObject);

        
    }

    private void printData(Coffee coffeeObject){
        Console.WriteLine($"Desc : {coffeeObject.getDescription()} || Cost : ${coffeeObject.getCost()}");
    }
}


public class FactoryMethodPatternSimulator : DesignPatternTest {
    public FactoryMethodPatternSimulator(TestExecutionSubject executionSubject){
        this.Description = "Facotry Pattern";
        executionSubject.registerTests(this);
    }

    public override void executeTests()
    {
        PizzaStore nyStore = new NYPizzaStore();
        PizzaStore chicagoStore = new ChicagoPizzaStore();

        TypeHeading("Ordering a cheese pizza from NyStore");
        nyStore.OrderPizza("Cheese");
        Console.WriteLine("__________________");

        TypeHeading("Ordering a peperroni pizza from ChicagoStore");
        chicagoStore.OrderPizza("Peperroni");
    }
}


public class SingletonPatternSimulator : DesignPatternTest {
    public SingletonPatternSimulator(TestExecutionSubject executionSubject){
        this.Description = "Singleton Pattern";
        executionSubject.registerTests(this);
    }

    public override void executeTests()
    {
        SingletonTest("Lazy Instantiation, not threadsafe"
        , SingletonClass_LazyInstantiation.getUniqueObject_LazyInstantiation_NotThreadSafe);

        SingletonTest("Lazy Instantiation, threadsafe with overhead"
        , SingletonClass_LazyInstantiation.getUniqueObject_LazyInstantiation_ThreadSafe_BigOverhead);

        SingletonTest("Lazy Instantiation, threadsafe, no overhead"
        , SingletonClass_LazyInstantiation.getUniqueObject_LazyInstantiation_ThreadSafe_MinimalOverhead);

        SingletonTest("Eager Instantiation, threadsafe"
        , SingletonClass_EagerInstantiation.getUniqueObject_Eagerinstantiation);

        //TestEager();
    }

    private void SingletonTest<t>(string title, Func<t> function) where t : class{
        Console.WriteLine(title);

        t obj1 = default(t), obj2 = default(t);

        Thread thread1 = new Thread(() => {
                obj1 = function();
            });

        Thread thread2 = new Thread(() => {
                obj2 = function();
            });

        thread1.Start();
        thread2.Start();

        Thread.Sleep(1500);

        Console.WriteLine($"Obj1 == Obj2 : {obj1 == obj2}, obj1 : {obj1}, obj2 : {obj2}\n");
    }
}


public class CommandPatternSimulator : DesignPatternTest{
    public CommandPatternSimulator(TestExecutionSubject executionSubject){
        this.Description = "Command Pattern Tests";
        executionSubject.registerTests(this);
    }

    public override void executeTests(){
        LightSwitch bedroomSwitch = new LightSwitch("BedroomSwitch");
        LightSwitch livingroomSwitch = new LightSwitch("LivingroomSwitch");
        HotTub hotTub = new HotTub();

        Command bedroomSwitchOn = new LightSwitchOnCommand(bedroomSwitch);
        Command bedroomSwitchOff = new LightSwitchOffCommand(bedroomSwitch);
        Command livingroomSwitchOn = new LightSwitchOnCommand(livingroomSwitch);
        Command livingroomSwitchOff = new LightSwitchOffCommand(livingroomSwitch);
        Command hottubOn = new HotTubOnCommand(hotTub);
        Command hottubOff = new HotTubOffCommand(hotTub);
        Command macroCommandOn = new MacroCommand(new List<Command> { livingroomSwitchOn, hottubOn });
        Command macroCommandOff = new MacroCommand(new List<Command> { livingroomSwitchOff, hottubOff });

        RemoteControl control = new RemoteControl();
        control.SetCommand(bedroomSwitchOn, bedroomSwitchOff, 1);
        control.SetCommand(livingroomSwitchOn, livingroomSwitchOff, 2);
        control.SetCommand(hottubOn, hottubOff, 3);
        control.SetCommand(macroCommandOn, macroCommandOff, 4);

        TypeHeading($"RemoteControl:\n{control.toString()}");

        control.ButtonOn(1);
        control.ButtonOff(1);
        control.ButtonOn(2);
        control.ButtonOn(3);
        control.Undo();
        control.ButtonOff(2);

        TypeHeading("\nTesting macro:");
        control.ButtonOn(4);
        control.Undo();
    }
}

public class AdapterPatternSimulator : DesignPatternTest{
    public AdapterPatternSimulator(TestExecutionSubject executionSubject) {
        this.Description="Adapter pattern tests";
        executionSubject.registerTests(this);
    }

    public override void executeTests()
    {
        Wolf actualWolf = new BlackWolf();
        Dog actualDog = new Doberman();
        Wolf dogAsWolf = new DogAdapter(actualDog);

        TypeHeading("Actual Wolf -:");
        actualWolf.Howl();
        actualWolf.Run();
        Console.WriteLine();

        TypeHeading("Actual Dog -:");
        actualDog.Bark();
        actualDog.Walk();
        Console.WriteLine();

        TypeHeading("Dog Adapter - Wolf interface wrapping dog object -:");
        dogAsWolf.Howl();
        dogAsWolf.Run();
    }
}

public class TemplatePatternSimulator : DesignPatternTest{
    public TemplatePatternSimulator(TestExecutionSubject executionSubject){
        this.Description = "Template Pattern Tests";
        executionSubject.registerTests(this);
    }

    public override void executeTests()
    {
        CaffeineBeverage coffeeBeverage = new CoffeeBeverage();
        CaffeineBeverage teaBeverage = new TeaBeverage();

        TypeHeading("Using Coffee object to call Prepare method");
        coffeeBeverage.prepareRecipe();
        Console.WriteLine();
        TypeHeading("Using Tea object to call Prepare method");
        teaBeverage.prepareRecipe();
    }
}


public class IteratorPatternSimulator : DesignPatternTest{
    public IteratorPatternSimulator(TestExecutionSubject executionSubject){
        this.Description = "Iterator Pattern Tests";
        executionSubject.registerTests(this);
    }

    public override void executeTests()
    {
        Menu dMenu = new DinerMenu();
        Menu pMenu = new PancakeMenu();

        Waitress client = new Waitress(pMenu, dMenu);
        client.printMenu();
    }
}
