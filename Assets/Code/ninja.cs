using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
public class ninja : MonoBehaviour
{
    [Header("Misc")]
    public float speed;
    public Rigidbody2D body;
    public Vector2 move;
    public SpriteRenderer sp;
    public Camera cam;
    Vector2 mousepos;
    public Transform fp;
    public Transform fparrow;
    public int HP;
    public int max_HP;
    public Health_bar HPbar;
    public ability[] abi;
    public shooting sh;
    public List<int> cards;
    public GameObject pause;
    public GameObject control;

    [Header("invisibility")]
    public int invis;
    public bool invisable = true;
    public float invisD = 10;
    public float inviscd = 5;
    public Color cl;
    public GameObject[] los;

    [Header("Dash")]
    public float flash;
    bool dashable = true;
    public float dashcd = 2;
    bool stop = false;

    [Header("Vents")]
    public GameObject vent;
    public bool ventable = false;
    public CapsuleCollider2D foot;
    public CapsuleCollider2D bodycollid;

    [Header("Experimental")]
    public Animator animator;

    private void Start()
    {
        HP = max_HP;
        los = GameObject.FindGameObjectsWithTag("FOV");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pause.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pause.SetActive(false);
                control.SetActive(false);
            }
            
        }
        if (Time.timeScale != 0)
        {
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");
            // Walk Vertical
            // Debug.Log(move.y);
            if ((Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.W)))
            {
                animator.SetFloat("Y_Speed", move.y);

            }
            // Walk Horizontal
            if ((Input.GetKeyDown(KeyCode.A)) || (Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyDown(KeyCode.D)) || (Input.GetKeyUp(KeyCode.D)))
            {
                animator.SetFloat("X_Speed", move.x);
            }
            //Dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && dashable)
            {
                abi[1].avability((int)dashcd);
                StartCoroutine(Dashing());
                Debug.Log("flash");
            }

            //Invisibility
            if (Input.GetKeyDown(KeyCode.Q) && invisable)
            {
                abi[0].avability((int)(inviscd + invisD));
                StartCoroutine(invisibility());
                Debug.Log("invis");
            }
            //Vents
            if (Input.GetKeyDown(KeyCode.Space) && ventable)
            {
                if (foot.excludeLayers == 128)
                {
                    SpriteRenderer[] ven = vent.GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer sr in ven)
                    {
                        sr.sortingLayerName = "vents";
                    }
                    TilemapRenderer[] vens = vent.GetComponentsInChildren<TilemapRenderer>();
                    foreach (TilemapRenderer sr in vens)
                    {
                        sr.sortingLayerName = "vents";
                    }

                    gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "vents";
                    sh.shootable = false;

                    foot.excludeLayers = 2880;
                    bodycollid.excludeLayers = 768;
                    invisable = false;
                }
                else
                {
                    SpriteRenderer[] ven = vent.GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer sr in ven)
                    {
                        sr.sortingLayerName = "Default";
                    }
                    TilemapRenderer[] vens = vent.GetComponentsInChildren<TilemapRenderer>();
                    foreach (TilemapRenderer sr in vens)
                    {
                        sr.sortingLayerName = "Default";
                    }

                    gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                    sh.shootable = true;
                    foot.excludeLayers = 128;
                    bodycollid.excludeLayers = 256;
                    invisable = true;
                }
                Debug.Log("vents");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                damagepl(5);
                //testing health
            }

            mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (!stop)
        {
            body.MovePosition(body.position + move * speed * Time.fixedDeltaTime);
        }
               


        //Targeting
        Vector2 lookdir = mousepos - body.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        fp.rotation = Quaternion.Euler(0, 0, angle);
        fparrow.rotation = Quaternion.Euler(0, 0, angle);

    }
    IEnumerator invisibility()
    {
        sp.color = cl;
        invis = 1;
        invisable = false;
        foot.excludeLayers = 640;
        bodycollid.excludeLayers = 768;
        foreach (GameObject fov in los)
        {
            fov.GetComponent<PolygonCollider2D>().excludeLayers = -1;
        }
        yield return new WaitForSeconds(invisD);
        sp.color = new Color(1,1,1,1);
        foreach (GameObject fov in los)
        {
            fov.GetComponent<PolygonCollider2D>().excludeLayers = 4086;
        }
        invis = 0;
        foot.excludeLayers = 128;
        bodycollid.excludeLayers = 256;
        yield return new WaitForSeconds(inviscd);
        invisable = true;
    }
    IEnumerator Dashing()
    {
        dashable = false;
        stop = true;
        yield return new WaitForSeconds(0.1f);
        body.MovePosition(body.position+move * flash);
        yield return new WaitForSeconds(0.1f);
        stop = false;
        yield return new WaitForSeconds(dashcd);
        dashable = true;
    }
    public void damagepl(int dmg)
    {
        HP = HP - dmg;
        if (HP <= 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log(HP );
            float fhp=(float)HP / (float)max_HP;
            Debug.Log(fhp);
            HPbar.sethealth(fhp);
        }

    }

}
