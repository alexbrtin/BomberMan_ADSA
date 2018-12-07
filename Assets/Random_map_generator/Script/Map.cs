using UnityEngine;
using System.Collections;

public class Map
{
    public Tile[] tuiles; 
    public int colonnes;
    public int ligne;


    public void NewMap(int largeur,int hauteur) // Méthode pour créer la map 
    {
        colonnes = largeur;// On initialise le nombre de colonne sur la largeur de la map choisi
        ligne = hauteur;// On initialise le nombre de ligne sur la hauteur de la map choisi
        tuiles = new Tile[colonnes * ligne]; // Pour gagner en espace si jamais on a à traiter
                                             // avec une très grande map on stock tout dans un tableau à une dimension
                                             // , il suffira d'effectuer le modulo de la taille du tableau par le nombre 
                                             // de ligne afin de savoir ou se placer
        Creer_Tuiles(); // On remplie la map avec l'ensemble des tuiles instanciées
    }
    private void Creer_Tuiles()// Méthode pour créer les tuiles qui composeront notre map 
    {
        int total = tuiles.Length; // total des tuiles composant notre map 

        for(int i = 0;i<total;i++) // Pour i allant de 0 à l'ensemble des tuiles de notre map 
        {
            var tuile = new Tile(); // On instancie une nouvelle tuile 
            tuile.id = i; // La tuile que on vient d'instancier prend comme id l'indice i  
            tuiles[i] = tuile; // La tuile instancié précédement est insérer dans le tableau de tuile 
                               // à la position i avec comme id i  
        }
    }

}
