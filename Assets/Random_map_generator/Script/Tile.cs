using UnityEngine;
using System.Collections;

public enum Voisins // Enumeration des vosins en fonction de leur cardinalité 
{
    Sud,// Cellule en dessous
    Est,// Cellule à droite
    Ouest,// Cellule à gauche 
    Nord// Cellule au dessus 
}


public class Tile
{
    public int id = 0; // Chaque Tuile (Tile) aura un id propre 
    public Tile[] voisins = new Tile[4];// On aura besoin de connaitre les voisins de la cellule en cours ( 4 voisins direct ) 

	
	
}
