using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour
{
     
    public static float Pixel_sprite = 1f; // = 1 Car on a choisi cette valeur au départ pour les sprites 
    public static float echelle = 1f; // = 1 Car ils sont tous à la même échelle 

    public Vector2 nativeResolution = new Vector2(160, 144); // Dimension GameBoy 
  
    void Awake() // Méthode 
    {
        var camera = GetComponent<Camera>(); // On passe la camera en référence de cette variable pour y attacher ce script
        if (camera.orthographic) // si la camera fonctionne de façon orthogonal ( comme on est en x,y,z)
        {
            var dir = Screen.height; // On récupère la taille de l'écran 
            var res = nativeResolution.y; // Res prend la valeur du y de la resolution native (144) dans notre cas 

            echelle = dir / res; // L'echelle s'adapte aux proportions de notre écran 
            Pixel_sprite *= echelle; // Pixel proportionnel à l'échelle  
            camera.orthographicSize = (dir / 2.0f) / Pixel_sprite; 
        }
    }
}
