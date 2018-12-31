using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Random_map_tester))] // pour associer cette editeur à la classe Random_map_tester 
public class RandomMapTesterEditor : Editor // Editor pour indiquer que on dévellope un editeur 
{

    public override void OnInspectorGUI() // On surcharge la méthode pour permettre de la custom nous même 
    {
        DrawDefaultInspector(); // Pour que unity fasse le lien entre ce qui a été défini avant dans l'éditeur et le nouvel ajout 

        var script = (Random_map_tester)target; // On attache notre script de génération aléatoire à cet variable 

        if (GUILayout.Button("Générer la map")) // SI le bouton a été créer  
        {
            if (Application.isPlaying) // Si l'application est lancée 
            {
                script.Generer_Map(); // On créer la map 
            }
        }
        if(GUILayout.Button("Chercher chemin"))
        {
            if (Application.isPlaying) // Si l'application est lancée 
            {
                script.Chercher_chemin(); // On créer la map 
            }
        }

    }

}

