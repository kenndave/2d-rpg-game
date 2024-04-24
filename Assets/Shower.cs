using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator camAnim;
    public float timer;
    public float limit;
    public AudioSource meteorsound;
    public void CamShake()
    {
        camAnim.SetTrigger("fire");
    }
    public void Start()
    {
        CamShake();
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= limit)
        {
            if (meteorsound)
            {
                meteorsound.enabled = false;
            }
            camAnim.ResetTrigger("fire");
            camAnim.SetTrigger("stop");
        }
    }
}
