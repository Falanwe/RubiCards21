using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
  
    public float StartSpeed = 5f;
    public float MaxSpeed = 20f;

   
    public float SpeedIncrease = 0.25f;

    private float currentSpeed;


    private Vector2 currentDir;

    private bool resetting = false;

    void Start()
    {
        currentDir = Random.insideUnitCircle.normalized;
        currentSpeed = StartSpeed;
        
    }

    void Update()
    {
     
        if (resetting)
            return;


        Vector2 moveDir = currentDir * currentSpeed * Time.deltaTime;
        transform.Translate(new Vector3(moveDir.x, 0f, moveDir.y));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            
            currentDir.y *= -1;
        }
        else if (other.tag == "Player")
        {
            
            currentDir.x *= -1;
        }
        else if (other.tag == "Goal")
        {
          
            StartCoroutine(resetBall());
      
            other.SendMessage("GetPoint", SendMessageOptions.DontRequireReceiver);
        }

     
        currentSpeed += SpeedIncrease;

    
        currentSpeed = Mathf.Clamp(currentSpeed, StartSpeed, MaxSpeed);
    }

    IEnumerator resetBall()
    {
      
        resetting = true;
        transform.position = Vector3.zero;

        currentDir = Vector3.zero;
        currentSpeed = 0f;
      
        yield return new WaitForSeconds(3f);

        Start();

        resetting = false;
    }
}