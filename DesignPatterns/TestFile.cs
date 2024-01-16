using System.Runtime.CompilerServices;
using System.Threading;

public class TestFile{
    public void SetupAndExecute(){
        TestExecutor testExecutor = new TestExecutor();

        var strategyPatternTests = new StrategyPatternSimulator(testExecutor);
        var observerPatternTests = new ObserverPatternSimulator(testExecutor);
        var decoratorPatternTests = new DecoratorPatternSimulator(testExecutor);
        var factoryPatternTests = new FactoryMethodPatternSimulator(testExecutor);
        var singletonPatternTests = new SingletonPatternSimulator(testExecutor);

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
        Console.WriteLine("\nTesting dynamic behavior allocation:");
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

        Console.WriteLine("Ordering a cheese pizza from NyStore");
        nyStore.OrderPizza("Cheese");
        Console.WriteLine("__________________");

        Console.WriteLine("Ordering a peperroni pizza from ChicagoStore");
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
