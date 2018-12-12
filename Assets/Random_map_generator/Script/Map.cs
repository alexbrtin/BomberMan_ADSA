using UnityEngine;
using System.Collections;
using System.Linq;

public enum type_tuile
{
    vide = -1,
    herbe = 15,
    box = 73,
    mur_ingame = 64,
    mur_outgame = 74,
    spawn = 0   
}
public class Map
{
    public Tile[] tuiles;
    public int colonnes;
    public int ligne;

    public Tile[] herbetuile
    {
        get
        {
            return tuiles.Where(t => t.id == (int)type_tuile.herbe).ToArray();
        }
    }
    public Tile[] spawntuile
    {
        get
        {
            return tuiles.Where(t => t.id == (int)type_tuile.spawn).ToArray();
        }
    }
    public Tile[] muroutgame
    {
        get
        {

            return tuiles.Where(t => t.id < (int)type_tuile.herbe && t.id > 0).ToArray();
        }
    }

    public void NewMap(int largeur, int hauteur) // Méthode pour créer la map 
    {
        colonnes = largeur;// On initialise le nombre de colonne sur la largeur de la map choisi
        ligne = hauteur;// On initialise le nombre de ligne sur la hauteur de la map choisi
        tuiles = new Tile[colonnes * ligne]; // Pour gagner en espace si jamais on a à traiter
                                             // avec une très grande map on stock tout dans un tableau à une dimension
                                             // , il suffira d'effectuer le modulo de la taille du tableau par le nombre 
                                             // de ligne afin de savoir ou se placer
        Creer_Tuiles(); // On remplie la map avec l'ensemble des tuiles instanciées
        Spawn();
    }
    public void Creer_terrain(int box_pourcentage, int muringame_pourcengage)
    {

        Remplir_map(herbetuile, muringame_pourcengage, type_tuile.mur_ingame);
        Remplir_map(herbetuile, box_pourcentage, type_tuile.box);

    }
    private void Creer_Tuiles()// Méthode pour créer les tuiles qui composeront notre map 
    {
        int total = tuiles.Length; // total des tuiles composant notre map 

        for (int i = 0; i < total; i++) // Pour i allant de 0 à l'ensemble des tuiles de notre map 
        {
            var tuile = new Tile(); // On instancie une nouvelle tuile 
            tuile.id = i; // La tuile que on vient d'instancier prend comme id l'indice i  
            tuiles[i] = tuile; // La tuile instancié précédement est insérer dans le tableau de tuile 
                               // à la position i avec comme id i  
        }
        Trouver_voisins();
        Spawn();
    }
    private void Trouver_voisins()// Méthode pour trouver les voisins de notre case 
    {
        for (int i = 0; i < ligne; i++) // Ligne et colonne corresponde à la hauteur et à la largeur de la map 
        {
            for (int j = 0; j < colonnes; j++)
            {
                var t = tuiles[colonnes * i + j];
                if (i < ligne - 1)
                {
                    t.Add_voisins(Voisins.Sud, tuiles[colonnes * (i + 1) + j]);
                }
                if (j < colonnes - 1)
                {
                    t.Add_voisins(Voisins.Est, tuiles[colonnes * i + j + 1]);
                }
                if (j > 0)
                {
                    t.Add_voisins(Voisins.Ouest, tuiles[colonnes * i + j - 1]);
                }
                if (i > 0)
                {
                    t.Add_voisins(Voisins.Nord, tuiles[colonnes * (i - 1) + j]);
                }
            }
        }
    }
    public void Remplir_map(Tile[] tab_t, int pourcentage, type_tuile type)
    {
        Spawn();
        int pourcentage_total = Mathf.FloorToInt((tab_t.Length * pourcentage) / 100); // calcule du taux de répartition dans la map 
        Mélange_FY(tab_t);
        for (int i = 0; i < pourcentage_total; i++)
        {
            var t = tab_t[i];

            if (type == type_tuile.vide) // si la tuile est vide 
            {
                t.Clear();

            }
            if (t.id > 0)
            {
                t.id = (int)type;
            }
        }
    }
    public void Mélange_FY(Tile[] tab_t)// Algo de mélange de Fisher-Yates 
    {
        Spawn();
        for (int i = 0; i < tab_t.Length; i++)
        {
            var tamp = tab_t[i];
            int r = Random.Range(i, tab_t.Length);
            if (tab_t[i].id >= 15 && tab_t[r].id >= 15)
            {
                tab_t[i] = tab_t[r];
                tab_t[r] = tamp;
            }
        }
    }
    void Spawn()
    {
        for (int i = 0; i < ligne; i++)
        {
            for (int j = 0; j < colonnes; j++)
            {
                var t = tuiles[colonnes * i + j];
                if ((j == 1 && i == 1) || (j == 2 && i == 1) || j == 1 && i == 2)
                {
                    t.spawn = true;
                    t.id = 0;
                }
                if ((j == colonnes - 2 && i == 1) || (j == colonnes - 3 && i == 1) || (j == colonnes - 2 && i == 2))
                {
                    t.spawn = true;
                    t.id = 0;
                }
                if ((j == 1 && i == ligne - 2) || (j == 2 && i == ligne - 2) || (j == 1 && i == ligne - 3))
                {
                    t.spawn = true;
                    t.id = 0;
                }
                if ((j == colonnes - 2 && i == ligne - 2) || (j == colonnes - 3 && i == ligne - 2) || (j == colonnes - 2 && i == ligne - 3))
                {
                    t.spawn = true;
                    t.id = 0;
                }               
            }
        }
    }
}

