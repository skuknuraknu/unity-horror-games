using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScare : MonoBehaviour {
    private AudioSource audios;
    public AudioClip scareSFX;
    public AudioClip door2SFX;
    public AudioClip tuyulSFX;
    public Animator doorAnim;
    public Animator doorAnim2;
    public Animator tuyulAnim;

    void Start() {
         audios = GetComponent<AudioSource>();
    }
    void Update() {
        
    }
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            StartCoroutine(DoorCloseCoroutine());
        }
    }
    private IEnumerator DoorCloseCoroutine() {
        yield return new WaitForSeconds(2);
        audios.PlayOneShot(scareSFX);
        yield return new WaitForSeconds(1);
        doorAnim.SetBool("open", false);
        doorAnim.SetTrigger("close");
        yield return new WaitForSeconds(1);
        audios.PlayOneShot(tuyulSFX);
        yield return new WaitForSeconds(1);
        tuyulAnim.SetTrigger("scare");
        yield return new WaitForSeconds(1);
        audios.PlayOneShot(door2SFX);
        doorAnim2.SetBool("open", true);
    }
}
