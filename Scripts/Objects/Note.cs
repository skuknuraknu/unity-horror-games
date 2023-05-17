using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Note : MonoBehaviour, IInteractable
{
    public GameObject paper;
    public string nameObject;
    public Sprite papersprite;
    public Image paperUI;
    public TextMeshProUGUI UItext;
    private AudioSource audios;
    public AudioClip suaraPlayer;
    [TextArea(7,10)]
    public string textInfo;

    void Start(){
        audios = GetComponent<AudioSource>();
    }
    public string GetInteractPromp(){
        return string.Format("Cek {0}", nameObject);
    }
    public void OnInteract(){
        Cursor.lockState = CursorLockMode.Locked;
        paper.SetActive(true);
        audios.PlayOneShot(suaraPlayer);
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            paper.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void CloseUI(){
        paper.SetActive(false);
    }
}
