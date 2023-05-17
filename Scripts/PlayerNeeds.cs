using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerNeeds : MonoBehaviour, IDamageable
{
    public Need Health;
    public Need Hunger;
    public Need Thirst;
    public float NoHungerHpDecay;
    public float NoThirstHpDecay;
    public UnityEvent getDamage;

    private void Start(){
        Health.currentValue = Health.startValue;
        Hunger.currentValue = Hunger.startValue;
        Thirst.currentValue = Thirst.startValue;
    }
    private void Update(){
        //reduce the value over time
        Hunger.Subtract(Hunger.decayRate * Time.deltaTime);
        Thirst.Subtract(Thirst.decayRate * Time.deltaTime);

        if(Hunger.currentValue == 0.0f){
            Health.Subtract(NoHungerHpDecay * Time.deltaTime);
        }if(Thirst.currentValue == 0.0f){
            Health.Subtract(NoThirstHpDecay * Time.deltaTime);
        }if(Health.currentValue == 0.0f){
            Die();
        }
        Health.uiSlider.fillAmount = Health.getPercentage();
        Thirst.uiSlider.fillAmount = Thirst.getPercentage();
        Hunger.uiSlider.fillAmount = Hunger.getPercentage();
    }

    public void heal(float amount){
        Health.Add(amount);
    }
    public void thirst(float amount){
        Thirst.Add(amount);
    }
    public void hunger(float amount){
        Hunger.Add(amount);
    }
    public void TakeDamage(int damageAmount){
        Health.Subtract(damageAmount);
        getDamage?.Invoke();
    }
    public void Die(){
        Debug.Log("Player Died");
    }
}
[System.Serializable]
public class Need{
    public float currentValue, maxValue, startValue, regenarateRate, decayRate;
    public Image uiSlider;
    public void Add(float amount){
        currentValue = Mathf.Min(currentValue+amount, maxValue);
    }
    public void Subtract(float amount){
        currentValue = Mathf.Max(currentValue -amount, 0.0f);
    }
    public float getPercentage(){
        return currentValue / maxValue;
    }
}
public interface IDamageable{
    void TakeDamage(int DamageAmount); 
}