using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScareMoment : MonoBehaviour
{
    public bool fear;
    private AudioSource audios;
    public AudioClip scareSFX;
    public AudioClip talkSFX;
    public TextMeshProUGUI infoText;
    public string[] talktext;
    public Animator cameraAnim;
    public Animator asunaAnim;
    public GameObject asuna;

    // Start is called before the first frame update
    void Start() {
        audios = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            cameraAnim.SetTrigger("Shake");
            asunaAnim.SetTrigger("Scare");
            audios.PlayOneShot(scareSFX);
            Invoke("PlayAudio", 2f);
            Invoke("StopAudio", 5f);
            infoText.text = talktext[Random.Range(0, talktext.Length)];
            audios.PlayOneShot(talkSFX);
        }
    }
    public void PlayAudio(){
        audios.Play();
    }
    public void StopAudio(){
        audios.Stop();
        infoText.text = string.Empty;
        asuna.SetActive(false);
    }
}
