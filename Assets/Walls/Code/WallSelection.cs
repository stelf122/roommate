using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSelection : MonoBehaviour
{
    public static event Action<WallSelection> Selected = delegate { };

    private void OnMouseDown()
    {
        Selected(this);
    }
}
