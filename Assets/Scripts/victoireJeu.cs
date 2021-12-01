using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victoireJeu : MonoBehaviour
{
    public GameObject[] particulesDeVictoire; //R�f�rence � la particule
    public AudioClip sonVictoire; //Son d'explosion de la grenade
    public GameObject texteVictoire; //R�f�rence au texte de victoire
    public bool victoireActive; //Condition pour pas que la fonction se r�ex�cute
    public static bool boule1Active; //Condition pour savoir si la boule 1 est d�pos�e
    public static bool boule2Active; //Condition pour savoir si la boule 2 est d�pos�e

      
    //Appeler cette fonction de victoire lorsque les deux socket ont �t� select par leur boule respecter. Ensuite, le socket principal avctive la victoire
    public void Victoire()
    {
        if (gameObject.name == "Socket")
        {
            boule1Active = true;

        }
        else if (gameObject.name == "Socket2")
        {
            boule2Active = true;
        }
    }

    public void Update()
    {
        if (victoireActive == false && boule1Active && boule2Active && gameObject.name == "Socket")
        {
            //Activer les particules de victoire en boucle
            InvokeRepeating("particulesVictoire", 0f, 0.1f);

            //Jouer le son de victoire
            GetComponent<AudioSource>().PlayOneShot(sonVictoire, 3f);

            //Activer le texte de victoire
            texteVictoire.SetActive(true);

            //Loader la sc�ne de d�part

            //Indiquer que la victoire est faite
            victoireActive = true;
        }
    }

    public void particulesVictoire()
    {
        //Piger un nombre al�atoire, activer la particule
        int randomEnfantActiver = Random.Range(0,particulesDeVictoire.Length);

        //Activer une particule al�toire dans le tableau
        particulesDeVictoire[randomEnfantActiver].gameObject.SetActive(true);

        //Piger un nombre al�atoire, d�sactiver la particule
        int randomEnfantDesactiver = Random.Range(0, particulesDeVictoire.Length);

        particulesDeVictoire[randomEnfantDesactiver].gameObject.SetActive(false);
    }
}
