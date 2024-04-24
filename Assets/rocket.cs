using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour
{
    public bool near = false;
    public int force;
    public Rigidbody2D rb;
    public int dmg;
    float timer = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ninja collss = collision.GetComponent<ninja>();
        if (collss != null)
        {
                Debug.Log("2");
                near = true;
        }
        Debug.Log("hit" + collision.name);
        Chase coll = collision.GetComponent<Chase>();
        if (coll == null)
        {
            termibot colls = collision.GetComponent<termibot>();
            if (colls != null)
            {
                colls.damage(dmg);
                gameObject.SetActive(false);
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
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.E) && near)
            {
                rb.AddForce(rb.GetComponent<Transform>().up * force, ForceMode2D.Impulse);
            }
        }
        timer += Time.deltaTime;
        if (timer > 20)
        {
            gameObject.SetActive(false); //Buat testing
        }
    }
}
