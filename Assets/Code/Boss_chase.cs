using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_chase : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed = 0.3f;
    public int HP = 100;
    public Transform los;
    public bool chasing = false;
    public bool loop = false;
    public int nextpoint = 0;
    public bool backpoint = false;
    public Transform[] point;
    public int chase_time = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();//gak perlu manual taruh
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction;
        if (chasing)
        {
            direction = player.position - transform.position;
        }
        else
        {
            direction = point[nextpoint].position - transform.position;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // anglenya ke arah character
        ///Debug.Log(angle);
        if ((angle < 360f && angle > 315f) || (angle <= 45f && angle > 0f) || (angle >= -45f && angle < 0f))
        {
            //rb.rotation = 90f; = BACKWARD WALKING animator.SetFloat("X_Speed", move.x);
        }
        else if ((angle <= 315f && angle > 225f) || (angle < -45f && angle >= -135f))
        {
            //rb.rotation = 0f; = RIGHT WALKING
        }
        else if ((angle <= 225f && angle > 135f) || (angle < -135f && angle >= -225f))
        {
            //rb.rotation = 270f; FRONT WALKING
        }
        else if ((angle <= 135f && angle > 45f) || (angle < -225f && angle >= -315))
        {
            //rb.rotation = 180f; LEFT WALKING
        }
        //rb.rotation = angle;
        //los.rotation = Quaternion.Euler(0, 0, angle + 90);
        direction.Normalize();
        movement = direction;
    }
    private void FixedUpdate()
    {
        if (chasing)
        {
            moveCharacter(movement);
        }
        else
        {
            moveCharacter(movement);
            if (transform.position.x < point[nextpoint].position.x + 0.05f && transform.position.x > point[nextpoint].position.x - 0.05f && transform.position.y < point[nextpoint].position.y + 0.05f && transform.position.y > point[nextpoint].position.y - 0.05f)
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

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    public void damage(int dmg)
    {
        HP = HP - dmg;
        if (HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public IEnumerator backtopatrol()
    {
        yield return new WaitForSeconds(chase_time);
        chasing = false;
    }
}