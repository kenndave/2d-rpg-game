using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke_bomb : MonoBehaviour
{
    public GameObject smoke;
    public float a = 1;
    private void OnEnable()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(time());
    }
    IEnumerator time()
    {

        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        smoke.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(5);
        while (a > 0)
        {
            smoke.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, a);
            yield return new WaitForSeconds(0.1f);
            a = a - 0.02f;
        }
        if (a < 0)
        {
            Debug.Log("deactivate");
            smoke.SetActive(false);
            smoke.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
}
