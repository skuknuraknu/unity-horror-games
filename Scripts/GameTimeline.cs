using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.InputSystem;


public class GameTimeline : MonoBehaviour {
    public string sceneName;
    public VideoPlayer videoPlayer;
    public Text skipText = null;
    private float gameTimer = 0;

    private void Awake(){
        QualitySettings.vSyncCount = 1;
        // mulai
        gameTimer = Time.time;
        // putar video
        StartCoroutine(PlayVideo());
    }
    IEnumerator PlayVideo(){
        bool videoLoadFailed = false;
        videoPlayer.Prepare(); 
        // looping sampai video siap untuk diputar
        while (!videoPlayer.isPrepared) {
            // kalau sudah lebih dari 11 detik video masih gagal
            if(Time.time > gameTimer + 11.0f){
                // video gagal diputar
                videoLoadFailed = true;
                break;
            }
            // jaga jga kalau error
            yield return null;
        }
        if(!videoLoadFailed){
            videoPlayer.Play();
        }
        gameTimer = Time.time;
        while(videoPlayer.isPlaying){
            if(Time.time > gameTimer+0.6f){
                skipText.gameObject.SetActive(!skipText.gameObject.activeSelf);
                gameTimer = Time.time;
            }
            if(Keyboard.current.anyKey.wasPressedThisFrame){
                // bawa video ke akhir 
                videoPlayer.time = videoPlayer.length;
            }
            yield return null;
        }
        // hilangkan skip text
        skipText.gameObject.SetActive(false);
        QualitySettings.vSyncCount = 0;
        // kita load scene
        AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(sceneName);
        sceneLoader.allowSceneActivation = false;
        sceneLoader.allowSceneActivation = true;
    }
}
