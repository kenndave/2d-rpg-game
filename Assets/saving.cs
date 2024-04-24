using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class saving : MonoBehaviour
{
    public savecaller Save;
    public Button[] button;
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        Save = GetComponent<savecaller>();
        Save.loadplayer();
        if (Save.player_saves < 8)
        {
            Save.player_saves = 7;
            Save.saveplayer();
        }
        for(int i=8; i<Save.player_saves+2; i++)
        {
            Debug.Log("level" + i);
            button[i - 8].interactable = true;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("save");
            Save.saveplayer();
        }
    }
    public void levelselect(int levelidx)
    {
        StartCoroutine(loadlevel(levelidx));
    }

    IEnumerator loadlevel(int index)
    {
        yield return new WaitForSeconds(2);
        AsyncOperation operate = SceneManager.LoadSceneAsync(index);
        while (!operate.isDone)
        {
            Debug.Log(operate.progress);
            yield return null;
        }
    }

}
