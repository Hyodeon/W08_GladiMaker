using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;  // 카메라의 트랜스폼
    public float shakeDuration = 0.5f; // 흔들리는 시간
    public float shakeAmount = 0.7f;   // 흔들리는 강도
    public float decreaseFactor = 1.0f; // 흔들림이 줄어드는 속도
    bool isShakeStart = false;

    public void startCameraShake(float shakeAmount, float shakeTime)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine(shakeAmount, shakeTime));
    }



    IEnumerator ShakeCoroutine(float shakeAmount, float shakeTime)
    {
        float elapsed = 0.0f;

        while (elapsed < shakeTime)
        {
            float x = Random.Range(-1f, 1f) * shakeAmount;
            float y = Random.Range(-1f, 1f) * shakeAmount;

            cameraTransform.localPosition = new Vector3(transform.position.x + x, transform.position.y + y, -10);

            elapsed += Time.deltaTime;

            yield return null;
        }
        cameraTransform.position = new Vector3(0, 0, -10);
    }
}
