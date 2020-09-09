using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

//Usada para tocar um som apos passar o mouse em um objeto
public class SoundHoverSelector : BaseSelector
{
    private AudioSource audioSource;

    [SerializeField] SoundSelector soundSelector;

    // Start is called before the first frame update
    void Start()
    {
        if (!audioSource)
            audioSource = GetComponent<AudioSource>();
    }

    public override void OnInteractionTrigger(InteractionModes mode)
    {
        soundSelector.PlaySound(audioSource);
        OnFinish();
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }
}
