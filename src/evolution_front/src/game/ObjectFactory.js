import * as THREE from "three";
import { GLTFLoader } from "three/examples/jsm/loaders/GLTFLoader";

import Ground from "../models/ground/scene.glb";
import Agama from "../models/animals/agama.glb";

const createCube = (ammo, sizes, color, position, mass) => {
  const geometry = new THREE.BoxBufferGeometry(
    sizes.width,
    sizes.height,
    sizes.depth
  );
  const material = new THREE.MeshPhongMaterial({ color: color });
  var cube = new THREE.Mesh(geometry, material);

  cube.position.x = position.x;
  cube.position.y = position.y;
  cube.position.z = position.z;

  let transform = new ammo.btTransform();
  transform.setIdentity();
  transform.setOrigin(
    new ammo.btVector3(cube.position.x, cube.position.y, cube.position.z)
  );
  transform.setRotation(
    new ammo.btQuaternion(cube.position.x, cube.position.y, cube.position.z, 1)
  );
  let motionState = new ammo.btDefaultMotionState(transform);

  let colShape = new ammo.btBoxShape(
    new ammo.btVector3(
      cube.scale.x * 0.5,
      cube.scale.y * 0.5,
      cube.scale.z * 0.5
    )
  );
  colShape.setMargin(0.05);

  let localInertia = new ammo.btVector3(0, 0, 0);
  colShape.calculateLocalInertia(mass, localInertia);

  let rbInfo = new ammo.btRigidBodyConstructionInfo(
    mass,
    motionState,
    colShape,
    localInertia
  );

  let body = new ammo.btRigidBody(rbInfo);
  cube.userData.physicsBody = body;

  return [cube, body];
};

const createLand = async (
  ammo,
  position,
  mass,
  onProgress = (xhr) => {},
  onError = (error) => console.log(error)
) => {
  let land,
    landRigid = null;

  let resolver = null;
  let rejecter = null;

  const loadAsync = new Promise(
    (resolve) => (resolver = resolve),
    (reject) => (rejecter = reject)
  );

  const loader = new GLTFLoader();
  loader.load(
    Ground,
    (gltf) => {
      gltf.scene.position.x = position.x;
      gltf.scene.position.y = position.y;
      gltf.scene.position.z = position.z;

      let transform = new ammo.btTransform();
      transform.setIdentity();
      transform.setOrigin(new ammo.btVector3(0, 0, 0));
      transform.setRotation(new ammo.btQuaternion(0, 0, 0, 1));

      let motionState = new ammo.btDefaultMotionState(transform);
      let localInertia = new ammo.btVector3(0, 0, 0);
      let colShape = new ammo.btBoxShape(new ammo.btVector3(10, 0.1, 10));
      colShape.setMargin(0.05);

      colShape.calculateLocalInertia(mass, localInertia);

      let rigidBodyInfo = new ammo.btRigidBodyConstructionInfo(
        mass,
        motionState,
        colShape,
        localInertia
      );
      let rBody = new ammo.btRigidBody(rigidBodyInfo);
      gltf.scene.userData.physicsBody = rBody;

      land = gltf.scene;
      landRigid = rBody;
      resolver();
    },
    onProgress,
    (error) => {
      rejecter();
      onError && onError(error);
    }
  );

  await loadAsync;

  return [land, landRigid];
};

const createAnimal = async (
  ammo,
  position,
  mass,
  onProgress = (xhr) => {},
  onError = (error) => console.log(error)
) => {
  let animal,
    animalRigid = null;

    let resolver = null;
    let rejecter = null;
  
    const loadAsync = new Promise(
      (resolve) => (resolver = resolve),
      (reject) => (rejecter = reject)
    );

  const loader = new GLTFLoader();
  loader.load(
    Agama,
    (gltf) => {
      gltf.scene.position.x = position.x;
      gltf.scene.position.y = position.y;
      gltf.scene.position.z = position.z;

      animal = gltf.scene;
      resolver();
    },
    onProgress,
    (error) => {
      rejecter();
      onError && onError(error);
    }
  );

  await loadAsync;

  return [animal, animalRigid];
};

export { createCube, createLand, createAnimal };
