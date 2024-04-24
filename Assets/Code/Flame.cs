using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public float limit;
    public Animator flm;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= limit)
        {
            flm.SetTrigger("now");
        }
    }
}
