using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CloseApplicationPressingEsc : MonoBehaviour
{
    public GameObject texto;
    float countDown;
    PlatformManager platformManager;
    private void Start()
    {
        platformManager = ReferenceManagerIndependent.Instance.PlatformManager;
        if (platformManager.CurrentVRPlatform != VRPlataform.PC)
            this.enabled = false;
    }

    private void Update()
    {
        if (platformManager.CurrentVRPlatform != VRPlataform.PC)
            return;

        if (Input.GetKey(KeyCode.Escape))
        {
            countDown -= Time.deltaTime;
            if (texto)
            {
                texto.SetActive(true);
                texto.GetComponent<TextMeshProUGUI>().text = "Segure Esc por " + (int)countDown + " segundos para fechar a aplicação";
            }
        }
        else
        {
            countDown = 5;
            if (texto)
                texto.SetActive(false);
        }
        

        if(countDown < 0)
        {
            Debug.Log("Closed");
            Application.Quit();
        }
        
    
    }


}
