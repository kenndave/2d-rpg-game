using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour
{
    public GameObject loading;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(loadlevel(6));
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
