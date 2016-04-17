using UnityEngine;
using System.Collections;

public class CameraShaker : MonoBehaviour {

    private bool shaking = false;
    private float shakeIntensity = 0.0f;
    private float shakeTimeLeft = 0.0f;

    public void Shake(float intensity, float duration)
    {
        shakeIntensity = intensity;
        shakeTimeLeft = duration;
        if(!shaking)
        {
            shaking = true;
            StartCoroutine(ShakeNBake());
        }
    }

    private IEnumerator ShakeNBake()
    {
        float shakeStartTime = Time.time;
        Vector3 shakeDir0 = Random.insideUnitCircle;
        float shakeSeed0 = Random.Range(60.0f, 90.0f);
        Vector3 shakeDir1 = Random.insideUnitCircle;
        float shakeSeed1 = Random.Range(60.0f, 90.0f);

        float shakeIntensity0 = Random.Range(0.0f, Mathf.Clamp( shakeIntensity / 10.0f, 0.1f, 0.3f));
        float shakeIntensity1 = Random.Range(0.0f, Mathf.Clamp( shakeIntensity / 10.0f, 0.1f, 0.3f));

        while (shakeTimeLeft > 0.0f)
        {
            transform.localPosition = shakeDir0 * Mathf.Sin((Time.time - shakeStartTime) * shakeSeed0) * shakeIntensity0 + shakeDir1 * Mathf.Sin((Time.time - shakeStartTime) * shakeSeed1 * shakeIntensity1);
            shakeTimeLeft -= Time.deltaTime;
            yield return null;
        }
        shaking = false;
    }
}
