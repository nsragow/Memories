using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbManager : MonoBehaviour
{
    private int activeColor;
    private float activeTime;
    private bool active;
    [SerializeField]
    public ColorChangeHandler handler;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    private void Update()
    {
        if (active)
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
        if (!active)
        {
            
            activeTime = time;
            Activate(color);
        }
        else
        {
            if(color != activeColor)
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
        active = true;
        activeColor = color;
        
        handler.Activate(color);

    }
    void Deactivate(int color)
    {
        active = false;
        
        handler.Deactivate(color);
    }

}
