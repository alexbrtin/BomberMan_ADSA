using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    public float vitesse_deplacement = 4f; // Vitesse de deplacement
   
    public Vector3 Pos_depart; // Position de départ 
    private bool deplacement;// boolean pour savoir si la camera est déjà en déplacement 

    
    void FixedUpdate()// Méthode qui permettra de déplacer la caméra dans la scène 
    {
        if (Input.GetMouseButtonDown(1))// Si il y a un clic droit de la souris (enfoncé)
        {
            Pos_depart = Input.mousePosition; // On récupère les coordonnées du curseur de la souris dans Pos_depart
            deplacement = true; // On passe le boolean du deplacement à true
           

        }
        if (Input.GetMouseButtonUp(1) && deplacement)// Si il y a un clic droit de la souris (relaché)
        {
            deplacement = false; // on arrete passe deplacement à true 
          
        }
        if (deplacement) // Si deplacement est a true 
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - Pos_depart); // On déplace la souris par rapport à la position
                                                                                               // initialie et la position ou elle est actuellement 
         
            Vector3 move = new Vector3(pos.x * vitesse_deplacement, pos.y * vitesse_deplacement, 0); // Deplacement 

            transform.Translate(move, Space.Self);// On change les coordonnées de la camera 
            
        }
    }
}
