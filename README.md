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
