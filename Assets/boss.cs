using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class boss : MonoBehaviour
{

    public Transform player;
    protected AIDestinationSetter dtarget;
    public GameObject body;
    public GameObject fire_point;
    public LayerMask msk;
    public int attacktime = 4;
    bool attackava = true;
    Transform bulletPos;
    public Animator animator;
    void Start()
    {
        bulletPos = fire_point.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();//gak perlu manual taruh
        dtarget = body.GetComponent<AIDestinationSetter>();
        dtarget.target = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -body.transform.rotation.z);
        } 
    }
    IEnumerator shooting()
    {
        attackava = false;
        yield return new WaitForSeconds(0.3f);
        Object_pooling.instance.spawnfrompool("Laser", bulletPos.position, bulletPos.rotation);
        Object_pooling.instance.spawnfrompool("Laser", (bulletPos.position) + new Vector3(-1.7f, 0, 0), bulletPos.rotation);
        yield return new WaitForSeconds(attacktime);
        attackava = true;
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Linecast(new Vector2(fire_point.GetComponent<Transform>().position.x, fire_point.GetComponent<Transform>().position.y), new Vector2(player.position.x, player.position.y - 0.5f), msk);
        if (hit)
        {
            if (hit)
            {
                Debug.Log("hitr " + hit.collider.name);
            }
            animator.SetBool("Attack", false);
        }
        else
        {
            Debug.Log("not hitr");
            if (attackava )
            {
                animator.SetBool("Attack",true);
                StartCoroutine(shooting());
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ninja player = collision.GetComponent<ninja>();
        if (player)
        {
            player.damagepl(5);
        }
    }
}
