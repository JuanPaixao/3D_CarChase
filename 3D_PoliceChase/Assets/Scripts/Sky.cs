using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    // Scroll main texture based on time

    float scrollSpeed = 0.5f;
    Renderer rend;
    public float offset, lightBehavior;
    public Light directionalLight;
    public string dayTime;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset += Time.deltaTime / 5 * scrollSpeed / 10;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

        if (offset <= 0.40f || offset >= 0.90f)
        {
            dayTime = "day";
        }
        else if (offset >= 0.41f && offset <= 0.89f)
        {
            dayTime = "night";
        }
        if (offset >= 1)
        {
            offset = 0;
        }
        if (dayTime == "day")
        {
            lightBehavior += Time.deltaTime / 5 * scrollSpeed / 3.75f;
        }
        else
        {
            lightBehavior -= Time.deltaTime / 5 * scrollSpeed / 5f;
        }
        if (lightBehavior >= 0.90f)
        {
            lightBehavior = 0.90f;
        }
        if (lightBehavior <= 0.0f)
        {
            lightBehavior = 0.0f;
        }
        directionalLight.intensity = Mathf.Lerp(0.9f, 0f, lightBehavior);
    }
}