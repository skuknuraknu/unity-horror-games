using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audios;
    public AudioClip startSound;
    public AudioClip bgSound;
    // Start is called before the first frame update
    void Start(){
        audios = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        
    }
    public void OnStartButton(){
        SceneManager.LoadScene("Level 1");
    }
    public void OnAboutButton(){
        SceneManager.LoadScene("About US");
    }
    public void OnHowButton(){
        SceneManager.LoadScene("How");
    }
    public void OnBackButton(){
        SceneManager.LoadScene("Menu");
    }
    public void OnExitButton(){
        Application.Quit();
    }
}
