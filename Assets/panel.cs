using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panel : MonoBehaviour
{
    public GameObject[] door;
    public int acces_code;
    public bool open = false;
    public bool near = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ninja coll = collision.GetComponent<ninja>();
        if (coll != null)
        {
            Debug.Log("1");
            if (coll.cards.Contains(acces_code))
            {
                Debug.Log("2");
                near = true;

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        near = false;
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKeyDown(KeyCode.E) && near)
            {
                Debug.Log("3");
                if (!open)
                {
                    Debug.Log("4");
                    foreach (GameObject dor in door)
                    {
                        dor.GetComponent<BoxCollider2D>().enabled = false;
                        dor.GetComponent<Transform>().localPosition = new Vector3(0, -0.75f, 0);
                        dor.GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                    open = true;
                }
                else
                {
                    Debug.Log("4.5");
                    foreach (GameObject dor in door)
                    {
                        dor.GetComponent<BoxCollider2D>().enabled = true;
                        dor.GetComponent<Transform>().localPosition = new Vector3(0, 0, 0);
                        dor.GetComponent<SpriteRenderer>().sortingOrder = 0;
                    }
                    open = false;
                }
            }
        }
    }
}
