using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickColorFromPlayer : MonoBehaviour
{

    // Start
    void Start()
    {
        
    }

    // Update
    void Update()
    {
        ColorBlock cb = this.GetComponent<Button>().colors;
        cb.highlightedColor = GameManager.instance.currentPlayerID == 1 ? Color.yellow : Color.red;
        this.GetComponent<Button>().colors = cb;        
    }
}
