using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

public static class RandomExtensions
{
    public static double NextDouble(
        this Random random,
        double minValue,
        double maxValue)
    {
        return random.NextDouble() * (maxValue - minValue) + minValue;
    }
}
