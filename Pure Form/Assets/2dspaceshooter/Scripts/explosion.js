#pragma strict

//this script animates the explosion that happens only one time before its destroyed

//here are the public variables that can be accessed in the inspector. the explosion animation uses 7 textures.
var fire0:Texture;
var fire1:Texture;
var fire2:Texture;
var fire3:Texture;
var fire4:Texture;
var fire5:Texture;
var fire6:Texture;
//the explosion sound
var explodeSound:AudioClip;

//private variables that this script keeps track of to do the explosion animation
private var blankTexture:Texture;
private var counter:float = 0.0;
private var frameRate:float = 32.0;

function Start () {
//play the sound once the explosion is spawned
audio.PlayOneShot(explodeSound);
}

function Update () {
//keep track of time based on the frameRate speed we chose in the private variable. default is 32, but can be changed to anything to change the speed of the animation
counter += Time.deltaTime*frameRate;

//here we change the explosion texture based on the counter
if(counter > 0 && renderer.material.mainTexture != fire0){
renderer.material.mainTexture = fire0;
}
if(counter > 1 && renderer.material.mainTexture != fire1){
renderer.material.mainTexture = fire1;
}
if(counter > 2 && renderer.material.mainTexture != fire2){
renderer.material.mainTexture = fire2;
}
if(counter > 3 && renderer.material.mainTexture != fire3){
renderer.material.mainTexture = fire3;
}
if(counter > 4 && renderer.material.mainTexture != fire4){
renderer.material.mainTexture = fire4;
}
if(counter > 5 && renderer.material.mainTexture != fire5){
renderer.material.mainTexture = fire5;
}
if(counter > 6 && renderer.material.mainTexture != fire6){
renderer.material.mainTexture = fire6;
}
if(counter > 7 && renderer.material.color.a != 0.0){
renderer.material.color.a = 0.0;
}
//if the counter is higher than framerate*1.5, we destroy the explosion animation object. This is so the sound can play before its destroyed since an audio source is attached.
if(counter > frameRate*1.5){
Destroy(gameObject);
}

}