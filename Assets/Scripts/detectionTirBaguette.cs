using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Script associ� � la baguette pour savoir si elle prise ou non
 * 
 * Par : Tristan Lapointe
 * 
 * Derni�re modification : 8 d�cembre 2021
 * 
*/
public class detectionTirBaguette : MonoBehaviour
{
    public Transform boutBaguette; //R�f�rence au bout de la baguette
    public GameObject particuleFeu; //R�f�rence � la particule de feu de la baguette
    public GameObject colliderFeu; //Collider du feu. C'est ce qui va toucher � l'ennemi

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
        //Si l'arme ne touche plus � un joueur
        if (collision.gameObject.tag == "Player")
        {
            tirFeuBaguette.boutBaguette = null;
            tirFeuBaguette.particuleFeu = null;
            tirFeuBaguette.colliderFeu = null;
        }
    }
}
