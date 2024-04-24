using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class range_attack : MonoBehaviour
{
    public range rangebd;
    bool attackava = true;
    public GameObject fire_point;
    public LayerMask msk;
    public AIPath aip;
    public float enemy_range=10;

    IEnumerator shooting()
    {
        attackava = false;
        yield return new WaitForSeconds(0.3f);
        rangebd.shoot();
        yield return new WaitForSeconds(rangebd.attacktime);
        attackava = true;
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Linecast(new Vector2(fire_point.GetComponent<Transform>().position.x, fire_point.GetComponent<Transform>().position.y), new Vector2(rangebd.player.position.x, rangebd.player.position.y - 0.5f), msk);
        if (hit|| !rangebd.chasing)
        {
            if (hit)
            {
            Debug.Log("hitr " + hit.collider.name);
            }
            aip.endReachedDistance = 0.1f;

        }
        else
        {
            Debug.Log("not hitr");
            if (attackava&&rangebd.player.GetComponent<ninja>().invis!=1)
            {
                StartCoroutine(shooting());
            }
                aip.endReachedDistance = enemy_range;
            
            
        }
    }
}
