using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit; // accès aux objets du XR Interaction Toolkit
using UnityEngine.InputSystem; // pour utiliser le nouveau InputSyteme
using UnityEngine.Events;
/*
 * Gestion du tir du gun
 * 
 * Par : Tristan Lapointe et Mathieu Dionne
 * 
 * Dernière modification : 2 décembre 2021
 * 
*/
public class tirBalle : MonoBehaviourPunCallbacks
{
    public bool peutTirer = true; //Vérifie si le joueur peut tirer
    public GameObject balle; //La balle qui est instanciéee au tir
    public static Transform boutFusil; //Détermine où est le bout du fusil (où la balle devrait spawn)
    public AudioClip tirSon; //Effet sonore du tir

    // L'action du contrôleur qui active/désactive le rayon. Peut être autre chose que le grip. Action à définir dans le tableau InputAction
    [SerializeField]
    InputActionReference inputActionReference_ActiveTrigger; 

    public override void OnEnable(){
        // s'exécute lorsque le script devient actif (enable)
        // incrémente la fonction qui sera appelée lorsque l'action sera effectuée
        inputActionReference_ActiveTrigger.action.performed += ActiveTrigger;
    }

    public override void OnDisable()
    {
        // s'exécute lorsque le script devient inactif (disable)
        // retire la fonction qui sera appelée lorsque l'action sera effectuée
        inputActionReference_ActiveTrigger.action.performed -= ActiveTrigger;
    }

    private void ActiveTrigger(InputAction.CallbackContext obj)
    {
        // Bouton enfoncé, on active le rayon
        // ReadValue<float> permet de récuperé la valeur de type float contenu dans le paramètre obj
        if (obj.ReadValue<float>() == 1f && boutFusil != null)
        {
            if (photonView.IsMine){
                //Instancier une nouvelle balle
                GameObject nouvelleBalle = PhotonNetwork.Instantiate(balle.name, boutFusil.transform.position, boutFusil.transform.rotation, 0, null);

                //Lui appliquer une v�locit� pour la projeter vers l'avant
                nouvelleBalle.GetComponent<Rigidbody>().velocity = boutFusil.transform.forward * 40;

                //Appeler la fonction pour le son du tir sur réseau
                photonView.RPC("JoueSonTir", RpcTarget.All);
            }
        }
    }

    //Fonction qui permet de jouer le son de tir
    [PunRPC]
    void JoueSonTir()
    {
        GetComponent<AudioSource>().PlayOneShot(tirSon);
    }
}

