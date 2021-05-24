using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public AudioClip sound;
    public AudioSource source;
    void Start()
    {
        source.PlayOneShot(sound);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void LoadTrainig()
    {
        SceneManager.LoadScene("Scenes/Training");
    }
}
