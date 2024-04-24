using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour
{
    public float timer;
    public float limit;
    private int levelToLoad;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0)) // Kalo left click, langsung ke index UI menu
        {
            FadeToLevel(6); // Straight to UI menu
        }

        if (timer >= limit) // Kalo animasi udh selesai, lanjutin storynya
        {
            if (SceneManager.GetActiveScene().buildIndex == 5)
            {
                FadeToLevel(6);
            }
            else
            {
                FadeToNextLevel(); // Lanjut Story
            }
        }
    }
    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void FadeToLevel(int indexLevel)
    {
        levelToLoad = indexLevel;
        if (SceneManager.GetActiveScene().buildIndex != 5)
        {
            animator.SetTrigger("FadeOut");
        }
        else
        {
            OnFadeComplete();
        }
        
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
