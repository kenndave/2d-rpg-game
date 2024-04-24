using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brute_attack : MonoBehaviour
{
    public Brute brutebd;
    bool attackava=true;
    public GameObject melee_range;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attackava&&brutebd.chasing)
        {
            StartCoroutine(attacking());
        }
        
    }
    IEnumerator attacking()
    {
        attackava = false;
        yield return new WaitForSeconds(0.3f);
        brutebd.attack();
        melee_range.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        melee_range.SetActive(false);
        yield return new WaitForSeconds(brutebd.attacktime);
        attackava = true;
    }
}
