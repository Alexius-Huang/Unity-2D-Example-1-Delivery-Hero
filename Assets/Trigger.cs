using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Triggered!");
    }
}
