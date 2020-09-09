using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAfterXTime : MonoBehaviour
{
    public float timeToTurnOff = 3;
    float countDown;
    // Start is called before the first frame update
    private void OnEnable()
    {
        countDown = timeToTurnOff;
    }

    // Update is called once per frame
    void Update()
    {
        if(countDown > 0)
        {
            countDown -= Time.deltaTime;
            if (countDown < 0)
                gameObject.SetActive(false);
        }
    }
}
