using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All constants go here.
/// </summary>
public class Constants
{
    public const int movement_left = -1;
    public const int movement_idle = 0;
    public const int movement_right = 1;

    public const float groundRaycast_offsetY = -0.3f; //Adapt this to the player's size
    public const float groundRaycast_lenght = 0.4f;
    public const float wallJumpRaycast_lenght = 0.4f;

    public static Vector2 wallJumpForce = new Vector2(9f, 12f);

    public const string tag_platform = "Platform";
}
