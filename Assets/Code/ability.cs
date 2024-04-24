using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ability : MonoBehaviour
{
    public Color cl;
    public Color cl2;
    public Image img;
    public TextMeshProUGUI text;
    public float time;
    public void avability(int timer)
    {
            text.text = "" + timer;
            text.enabled = true;
            img.color = cl2;
            StartCoroutine(countdown(timer));
    }
    IEnumerator countdown(int timer)
    {
        while (timer > 0)
        {
            timer =timer - 1;
            yield return new WaitForSeconds(1);
            time = timer;
            text.text = ""+timer;
        }
        yield return new WaitForEndOfFrame();
        text.enabled = false;
        img.color = cl;
    }
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            avability(10,1);
        }
    }*/
}
