using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Sprite AttackBot;
    public Sprite NormalBot;
    public GameObject bullet;
    public Transform bulletPos; // Posisi bulletnya transform kemana
    private GameObject player;
    private float timer; // Durasi bullet
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Range deteksi
        float distance = Vector2.Distance(transform.position, player.transform.position); // every frame representasi jarak enemy ke player
        // Debug.Log(distance);

        if (distance < 10)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
        else
        {
            // Animasi balik
            this.gameObject.GetComponent<SpriteRenderer>().sprite = NormalBot;
        }
    }

    void shoot()
    {

        // Animasi shooting
        this.gameObject.GetComponent<SpriteRenderer>().sprite = AttackBot;

        //dua Laser satu dari masing2 mata
        //bukan spawn cuman activate
    Object_pooling.instance.spawnfrompool("Laser", bulletPos.position, bulletPos.rotation);
    Object_pooling.instance.spawnfrompool("Laser", (bulletPos.position)+new Vector3(-1.7f,0,0), bulletPos.rotation);

    }
}
