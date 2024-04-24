using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class termibot : MonoBehaviour
{
    public GameObject body;
    public int HP = 100;
    public int max_hp = 100;
    public Health_bar hpbar;
    bool attackava = true;
    public Transform bulletPos;
    public float attacktime;
    public GameObject[] door;
    public Rigidbody2D rb;
    public bool left=true;
    public float speed;
    void Start()
    {
        HP = max_hp;
    }
    public void shoot()
    {
        Object_pooling.instance.spawnfrompool("Laser", bulletPos.position, bulletPos.rotation);
    }

    IEnumerator shooting()
    {
        attackava = false;
        yield return new WaitForSeconds(0.3f);
        shoot();
        yield return new WaitForSeconds(attacktime);
        attackava = true;
    }
    private void FixedUpdate()
    {
        if (attackava)
        {
            StartCoroutine(shooting());
        }
        if (transform.localPosition.x > 1.1)
        {
            left = true; 
        }
        else if(transform.localPosition.x < -1.1)
        {
            left = false;
        }
        if (left)
        {
            rb.MovePosition((Vector2)transform.position + new Vector2(-1, 0) * speed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition((Vector2)transform.position + new Vector2(1, 0) * speed * Time.fixedDeltaTime);
        }

    }

    public void damage(int dmg)
    {
        HP = HP - dmg;
        if (HP <= 0)
        {
            StopAllCoroutines();
            foreach (GameObject dor in door)
            {
                dor.GetComponent<BoxCollider2D>().enabled = false;
                dor.GetComponent<Transform>().localPosition = new Vector3(0, -0.75f, 0);
                dor.GetComponent<SpriteRenderer>().sortingOrder = -2;
            }
            body.SetActive(false);
        }
        else
        {
            Debug.Log(HP);
            float fhp = (float)HP / (float)max_hp;
            Debug.Log(fhp);
            hpbar.sethealth(fhp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ninja player = collision.GetComponent<ninja>();
        if (player)
        {
            player.damagepl(15);
        }
    }
}
