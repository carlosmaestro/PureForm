#pragma strict

// here are public variables for the enemy ship that can be accessed in the inspector
var health:int = 2;
var explosion:GameObject;
var expOrb:GameObject;
var enemyBullet:GameObject;
var expDrop:int = 3;
var hitSound:AudioClip;
var fireRate:float = 2.0;

//heres the private variable counter to keep track of time for fire rate.
private var counter:float = 0.0;

//we get the ship moving left once it is spawned.
function Start () {
rigidbody.velocity.x = -2.5;
}

function Update () {
//here we make counter count based on time for the fire rate
counter += Time.deltaTime;

//if the ship goes too far left, we destroy it.
if(transform.position.x < -12){
Destroy(gameObject);
}

//here we shoot 4 bullets if the counter counts higher than the fire rate.
if(counter > fireRate){
var custom1 = Instantiate(enemyBullet, transform.position - Vector3(0.5,0.1,0), Quaternion.Euler(-90,0,0));
var custom2 = Instantiate(enemyBullet, transform.position - Vector3(0.5,0.1,0), Quaternion.Euler(-90,0,0));
var custom3 = Instantiate(enemyBullet, transform.position- Vector3(0.5,0.1,0), Quaternion.Euler(-90,0,0));
var custom4 = Instantiate(enemyBullet, transform.position- Vector3(0.5,0.1,0), Quaternion.Euler(-90,0,0));
//to make the bullets spread, we add extra z velocity to each one to they all move on their own path.
custom1.rigidbody.velocity.z = 3;
custom2.rigidbody.velocity.z = 1;
custom3.rigidbody.velocity.z = -1;
custom4.rigidbody.velocity.z = -3;
counter = 0.0;
}

//end of function update
}

//if a bullet hits the ship, the bullets sends us the hit message to trigger this function to bring down the ships health
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

//if health is 0, then this function is triggered to spawn some orbs, spawn the explosion animation object, and destroy itself
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