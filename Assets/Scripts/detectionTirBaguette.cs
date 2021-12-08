using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionTirBaguette : MonoBehaviour
{
    public Transform boutBaguette; //Bout de la baguette
    public GameObject particuleFeu; //Particule de feu
    public GameObject colliderFeu; //Collider du feu. C'est ce qui va toucher à l'ennemi

    void Start()
    {       
        //Ignorer la collision avec les ennemis
        Physics.IgnoreLayerCollision(12, 10);      
    }

    public void OnTriggerEnter(Collider collision)
    {
        //Si l'arme touche un joueur
        if (collision.gameObject.tag == "Player")
        {
            tirFeuBaguette.boutBaguette = boutBaguette;
            tirFeuBaguette.particuleFeu = particuleFeu;
            tirFeuBaguette.colliderFeu = colliderFeu;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        //Si l'arme ne touche plus à un joueur
        tirFeuBaguette.boutBaguette = null;
        tirFeuBaguette.particuleFeu = null;
        tirFeuBaguette.colliderFeu = null;
    }

}
