using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    private const float maxAngle = 10f;
    private IEnumerator currentShakeCoroutine;
    
    public void StartShake(Properties properties)
    {
        if (currentShakeCoroutine != null)
        {
            StopCoroutine(currentShakeCoroutine);
        }

        currentShakeCoroutine = Shake(properties);
        
        StartCoroutine(currentShakeCoroutine);
    }

    IEnumerator Shake(Properties properties)
    {
        float completionPercent = 0;
        float movePercent = 0; // How far the camera is between waypoints

        float angle_radians = properties.angle * Mathf.Deg2Rad - Mathf.PI;
        Vector3 previousWaypoint = Vector3.zero;
        Vector3 currentWaypoint = Vector3.zero;
        float moveDistance = 0; // Distance between 2 way points
        
        Quaternion targetRotation = Quaternion.identity;
        Quaternion previousRotation = Quaternion.identity;

        do
        {
            if (movePercent >= 1 || completionPercent == 0) // Reached curentWaypoint, now need to figure out next way point
            {
                float dampingFactor = DampingCurve(completionPercent, properties.dampingPercent);
                float noiseAngle = (Random.value - .5f) * 2 * Mathf.PI / 2f; // Random.Value = [0, 1]; 想要一个数介于[-PI/2, PI/2]，就先制造一个[-1, 1]，再乘 PI/2
                angle_radians += Mathf.PI + noiseAngle * properties.noisePercent; // 确保在⚪的下半部分晃动 
                currentWaypoint = new Vector3(Mathf.Cos(angle_radians), Mathf.Sin(angle_radians)) * properties.strength * dampingFactor; // 相机平面坐标移动
                previousWaypoint = transform.localPosition;
                moveDistance = Vector3.Distance(currentWaypoint, previousWaypoint);

                targetRotation = Quaternion.Euler(new Vector3(currentWaypoint.y, currentWaypoint.x).normalized * properties.rotationPercent * dampingFactor * maxAngle);
                previousRotation = transform.localRotation;
                
                movePercent = 0;
            }

            completionPercent += Time.deltaTime / properties.duration;
            movePercent += Time.deltaTime / moveDistance * properties.speed; // 如果想要保持一个恒定的速度，镜头要移动得越远，movePercent 增长就应该越慢
            transform.localPosition = Vector3.Lerp(previousWaypoint, currentWaypoint, movePercent);
            transform.localRotation = Quaternion.Slerp(previousRotation, targetRotation, movePercent);

            yield return null; // Wait for a frame between each execution of the while loop
        } while (moveDistance > 0); // 要是成立就一直循环
    }

    float DampingCurve(float x, float dampingPercent)
    {
        x = Mathf.Clamp01(x);
        float a = Mathf.Lerp(2, .25f, dampingPercent);
        float b = 1 - Mathf.Pow(x, a);
        return b * b * b;
    }
    
    [System.Serializable] // Attribute
    public class Properties // Subclass 
    {
        public float angle;    // 逆时针 0 90 180 270，initial direction of the shake 
        public float strength; // How far the shake can move the camera
        public float speed;    // How fast the camera move during the shake
        public float duration; // How long the shaking lasts 
        [Range(0,1)]
        public float noisePercent;   // Allow us to introduce some random variation of the shake
        [Range(0,1)]
        public float dampingPercent; // How quickly of the shake decreases of the time, 圆圈的缩小速度，趋近于 0 匀速缩小；趋近于 1 刚开始很快，越往后越慢
        [Range(0,1)]
        public float rotationPercent; // How much the movement of the shake affects camera's rotation

        public Properties(float angle, float strength, float speed, float duration, float noisePercent, float dampingPercent, float rotationPercent) // 构造函数
        {
            this.angle = angle;
            this.strength = strength;
            this.speed = speed;
            this.duration = duration;
            this.noisePercent = noisePercent;
            this.dampingPercent = dampingPercent;
            this.rotationPercent = rotationPercent;
        }
    }
}
