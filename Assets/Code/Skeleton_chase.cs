using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_chase : MonoBehaviour
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

    [Header("Experimental")]
    public Animator animator;
    public bool Right_angle;
    public bool Left_angle;
    public bool Front_angle;
    public bool Back_angle;
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
        Right_angle = (angle < 360f && angle > 315f) || (angle <= 45f && angle > 0f) || (angle >= -45f && angle < 0f);
        Left_angle = (angle <= 225f && angle > 135f) || (angle < -135f && angle >= -225f);
        Back_angle = (angle <= 135f && angle > 45f) || (angle < -225f && angle >= -315);
        Front_angle = (angle <= 315f && angle > 225f) || (angle < -45f && angle >= -135f);
        /// Walking Animation
        ///

        if (Right_angle && (animator.GetFloat("RightV") == 0 || animator.GetFloat("RightV") == 2)) 
        {
            animator.SetFloat("RightV", 2); animator.SetFloat("LeftV", -1); animator.SetFloat("FrontV", -1); animator.SetFloat("BackV", -1);
        }
        else if (Left_angle && (animator.GetFloat("LeftV") == 0 || animator.GetFloat("LeftV") == 2))
        {
            animator.SetFloat("RightV", -1); animator.SetFloat("LeftV", 2); animator.SetFloat("FrontV", -1); animator.SetFloat("BackV", -1);
        }
        else if (Back_angle && (animator.GetFloat("BackV") == 0 || animator.GetFloat("BackV") == 2))
        {
            animator.SetFloat("RightV", -1); animator.SetFloat("LeftV", -1); animator.SetFloat("FrontV", -1); animator.SetFloat("BackV", 2);
        }
        else if (Front_angle && (animator.GetFloat("FrontV") == 0 || animator.GetFloat("FrontV") == 2))
        {
            animator.SetFloat("RightV", -1); animator.SetFloat("LeftV", -1); animator.SetFloat("FrontV", 2); animator.SetFloat("BackV", -1);
        }

        // Stance Position
        else if (Right_angle)
        {
            //rb.rotation = 90f; = RIGHT WALKING 
            animator.SetFloat("BackV", -1); animator.SetFloat("RightV", 0); animator.SetFloat("FrontV", -1); animator.SetFloat("LeftV", -1);
        }
        else if (Back_angle)
        {
            //rb.rotation = 0f; = BACKWARD WALKING
            animator.SetFloat("BackV", 0); animator.SetFloat("RightV", -1); animator.SetFloat("FrontV", -1); animator.SetFloat("LeftV", -1);
        }
        else if (Left_angle)
        {
            //rb.rotation = 270f; LEFT WALKING
            animator.SetFloat("BackV", -1); animator.SetFloat("RightV", -1); animator.SetFloat("FrontV", -1); animator.SetFloat("LeftV", 0);
        }
        else if (Front_angle)
        {
            //rb.rotation = 180f; FRONT WALKING
            animator.SetFloat("BackV", -1); animator.SetFloat("RightV", -1); animator.SetFloat("FrontV", 0); animator.SetFloat("LeftV", -1);
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
