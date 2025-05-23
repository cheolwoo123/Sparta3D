using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface IDamagalbe
{
    void TakePhysicalDamage(int damage);
}

public class PlayerCondition : MonoBehaviour, IDamagalbe
{

    public UICondion uiCondition;
    Condition health { get { return uiCondition.health; } }
    
    
    public event Action onTakeDamage;

    
    
    void Update()
    {
      

        if(health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

 


    public void Die()
    {
        Debug.Log("»ç¸Á");
        Application.Quit();
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();

    }

   
}
