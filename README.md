What is this?
==============
A small example of the service locator pattern in Unity3D.

Register a Service
==============

```c#
public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.RegisterService(this);
    }
}
```
Register a service using a custom ID
```c#
public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.RegisterService("MyCustomID" , this);
    }
}
```

Getting a Service
==============

```c#
public class Player : MonoBehaviour
{
    private void Start()
    {
        GameManager gm = ServiceLocator.GetService<GameManager>();
        gm.DoSomething();
    }
}
```
Get service using custom id
```c#
public class Player : MonoBehaviour
{
    private void Start()
    {
        GameManager gm = ServiceLocator.GetService<GameManager>("MyCustomID");
        gm.DoSomething();
    }
}
```
