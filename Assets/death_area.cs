using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death_area : MonoBehaviour
{
    public Transform bot;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bot.position = new Vector2(collision.GetComponent<Transform>().position.x,bot.position.y);
    }
}
