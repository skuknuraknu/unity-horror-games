using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad2 : MonoBehaviour, IInteractable {
     public string nameObject;
    public bool unlocked = false;
    public TextMeshProUGUI textCode;
    public GameObject keypadPanel;
    public string password = "2022";
    public AudioSource audios;
    public float volume = 0.5f;
    public AudioClip typeSound;
    public AudioClip validSound;
    public AudioClip invalid;
    public AudioClip doorOpenSound;
    public Animator doorAnim;

    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        if(keypadPanel.activeInHierarchy){
            Cursor.lockState = CursorLockMode.None;
            PlayerController.instance.canLook = false;
        }
    }
    public void Execute(){
        if(textCode.text == password){
            textCode.text = "Valid";
            textCode.color = Color.green;
            audios.PlayOneShot(validSound);
            unlocked = true;
            doorAnim.SetBool("open", true);
            audios.PlayOneShot(doorOpenSound);
            Invoke("closeKeyPad", 1);
        }else{
            audios.PlayOneShot(invalid);
            textCode.text = "Invalid";
            textCode.color = Color.red;
            StartCoroutine("ResetPW");
        }
    }
    public void Number(int number){
        textCode.text += number.ToString();
        audios.PlayOneShot(typeSound);
    }
    public void closeKeyPad(){
        keypadPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        PlayerController.instance.canLook = true;
        if(unlocked == true){
            GetComponent<BoxCollider>().enabled = false;
        }
    }
    IEnumerator ResetPW(){
        yield return new WaitForSeconds(1);
        textCode.text = string.Empty;
        textCode.color = Color.black;
    }
    public string GetInteractPromp(){
        return string.Format("Cek {0}", nameObject);
    }
    public void OnInteract(){
        keypadPanel.SetActive(true);
    }
}
