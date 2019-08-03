using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Implement for handling orb activation and timer resets
/// </summary>
public abstract class ColorChangeHandler : MonoBehaviour
{
    public abstract void Activate(int color);
 

    public abstract void Deactivate(int color);
}
