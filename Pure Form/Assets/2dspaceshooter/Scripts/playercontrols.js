#pragma strict

//open variables that are accessed through inspector including ship speed and if the ship can be controls on 2-axis (left,right,up,down vs. up,down)
var shipSpeed:float = 16.0;
var twoAxis = true;

//we access the maincamera during start so we can use it for the mobile controls
private var mainCamera:GameObject;

function Start () {
//here we find the camera to set it up as the mainCamera variable
mainCamera = GameObject.Find("Main Camera");
}

function Update () {

#if UNITY_WEBPLAYER
//this is for web builds
//This checks to see if the player is pressing W, S, Up, or Down. This is connected to the else{} statement below
if(Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("up") || Input.GetKey("down")){
//If the player presses A, add velocity to move left.
if(Input.GetKey("s") || Input.GetKey("down")){
	if(rigidbody.velocity.z > 0){
		rigidbody.velocity.z = 0;
	}
	if(rigidbody.velocity.z > -shipSpeed){
		rigidbody.velocity.z -= 48*Time.deltaTime;
	}
}
//if the player pressed D, add velocity to move right.
if(Input.GetKey("w")|| Input.GetKey("up")){
	if(rigidbody.velocity.z < 0){
		rigidbody.velocity.z = 0;
	}
	if(rigidbody.velocity.z < shipSpeed){
		rigidbody.velocity.z += 48*Time.deltaTime;
	}
}
}else{
//use else to do the opposite of an if() statement. this stops the player if lets go of A or D
rigidbody.velocity.z = 0.0;
}

if(twoAxis == true){
if(Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("left") || Input.GetKey("right")){
//If the player presses A, add velocity to move left.
if(Input.GetKey("a") || Input.GetKey("left")){
	if(rigidbody.velocity.x > 0){
		rigidbody.velocity.x = 0;
	}
	if(rigidbody.velocity.x > -shipSpeed){
		rigidbody.velocity.x -= 48*Time.deltaTime;
	}
}
//if the player pressed D, add velocity to move right.
if(Input.GetKey("d")|| Input.GetKey("right")){
	if(rigidbody.velocity.x < 0){
		rigidbody.velocity.x = 0;
	}
	if(rigidbody.velocity.x < shipSpeed){
		rigidbody.velocity.x += 48*Time.deltaTime;
	}
}
}else{
rigidbody.velocity.x = 0.0;
}
}
#endif

#if UNITY_STANDALONE
//This is for desktop builds
//This checks to see if the player is pressing W, S, Up, or Down. This is connected to the else{} statement below
if(Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("up") || Input.GetKey("down")){
//If the player presses A, add velocity to move left.
if(Input.GetKey("s") || Input.GetKey("down")){
	if(rigidbody.velocity.z > 0){
		rigidbody.velocity.z = 0;
	}
	if(rigidbody.velocity.z > -shipSpeed){
		rigidbody.velocity.z -= 48*Time.deltaTime;
	}
}
//if the player pressed D, add velocity to move right.
if(Input.GetKey("w")|| Input.GetKey("up")){
	if(rigidbody.velocity.z < 0){
		rigidbody.velocity.z = 0;
	}
	if(rigidbody.velocity.z < shipSpeed){
		rigidbody.velocity.z += 48*Time.deltaTime;
	}
}
}else{
//use else to do the opposite of an if() statement. this stops the player if lets go of A or D
rigidbody.velocity.z = 0.0;
}

if(twoAxis == true){
if(Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("left") || Input.GetKey("right")){
//If the player presses A, add velocity to move left.
if(Input.GetKey("a") || Input.GetKey("left")){
	if(rigidbody.velocity.x > 0){
		rigidbody.velocity.x = 0;
	}
	if(rigidbody.velocity.x > -shipSpeed){
		rigidbody.velocity.x -= 48*Time.deltaTime;
	}
}
//if the player pressed D, add velocity to move right.
if(Input.GetKey("d")|| Input.GetKey("right")){
	if(rigidbody.velocity.x < 0){
		rigidbody.velocity.x = 0;
	}
	if(rigidbody.velocity.x < shipSpeed){
		rigidbody.velocity.x += 48*Time.deltaTime;
	}
}
}else{
rigidbody.velocity.x = 0.0;
}
}
#endif


#if UNITY_ANDROID
//here are android controls which will only be added to android platforms
if(Input.touchCount > 0){
	for(var touch2 : Touch in Input.touches) {
var tapPosition = mainCamera.camera.ScreenToWorldPoint(touch2.position);
tapPosition = tapPosition + Vector3(4,0,0);
		if(tapPosition.z > transform.position.z + 0.1 || tapPosition.z < transform.position.z - 0.1){
		transform.position.z = Mathf.SmoothStep(tapPosition.z,transform.position.z,Time.deltaTime*32);
		}
		//if twoaxis is checked, then the ship can move left and right as well
		if(twoAxis == true){
		if(tapPosition.x > transform.position.x + 0.1 || tapPosition.x < transform.position.x - 0.1){
		transform.position.x = Mathf.SmoothStep(tapPosition.x,transform.position.x,Time.deltaTime*32);
		}
		}
	}
}
#endif

#if UNITY_IOS
//here are ios controls which will only be added to ios platforms
if(Input.touchCount > 0){
	for(var touch2 : Touch in Input.touches) {
var tapPosition = mainCamera.camera.ScreenToWorldPoint(touch2.position);
tapPosition = tapPosition + Vector3(4,0,0);
		if(tapPosition.z > transform.position.z + 0.1 || tapPosition.z < transform.position.z - 0.1){
		transform.position.z = Mathf.SmoothStep(tapPosition.z,transform.position.z,Time.deltaTime*20);
		}
		//if twoaxis is checked, then the ship can move left and right as well
		if(twoAxis == true){
		if(tapPosition.x > transform.position.x + 0.1 || tapPosition.x < transform.position.x - 0.1){
		transform.position.x = Mathf.SmoothStep(tapPosition.x,transform.position.x,Time.deltaTime*20);
		}
		}
	}
}
#endif

//end of function update
}