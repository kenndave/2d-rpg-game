using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOS : MonoBehaviour
{
    public Transform body;
    public Transform player;
    public Vector3 target;
    public LayerMask msk;
    public int mask;
    public Chase ch;
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("collision");
        target = collision.GetComponent<Transform>().position;
        RaycastHit2D hit = Physics2D.Linecast(new Vector2(body.position.x, body.position.y), new Vector2(collision.GetComponent<Transform>().position.x, collision.GetComponent<Transform>().position.y - 0.5f), msk);
        if (hit)
        {
            Debug.Log("hit " + hit.collider.name);
        }
        else
        {
            Debug.Log("not hit");
            gameObject.GetComponent<Transform>().localScale = new Vector3(ch.chase_range,ch.chase_range, 1);
            ch.chasing = true;
            StopAllCoroutines();
            StartCoroutine(ch.backtopatrol());
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(body.position,new Vector3(target.x,target.y-0.5f,target.z));
    }
    public void stopingcoroutines()
    {
        StopAllCoroutines();
    }
}
