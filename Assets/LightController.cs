using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light directionalLight;  // Directional Light
    public Transform player;        // player object
    public float lightIncreaseSpeed = 1f;
    public float maxLightIntensity = 1f;

    private bool shouldIncreaseLight = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check coordinates
        if (player.position.x <= -35 && player.position.z >= -15 && player.position.z <= -5)
        {
            shouldIncreaseLight = true;
        }

        // Increase light
        if (shouldIncreaseLight && directionalLight.intensity < maxLightIntensity)
        {
            directionalLight.intensity += lightIncreaseSpeed * Time.deltaTime;
        }

    }
}
