using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour {
    private AudioSource audios;
    public AudioClip doorSFX;
    public Animator doorAnim;

    void Start() {
        audios = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            StartCoroutine(DoorCloseCoroutine());
        }
    }
     private IEnumerator DoorCloseCoroutine() {
        yield return new WaitForSeconds(1);
        audios.PlayOneShot(doorSFX);
        yield return new WaitForSeconds(1);
        doorAnim.SetBool("open", false);
        doorAnim.SetTrigger("close");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("End");
    }
}
