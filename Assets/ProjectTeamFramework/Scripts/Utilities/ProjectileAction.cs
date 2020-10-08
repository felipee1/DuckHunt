using UnityEngine;
using System.Collections;

public class ProjectileAction : MonoBehaviour {
   
    public int score = 0; //Pontuação do player
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
             
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.tag);       //Pois é irmao pra sabermos se isso é um pato

        if (other.tag == "Duck") //Caso o objeto atingindo seja um pato, o mesmo sera removido da tela
        {
           // print("You killed the duck, good work assassin");
            Destroy(this.gameObject); //Remove o projetil
            Destroy(other.gameObject); //Remove o pato que foi acertado pelo projetil
            score += score + 1; //Soma 1 ponto na pontuacao atual
           // print("Score> ", score);
            //Verificar como o score sera exibido em tela
        }
    }

}