var scene, camera, renderer, mesh;
var meshFloor, ambientLight, light;

var crate, crateTexture, crateNormalMap, crateBumpMap;

var keyboard = {};
var player = { height:1.8, speed:0.2, turnSpeed:Math.PI*0.02 };
var USE_WIREFRAME = false;
var textLoader = new THREE.TextureLoader();
function init(){
	scene = new THREE.Scene();
	camera = new THREE.PerspectiveCamera(90, 1280/720, 0.1, 1000);
	
	
	
	ambientLight = new THREE.AmbientLight(0xffffff, 0.2);
	scene.add(ambientLight);
	

	var geometry = new THREE.PlaneGeometry(150, 100, 32);
	var texture = textLoader.load('textures/asphalt.jpg');
	var material = new THREE.MeshLambertMaterial({ map : texture, side: THREE.DoubleSide });
	var plane = new THREE.Mesh(geometry, material);
	plane.rotation.x = Math.PI / 2.0;
	plane.position.x = 15;
	plane.position.z = 15;
	scene.add(plane);


	
	
	camera.position.set(0, player.height, -5);
	camera.lookAt(new THREE.Vector3(0,player.height,0));
	
	renderer = new THREE.WebGLRenderer();
	renderer.setPixelRatio(window.devicePixelRatio);
	renderer.setSize(window.innerWidth, window.innerHeight+5);

	renderer.shadowMap.enabled = true;
	renderer.shadowMap.type = THREE.BasicShadowMap;
	
	document.body.appendChild(renderer.domElement);
	var sphericalSkyboxGeometry = new THREE.SphereGeometry(200, 32, 32);
                var sphericalSkyboxMaterial = new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/skybox/cave.jpg"), side: THREE.DoubleSide });
                var sphericalSkybox = new THREE.Mesh(sphericalSkyboxGeometry, sphericalSkyboxMaterial);
                scene.add(sphericalSkybox);
	animate();
}

function animate(){
	requestAnimationFrame(animate);
	

	
	if(keyboard[87]){ // W key
		camera.position.x -= Math.sin(camera.rotation.y) * player.speed;
		camera.position.z -= -Math.cos(camera.rotation.y) * player.speed;
	}
	if(keyboard[83]){ // S key
		camera.position.x += Math.sin(camera.rotation.y) * player.speed;
		camera.position.z += -Math.cos(camera.rotation.y) * player.speed;
	}
	if(keyboard[65]){ // A key
		camera.rotation.y -= player.turnSpeed;
	}
	if(keyboard[68]){ // D key
		camera.rotation.y += player.turnSpeed;
	}
	
	renderer.render(scene, camera);
}

function keyDown(event){
	keyboard[event.keyCode] = true;
}

function keyUp(event){
	keyboard[event.keyCode] = false;
}

window.addEventListener('keydown', keyDown);
window.addEventListener('keyup', keyUp);

window.onload = init;
