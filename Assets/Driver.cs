using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float steeringSpeed = 200;
    [SerializeField] float movingSpeed = 10;
    [SerializeField] float slowSpeed = 5;
    [SerializeField] float boostSpeed = 20;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Booster")
        {
            movingSpeed = boostSpeed;
        }
        else if (other.tag == "Damper")
        {
            movingSpeed = slowSpeed;
        }
    }

    void Update()
    {
        float steeringAmount = Input.GetAxis("Horizontal") * steeringSpeed * Time.deltaTime;
        float movingAmount = Input.GetAxis("Vertical") * movingSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steeringAmount);
        transform.Translate(0, movingAmount, 0);
    }
}
