using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timing : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public float limit;
    public Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= limit)
        {
            anim.SetTrigger("movin");
        }
    }
}
