using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTest : MonoBehaviour
{
    public CameraShake.Properties testProperties; // testProperties 是显示在 inspector 里的名
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<CameraShake>().StartShake(testProperties);
            //FindObjectOfType<CameraShake>().StartShake(new CameraShake.Properties());
        }
    }
}
