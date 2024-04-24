using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Brute : Chase
{
    public Transform attackpoint;
    public float range;
    public LayerMask playerlayer;
    [Header("Experimental")]
    public Animator animator;
    public bool Right_angle;
    public bool Left_angle;
    public bool Front_angle;
    public bool Back_angle;
    public float angle;
    public AIPath aip;
    public Sprite front;
    public Sprite back;
    public Sprite left;
    public Sprite right;
    public void attack()
    {
        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(attackpoint.position,range,playerlayer);
        foreach(Collider2D player in hitplayer)
        {
            ninja target_hit=player.GetComponent<ninja>();
            if (target_hit)
            {
                Debug.Log("hit" + target_hit.name);
                target_hit.damagepl(attackdmg);
            }
        }

    }
    public override void Update()
    {
        if (Time.timeScale != 0)
        {
            base.Update();
            if (animator)
            {
                angle = body.transform.eulerAngles.z;
                Right_angle = (angle <= 315f && angle > 225f);
                Left_angle = (angle <= 135f && angle > 45f);
                Back_angle = (angle <= 45f && angle >= 0) || (angle <= 315f && angle >= 361f);
                Front_angle = (angle <= 225 && angle > 135f);
                /// Walking Animation
                ///

                if (Right_angle && (animator.GetFloat("RightV") == 0 || animator.GetFloat("RightV") == 2) && !aip.reachedEndOfPath)
                {
                    animator.SetFloat("RightV", 2); animator.SetFloat("LeftV", -1); animator.SetFloat("FrontV", -1); animator.SetFloat("BackV", -1);
                }
                else if (Left_angle && (animator.GetFloat("LeftV") == 0 || animator.GetFloat("LeftV") == 2) && !aip.reachedEndOfPath)
                {
                    animator.SetFloat("RightV", -1); animator.SetFloat("LeftV", 2); animator.SetFloat("FrontV", -1); animator.SetFloat("BackV", -1);
                }
                else if (Back_angle && (animator.GetFloat("BackV") == 0 || animator.GetFloat("BackV") == 2) && !aip.reachedEndOfPath)
                {
                    animator.SetFloat("RightV", -1); animator.SetFloat("LeftV", -1); animator.SetFloat("FrontV", -1); animator.SetFloat("BackV", 2);
                }
                else if (Front_angle && (animator.GetFloat("FrontV") == 0 || animator.GetFloat("FrontV") == 2) && !aip.reachedEndOfPath)
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
            }
            else
            {
                angle = body.transform.eulerAngles.z;
                if (angle <= 315f && angle > 225f)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = right;
                }
                else if (angle <= 135f && angle > 45f)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = left;
                }
                else if (angle <= 225 && angle > 135f)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = front;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = back;
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (!attackpoint)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, range);
    }

}
