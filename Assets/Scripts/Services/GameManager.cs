using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static CourutineHandler courutineHandler;

    private void Awake()
    {
        courutineHandler = GetComponent<CourutineHandler>();
    }
}
