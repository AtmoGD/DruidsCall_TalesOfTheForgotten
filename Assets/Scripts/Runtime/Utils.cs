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

    public static float Remap(float _value, float _from1, float _to1, float _from2, float _to2)
    {
        return (_value - _from1) / (_to1 - _from1) * (_to2 - _from2) + _from2;
    }

    public static float Remap01(float _value, float _from1, float _to1)
    {
        return Remap(_value, _from1, _to1, 0f, 1f);
    }
}
