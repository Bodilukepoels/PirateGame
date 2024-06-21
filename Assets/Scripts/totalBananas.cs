using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class totalBananas
{
    private static int _bananas;
    public static int bananas
    {
        get { return _bananas; }
        set
        {
            _bananas = value;
            OnBananasChanged?.Invoke(_bananas);
        }
    }

    public static event Action<int> OnBananasChanged;
}
