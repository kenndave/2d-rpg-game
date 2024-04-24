using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savecaller : MonoBehaviour
{
    public int player_saves;


    public void saveplayer()
    {
        save.saveplayer(player_saves);
    }

    public void loadplayer()
    {
        playerdata data = save.loadplayer();
        if (data!=null)
        {
        player_saves = data.level;
        }
        

    }
}
