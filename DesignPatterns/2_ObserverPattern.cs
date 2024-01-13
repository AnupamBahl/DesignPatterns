/// <summary>
/// Defines 1-many relationship between objects so that when one object changes state
/// all other objects are notified and update automatically
/// 
/// Loose coupling between objects - subjects only know about the interface of the objects
/// and use that knowledge to subscribe/unsubscribe observers
/// 
/// Loose coupling allows for flexibility 
///  - We can add more observers anytime, without affecting or having to change the subjects
///  - Subjects and observers can be used/changed without affecting each other
/// 
/// </summary>


#region Subject

public interface Subject{
    public void subscribeObserver(Observer o);
    public void unsubscribeObserver(Observer o);
    public void notify(float temp, float humi);
}

/// <summary>
/// Pushing state data to observers vs pulling data
///     - Pushing data - allows WeatherData class to keep internal state private and only give data it wants to give
///     - Pulling data - let observers use getter methods
///         - More flexibility. Observers wont' have to immediately act on the data they get or loose it.
///         - Easier modifications. If there's more data, we wont' have to update notify method, instead only add more
///         getter methods for more data. 
/// </summary>
public class WeatherObject : Subject{
    private float temperature;
    private float humidity;
    private List<Observer> observers;

    public WeatherObject(){
        this.observers = new List<Observer>();
    }

    // Subject methods
    public void subscribeObserver(Observer o){
        this.observers.Add(o);
    }

    public void unsubscribeObserver(Observer o){
        observers.Remove(o);
    }

    public void notify(float temp, float humi){
        if (this.observers != null && this.observers.Count() > 0){
            observers.ForEach(o => o.update(temp, humi));
        }
    }

    // Getter Methods
    public float getTemperature(){
        return temperature;
    }

    public float getHumidity(){
        return humidity;
    }


    //Test Methods
    public void updateWeatherData(float newTemp, float newHumi){
        this.temperature = newTemp;
        this.humidity = newHumi;
        notify(this.temperature, this.humidity);
    }
}

#endregion


#region Observers

public interface Observer{
    public void update(float temp, float humi);
}

public interface Display{
    public void display();
}

public class GeneralDisplay : Observer, Display{
    private float temp;
    private float humi;
    private WeatherObject weatherObject;

    public GeneralDisplay(WeatherObject obj){
        this.weatherObject = obj;
        this.weatherObject.subscribeObserver(this);
    }

    public void update(float temp, float humi){
        this.temp = temp;
        this.humi = humi;
        this.display();
    }

    public void display(){
        Console.WriteLine($"New Temperature : {this.temp}F. New Humidity : {this.humi}%");
    }
}

public class DeltaDisplay : Observer, Display{
    private float oldTemp;
    private float oldHumi;
    private float newTemp;
    private float newHumi;
    private WeatherObject weatherObject;

    public DeltaDisplay(WeatherObject obj){
        this.weatherObject = obj;
        this.weatherObject.subscribeObserver(this);
    }

    public void update(float temp, float humi){
        this.oldTemp = this.newTemp;
        this.oldHumi = this.newHumi;
        this.newTemp = temp;
        this.newHumi = humi;

        this.display();
    }

    public void display(){
        Console.WriteLine($"Temperature delta : {this.newTemp - this.oldTemp}F. Humidity Delta : {this.newHumi - this.oldHumi}%");
    }

}

#endregion