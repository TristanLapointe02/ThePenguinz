using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionEpee : MonoBehaviour
{
    public static bool epeePrise; //Détecter si l'épée est prise ou non

    public void OnTriggerEnter(Collider collision)
    {
        //Si l'arme touche un joueur
        if (collision.gameObject.tag == "Player")
        {
            epeePrise = true;
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        //Si l'arme touche un joueur
        if (collision.gameObject.tag == "Player")
        {
            epeePrise = false;
        }
    }
}
