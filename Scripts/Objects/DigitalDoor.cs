using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitalDoor : MonoBehaviour, IInteractable {
    public Keypad keypad;
    public bool isOpen;
    public string nameObject;
    public Animator anim;
    private AudioSource audios;
    public AudioClip opedDoor, closeDoor, lockedDoor;

    void start(){
        anim = GetComponent<Animator>();
        audios = GetComponent<AudioSource>();
    }
    public string GetInteractPromp(){
        return isOpen ? "Close door" : "Open door";
    }
    public void OnInteract(){
        if(keypad.unlocked == true){
            isOpen = !isOpen;
            if(isOpen){
                anim.SetBool("Open", true);
                audios.PlayOneShot(opedDoor);
            }else{
                anim.SetBool("Open", false);
                audios.PlayOneShot(closeDoor);
            }
        }else{
            audios.PlayOneShot(lockedDoor);
        }
    }
}
