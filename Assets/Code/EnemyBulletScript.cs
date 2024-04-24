using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject player; // referensi main character buat diikutin

    private Rigidbody2D rb; // fisikny
    public float force; // kaitan ama speed + powernya nanti bulletnya

    private float timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0; // Karena gameobject yang dipakai sama maka harus ada pengreset
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        // Dapetin direction biar bulletnya terbang ke player
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        // Rotasinya
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        // Ini timer bulletnya kalo udh 10 detik. Tapi kalau collide aja deh
        timer += Time.deltaTime;

        if (timer > 7)
        {
            gameObject.SetActive(false); //Buat testing
        }
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<ninja>().damagepl(5);
            gameObject.SetActive(false); //Tidak didestroy cuman di deactivated
        }
       // else if (other.gameObject.CompareTag("
	}
}
