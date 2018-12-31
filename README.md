#BomberMan_ADSA
Projet ADSA at ESILV: Bomberman on unity
----------------------------------------------------------------------------------------------------------------------------------------------
Git : https://github.com/alexbrtin/BomberMan_ADSA

Guillaume GERARD  = > GuillaumeeG
Alexandre BROUTIN = > alexbrtin
Ugo CHIRAT 		  = > Ugo10 

----------------------------------------------------------------------------------------------------------------------------------------------
Movie : https://www.youtube.com/watch?v=SHCnmDnOL_0&feature=youtu.be&fbclid=IwAR1Dxlhwr-0RLwHYn5FbRhcPt-RgZsQhRvpq8M71g52tKv6CiBo8SyIeOOw

----------------------------------------------------------------------------------------------------------------------------------------------

The Bomberman isn't finish but the principal function we wanted to implement are here.

1) The random generation of the map :
   - You can change each parameter of the map such as : 
			- width
			- height
			- percentage of destructible block wich can spawn 
			- percentage of indestructible block wich can spawn ( take care to don't block your character; the functiun to avoid that isn't implement)
			
	-There is 4 spaw position available for 4 character ( one player and three monster )

2) The character 
	- You can move your character and drop bomb but the destruction of block isn't working 
	- use the up,down,left and right key
	
3) Monster 
	- The monster are not available in this version but the algorythm who rule their move is here 
	- We used A* to find the fastest way possible the way to reach the character 
	- Here to visualize it as there isn't any monster we manualy enter a start and end position and the algorythm show us the way)

----------------------------------------------------------------------------------------------------------------------------------------------
To test our game you need to launch it with unity ( after the 5.3 version because we devellop it in this unity version ) 