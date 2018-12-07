using UnityEngine;
using System.Collections;

public class Random_map_tester : MonoBehaviour
{
    [Header("Dimension de la Map")] // Noms de l'area  
    public int LargeurMap = 20; // On met par défaut la largeur de la map sur 20
    public int HauteurMap = 20; // On met par défaut la hauteur de la map sur 20

    [Space] // Espacement dans l'inspecteur 
    [Header("Visualisation de la Map")] // Noms de l'espace dans l'inspecteur   
    public GameObject mapContainer;
    public GameObject tilePrefab;
    public Vector2 Taille_tuile = new Vector2(16, 16); // taille en pixel d'une tuile 

    [Space] // Espacement dans l'inspecteur 
    [Header("Texture de la Map")] // Noms de l'espace dans l'inspecteur   
    public Texture2D Map_Texture; // Texture du gameObject


    public Map map; 

    // Use this for initialization
    void Start ()
    {
        map = new Map(); // Instanciation de la map 

	}
	
	// Update is called once per frame
	public void Generer_Map ()
    {
        map.NewMap(LargeurMap, HauteurMap);
       
        Supprimer_Tuile();
        CreateGrid();
    }
    void CreateGrid() // Creation de la grille de Sprite 
    {
        Sprite[] Sprites = Resources.LoadAll<Sprite>(Map_Texture.name);// On charche dans le Tableau de Sprite Sprites toutes 
                                                                       // les textures que on a découpé dans l'éditeur
        var indice_mur_inside = 91;
        var indice_mur_outside = 75;
        var indice_carton = 90;
        var indice_herbe = 1;
        var indice_terre = 0;
        var total = map.tuiles.Length; // total prend la valeur de la taille du tableau de tuile dans la classe "map"
        var Max_nb_colonne = map.colonnes; // Nombre de colonne dans notre map
        var colonne = 0; // On instancie le nombre de colonne a 0 ( on va parcourir notre map grace à ça ) 
        var ligne = 0; // Même principe que pour les colonnes 

        for (int i = 0; i < total; i++) // Pour i allant de 0 à la taille de notre tableau 
        {

            colonne = i % Max_nb_colonne; // Colonne prend la valeur de i modulo le nombre maximum de colonne
                                           

            var Tuile_X = colonne * Taille_tuile.x; // Position en x de la tuile 
            var Tuile_Y = -ligne * Taille_tuile.y; // Position en y de la tuile 

            var Game_object = Instantiate(tilePrefab); // On instancie un GameObject
            Game_object.name = "Tuile_ " + i; // Il prendra le nom "tuile" + l'indice dans le tableau 
            Game_object.transform.SetParent(mapContainer.transform); // On place le game object en tant qu'enfant de la grille dans la hierarchie 
            Game_object.transform.position = new Vector3(Tuile_X, Tuile_Y, 0); // On change la position actuelle 

            var id_sprite = 1; // A modifier 
            var sprite_render = Game_object.GetComponent<SpriteRenderer> (); // Pour obtenir l'instance du Sprite renderer
            sprite_render.sprite = Sprites[id_sprite];
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
