using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class totalBananas
{
    private static int _bananas;

    // Toegang te krijgen tot het aantal bananen
    public static int bananas
    {
        get { return _bananas; }
        set
        {
            _bananas = value;
            OnBananasChanged?.Invoke(_bananas); // Roep het event aan dat er iets aan de bananen is gewijzigd
        }
    }

    public static event Action<int> OnBananasChanged;
}