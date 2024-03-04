/// <summary>
/// Composite Pattern - Helps represent objects as part-whole heirarchies (tree structures where both leaves and nodes 
/// inherit from the same interface). It lets clients treat individual objects and compositions of objects uniformly.
/// 
///     - Component interface - the interface that all components of the tree structure inherit from
///     - Component - any object in a structure (leaf or node). 
///     - This pattern assigns two responsibilities to the interface. It needs to have functions for both the nodes(add 
///     another node, remove another node) and the leaves (store information, or do some function)
///     - Tradeoff of violating single responsibility principle is that this pattern provides more visibility to the client
///     
/// </summary>
/// 


// Common interface for both leaves and nodes of the tree
public abstract class MenuComponent{

    // Sub-component (nodes) methods
    public virtual void add(MenuComponent comp){
        throw new InvalidOperationException();
    }

    public virtual void remove(MenuComponent comp){
        throw new InvalidOperationException();
    }

    public virtual MenuComponent getChild(int index){
        throw new InvalidOperationException();
    }


    // Leaf items
    public virtual String getName(){
        throw new InvalidOperationException();
    }

    public virtual String getDescription(){
        throw new InvalidOperationException();
    }

    public virtual double getPrice(){
        throw new InvalidOperationException();
    }

    public virtual bool isVegetarian(){
        throw new InvalidOperationException();
    }

    // Items for both
    public virtual void print(){
        throw new InvalidOperationException();
    }

}

// Non leaf nodes, responsible for adding/removing nodes and traversing to the leaf nodes
// It will only override methods it needs
public class CafeMenu : MenuComponent{
    String name;
    String description;
    List<MenuComponent> components = new List<MenuComponent>();

    public CafeMenu(String name, String desc){
        this.name = name;
        this.description = desc;
    }

    public override void add(MenuComponent comp){
        this.components.Add(comp);
    }

    public override void remove(MenuComponent comp){
        this.components.Remove(comp);
    }

    public override MenuComponent getChild(int idx){
        return this.components[idx];
    }

    public override String getName(){
        return this.name;
    }

    public override string getDescription()
    {
        return this.description;
    }

    public override void print(){
        Console.WriteLine("\n" + getName());
        Console.WriteLine(", " + getDescription());
        Console.WriteLine("-----------------");

        // For each subcomponent/leaf, print their details
        foreach (var comp in this.components){
            comp.print();
        }
    }
}


// Leaf node - actually stores the menu items and their prices.
public class CafeMenuItem : MenuComponent{
    string name;
    string description;
    bool vegetarian;
    double price;

    public CafeMenuItem(String name, String desc, bool veg, double p){
        this.name = name;
        this.description = desc;
        this.vegetarian = veg;
        this.price = p;
    }

    public override string getName()
    {
        return this.name;
    }

    public override string getDescription()
    {
        return this.description;
    }

    public override double getPrice()
    {
        return this.price;
    }

    public override bool isVegetarian()
    {
        return this.vegetarian;
    }

    public override void print()
    {
        Console.WriteLine(" " + getName());
        if (isVegetarian()){
            Console.WriteLine("(v)");
        }

        Console.WriteLine(", " + getPrice().ToString());
        Console.WriteLine("    --" + getDescription());
    }
        
}

public class CompositePatternClient{
    MenuComponent menu;

    public CompositePatternClient(MenuComponent mainMenu){
        this.menu = mainMenu;
    }

    public void printMenu(){
        this.menu.print();
    }
}
