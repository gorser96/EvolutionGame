import { useRef, useEffect } from "react";
import * as THREE from "three";

import { useWindowSize } from "../hooks/WindowSize";
import Scene from "../../game/scene";
import "./SceneContainer.css";
import { createCube, createAnimal, createLand } from "../../game/ObjectFactory";
import TopMenu from "./TopMenu";

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

      let [cube, rigidCube] = createCube(
        ammo,
        { width: 1, height: 1, depth: 1 },
        "#8AC",
        { x: 0, y: 50, z: 0 },
        1
      );
      scene.current.addObject("cube", { obj: cube, physics: rigidCube });

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

      let [land, landRigid] = await createLand(ammo, { x: 5, y: 14, z: 15 }, 0);

      scene.current.addObject("location", {
        obj: land,
        physics: landRigid,
      });

      let [animal] = await createAnimal(ammo, { x: 0, y: 0, z: 0 }, 0);
      scene.current.addObject("agama", { obj: animal });
    }
  };

  const gameLoop = (objs) => {
    /*
    objs["cube"].rotation.x += 0.01;
    objs["cube"].rotation.y += 0.01;
    */
  };

  useEffect(() => {
    if (scene.current) {
      scene.current.resizeScene(width, height);
    }
  }, [height, width]);

  // Данный узел будет контейнером для canvas (который создаст three.js)
  return (
    <>
      <TopMenu />
      <div id="game_container" className="game-window" ref={threeRef} />
    </>
  );
};

export default SceneContainer;
