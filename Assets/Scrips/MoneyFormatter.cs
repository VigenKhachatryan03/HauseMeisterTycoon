using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public static class MoneyFormatter
{
    /* 
     * 500 = 500
     * 5000 = 5K
     * 500 000 = 500K
     * 5 000 000 = 5M
     * 5 000 000 000 = 5B
     */

    public static string FormatMoney(BigInteger value)
    {
        string moneyFormat = "{0}";

        if (value >= 1000000000)
        {
            moneyFormat = "{0:#,0,,, B}";
        }
        else if(value >= 1000000)
        {
            moneyFormat = "{0:#,0,, M}";  
        }else if (value >= 1000)
        {
            moneyFormat = "{0:#,0, K}";  
        }
        return string.Format(moneyFormat + "€", value);
    }
}
