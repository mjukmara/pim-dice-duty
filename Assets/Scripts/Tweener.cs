using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tweener
{
    public static IEnumerator RunTween(Func<float, float, float, float> TweenFunction, float speed, Action<float> progressCallback, Action onFinishCallback)
    {
        float startedAt = Time.time;
        float endAt = Time.time + 1f / speed;
        float range = endAt - startedAt;

        while (Time.time - startedAt < 1f)
        {
            float currentAt = Time.time - startedAt;
            if (currentAt > endAt) break;

            float progress = currentAt / range;
            float result = TweenFunction(progress/speed, 0f, 1f);
            progressCallback?.Invoke(result);
            yield return null;
        }
        onFinishCallback?.Invoke();
    }
}
