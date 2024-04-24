using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public int dmg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit" + collision.name);
        Chase coll = collision.GetComponent<Chase>();
        if (coll == null)
        {
            termibot colls = collision.GetComponent<termibot>();
            if (colls!= null)
            {
                colls.damage(dmg);
                gameObject.SetActive(false);
            }
            else
            {
                Small_boss collss = collision.GetComponent<Small_boss>();
                if (collss != null)
                {
                    collss.damage(dmg);
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (coll != null)
            {
                coll.getting_hit();
                coll.damage(dmg);
                gameObject.SetActive(false);
            }
        }

        gameObject.SetActive(false);
    }
}
