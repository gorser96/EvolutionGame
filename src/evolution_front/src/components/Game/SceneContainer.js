import { useRef, useEffect, useState } from "react";
import * as THREE from "three";
import { GLTFLoader } from "three/examples/jsm/loaders/GLTFLoader";

import { useWindowSize } from "../hooks/WindowSize";
import Scene from "../../game/scene";
import "./SceneContainer.css";
import Ground from "../../models/ground/scene.glb";

// Размеры сцены и квадрата
const rectSizes = { width: 200, height: 200 };

const SceneContainer = (props) => {
  const [width, height] = useWindowSize();
  const threeRef = useRef(); // Используется для обращения к контейнеру для canvas
  const scene = useRef(); // Служит для определения, создан ли объект, чтобы не создавать повторный
  const [color, colorChange] = useState("blue"); // Состояние отвечает за цвет квадрата

  // Handler служит для того, чтобы изменить цвет
  const colorChangeHandler = () => {
    // Просто поочерёдно меняем цвет с серого на синий и с синего на серый
    colorChange((prevColor) => (prevColor === "grey" ? "blue" : "grey"));
  };

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
        color,
        rectSizes,
        sceneSizes,
        colorChangeHandler,
        canvasContainer: threeRef.current,
      });
      scene.current.loopAction = gameLoop;

      createObjects();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [color, height, width]);

  const createObjects = () => {
    if (scene.current) {
      const geometry = new THREE.BoxBufferGeometry(1, 1, 1);
      const material = new THREE.MeshPhongMaterial({ color: '#8AC' });
      var cube = new THREE.Mesh(geometry, material);

      cube.position.x = 0;
      cube.position.y = 0;

      scene.current.addObject("cube", cube);

      const axesHelper = new THREE.AxesHelper(20);
      axesHelper.setColors(
        new THREE.Color(255, 0, 0),
        new THREE.Color(0, 255, 0),
        new THREE.Color(0, 0, 255)
      );
      scene.current.addObject("xyz", axesHelper);

      const pointLight = new THREE.SpotLight(0xffffff, 1, 50);
      pointLight.position.set(0, 10, 0);
      scene.current.addObject("light", pointLight);

      const loader = new GLTFLoader();
      loader.load(
        Ground,
        (gltf) => {
          console.log(gltf);
          gltf.scene.position.x = 5;
          gltf.scene.position.y = 14;
          gltf.scene.position.z = 15;
          scene.current.addObject("location", gltf.scene);
        },
        (xhr) => {
          console.log((xhr.loaded / xhr.total) * 100 + "% loaded");
        },
        (error) => console.log(error)
      );
    }
  };

  const gameLoop = (objs) => {
    objs["cube"].rotation.x += 0.01;
    objs["cube"].rotation.y += 0.01;
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
