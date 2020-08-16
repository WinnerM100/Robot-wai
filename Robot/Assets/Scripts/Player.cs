using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //public CharacterController MobileInput;
    public float speed;
    public float dashSpeed;
    public float dashSpeed2;
    public GameObject robot;
    public int dashes = 2;
    public float LANE_DISTANCE;
    public Vector3 targetPos;

    private Rigidbody2D robotBody;

    private float screenWidth;

    void Start()
    {
        screenWidth = Screen.width; //here just in case
        robotBody = robot.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > (float)15/16 * screenWidth) //dreapta
            {
                move(1.0f);
            }
            if (Input.GetTouch(i).position.x < screenWidth / 16) //stanga
            {
                move(-1.0f);
            }
            ++i;
        }
        //if (Time.timeScale >= 1f)
        //{
        if (MobileInput.Instance.SwipeUp && dashes>0)
        {
            targetPos = new Vector3(transform.position.x, transform.position.y + LANE_DISTANCE, transform.position.z);
            dashes--;
            Vector3 direction = targetPos - transform.position;
            robotBody.AddForce(transform.position + direction *  dashSpeed2 * Time.deltaTime);
        }
        if (MobileInput.Instance.SwipeDown && dashes>0)
        {
            float pound = 3.0f * dashSpeed2;
            targetPos = new Vector3(transform.position.x, transform.position.y - LANE_DISTANCE, transform.position.z);
            dashes--;
            Vector3 direction = targetPos - transform.position;
            robotBody.AddForce(transform.position + direction * pound * Time.deltaTime);
        }
        if (MobileInput.Instance.SwipeLeft && dashes > 0)
        {
            targetPos = new Vector3(transform.position.x - LANE_DISTANCE, transform.position.y, transform.position.z);
            dashes--;
            Vector3 direction = targetPos - transform.position;
            robotBody.MovePosition(transform.position + direction * dashSpeed * Time.deltaTime);
        }
        if (MobileInput.Instance.SwipeRight && dashes > 0)
        {
            targetPos = new Vector3(transform.position.x + LANE_DISTANCE, transform.position.y, transform.position.z);
            dashes--;
            Vector3 direction = targetPos - transform.position;
            robotBody.MovePosition(transform.position + direction * dashSpeed * Time.deltaTime);
        }
    }
    
    //functie pt editor
    private void FixedUpdate()
    {
#if UNITY_EDITOR
        move(Input.GetAxis("Horizontal"));
#endif
    }
    
    private void move(float horizontalInput) //butoane
    {
        robotBody.AddForce(new Vector2(horizontalInput * speed * Time.deltaTime, 0)); //functie pt miscare
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
