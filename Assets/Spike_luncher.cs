using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_luncher : MonoBehaviour
{
    public Transform[] firepoint;
    bool shootable=true;
    public int middlepoint=2;
    public void shoot(int loc)
    {
        if (loc <= middlepoint)
        {
        Object_pooling.instance.spawnfrompool("Sleft", firepoint[loc].position, firepoint[loc].rotation);
        }
        else
        {
        Object_pooling.instance.spawnfrompool("Sright", firepoint[loc].position, firepoint[loc].rotation);
        }
        
        
    }

    void Update()
    {
        if (shootable)
        {
            StartCoroutine(spikelunch());
        }
    }
    IEnumerator spikelunch()
    {
        int pick;
        pick = Random.Range(0, 6);
        Debug.Log("pick" + pick);
        shoot(pick);
        shootable = false;
        yield return new WaitForSeconds(5);
        shootable = true;
    }
}
