#pragma strict

//are the public variables for ship health that can be accessed in the inspector. we are using the explosion animation object and gameover text for when we die.
var explosion:GameObject;
var gameOverText:GUIText;

function Start () {
//make the gameover text not enabled on start so it doesn't say gameover while playing.
gameOverText.guiText.enabled = false;
}

//if we're hit by an enemy or bullet, we die. (one hit kill i know, sucks right?)
function OnTriggerEnter (other : Collider){
if(other.tag == "enemy" || other.tag == "enemybullet"){
//here we spawn the explosion animation.
Instantiate(explosion,transform.position,Quaternion.Euler(-90,Random.Range(-180,180),0));
death();
}
}

//on death we need to do some stuff so the ship can look like it explodes and the gameover text shows up.
function death () {
//here we turn off a bunch of stuff like the renderer and scripts for the ship so its not controllable or viewable by the player anymore, and not able to be hit by enemies.
var playerControls = gameObject.GetComponent(playercontrols);
var weaponSystem = gameObject.GetComponent(weaponsystem);
var fire = GameObject.Find("fire");
playerControls.enabled = false;
weaponSystem.enabled = false;
fire.renderer.enabled = false;
collider.enabled = false;
renderer.material.color.a = 0.0;

//here we turn the game over text back on
gameOverText.guiText.enabled = true;
//here we wait for 3 seconds so the scene doesn't load right away again.
yield WaitForSeconds (3);
//here we find the name of the scene we were just playing, and load it again.
var lvlName:String = Application.loadedLevelName;
Application.LoadLevel(lvlName);
}