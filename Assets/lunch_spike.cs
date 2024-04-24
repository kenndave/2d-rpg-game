using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lunch_spike : MonoBehaviour
{
    bool shootable = true;
    public void shoot()
    {
            Object_pooling.instance.spawnfrompool("Rocket", transform.position, transform.rotation);
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
        int pickx;
        int picky;
        pickx = Random.Range(3, 11);
        picky = Random.Range(7, 16);
        Debug.Log("pickx" + pickx);
        Debug.Log("picky" + picky);
        shootable = false;
        transform.position = new Vector2(pickx, picky);
        shoot();
        yield return new WaitForSeconds(15);
        shootable = true;
    }
}
