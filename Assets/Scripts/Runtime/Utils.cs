using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float FindTimeInCurve(AnimationCurve _curve, float _value, float _stepSize = 0.01f)
    {
        float time = 0f;
        float value = 0f;
        while (value < _value && time < _curve.keys[^1].time)
        {
            value = _curve.Evaluate(time);
            time += _stepSize;
        }
        return time;
    }
}
