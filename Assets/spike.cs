using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{

    private Rigidbody2D rb; // fisikny
    public float speed;
    public int damage;

    private float timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition((Vector2)transform.position + new Vector2(1, 0) * speed * Time.fixedDeltaTime);
        // Ini timer bulletnya kalo udh 10 detik. Tapi kalau collide aja deh
        timer += Time.deltaTime;

        if (timer > 6)
        {
            gameObject.SetActive(false); //Buat testing
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<ninja>().damagepl(damage);
            gameObject.SetActive(false); //Tidak didestroy cuman di deactivated
        }
        // else if (other.gameObject.CompareTag("
    }
}
