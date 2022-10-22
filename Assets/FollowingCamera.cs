using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    // Camera's position should follow the driver
    [SerializeField] GameObject objectToFollow;

    void LateUpdate()
    {
        transform.position = objectToFollow.transform.position - new Vector3(0, 0, 10);
    }
}
