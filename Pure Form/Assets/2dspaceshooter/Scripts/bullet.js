#pragma strict

//here is the bullet speed that can be access in the inspector because its a public variable
var bulletSpeed:float = 24.0;

//we get the bullet moving at the speed set to the bullet in the inspector.
function Start () {
rigidbody.velocity.x = bulletSpeed;
}


function Update () {
//if the bullet goes too far right, we destroy the bullet.
if(transform.position.x > 12){
Destroy(gameObject);
}
}

//if the bullet hits an object tagged as an enemy, it'll send them a message that they were hit, then destroy itself.
function OnTriggerEnter (other : Collider) {
if(other.tag == "enemy"){
other.BroadcastMessage("hit", SendMessageOptions.DontRequireReceiver);
Destroy(gameObject);
}
}