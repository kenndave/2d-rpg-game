using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class venting : MonoBehaviour
{
    ninja player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ninja>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.invis != 1)
        {
        player.ventable = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.ventable = false;
    }
}
