using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    private Vector3 offset;   // The current offset between camera and player
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        // Set the camera's position to the player's position plus the stored offset
        transform.position = player.position + offset;
    }
}