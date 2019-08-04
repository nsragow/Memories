using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public float maxVolume;
    public AudioClip switchSound;
    public float switchVolume;
    //These need to be in ascending order
    [SerializeField]
    public float[] timeStamps;
    
    [SerializeField]
    public AudioClip[] clips;
    [SerializeField]
    public int[] colorIds;
    private AudioSource[] sources;
    private float[] fadeDurations;


    private AudioSource switchSource;
    private bool active;
    private int activeColor;
    private int index;

    private float timeLeftSubSection;

    private int currentClip;




    // Start is called before the first frame update
    void Start()
    {
        switchSource = gameObject.AddComponent<AudioSource>();
        switchSource.volume = switchVolume;


        if(timeStamps.Length != clips.Length || colorIds.Length != clips.Length)
        {
            Debug.LogError("Array length in Sound Manager must all match!;");
        }
        sources = new AudioSource[clips.Length];
        fadeDurations = new float[clips.Length];
        float lastFadeTime = 0f;
        int lastColor = colorIds[0];
        for(int i = 0; i < clips.Length; i++)
        {
            float currentFadeTime = timeStamps[i];
            if(lastColor != colorIds[i])
            {
                lastFadeTime = 0f;
                lastColor = colorIds[i];
            }
            fadeDurations[i] = timeStamps[i] - lastFadeTime;
            lastFadeTime = timeStamps[i];
            AudioSource aS = gameObject.AddComponent<AudioSource>();
            aS.clip = clips[i];
            aS.volume = 0f;
            aS.loop = true;
            aS.Play();

            sources[i] = aS;
        }
    }

    public void AddTime(int color, float time)
    {
        print("adding time");
        if (active)
        {
            if(color == activeColor)
            {
                AddCurrentTime(time);
                
            }
            else
            {
                TurnOffCurrentSounds();
                StartSound(color, time);
            }
        }
        else
        {
            active = true;
            StartSound(color, time);
        }
    }

    private void Update()
    {
        if (active)
        {
            RemoveTime(Time.deltaTime);
        }
    }

    private void TurnOffCurrentSounds()
    {
        foreach(AudioSource s in sources)
        {
            s.volume = 0f;
        }
        active = false;
        timeLeftSubSection = 0f;
        
    }
    private void StartSound(int color, float time)
    {
        switchSource.PlayOneShot(switchSound);
        active = true;
        activeColor = color;

        timeLeftSubSection = 0f;
        index = 0;
        bool found = false;
        for(; index < colorIds.Length; index++)
        {
            if (colorIds[index] == color)
            {
                found = true;
                //this is the right index to start
                break;
            }
        }
        if (!found)
        {
            Debug.LogError("did not find the right color!!!!");
        }

        AddCurrentTime(time);
        
    }
    private void AddCurrentTime(float time)
    {
        float leftoverTime = 0f;
        //timerLeft += time;
        timeLeftSubSection += time;
        if(timeLeftSubSection > fadeDurations[index])
        {
            leftoverTime = timeLeftSubSection - fadeDurations[index];
            sources[index].volume = maxVolume;
            print("playing " + index);
            SoundUp();
            AddCurrentTime(leftoverTime);
        }
        else
        {
            float volumeIncrease = TimeToVolume(time);
            sources[index].volume = Math.Min(maxVolume, sources[index].volume + volumeIncrease);
            
        }
    }
    private void RemoveTime(float time)
    {
        float leftoverTime = 0f;
        //timerLeft += time;
        timeLeftSubSection -= time;
        if (timeLeftSubSection < 0)
        {
            leftoverTime = timeLeftSubSection;
            sources[index].volume = 0f;
            print("turning off " + index);
            SoundDown();
            RemoveTime(leftoverTime);
        }
        else
        {
            float volumeIncrease = TimeToVolume(time);
            if (sources[index].volume - volumeIncrease <= 0)
            {
                print("reached zero for " + index);
            }
            sources[index].volume = Math.Max(sources[index].volume - volumeIncrease,0f);
        }
    }

    private float TimeToVolume(float time)
    {
        float maxDuration = fadeDurations[index];

        float normVol = (time / maxDuration) * maxVolume;
        return normVol;
    }

    private void SoundDown()
    {
        //what about moving to next color
        if(index-1>=0 && colorIds[index] == colorIds[index - 1])
        {
            index--;
            active = true;

        }
        else
        {
            active = false;
        }
        timeLeftSubSection = fadeDurations[index];
    }
    private void SoundUp()
    {
        if(!(index+1 >= sources.Length || colorIds[index] != colorIds[index + 1])) // this is the end of the color
        {
            index++;

        }
        active = true;
        timeLeftSubSection = 0f;
    }
}
