﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Random_map_tester : MonoBehaviour
{   
    [Header("Dimension de la Map")] // Noms de l'area  
    public int LargeurMap = 20; // On met par défaut la largeur de la map sur 20
    public int HauteurMap = 20; // On met par défaut la hauteur de la map sur 20

    [Space] // Espacement dans l'inspecteur 
    [Header("Visualisation de la Map")] // Noms de l'espace dans l'inspecteur   
    public GameObject mapContainer; // Game object de la map 
    public GameObject PrefabsGrass; // Prefab du sol de la map 
    public GameObject WallOutsidePrefab;
    public GameObject Spawn;
    public GameObject WallInsidePrefab;
    public GameObject BoxPrefab;
    public GameObject Perso;
    public GameObject Chemin;
    public int start_chemin = 22;
    public int arrive_chemin = 222;
    public Vector2 Taille_tuile = new Vector2(16, 16); // taille en pixel d'une tuile


    [Space] // Espacement dans l'inspecteur 
    [Header("Texture de la Map")] // Noms de l'espace dans l'inspecteur   
    public Texture2D Map_Texture; // Texture du gameObject

    [Space] // Espacement dans l'inspecteur 
    [Header("Decorations")] // Noms de l'espace dans l'inspecteur   
    [Range(0, 90)] // pourcentage du gameObject
    public int PourcentageBox = 40;
    [Range(0, 90)] // pourcentage du gameObject
    public int PourcentageMurInGame = 15;
    public Map map;
    public Search recherche;
    // Use this for initialization
    void Start()
    {
        map = new Map(); // Instanciation de la map 
        recherche = new Search(map);
    }

   
    public void Generer_Map()
    {
        map.NewMap(LargeurMap, HauteurMap);
        map.Creer_terrain(PourcentageBox, PourcentageMurInGame);
        
        
        CreateGrid();      
    }
   public void Chercher_chemin()
    {
        if (map.tuiles[start_chemin].passable == true && map.tuiles[arrive_chemin].passable == true)
        {
            map.adjacent_tile();       
            recherche.Start(map.tuiles[start_chemin], map.tuiles[arrive_chemin]);
            while (!recherche.fini) // tant que la recherche n'est pas fini on continue  
            {
                recherche.Step();
            }
           
            print("Count node " + recherche.chemin.Count + " iterations" + recherche.iterations);
        }        
        if(map.tuiles[start_chemin].passable != true)
        {
            Debug.Log("La position de départ : " + start_chemin + " n'est pas correcte");
        }
        if (map.tuiles[arrive_chemin].passable != true)
        {
            Debug.Log("La position d'arrivée : " + arrive_chemin + " n'est pas correcte");
        }
        if(recherche.chemin.Count == 0)
        {
            Debug.Log("Pas de chemin possible");
        }
        montrer_le_chemin();
    }
    void CreateGrid() // Creation de la grille de Sprite 
    {
        Supprimer_Tuile();
        Sprite[] Sprites = Resources.LoadAll<Sprite>(Map_Texture.name);// On charche dans le Tableau de Sprite Sprites toutes 
                                                                       // les textures que on a découpé dans l'éditeur   

        var total = map.tuiles.Length; // total prend la valeur de la taille du tableau de tuile dans la classe "map"
        var Max_nb_colonne = map.colonnes; // Nombre de colonne dans notre map
        var Max_nb_ligne = map.ligne; // Nombre de ligne dans notre map 
        var colonne = 0; // On instancie le nombre de colonne a 0 ( on va parcourir notre map grace à ça ) 
        var ligne = 0; // Même principe que pour les colonnes 
        var nb_wall_outside = 0; // nombre de mur composant notre map ( pour nommer les game object )   
        var nb_grass = 0;
        var nb_spawn = 0;
        var nb_box = 0;
        var nb_wall_inside = 0;
        var personnage_1 = Instantiate(Perso);
        for (int i = 0; i < total; i++) // Pour i allant de 0 à la taille de notre tableau 
        {
            colonne = i % Max_nb_colonne; // Colonne prend la valeur de i modulo le nombre maximum de colonne      

            var Tuile_X = colonne * Taille_tuile.x; // Position en x de la tuile 
            var Tuile_Y = -ligne * Taille_tuile.y; // Position en y de la tuile 
            var t = map.tuiles[i];
            var id_sprite = t.id;

            var grass_dirt_go = Instantiate(PrefabsGrass); // On instancie un GameObject
            grass_dirt_go.name = "Grass_ " + nb_grass; // Il prendra le nom "tuile" + l'indice dans le tableau 
            grass_dirt_go.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
            grass_dirt_go.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle 
            var sprite_render = grass_dirt_go.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
            sprite_render.sprite = Sprites[1];
            nb_grass++;

            if (id_sprite == 0)
            {
                var Spawngo = Instantiate(Spawn); // On instancie un GameObject
                Spawngo.name = "Spawn " + nb_spawn; // Il prendra le nom "tuile" + l'indice dans le tableau 
                Spawngo.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
                if (nb_spawn == 1)
                {
                    personnage_1.name = "Joueur";
                    personnage_1.transform.SetParent(mapContainer.transform);
                    personnage_1.transform.position = new Vector3(Tuile_X, Tuile_Y, 0);
                }
                Spawngo.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle 
                var Spawn_sprite_render = Spawngo.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
                Spawn_sprite_render.sprite = Sprites[0];
                nb_spawn++;
                map.tuiles[i].spawn = true;
                map.tuiles[i].passable = true;
                var moveScript = Camera.main.GetComponent<MoveCamera>();
                moveScript.player = personnage_1;

            }
            if (id_sprite < 15 && id_sprite > 0)
            {
                var Wall_out = Instantiate(WallOutsidePrefab); // On instancie un GameObject
                Wall_out.name = "Wall_out_" + nb_wall_outside; // Il prendra le nom "tuile" + l'indice dans le tableau 
                Wall_out.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
                Wall_out.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle 
                var Wall_out_sprite_render = Wall_out.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
                Wall_out_sprite_render.sprite = Sprites[74];
                nb_wall_outside++;
                map.tuiles[i].mur = true;
                map.tuiles[i].passable = false;
            }
            if (id_sprite == 73)
            {
                var Box = Instantiate(BoxPrefab); // On instancie un GameObject
                Box.name = "Box_ " + nb_box; // Il prendra le nom "tuile" + l'indice dans le tableau 
                Box.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
                Box.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle 
                var Box_sprite_render = Box.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
                Box_sprite_render.sprite = Sprites[id_sprite];
                map.tuiles[i].carton = true;
                map.tuiles[i].passable = false;
                nb_box++;
            }
            if (id_sprite == 64)
            {
                var Wall_in = Instantiate(WallInsidePrefab); // On instancie un GameObject
                Wall_in.name = "Wall_in_ " + nb_wall_inside; // Il prendra le nom "tuile" + l'indice dans le tableau 
                Wall_in.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
                Wall_in.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle 
                var Wall_in_Box_sprite_render = Wall_in.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
                Wall_in_Box_sprite_render.sprite = Sprites[id_sprite];
                nb_wall_inside++;
                map.tuiles[i].passable = false;
                map.tuiles[i].mur = true;
            }

            if (colonne == (Max_nb_colonne - 1)) // si le numéro de la colonne courante est égales au nombre de colonne de la map 
            {
                ligne++; // On passe à la ligne suivante 
            }

        }
    }



    void Supprimer_Tuile()
    {
        var enfant = mapContainer.transform.GetComponentsInChildren<Transform>();
        for (var i = enfant.Length - 1; i > 0; i--)
        {
            Destroy(enfant[i].gameObject);
        }
        
    }

    void montrer_le_chemin() // Fonction de test pour visualiser le chemin générer par A* ( classe Search ) 
    {
        supprimer_chemin();
        var total = map.tuiles.Length; // total prend la valeur de la taille du tableau de tuile dans la classe "map"
        var Max_nb_colonne = map.colonnes; // Nombre de colonne dans notre map
        var Max_nb_ligne = map.ligne; // Nombre de ligne dans notre map 
        var colonne = 0; // On instancie le nombre de colonne a 0 ( on va parcourir notre map grace à ça ) 
        var ligne = 0; // Même principe que pour les colonnes 

        for (int i = 0; i < total; i++) // Pour i allant de 0 à la taille de notre tableau 
        {
            colonne = i % Max_nb_colonne; // Colonne prend la valeur de i modulo le nombre maximum de colonne      

            var Tuile_X = colonne * Taille_tuile.x; // Position en x de la tuile 
            var Tuile_Y = -ligne * Taille_tuile.y; // Position en y de la tuile 

            foreach (Tile t in recherche.chemin)
            {
                if (t == map.tuiles[i])
                {
                    var chemin = Instantiate(Chemin);
                    chemin.transform.SetParent(mapContainer.transform);
                    chemin.transform.position = new Vector3(Tuile_X, Tuile_Y, 0);
                }
            }
            if (colonne == (Max_nb_colonne - 1)) // si le numéro de la colonne courante est égales au nombre de colonne de la map 
            {
                ligne++; // On passe à la ligne suivante 
            }
        }
        recherche.acceptable.Clear();// On réinitialise nos liste 
        recherche.chemin.Clear();// On réinitialise nos liste 
        recherche.vue.Clear();// On réinitialise nos liste 
        recherche.fini = false;
    }
    void supprimer_chemin()// si on veut générer différents chemin possible 
    {
        var t = GameObject.FindGameObjectsWithTag("Chemin");
        foreach(GameObject go in t)
        {
            Destroy(go);
        }
    }    
   }
   