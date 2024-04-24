using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public GameObject effect;

    private Shake shake;
    void Start()
    {
        //shake = GameObject.FindGameObjectsWithTag("ScreenShake").GetComponent<Shake>();
        // shake = GameObject.FindGameObjectsWithTag("ScreenShake").GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        shake.CamShake();
        Destroy(other.gameObject);
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

