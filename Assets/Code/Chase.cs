using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Chase : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    public GameObject body;
    protected AIDestinationSetter dtarget;
    public int HP = 100;
    public Transform los;
    public GameObject lost;
    public bool chasing = false;
    public bool loop = false;
    public int nextpoint = 0;
    public bool backpoint = false;
    public Transform[] point;
    public int chase_time = 7;
    public bool chasing_shu = false;
    public int attackdmg=10;
    public int attacktime = 3;
    public int max_hp=100;
    public Health_bar hpbar;
    public int chase_range;
    // Start is called before the first frame update
    void Start()
    {
        HP = max_hp;
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();//gak perlu manual taruh
        dtarget = body.GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Time.timeScale != 0)
        {
            if (player.GetComponent<ninja>().invis == 1)
            {
                lost.GetComponent<LOS>().stopingcoroutines();
                lost.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                chasing = false;
                chasing_shu = false;
            }
            Vector3 direction;
            if (chasing)
            {
                direction = lost.GetComponent<LOS>().target - transform.position;
                if (dtarget.target != player)
                {
                    dtarget.target = player;
                }
            }
            else if (chasing_shu)
            {
                direction = player.position - transform.position;
                if (dtarget.target != player)
                {
                    dtarget.target = player;
                }
            }
            else
            {
                direction = point[nextpoint].position - transform.position;
                if (dtarget.target != point[nextpoint])
                {
                    dtarget.target = point[nextpoint];
                }
            }
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -body.transform.rotation.z);
            direction.Normalize();
        }
    }
    private void FixedUpdate()
    {
        if (chasing)
        {
            //moveCharacter(movement);
        }
        else
        {
            //moveCharacter(movement);
            if(body.transform.position.x < point[nextpoint].position.x + 0.2f&& body.transform.position.x > point[nextpoint].position.x - 0.2f&& body.transform.position.y < point[nextpoint].position.y + 0.2f&& body.transform.position.y > point[nextpoint].position.y - 0.2f)
            {
                if (loop)
                {
                    if ((point.Length == nextpoint + 1))
                    {
                        nextpoint = 0;
                    }
                    else
                    {
                        nextpoint++;
                    }
                }
                else
                {
                    if ((point.Length == nextpoint + 1))
                    {
                        nextpoint--;
                        backpoint = true;
                    }
                    else
                    {
                        if (backpoint)
                        {

                            if (nextpoint == 0)
                            {
                                backpoint = false;
                                nextpoint++;
                            }
                            else
                            {
                                nextpoint--;
                            }
                        }
                        else
                        {
                            nextpoint++;
                        }
                    }
                }
                
            }
        }
    }

    /*void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.fixedDeltaTime);
    }*/

    public void damage(int dmg)
    {
        HP = HP - dmg;
        if (HP <= 0)
        {
            StopAllCoroutines();
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
    public IEnumerator backtopatrol()
    {
        yield return new WaitForSeconds(chase_time);
        // Debug.Log("stop chase");
        lost.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        chasing = false;
        chasing_shu = false;
    }
    public void getting_hit()
    {
        lost.GetComponent<Transform>().localScale = new Vector3(chase_range, chase_range, 1);
        chasing_shu = true;
        if (chasing)
        {
            chasing = false;
            lost.GetComponent<LOS>().stopingcoroutines();
            StartCoroutine(backtopatrol());
        }
    }

}
