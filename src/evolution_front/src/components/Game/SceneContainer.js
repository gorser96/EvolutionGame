import { useRef, useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import { useWindowSize } from "../hooks/WindowSize";
import Scene from "../../game/scene";
import "./SceneContainer.css";

// Размеры сцены и квадрата
const rectSizes = { width: 200, height: 200 };

const SceneContainer = (props) => {
  const { uid } = useParams();
  const [width, height] = useWindowSize();
  const threeRef = useRef(); // Используется для обращения к контейнеру для canvas
  const three = useRef(); // Служит для определения, создан ли объект, чтобы не создавать повторный
  const [color, colorChange] = useState("blue"); // Состояние отвечает за цвет квадрата

  // Handler служит для того, чтобы изменить цвет
  const colorChangeHandler = () => {
    // Просто поочерёдно меняем цвет с серого на синий и с синего на серый
    colorChange((prevColor) => (prevColor === "grey" ? "blue" : "grey"));
  };

  // Создание объекта класса Three, предназначенного для работы с three.js
  useEffect(() => {
    // Если объект класса "Three" ещё не создан, то попадаем внутрь
    if (!three.current) {
      const sceneSizes = {
        width: width,
        height: height,
      };

      // Создание объекта класса "Three", который будет использован для работы с three.js
      three.current = new Scene({
        color,
        rectSizes,
        sceneSizes,
        colorChangeHandler,
        canvasContainer: threeRef.current,
      });
    }
  }, [color, height, width]);

  // при смене цвета вызывается метод объекта класса Three
  useEffect(() => {
    if (three.current) {
      three.current.resizeScene(width, height);
    }
  }, [height, width]);

  // Данный узел будет контейнером для canvas (который создаст three.js)
  return (
    <div
      id="game_container"
      className="game-window"
      ref={threeRef}
    />
  );
};

export default SceneContainer;
