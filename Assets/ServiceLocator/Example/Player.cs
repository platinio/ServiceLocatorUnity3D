using UnityEngine;

namespace Platinio
{
    public class Player : MonoBehaviour
    {

        private void Start()
        {
            GameManager gm = ServiceLocator.GetService<GameManager>();
            gm.DoSomething();
        }
    }

}

