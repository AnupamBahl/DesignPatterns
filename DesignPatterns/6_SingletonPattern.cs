/// <summary>
/// Singleton Pattern - ensures a class only has one instance created and provides a global point of 
/// access to the instance. 
/// 
/// Problems
/// - Non enum methods : reflection, multiple class loaders, serialization/deserialization can all cause
/// the class to have multiple instances
/// - Singleton pattern defies loose coupling principle. If sinleton class object changes, you need to 
/// updated every place that uses that object/class
/// - Singleton pattern violates single responsibility principle. Sigleton class has the extra responsibility
/// to instantiate and provide global access to its unique instance. 
/// </summary>
/// 

public class SingletonClass_LazyInstantiation{

    // Having a static reference variable to the class itself let's the class store it's unique object
    // which can be accessed without having an object of the class
    private static SingletonClass_LazyInstantiation uniqueObject;


    // Having a private constructor allows only the class methods to instantiate the class
    private SingletonClass_LazyInstantiation(){
    }


    /// <summary>
    /// Method 1 - lets a private staic getter method instantiate the unique object in a lazy fashion (object 
    /// created when the method below is called the first time, not before). If multiple threads call this code
    /// simultaneously, we might get multiple objects created.
    /// </summary>
    
    public static SingletonClass_LazyInstantiation getUniqueObject_LazyInstantiation_NotThreadSafe(){
        if (uniqueObject == null)
        {
            Thread.Sleep(1000);
            uniqueObject = new SingletonClass_LazyInstantiation();
        }

        return uniqueObject;
    }


    /// <summary>
    /// Method 2 - locks lazy instantiation so only thread can enter at a time. Thread safe but every call to
    /// get the object does a lock. This is 100 times slower than a regular method.
    /// </summary>
    public static SingletonClass_LazyInstantiation getUniqueObject_LazyInstantiation_ThreadSafe_BigOverhead(){
        object o = new object();

        lock(o){
            if (uniqueObject == null){
                Thread.Sleep(1000);
                uniqueObject = new SingletonClass_LazyInstantiation();
            }
        }

        return uniqueObject;
    }


    /// <summary>
    /// Method 3 - Double null check ensures we syncrhonize(lock) only once, when the object is null and 
    /// not with every call. Reduces the overhead of method 2.
    /// </summary>
    public static SingletonClass_LazyInstantiation getUniqueObject_LazyInstantiation_ThreadSafe_MinimalOverhead(){
        object o = new object();

        if (uniqueObject == null){
            lock(o){
                if (uniqueObject == null){
                    Thread.Sleep(1000);
                    uniqueObject = new SingletonClass_LazyInstantiation();
                }
            }
        }

        return uniqueObject;
    }
}



public class SingletonClass_EagerInstantiation{

    /// <summary>
    /// Eager instantiation - is thread safe becuase compiler creates instance during class loading(unless you have
    /// multiple loaders) but has larger overhead. If the instantiation is complex, this can cause delays with class
    /// loading.
    /// </summary>
    private static SingletonClass_EagerInstantiation uniqueObject = new SingletonClass_EagerInstantiation();

    private SingletonClass_EagerInstantiation(){

    }

    /// <summary>
    /// Thread safe without locks or null checks
    /// </summary>
    public static SingletonClass_EagerInstantiation getUniqueObject_Eagerinstantiation(){
        Thread.Sleep(1000);
        return uniqueObject;
    }
}
