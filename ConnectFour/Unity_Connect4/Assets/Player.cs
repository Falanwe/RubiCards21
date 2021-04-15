using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public int playerID;
    public bool canPlay;

    private void Awake()
    {
        instance = this;
    }

    // Start
    void Start()
    {
        canPlay = playerID == 1 ? true : false;    
    }

}
