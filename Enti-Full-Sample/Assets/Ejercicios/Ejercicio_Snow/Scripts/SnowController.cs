using UnityEngine;

public class SnowController : MonoBehaviour
{
    public enum SnowState
    {
        Rising,
        HoldMax,
        Falling,
        HoldMin
    }

    public float durationUp = 5f;
    public float holdMax = 3f;
    public float durationDown = 5f;
    public float holdMin = 3f;

    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public Material snowMaterial;

    private SnowState state;
    private float t;

    void Update()
    {
        t += Time.deltaTime;

        switch (state)
        {
            case SnowState.Rising:
                SetSnow(Mathf.Lerp(0, 1, curve.Evaluate(t / durationUp)));
                if (t >= durationUp)
                {
                    t = 0;
                    state = SnowState.HoldMax;
                }
                break;

            case SnowState.HoldMax:
                SetSnow(1f);
                if (t >= holdMax)
                {
                    t = 0;
                    state = SnowState.Falling;
                }
                break;

            case SnowState.Falling:
                SetSnow(Mathf.Lerp(1, 0, curve.Evaluate(t / durationDown)));
                if (t >= durationDown)
                {
                    t = 0;
                    state = SnowState.HoldMin;
                }
                break;

            case SnowState.HoldMin:
                SetSnow(0f);
                if (t >= holdMin)
                {
                    t = 0;
                    state = SnowState.Rising;
                }
                break;
        }
    }

    void SetSnow(float value)
    {
        if (snowMaterial != null)
            snowMaterial.SetFloat("_Snow_Amount", value);
    }
}