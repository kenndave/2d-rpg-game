using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public GameObject loading;
    
    public void continues()
    {
        Time.timeScale = 1;
    }
    public void levelselect()
    {
        StartCoroutine(loadlevel());
    }
    IEnumerator loadlevel()
    {
        loading.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
        AsyncOperation operate = SceneManager.LoadSceneAsync(6);
        while (!operate.isDone)
        {
            yield return null;
        }
    }


}
