using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondion : MonoBehaviour
{

    public Condition health;
   
    // Start is called before the first frame update
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
        
    }

   
}
