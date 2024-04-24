using UnityEngine;


public class Scenegravity : MonoBehaviour
{
    public float speed; // kecepatan gravitas animasi
    private float move = 0;
    public float timer = 0;
    public float time; // gap time sebelum animasi mulai
    public float limit; // titik berhenti animasi
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= limit)
        {
            Debug.Log(timer);
            this.move = 0; // Berhenti animasi
        }
        timer += Time.deltaTime;
        if (timer > time && timer < limit)
        {
            move = 1; // Animasi mulai jalan
        }
        rb.velocity = new Vector2(rb.velocity.x, move * speed);
    }
}
