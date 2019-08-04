using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class BoardManager : MonoBehaviour
{
    // Private Variables
    private Vector3 posCheck = new Vector3(10000, 10000, 10000); // A check for respawn purposes
    private int level = 1;
    private float timeRate = 1f;

    public GameObject Player;
    private PlayerAnim P_Anim;

    public SoundManager soundManager;

    public SpriteShape Neutral;
    public SpriteShape Green;
    public SpriteShape Orange;
    public SpriteShape Red;
    public SpriteShape Blue;
    public SpriteShape Yellow;

    public Dictionary<int, SpriteShape> colors = new Dictionary<int, SpriteShape>();
    public Dictionary<int, Color> orb_colors = new Dictionary<int, Color>();
    public SpriteShape current_color = null;

    // Public Variables
    public Vector3 respawn;
    public bool gameOver = false;
    public float color_time;

    private void Awake()
    {
        //add colors to dictionary for outside access.
        colors.Add(0, Neutral);
        orb_colors.Add(0, new Color(0.4784314f, 0.2313726f, 0.2196079f, 1f));

        colors.Add(1, Green);
        orb_colors.Add(1, new Color(0.3843138f, 0.7058824f, 0.3843138f, 1f));

        colors.Add(2, Orange);
        orb_colors.Add(2, new Color(0.8666667f, 0.4039216f, 0.1372549f, 1f));

        colors.Add(3, Red);
        orb_colors.Add(3, new Color(0.7372549f, 0.1098039f, 0.1098039f, 1f));

        colors.Add(4, Blue);
        orb_colors.Add(4, new Color(0.5803922f, 0.7450981f, 0.9058824f, 1f));

        colors.Add(5, Yellow);
        orb_colors.Add(5, new Color(0.9058824f, 0.8117648f, 0.4509804f, 1f));
    }

    // Start is called before the first frame update
    void Start()
    {
        ///StartCoroutine(GameTime());
        if (Player != null)
            P_Anim = Player.GetComponent<PlayerAnim>();
        set_color(0, 0);
        //StartCoroutine(GameTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void set_color(int key, float time)
    {

        current_color = colors[key];
        gameObject.BroadcastMessage("update_color", key);
        gameObject.BroadcastMessage("call_fade_out", time);
        if(key != 0)//There is no sound for neutral, so skip adding sound!
        {
            update_time(key, time);

        }
        if (Player != null)
        {
            P_Anim.ChangeColor(key);
        }
    }

    //Called to setup up the level.
    public void Setup_Level(GameObject player)
    {
        respawn = posCheck;

        if (player != null)
        {
            /// Do Something
        }
    }


    // Do something every given amount of seconds;
    IEnumerator GameTime()
    {
        float c_time = 0f;
        while (!gameOver)
        {
            c_time += 1;
            print(c_time);
            yield return new WaitForSeconds(timeRate);
            /// Call function
        }
    }

    public void update_time(int color, float newTime)
    {
        //Do time things
        if (soundManager != null)
            soundManager.AddTime(color, newTime);
    }

}

