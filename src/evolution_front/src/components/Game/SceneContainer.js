import { useRef, useEffect } from "react";
import * as THREE from "three";
import { GLTFLoader } from "three/examples/jsm/loaders/GLTFLoader";

import { useWindowSize } from "../hooks/WindowSize";
import Scene from "../../game/scene";
import "./SceneContainer.css";
import Ground from "../../models/ground/scene.glb";
import Agama from "../../models/animals/agama.glb";

const SceneContainer = (props) => {
  const [width, height] = useWindowSize();
  const threeRef = useRef(); // Используется для обращения к контейнеру для canvas
  const scene = useRef(); // Служит для определения, создан ли объект, чтобы не создавать повторный

  // Создание объекта класса Three, предназначенного для работы с three.js
  useEffect(() => {
    // Если объект класса "Three" ещё не создан, то попадаем внутрь
    if (!scene.current && width !== 0 && height !== 0) {
      const sceneSizes = {
        width: width,
        height: height,
      };

      // Создание объекта класса "Three", который будет использован для работы с three.js
      scene.current = new Scene({
        sceneSizes,
        canvasContainer: threeRef.current,
      });
      scene.current.loopAction = gameLoop;

      createObjects();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [height, width]);

  const createObjects = async () => {
    if (scene.current) {
      let ammo = await scene.current.getAmmo();

      const geometry = new THREE.BoxBufferGeometry(1, 1, 1);
      const material = new THREE.MeshPhongMaterial({ color: "#8AC" });
      var cube = new THREE.Mesh(geometry, material);

      cube.position.x = 0;
      cube.position.y = 50;
      let mass = 1;

      let transform = new ammo.btTransform();
      transform.setIdentity();
      transform.setOrigin(
        new ammo.btVector3(cube.position.x, cube.position.y, cube.position.z)
      );
      transform.setRotation(
        new ammo.btQuaternion(
          cube.position.x,
          cube.position.y,
          cube.position.z,
          1
        )
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
      scene.current.addObject("cube", { obj: cube, physics: body });

      const axesHelper = new THREE.AxesHelper(20);
      axesHelper.setColors(
        new THREE.Color(255, 0, 0),
        new THREE.Color(0, 255, 0),
        new THREE.Color(0, 0, 255)
      );
      scene.current.addObject("xyz", { obj: axesHelper });

      const pointLight = new THREE.SpotLight(0xffffff, 1, 50);
      pointLight.position.set(0, 10, 0);
      scene.current.addObject("light", { obj: pointLight });

      const loader = new GLTFLoader();
      loader.load(
        Ground,
        (gltf) => {
          gltf.scene.position.x = 5;
          gltf.scene.position.y = 14;
          gltf.scene.position.z = 15;

          let mass = 0;
          let transform = new ammo.btTransform();
          transform.setIdentity();
          transform.setOrigin(
            new ammo.btVector3(
              0,
              0,
              0
            )
          );
          transform.setRotation(new ammo.btQuaternion(0, 0, 0, 1));

          let motionState = new ammo.btDefaultMotionState(transform);
          let localInertia = new ammo.btVector3(0, 0, 0);
          let colShape = new ammo.btBoxShape(
            new ammo.btVector3(
              10,
              0.1,
              10
            )
          );
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

          scene.current.addObject("location", {
            obj: gltf.scene,
            physics: rBody,
          });
        },
        (xhr) => {
          console.log((xhr.loaded / xhr.total) * 100 + "% loaded");
        },
        (error) => console.log(error)
      );

      loader.load(
        Agama,
        (gltf) => {
          gltf.scene.position.x = 0;
          gltf.scene.position.y = 0;
          gltf.scene.position.z = 0;
          scene.current.addObject("agama", { obj: gltf.scene });
        },
        (xhr) => {
          console.log((xhr.loaded / xhr.total) * 100 + "% loaded");
        },
        (error) => console.log(error)
      );
    }
  };

  const gameLoop = (objs) => {
    /*
    objs["cube"].rotation.x += 0.01;
    objs["cube"].rotation.y += 0.01;
    */
  };

  // при смене цвета вызывается метод объекта класса Three
  useEffect(() => {
    if (scene.current) {
      scene.current.resizeScene(width, height);
    }
  }, [height, width]);

  // Данный узел будет контейнером для canvas (который создаст three.js)
  return <div id="game_container" className="game-window" ref={threeRef} />;
};

export default SceneContainer;
