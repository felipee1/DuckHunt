using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;


//Usada para trocar a cor de um objeto, a partir do clique em um objeto
public class ColorSelector : BaseSelector
{
    public Color cor;
    public Material defaultMaterial;
    public MeshRenderer target;

    private void Start()
    {
        //transform.GetComponent<MeshRenderer>().material.color = cor;
    }

    //QUANDO O BOTAO É ATIVADO, ESSA FUNCAO EH CHAMADA. AQUI VOCE DEVE PROGRAMAR O SEU CODIGO PARA O SEU BOTAO...
    public override void OnInteractionTrigger(InteractionModes mode)
    {
        //print($"OnInteractionTrigger {this.gameObject}");
        target.material = defaultMaterial;
        target.material.color = cor;
        OnFinish();
    }

    protected override void OnFinish()
    {
        base.Finished(this.gameObject);
    }

}

