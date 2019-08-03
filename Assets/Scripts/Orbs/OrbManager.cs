using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    public SoundManager soundManager;
    private int activeColor;
    private float activeTime;
    private bool colorActive;
    [SerializeField]
    public ColorChangeHandler handler;

    // Start is called before the first frame update
    void Start()
    {
        colorActive = false;
    }

    private void Update()
    {
        if (colorActive)
        {
            activeTime -= Time.deltaTime;
            if (activeTime <= 0f)
            {
                Deactivate(activeColor);
            }
        }
    }


    public void Activated(int color, float time)
    {
        soundManager.AddTime(color, time);
        if (!colorActive)
        {
            
            activeTime = time;
            Activate(color);
            
        }
        else
        {
            
            if (color != activeColor)
            {
                Deactivate(activeColor);
                
                activeTime = time;
                Activate(color);

            }
            else
            {
                activeTime += time;
            }

        }
    }
    void Activate(int color)
    {
        colorActive = true;
        activeColor = color;
        
        handler.Activate(color);

    }
    void Deactivate(int color)
    {
        colorActive = false;
        
        handler.Deactivate(color);
    }

}
