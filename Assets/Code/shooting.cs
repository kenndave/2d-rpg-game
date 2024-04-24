using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform fp;
    public ability abi;
    public bool smokeable=true;
    public bool shurikenable = true;
    public float shurikencd= 0.5f;
    public int smokecd = 5;
    public float force = 20f;
    public bool shootable=true;

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetButton("Fire1") && shootable && shurikenable)
            {
                shoot();
                StartCoroutine(shuriken());
            }
            if (Input.GetButtonDown("Fire2") && smokeable && shootable)
            {
                abi.avability(smokecd);
                shoots();
                StartCoroutine(Smoking());
            }
        }
    }

    void shoot()
    {
        GameObject shu = Object_pooling.instance.spawnfrompool("Shuriken", fp.position, fp.rotation);
        if (shu == null)
        {
            Debug.LogWarning("error");
        }
        Rigidbody2D rb = shu.GetComponent < Rigidbody2D>();
        rb.angularVelocity = -360*10;
        rb.velocity =new Vector2(0,0);
        rb.AddForce(fp.up * force, ForceMode2D.Impulse);
    }
    void shoots()
    {
        GameObject shu = Object_pooling.instance.spawnfrompool("Smoke", fp.position, fp.rotation);
        if (shu == null)
        {
            Debug.LogWarning("error");
        }
        Rigidbody2D rb = shu.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(fp.up * force, ForceMode2D.Impulse);
    }

    IEnumerator Smoking()
    {
        smokeable= false;
        yield return new WaitForSeconds(smokecd);
        smokeable = true;
    }
    IEnumerator shuriken()
    {
        shurikenable = false;
        yield return new WaitForSeconds(shurikencd);
        shurikenable = true;
    }
}
