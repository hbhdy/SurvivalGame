using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilFunction
{
    public static int CalcDamage(float damage, float defence, bool critical = false)
    {
        if (damage <= 0)
            damage = 0;

        float result = damage;

        result = result - defence;

        if (result <= 0)
            result = 0;

        return (int)result == 0 ? 1 : (int)result;
    }
}
