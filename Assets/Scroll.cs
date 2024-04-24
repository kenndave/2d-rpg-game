using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public float limit;
    public Animator scroll;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= limit)
        {
            scroll.SetTrigger("scroll");
        }

    }
}
