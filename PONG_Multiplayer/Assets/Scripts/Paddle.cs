using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour
{
   
    public float MoveSpeed = 10f;
    public float MoveRange = 10f;

 
    public bool AcceptsInput = true;
    public string Axis = "Vertical";

    void Update()
    {
       
        if (!AcceptsInput)
            return;

     
        float input = Input.GetAxis(Axis);
  
        Vector3 pos = transform.position;
        pos.z += input * MoveSpeed * Time.deltaTime;
        pos.z = Mathf.Clamp(pos.z, -MoveRange, MoveRange);
        transform.position = pos;
    }
}