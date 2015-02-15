#pragma strict

//here are the 3 enemys we want to spawn (the orb, asteroid, and ship)
var enemy1:GameObject;
var enemy2:GameObject;
var enemy3:GameObject;
//here is the minimum and maximum height we want them to spawn. it can be changed in the inspector
var maxHeight:float = 6.0;
var minHeight:float = -6.0;

//here are private variables we use for the spawner
private var counter:float = 0.0;
private var player:GameObject;
private var randomChoice:int;
private var spawnRate:float = 2.0;

function Update () {
//counter counts based on time here so the spawner can spawn based on time
counter += Time.deltaTime;
//here we make the spawner slowly spawn faster until it is spawning 3 a second.
if(spawnRate > 0.33){
spawnRate -= Time.deltaTime/80;
}
//if the counter is higher than the spawnrate number, it'll spawn an object
if(counter > spawnRate){
//before one is spawned we have to decide at random which one to spawn
randomChoice = Random.Range(1,6);
//asteroids take up 3 out of the 5 possible spawns, making it spawn the most often
if(randomChoice >= 1 && randomChoice <= 3){
Instantiate(enemy1, Vector3(transform.position.x,0,Random.Range(minHeight,maxHeight)), Quaternion.Euler(-90,0,0));
}
//if the random number ends up being 4, it'll spawn the orb, set as enemy2
if(randomChoice == 4){
Instantiate(enemy2, Vector3(transform.position.x,0,Random.Range(minHeight/3,maxHeight/3)), Quaternion.Euler(-90,0,0));
}
//if the random number ends up being 5, it'll spawn the ship, set as enemy3
if(randomChoice == 5){
Instantiate(enemy3, Vector3(transform.position.x,0,Random.Range(minHeight,maxHeight)), Quaternion.Euler(-90,90,0));
}

counter = 0.0;
}

}