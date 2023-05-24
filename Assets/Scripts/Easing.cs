using System;
using UnityEngine;

//refernce - https://gist.github.com/Kryzarel/bba64622057f21a1d6d44879f9cd7bd4

namespace EasingAnimation
{
    public class Easing
    {
        // Ease Out Expo
        public static float EaseOutExpo(float t)
        {
            return Mathf.Approximately(t, 1f) ? 1f : 1f - Mathf.Pow(2f, -10f * t);
        }

        public static float OutElastic(float t)
        {
            float p = 0.3f;
            return (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t - p / 4) * (2 * Math.PI) / p) + 1;
        }

        public static float EaseOutElastic(float t, float amplitude, float period)
        {
            if (t == 0f || t == 1f)
                return t;

            float s = period / (2f * Mathf.PI) * Mathf.Asin(1f / amplitude);
            return (amplitude * Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - s) * (2f * Mathf.PI) / period) + 1f);
        }
    }
}