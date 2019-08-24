using UnityEngine;

namespace Platinio
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator.RegisterService(this);
        }

        public void DoSomething()
        {
            Debug.Log("Call to do something");
        }
    }
}

