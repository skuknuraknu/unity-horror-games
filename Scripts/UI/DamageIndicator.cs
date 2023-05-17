using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
   public Image damageImage;
   public float flashSpeed;
   private Coroutine fadeAwayImage;

   public void Flashing(){
        if(fadeAwayImage != null)
            StopCoroutine(fadeAwayImage);
        damageImage.enabled = true;
        damageImage.color = Color.white;
        fadeAwayImage = StartCoroutine(FadeAwayImage());
   }
   IEnumerator FadeAwayImage(){
        float imageAlpha = 1.0f;
        while(imageAlpha > 0.0f){
            imageAlpha -= (1.0f / flashSpeed) * Time.deltaTime;
            damageImage.color = new Color(1.0f,1.0f,1.0f, imageAlpha);
            yield return null;
        }
        damageImage.enabled = false;
   }
}
