using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    ninja player;

    public int cards_number;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ninja>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.foot.excludeLayers == 128)
        {
            player.cards.Add(cards_number);
            gameObject.SetActive(false);
        }

    }
}
