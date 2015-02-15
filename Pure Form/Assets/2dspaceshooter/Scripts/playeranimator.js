#pragma strict

//This ship only uses 3 different textures to create the look that its turning one way or the other
var shipUp:Texture;
var shipIdle:Texture;
var shipDown:Texture;


//Here will access the camera to use for mobile controls, we'll find it in function start.
private var mainCamera:GameObject;

function Start () {
mainCamera = GameObject.Find("Main Camera");
}

function Update () {

#if UNITY_WEBPLAYER
//here we base which ship animation is used on z's velocity of the ship
if(rigidbody.velocity.z == 0 && renderer.material.mainTexture != shipIdle){
renderer.material.mainTexture = shipIdle;
}
if(rigidbody.velocity.z > 0 && renderer.material.mainTexture != shipUp){
renderer.material.mainTexture = shipUp;
}
if(rigidbody.velocity.z < 0 && renderer.material.mainTexture != shipDown){
renderer.material.mainTexture = shipDown;
}
#endif

#if UNITY_STANDALONE
//here we base which ship animation is used on z's velocity of the ship
if(rigidbody.velocity.z == 0 && renderer.material.mainTexture != shipIdle){
renderer.material.mainTexture = shipIdle;
}
if(rigidbody.velocity.z > 0 && renderer.material.mainTexture != shipUp){
renderer.material.mainTexture = shipUp;
}
if(rigidbody.velocity.z < 0 && renderer.material.mainTexture != shipDown){
renderer.material.mainTexture = shipDown;
}
#endif

#if UNITY_ANDROID
//here we base which ship animation is used on if the finger is touch above or below the ship
if(Input.touchCount > 0){
	for(var touch1 : Touch in Input.touches) {
		var tapPosition = mainCamera.camera.ScreenToWorldPoint(touch1.position);
		if(tapPosition.z > transform.position.z + 0.25 || tapPosition.z < transform.position.z - 0.25){
			if(tapPosition.z > transform.position.z + 0.25){
				renderer.material.mainTexture = shipUp;
			}
			if(tapPosition.z < transform.position.z - 0.25){
				renderer.material.mainTexture = shipDown;
			}
		}else{
			renderer.material.mainTexture = shipIdle;
		}
	}
}else{
	renderer.material.mainTexture = shipIdle;
}
#endif

#if UNITY_IPHONE
//here we base which ship animation is used on if the finger is touch above or below the ship
if(Input.touchCount > 0){
	for(var touch1 : Touch in Input.touches) {
		var tapPosition = mainCamera.camera.ScreenToWorldPoint(touch1.position);
		if(tapPosition.z > transform.position.z + 0.25 || tapPosition.z < transform.position.z - 0.25){
			if(tapPosition.z > transform.position.z + 0.25){
				renderer.material.mainTexture = shipUp;
			}
			if(tapPosition.z < transform.position.z - 0.25){
				renderer.material.mainTexture = shipDown;
			}
		}else{
			renderer.material.mainTexture = shipIdle;
		}
	}
}else{
	renderer.material.mainTexture = shipIdle;
}
#endif

}