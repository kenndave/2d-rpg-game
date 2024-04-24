using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    public Animator camAnim;
    public float timer;
    public float limit;
    public void CamShake()
    {
        camAnim.SetTrigger("shake");
    }

    public void CamStop()
    {
        camAnim.ResetTrigger("shake");
        camAnim.SetTrigger("stop");
    }

    public void Start()
    {
        timer = 0;
        CamShake();
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= limit)
        {
            CamStop();
        }
    }
}
