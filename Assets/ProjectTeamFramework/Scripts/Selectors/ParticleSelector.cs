using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

//Usada para ativar e desativar uma particular a partir do clique em um objeto
public class ParticleSelector : BaseSelector
{
    private ParticleSystem particle;
    public bool interactAgainToStop = false;

    bool currentlyPlaying;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        particle.Stop();
    }
    
    public override void OnInteractionTrigger(InteractionModes mode)
    {
        if (!particle.isPlaying)
        {
            particle.Play();
            currentlyPlaying = true;
        }
        else {
            if (interactAgainToStop)
            {
                currentlyPlaying = false;
                particle.Stop();
                OnFinish();
            }
        }        
    }

    private void Update()
    {
        if(currentlyPlaying && !particle.isPlaying)
        {
            currentlyPlaying = false;
            OnFinish();
        }
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}
