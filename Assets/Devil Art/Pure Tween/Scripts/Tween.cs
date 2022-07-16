using UnityEngine;

namespace PureTween
{
    public static class Tween
    {
        private const float HALF_PI = Mathf.PI * .5f;
        private const float DOUBLE_PI = Mathf.PI * 2.0f;

        public static float Linear(float progress, float from, float to)
        {
            return from + (to - from) * progress;
        }
        public static float InQuad(float progress, float from, float to)
        {
            return from + (to - from) * progress * progress;
        }
        public static float OutQuad(float progress, float from, float to)
        {
            return from - (to - from) * progress * (progress - 2.0f);
        }
        public static float InOutQuad(float progress, float from, float to)
        {
            if (progress < 0.5f)
                return from + (to - from) * progress * progress * 2.0f;
            return from - (to - from) * (2.0f * progress * (progress - 2.0f) + 1.0f);
        }
        public static float InCubic(float progress, float from, float to)
        {
            return from + (to - from) * progress * progress * progress;
        }
        public static float OutCubic(float progress, float from, float to)
        {
            progress -= 1.0f;
            return from + (to - from) * (progress * progress * progress + 1.0f);
        }
        public static float InOutCubic(float progress, float from, float to)
        {
            if (progress < 0.5f)
                return from + (to - from) * 4.0f * progress * progress * progress;
            else
            {
                progress -= 1.0f;
                return from + (to - from) * (4.0f * progress * progress * progress + 1.0f);
            }
        }
        public static float InQuart(float progress, float from, float to)
        {
            return from + (to - from) * progress * progress * progress * progress;
        }
        public static float OutQuart(float progress, float from, float to)
        {
            progress -= 1.0f;
            return from + (to - from) * (progress * progress * progress * (-progress) + 1.0f);
        }
        public static float InOutQuart(float progress, float from, float to)
        {
            if (progress < 0.5f)
                return from + (to - from) * 8.0f * progress * progress * progress * progress;
            else
            {
                progress -= 1.0f;
                return from + (to - from) * (-8.0f * progress * progress * progress * progress + 1.0f);
            }
        }
        public static float InQuint(float progress, float from, float to)
        {
            return from + (to - from) * progress * progress * progress * progress * progress;
        }
        public static float OutQuint(float progress, float from, float to)
        {
            progress -= 1.0f;
            return from + (to - from) * (progress * progress * progress * progress * progress + 1.0f);
        }
        public static float InOutQuint(float progress, float from, float to)
        {
            if (progress < 0.5f)
                return from + (to - from) * 16.0f * progress * progress * progress * progress * progress;
            else
            {
                progress -= 1.0f;
                return from + (to - from) * (16.0f * progress * progress * progress * progress * progress + 1.0f);
            }
        }
        public static float InSine(float progress, float from, float to)
        {
            return from + (to - from) * (1.0f - Mathf.Cos(progress * HALF_PI));
        }
        public static float OutSine(float progress, float from, float to)
        {
            return from + (to - from) * Mathf.Sin(progress * HALF_PI);
        }
        public static float InOutSine(float progress, float from, float to)
        {
            return from + (to - from) * 0.5f * (1.0f - Mathf.Cos(progress * Mathf.PI));
        }
        public static float InExp(float progress, float from, float to)
        {
            if (progress == 0.0f)
                return from;
            else
                return from + (to - from) * Mathf.Pow(2.0f, 10.0f * (progress - 1.0f));
        }
        public static float OutExp(float progress, float from, float to)
        {
            if (progress == 1.0f)
                return to;
            else
                return from + (to - from) * (1.0f - Mathf.Pow(2.0f, -10.0f * progress));
        }
        public static float InOutExp(float progress, float from, float to)
        {
            if (progress == 0.0f)
                return from;
            if (progress == 1.0f)
                return to;
            if (progress < 0.5f)
                return from + (to - from) * 0.5f * Mathf.Pow(2.0f, 20.0f * progress - 10.0f);
            else
                return from + (to - from) * 0.5f * (2.0f - Mathf.Pow(2.0f, -20.0f * progress + 10.0f));
        }
        public static float InCirc(float progress, float from, float to)
        {
            return from + (to - from) * (1.0f - Mathf.Sqrt(1.0f - progress * progress));
        }
        public static float OutCirc(float progress, float from, float to)
        {
            return from + (to - from) * Mathf.Sqrt((2.0f - progress) * progress);
        }
        public static float InOutCirc(float progress, float from, float to)
        {
            if (progress < 0.5f)
                return from + (to - from) * 0.5f * (1.0f - Mathf.Sqrt(1.0f - 4.0f * progress * progress));
            else
            {
                progress = progress * 2.0f - 2.0f;
                return from + (to - from) * 0.5f * (Mathf.Sqrt(1.0f - progress * progress) + 1.0f);
            }
        }
        public static float InElastic(float progress, float from, float to)
        {
            if (progress == 0.0f)
                return from;
            if (progress == 1.0f)
                return to;
            return from + (to - from) * -Mathf.Pow(2.0f, 10.0f * progress - 10.0f) * Mathf.Sin((3.33f * progress - 3.58f) * DOUBLE_PI);
        }
        public static float OutElastic(float progress, float from, float to)
        {
            if (progress == 0.0f)
                return from;
            if (progress == 1.0f)
                return to;
            return from + (to - from) * (Mathf.Pow(2.0f, -10.0f * progress) * Mathf.Sin((3.33f * progress - 0.25f) * DOUBLE_PI) + 1.0f);
        }
        public static float InOutElastic(float progress, float from, float to)
        {
            if (progress == 0.0f)
                return from;
            if (progress == 1.0f)
                return to;
            if (progress < 0.5f)
                return from + (to - from) * -0.5f * Mathf.Pow(2.0f, 20.0f * progress - 10.0f) * Mathf.Sin((4.45f * progress - 2.475f) * DOUBLE_PI);
            else
                return from + (to - from) * (Mathf.Pow(2.0f, -20.0f * progress + 10.0f) * Mathf.Sin((4.45f * progress - 2.475f) * DOUBLE_PI) * 0.5f + 1.0f);
        }
        public static float InBack(float progress, float from, float to)
        {
            return from + (to - from) * progress * progress * (2.70158f * progress - 1.70158f);
        }
        public static float OutBack(float progress, float from, float to)
        {
            progress -= 1.0f;
            return from + (to - from) * (progress * progress * (2.70158f * progress + 1.70158f) + 1.0f);
        }
        public static float InOutBack(float progress, float from, float to)
        {
            if (progress < 0.5f)
                return from + (to - from) * progress * progress * (14.379636f * progress - 5.189818f);
            else
            {
                progress -= 1.0f;
                return from + (to - from) * (progress * progress * (14.379636f * progress + 5.189818f) + 1.0f);
            }
        }
        public static float InBounce(float progress, float from, float to)
        {
            if (progress > 0.636364f)
            {
                progress = 1.0f - progress;
                return from + (to - from) * (1.0f - 7.5625f * progress * progress);
            }
            else if (progress > 0.27273f)
            {
                progress = 0.454546f - progress;
                return from + (to - from) * (0.25f - 7.5625f * progress * progress);
            }
            else if (progress > 0.090909f)
            {
                progress = 0.181818f - progress;
                return from + (to - from) * (0.0625f - 7.5625f * progress * progress);
            }
            else
            {
                progress = 0.045455f - progress;
                return from + (to - from) * (0.015625f - 7.5625f * progress * progress);
            }
        }
        public static float OutBounce(float progress, float from, float to)
        {
            if (progress < 0.363636f)
                return from + (to - from) * 7.5625f * progress * progress;
            else if (progress < 0.72727f)
            {
                progress -= 0.545454f;
                return from + (to - from) * (7.5625f * progress * progress + 0.75f);
            }
            else if (progress < 0.909091f)
            {
                progress -= 0.818182f;
                return from + (to - from) * (7.5625f * progress * progress + 0.9375f);
            }
            else
            {
                progress -= 0.954545f;
                return from + (to - from) * (7.5625f * progress * progress + 0.984375f);
            }
        }
        public static float InOutBounce(float progress, float from, float to)
        {
            if (progress < 0.5f)
            {
                if (progress > 0.318182f)
                {
                    progress = 1.0f - progress * 2.0f;
                    return from + (to - from) * (0.5f - 3.78125f * progress * progress);
                }
                else if (progress > 0.136365f)
                {
                    progress = 0.454546f - progress * 2.0f;
                    return from + (to - from) * (0.125f - 3.78125f * progress * progress);
                }
                else if (progress > 0.045455f)
                {
                    progress = 0.181818f - progress * 2.0f;
                    return from + (to - from) * (0.03125f - 3.78125f * progress * progress);
                }
                else
                {
                    progress = 0.045455f - progress * 2.0f;
                    return from + (to - from) * (0.007813f - 3.78125f * progress * progress);
                }
            }
            if (progress < 0.681818f)
            {
                progress = progress * 2.0f - 1.0f;
                return from + (to - from) * (3.78125f * progress * progress + 0.5f);
            }
            else if (progress < 0.863635f)
            {
                progress = progress * 2.0f - 1.545454f;
                return from + (to - from) * (3.78125f * progress * progress + 0.875f);
            }
            else if (progress < 0.954546f)
            {
                progress = progress * 2.0f - 1.818182f;
                return from + (to - from) * (3.78125f * progress * progress + 0.96875f);
            }
            else
            {
                progress = progress * 2.0f - 1.954545f;
                return from + (to - from) * (3.78125f * progress * progress + 0.992188f);
            }
        }
    }
}