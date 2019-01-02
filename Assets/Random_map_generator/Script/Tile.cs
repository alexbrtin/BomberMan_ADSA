using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
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
    public bool mur = false;
    public bool carton = false;
    public bool spawn = false;
    public bool passable = true; 
    public Tile precedente = null;
    public List<Tile> adjacente = new List<Tile>();
    public void Add_voisins(Voisins v,Tile t)
    {
        voisins[(int)v] = t;
        calcule_id();
    }
    public void Supp_voisins(Tile t)
    {
        int total = voisins.Length;
        for(int i = 0;i<total;i++)
        {
            if(voisins[i] != null)
            {
                if(voisins[i].id == t.id) // chaque tuile a un id unique et donc si on trouve cet id dans le tableau
                {
                    voisins[i] = null; // on le supprime du tableau 
                }
            }
        }
        calcule_id(); // On recalcule les id
    }
    public void Clear() // pour retirer la référence de la tuile de tous ses voisins pour qu'elle n'existe dans aucun tableau de voisins
    {
        int total = voisins.Length;
        for(int i = 0;i < total;i++)
        {
            var t = voisins[i]; // voisins de notre tuile
            if(t != null) // si le voisin existe 
            {
                t.Supp_voisins(this);
                voisins[i] = null;
            }
        }
        calcule_id();
    }
    private void calcule_id()// Méthode qui en fonction des voisins de la tuile en cours va lui donner un id
    {
        
        var valeur_voisin = new StringBuilder(); //Stringbuilder permet de concaténer des string 
        foreach(Tile t in voisins)
        {
            valeur_voisin.Append(t == null ? "0" : "1");
        }
        id = Convert.ToInt32(valeur_voisin.ToString(), 2); // On convertit la chaine de caractère obtenue en int celon la base 2 
                                                           // Par exemple si une tuile à un voisin à l'ouest et un voisin au sud
                                                           // Celon l'énumération définit avant ça donnera 1 Sud 0 Est 1 Ouest 0 Nord 
                                                           // Donc 1010 ce qui donne 10
     

   
    }


	
}
