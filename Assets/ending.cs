using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ending : MonoBehaviour
{
    public GameObject loading;
    public savecaller Save;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ninja coll = collision.GetComponent<ninja>();
        if (coll)
        {
            Save.loadplayer();
            if(Save.player_saves< SceneManager.GetActiveScene().buildIndex)
            {
                Save.player_saves = SceneManager.GetActiveScene().buildIndex;
                Save.saveplayer();
            }
            if (SceneManager.GetActiveScene().buildIndex  < 15)
            {
            StartCoroutine(loadlevel(SceneManager.GetActiveScene().buildIndex + 1));
            }
            else
            {
                StartCoroutine(loadlevel(16));
            }

            
        }
    }
    public void levelselect(int levelidx)
    {
        StartCoroutine(loadlevel(levelidx));
    }

    IEnumerator loadlevel(int index)
    {
        loading.SetActive(true);
        yield return new WaitForSeconds(2);
        AsyncOperation operate = SceneManager.LoadSceneAsync(index);
        while (!operate.isDone)
        {
            yield return null;
        }
    }
}
