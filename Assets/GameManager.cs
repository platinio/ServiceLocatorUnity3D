using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void OnPlayerConnect(Player p)
    {
        ServiceLocator.RegisterService("MiIdUnico" , p);
    }
}
