using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherShooting : MonoBehaviour
{
    public Sprite AttackBot;
    public Animator animator;
    public Sprite NormalBot;
    public GameObject bullet;
    public Transform bulletPos; // Posisi bulletnya transform kemana
    private Transform player;
    public bool chasing = false;
    private float timer; // Durasi bullet
    private Rigidbody2D rb;
    public int nextpoint = 0;
    public bool backpoint = false;
    public Transform[] point;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();//gak perlu manual taruh
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 direction;
        /*if (chasing)
        {
            direction = player.position - transform.position;
        }
        else
        {
            direction = point[nextpoint].position - transform.position;
        } */
        direction = player.position - transform.position;
        // Range deteksi
        float distance = Vector2.Distance(transform.position, player.transform.position); // every frame representasi jarak enemy ke player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // anglenya ke arah character
        // Debug.Log(distance);
        animator.SetFloat("V", angle);
        Debug.Log("Angle: " + angle);
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
            if (AttackBot) //selama sprite belum ada
            {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = NormalBot;
            }
            
        }
    }

    void shoot()
    {

        // Animasi shooting
        if (AttackBot) //selama sprite belum ada
        {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = AttackBot; 
        }
        

        
        Object_pooling.instance.spawnfrompool("Laser", bulletPos.position, bulletPos.rotation);
        

    }
}
