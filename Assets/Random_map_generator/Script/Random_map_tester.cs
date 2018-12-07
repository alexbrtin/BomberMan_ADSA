using UnityEngine;
using System.Collections;

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
    [Header("Remplissage de la map")] // Noms de l'area  
    public int pourcentage_Box = 50;
        





     

    [Space] // Espacement dans l'inspecteur 
    [Header("Texture de la Map")] // Noms de l'espace dans l'inspecteur   
    public Texture2D Map_Texture; // Texture du gameObject


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

        Supprimer_Tuile();
        CreateGrid();
    }
    void CreateGrid() // Creation de la grille de Sprite 
    {
        Sprite[] Sprites = Resources.LoadAll<Sprite>(Map_Texture.name);// On charche dans le Tableau de Sprite Sprites toutes 
                                                                       // les textures que on a découpé dans l'éditeur
        var indice_mur_inside = 75; // Indice dans mon tableau de sprites des composants de la map 
        
        var indice_mur_outside = 91;
        
        var indice_carton = 90;
        
        var indice_herbe = 1;
        
        var indice_terre = 0;
        


        var total = map.tuiles.Length; // total prend la valeur de la taille du tableau de tuile dans la classe "map"
        var Max_nb_colonne = map.colonnes; // Nombre de colonne dans notre map
        var Max_nb_ligne = map.ligne; // Nombre de ligne dans notre map 
        var colonne = 0; // On instancie le nombre de colonne a 0 ( on va parcourir notre map grace à ça ) 
        var ligne = 0; // Même principe que pour les colonnes 
        var nb_wall_outside = 0; // nombre de mur composant notre map ( pour nommer les game object ) 
        var nb_box = 0; // nombre de mur composant notre map ( pour nommer les game object ) 

        for (int i = 0; i < total; i++) // Pour i allant de 0 à la taille de notre tableau 
        {
            colonne = i % Max_nb_colonne; // Colonne prend la valeur de i modulo le nombre maximum de colonne
            var test_ligne = 0;
            if (colonne > 0)
            {
                test_ligne = Max_nb_colonne % colonne;
            }

            var Tuile_X = colonne * Taille_tuile.x; // Position en x de la tuile 
            var Tuile_Y = -ligne * Taille_tuile.y; // Position en y de la tuile 

            var grass_dirt_go = Instantiate(tile_grass); // On instancie un GameObject

            grass_dirt_go.name = "Tuile_ " + i; // Il prendra le nom "tuile" + l'indice dans le tableau 
            grass_dirt_go.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
            grass_dirt_go.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle 

            var sprite_render = grass_dirt_go.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
            sprite_render.sprite = Sprites[indice_herbe];
            if (colonne == (Max_nb_colonne - 1)) // si le numéro de la colonne courante est égales au nombre de colonne de la map 
            {
                ligne++; // On passe à la ligne suivante 
            }

            if (colonne == 0 || colonne == Max_nb_colonne - 1|| ligne == 0 || ligne == Max_nb_ligne-1)
            {             
                var Wall_outside_go = Instantiate(Wall_outside_Prefab);
                Wall_outside_go.name = "Wall_outside_" + nb_wall_outside;
                Wall_outside_go.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
                Wall_outside_go.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle                 
                var Wall_outside_renderer = Wall_outside_go.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
                Wall_outside_renderer.sprite = Sprites[indice_mur_outside];
                nb_wall_outside++;
                map.tuiles[i].mur = true;
            }
            if(map.tuiles[i].mur == false && !Spawn(Max_nb_colonne,Max_nb_ligne,ligne,colonne))
            {
                var Box_go = Instantiate(Box_Prefab);
                Box_go.name = "Box_" + nb_box;
                Box_go.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
                Box_go.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle        
                var Box_renderer = Box_go.GetComponent<SpriteRenderer>(); // Pour obtenir l'instance du Sprite renderer
                Box_renderer.sprite = Sprites[indice_carton];
                nb_box++;
                map.tuiles[i].carton = true;
            }
        }

    }
    bool Spawn(int Maxcolonne,int Maxligne,int ligne,int colonne)
    {
        bool spawn = false;
        if((colonne == 1 && ligne == 1) || (colonne == 2 && ligne == 1) || colonne == 1 && ligne == 2)
        {
            return true;
        }
        if ((colonne == Maxcolonne-2 && ligne == 1) || (colonne == Maxcolonne-3 && ligne == 1) || (colonne == Maxcolonne-2 && ligne == 2))
        {
            return true;
        }
        if ((colonne == 1 && ligne == Maxligne -2) || (colonne == 2 && ligne == Maxligne-2) || (colonne == 1 && ligne == Maxligne-3))
        {
            return true;
        }
        if ((colonne == Maxcolonne-2 && ligne == Maxligne-2) || (colonne == Maxcolonne - 3 && ligne == Maxligne - 2) || (colonne == Maxcolonne-2 && ligne == Maxligne-3))
        {
            return true;
        }
        return spawn;
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
