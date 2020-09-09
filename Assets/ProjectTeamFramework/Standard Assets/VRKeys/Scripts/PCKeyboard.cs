using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRKeys;

[RequireComponent(typeof(Keyboard))]
public class PCKeyboard : MonoBehaviour
{


    Keyboard keyboard;

    // Start is called before the first frame update
    void Start()
    {
        keyboard = GetComponent<Keyboard>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c >= 'a' && c <= 'z')
            {
                keyboard.AddCharacter(c.ToString());    
            }
        }
    }
}
