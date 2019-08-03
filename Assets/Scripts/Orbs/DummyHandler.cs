using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHandler : ColorChangeHandler
{
    public override void Activate(int color)
    {
        Debug.LogWarning("color active!");
    }

    public override void Deactivate(int color)
    {
        Debug.LogWarning("color deactive!");
    }
}
