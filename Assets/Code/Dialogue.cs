using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public float timer;
    public float limit;
    private bool start = false;
    public AudioSource typingSound;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer + limit);
        if (timer < limit)
        {
            timer += Time.deltaTime;
        }

        if (start && timer >= 3.5)
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
        if (timer >= limit)
        {   
            if (!start)
            {
                StartDialogue();
                this.start = true;
            }
            else if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                textComponent.text = lines[index];
            }
            timer = 0;
        }
        

    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            typingSound.enabled = true;
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        typingSound.enabled = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

