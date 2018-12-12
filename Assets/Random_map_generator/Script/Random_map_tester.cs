using UnityEngine;
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
    public GameObject tile_grass; // Prefab du sol de la map 
    public GameObject Wall_outside_Prefab;
    public GameObject Wall_inside_Prefab;
    public GameObject Box_Prefab;    
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
    // Use this for initialization
    void Start()
    {
        map = new Map(); // Instanciation de la map 

    }

    // Update is called once per frame
    public void Generer_Map()
    {
        map.NewMap(LargeurMap, HauteurMap);
        map.Creer_terrain(PourcentageBox, PourcentageMurInGame);
        
        
        CreateGrid();
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

        for (int i = 0; i < total; i++) // Pour i allant de 0 à la taille de notre tableau 
        {
            colonne = i % Max_nb_colonne; // Colonne prend la valeur de i modulo le nombre maximum de colonne            
            

            var Tuile_X = colonne * Taille_tuile.x; // Position en x de la tuile 
            var Tuile_Y = -ligne * Taille_tuile.y; // Position en y de la tuile 

           
            var t = map.tuiles[i];
            var id_sprite = t.id;

           

            
            if (id_sprite == 15)
            {
                var grass_dirt_go = Instantiate(tile_grass); // On instancie un GameObject
                grass_dirt_go.name = "Grass_ " + nb_grass; // Il prendra le nom "tuile" + l'indice dans le tableau 
                grass_dirt_go.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
                grass_dirt_go.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle 
                var sprite_render = grass_dirt_go.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
                sprite_render.sprite = Sprites[1];
                nb_grass++;
            }
            if(id_sprite == 0)
            {
                var grass_dirt_go = Instantiate(tile_grass); // On instancie un GameObject
                grass_dirt_go.name = "Spawn " + nb_spawn; // Il prendra le nom "tuile" + l'indice dans le tableau 
                grass_dirt_go.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
                grass_dirt_go.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle 
                var sprite_render = grass_dirt_go.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
                sprite_render.sprite = Sprites[0];
                nb_spawn++;
            }
            if (id_sprite <15 && id_sprite>0)
            {
                var grass_dirt_go = Instantiate(Wall_outside_Prefab); // On instancie un GameObject
                grass_dirt_go.name = "Wall_ " + nb_wall_outside; // Il prendra le nom "tuile" + l'indice dans le tableau 
                grass_dirt_go.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
                grass_dirt_go.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle 
                var sprite_render = grass_dirt_go.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
                sprite_render.sprite = Sprites[91];
                nb_wall_outside++;
            }
            if(id_sprite>15)
            {
                var grass_dirt_go = Instantiate(Box_Prefab); // On instancie un GameObject
                grass_dirt_go.name = "Box_ " + nb_box; // Il prendra le nom "tuile" + l'indice dans le tableau 
                grass_dirt_go.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
                grass_dirt_go.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle 
                var sprite_render = grass_dirt_go.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
                sprite_render.sprite = Sprites[90];
                nb_box++;
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

}
