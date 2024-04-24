using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class Small_boss : MonoBehaviour
{
    public GameObject body;
    public GameObject head;
    public Transform[] point;
    protected AIDestinationSetter dtarget;
    public GameObject[] door;
    public int nextpoint = 0;
    public int HP = 100;
    public int max_hp = 100;
    public int armor = 500;
    public int max_armor = 500;
    public Health_bar hpbar;
    public RectTransform Hfill;
    public RectTransform Afill;
    public Animator animator;
    private void Start()
    {
        max_hp = HP;
        max_armor = armor;
        dtarget = body.GetComponent<AIDestinationSetter>();
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (dtarget.target != point[nextpoint])
            {
                dtarget.target = point[nextpoint];
            }
            gameObject.transform.rotation = Quaternion.Euler(0, 0, -body.transform.rotation.z);
        }

    }
    private void FixedUpdate()
    {
        if (body.transform.position.x < point[nextpoint].position.x + 0.2f && body.transform.position.x > point[nextpoint].position.x - 0.2f && body.transform.position.y < point[nextpoint].position.y + 0.2f && body.transform.position.y > point[nextpoint].position.y - 0.2f)
        {
            nextpoint = Random.Range(0, 8);

        }
    }

    public void damage(int dmg)
    {
        
        if (armor <= 0)
        {
            HP = HP - dmg;
            if (HP <= 0)
        {
            StopAllCoroutines();
            foreach (GameObject dor in door)
            {
                dor.GetComponent<BoxCollider2D>().enabled = false;
                dor.GetComponent<Transform>().localPosition = new Vector3(0, -0.75f, 0);
                dor.GetComponent<SpriteRenderer>().sortingOrder = -2;
            }
            head.SetActive(false);
            body.SetActive(false);
        }
        else
        {
            Debug.Log(HP);
            float fhp = (float)HP / (float)max_hp;
            Debug.Log(fhp);
            hpbar.sethealth(fhp);
        }
        }
        else
        {
            armor = armor - (dmg - 2);
            float fhp = (float)armor / (float)max_armor;
            hpbar.sethealth(fhp);
            if (armor <= 0)
            {
                hpbar.GetComponent<Slider>().fillRect = Hfill;
                animator.SetBool("Break", true);
            }
        }

    }
}
