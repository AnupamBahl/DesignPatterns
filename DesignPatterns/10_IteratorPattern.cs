

// The object that is being stored using different data strcutures by multiple aggregate classes. We need a unifiedj
// iterator for this object.
using System.Net.WebSockets;

/// <summary>
/// Iterator pattern - provides a way to traverse the elements of an aggregate(aggregate is a group/collection of objects, 
/// stored in some data structure), without exposing the underlying implementation of the aggregate.
///     - It places the responsibility of traversal on a class separate than the aggregate class itself.
///     - Promotes cohesion through single responsibility principle.
/// </summary>
/// 
public class MenuItem{
    public string name;
    public double price;

    public MenuItem(string n, double p){
        this.name = n;
        this.price = p;
    }
}


public interface Menu{
    // Different menu's can have different data structures to store menu items. They should all return
    // a common iterator to unify sequential traversal of elements.
    public Iterator createIterator();
}

public class DinerMenu : Menu{
    MenuItem[] items;
    const int MaxItems = 10;
    int numberOfItems = 0;

    public DinerMenu(){
        items = new MenuItem[MaxItems];
        addItem("Burger", 10.3);
        addItem("Chicken", 13.4);
    }

    // Creates an iterator and returns it. The burden of iteration falls on another calls (defined below), hence
    // separating responsibilities.
    public Iterator createIterator(){
        return new DinerIterator(this.items);
    }

    public void addItem(string n, double p){
        if (numberOfItems >= MaxItems){
            Console.WriteLine("maximum count reached");
        }
        else{
            this.items[numberOfItems] = new MenuItem(n, p);
            numberOfItems += 1;
        }
    }
}


public class PancakeMenu : Menu{
    List<MenuItem> items;

    public PancakeMenu(){
        items = new List<MenuItem>();
        items.Add(new MenuItem("Vanilla pancake", 5.0));
        items.Add(new MenuItem("Chocolate pancake", 7.5));
    }

    // Creates an iterator and returns it. The burden of iteration falls on another calls (defined below), hence
    // separating responsibilities.
    public Iterator createIterator(){
        return new PancakeIterator(this.items);
    }

    public void addItem(string n, double p){
        this.items.Add(new MenuItem(n, p));
    }
}





// Iterator Interface. Iterators for all aggregate classes will inherit from this
public interface Iterator{
    public bool hasNext();
    public MenuItem next();

}

// Concrete iterator capable of iterating for aggregate class DinerMenu
public class DinerIterator : Iterator{
    MenuItem[] items;
    int position = 0;

    public DinerIterator(MenuItem[] lst){
        this.items = lst;
    }

    public MenuItem next(){
        MenuItem menuItem = items[position];
        position += 1;
        return menuItem;
    }

    public bool hasNext(){
        if (position >= items.Length || items[position] == null){
            return false;
        }

        return true;
    }
}

// Concrete iterator capable of iterating for aggregate class PancakeMenu
public class PancakeIterator : Iterator{
    List<MenuItem> items;
    int position = 0;

    public PancakeIterator(List<MenuItem> items){
        this.items = items;
    }

    public MenuItem next(){
        var res = this.items[position];
        position += 1;

        return res;
    }

    public bool hasNext(){
        return position < items.Count();
    }
}


// The client class - that has to combine both menus but doesn't want to deal with different traversal
// techniques for different data stores across multiple aggregators

public class Waitress{
    Menu pancakeMenu;
    Menu dinerMenu;

    public Waitress(Menu pMenu, Menu dMenu){
        this.pancakeMenu = pMenu;
        this.dinerMenu = dMenu;
    }

    public void printMenu(){
        Iterator pIterator = pancakeMenu.createIterator();
        Iterator dIterator = dinerMenu.createIterator();

        Console.WriteLine("Diner Menu:");
        printMenu(dIterator);
        Console.WriteLine("*************\n");
        Console.WriteLine("Pancake Menu:");
        printMenu(pIterator);
    }

    private void printMenu(Iterator iterator){
        while (iterator.hasNext()){
            var item = iterator.next();
            Console.WriteLine($"Item name:{item.name}");
            Console.WriteLine($"Item price: {item.price}");
            Console.WriteLine();
        }
    }
}
