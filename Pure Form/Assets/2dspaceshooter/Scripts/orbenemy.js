#pragma strict

//here are the public variables for the orb enemy so we can change them in the inspector
var health:int = 2;
var explosion:GameObject;
var expOrb:GameObject;
var bullet:GameObject;
var shotSpeed:float = 1.0;
var expDrop:int = 3;
var hitSound:AudioClip;

//private variables that the script uses, target will be set as the player in function start so we can track where the player is and make the orb's bullets follow him
private var target:GameObject;
private var zPosition:float;
//counter to keep track of time so the orb will fire at a specific rate.
private var counter:float = 0.0;

function Start () {
rigidbody.velocity.x = -4;
rigidbody.angularVelocity.y = Random.Range(-6,6);
zPosition = transform.position.z;
target = GameObject.Find("player");
}

function Update () {
//keeping track of time with counter here.
counter += Time.deltaTime;

//if the orb goes too far left, destroy it so its not being used anymore
if(transform.position.x < -12){
Destroy(gameObject);
}

//here we use Sin to give the orb the ability to move in a wave. without this it'd just go left with no wave.
if(Time.timeScale == 1){
transform.position.z = zPosition + Mathf.Sin(Time.time *2)*2;
}

//here we check if a bullet can be shot
if(counter > shotSpeed && target != null){
if(transform.position.x > target.transform.position.x + 6){
Instantiate(bullet,transform.position,Quaternion.Euler(-90,0,0));
}
counter = 0.0;
}

//end of function update
}

//we get receive a message from the player's bullet that it has been hit and it's health will go down.
function hit () {
health -= 1;
if(health != 0){
if(audio.enabled == true){
audio.PlayOneShot(hitSound);
}
}
if(health <= 0){
onDeath();
}
}

//once health is 0, this function will trigger and unleash some experience orbs and the explosion animation object
function onDeath () {
Instantiate(expOrb,transform.position,Quaternion.Euler(-90,0,0));
expDrop -= 1;
if(expDrop <= 0){
Instantiate(explosion,transform.position,Quaternion.Euler(-90,Random.Range(-180,180),0));
Destroy(gameObject);
}
if(expDrop > 0){
onDeath();
}
}