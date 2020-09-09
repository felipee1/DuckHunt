using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Summary
 * Used to alternate between two gameobjects (GO) when the player gets close to me.
 * I.e. when the player is far show closeObject, when he is close show farObject
 */

public class SwitchTwoGOsWhenPlayerisCloseOrFar : MonoBehaviour
{

    Transform player;
    [SerializeField] GameObject closeGO, farGO;
    [SerializeField] float thresholdDistance = 5;
    bool wasFar = true;

    // Start is called before the first frame update
    void Start()
    {
        player = ReferenceManagerIndependent.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        bool isFar = Vector3.Distance(player.position, this.transform.position) >= thresholdDistance;
               
        if (isFar != wasFar)        
            Switch(isFar);        

        wasFar = isFar;
    }

    void Switch(bool isFar)
    {
        closeGO.SetActive(!isFar);
        farGO.SetActive(isFar);
    }
}
