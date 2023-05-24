

using EasingAnimation;
using UnityEngine;

public class EaseTest : MonoBehaviour
{
    public Transform target;
    public Vector3 targetScale;
    public float duration = 1.0f;
    public float amplitude = 1.0f;
    public float period = 0.3f;

    private Vector3 initialScale;
    private float elapsedTime = 0f;

    private void Start()
    {
        initialScale = transform.localScale;
    }

    private void Update()
    {
        if (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float easedT = Easing.EaseOutElastic(t, amplitude, period);

            transform.localScale = Vector3.Lerp(initialScale, targetScale, easedT);

            elapsedTime += Time.deltaTime;
        }
        else
        {
            transform.localScale = targetScale;
        }
    }
}
