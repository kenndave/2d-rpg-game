using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public float limit;
    private bool fadeyet = false;
    public Animator bg;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= limit)
        {
            bg.SetTrigger("white");
            this.fadeyet = true;
            timer = 0;
        }
        if (fadeyet && timer >= 1)
        {
            bg.SetTrigger("flash");
        }
    }
}
