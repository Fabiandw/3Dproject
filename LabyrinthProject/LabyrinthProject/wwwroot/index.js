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

    var wallGeometry = new THREE.CubeGeometry(2, 3, 1.1, 1, 1, 1);
    var wallMaterial = new THREE.MeshBasicMaterial({ color: 0xffffff });
    var wireMaterial = new THREE.MeshBasicMaterial({ color: 0x000000, wireframe: true });

    var wall = new THREE.Mesh(wallGeometry, wallMaterial);
    wall.position.set(0, 1.5, 0);
    scene.add(wall);
    var wall = new THREE.Mesh(wallGeometry, wireMaterial);
    wall.position.set(0, 1.5, 0);
    scene.add(wall);
	
	
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

           /**
            * Load an OBJ model from the server
            * @param {string} modelPath
            * @param {string} modelName
            * @param {string} texturePath
            * @param {string} textureName
            * @param {function(THREE.Mesh): void} onload
            * @return {void}
            */
            function loadOBJModel(modelPath, modelName, texturePath, textureName, onload) {
                new THREE.MTLLoader()
                    .setPath(texturePath)
                    .load(textureName, function (materials) {

                        materials.preload();

                        new THREE.OBJLoader()
                            .setPath(modelPath)
                            .setMaterials(materials)
                            .load(modelName, function (object) {
                                onload(object);
                            }, function () { }, function (e) { console.log("Error loading model"); console.log(e); });
                    });
            }

            exampleSocket = new WebSocket("ws://" + window.location.hostname + ":" + window.location.port + "/connect_client");
            exampleSocket.onmessage = function (event) {
                var command = parseCommand(event.data);

                if (command.command == "update") {
                    if (Object.keys(worldObjects).indexOf(command.parameters.guid) < 0) {
                        if (command.parameters.type == "player") {

                        }
                        else if (command.parameters.type == "wall") {
                            //Position and Dimensions need to be optained from the object, but idk if this works
                            var geometry = new THREE.PlaneGeometry(command.parameters.length, command.parameters.width, command.parameters.height);
                            var material = new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/wall.png"), side: THREE.DoubleSide });
                            var wall = new THREE.Mesh(geometry, material);

                            var walls = new THREE.Group();
                            walls.add(wall);

                            scene.add(walls);
                            worldObjects[command.parameters.guid] = walls;
                        }
                        else if (command.parameters.type == "decoration") {
                            var decorations = new THREE.Group();
                            worldObjects[command.parameters.guid] = decorations;

                            //Examples of decoration types
                            if (command.parameters.decoType == "tree") {
                                loadOBJModel("models/tree/", "tree.obj", "models/tree/", "tree.mtl", (mesh) => {
                                    decorations.add(mesh);
                                    scene.add(decorations);
                                });
                            }
                            else if (command.parameters.decoType == "torch") {
                                loadOBJModel("models/torch/", "torch.obj", "models/torch/", "torch.mtl", (mesh) => {
                                    decorations.add(mesh);
                                    scene.add(decorations);
                                });
                            }
                        }
                    }

                    var object = worldObjects[command.parameters.guid];

                    object.position.x = command.parameters.x;
                    object.position.y = command.parameters.y;
                    object.position.z = command.parameters.z;

                    object.rotation.x = command.parameters.rotationX;
                    object.rotation.y = command.parameters.rotationY;
                    object.rotation.z = command.parameters.rotationZ;
                }
            }

window.onload = init;
