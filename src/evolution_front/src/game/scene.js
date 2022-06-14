import * as THREE from "three";
import { OrbitControls } from "three/examples/jsm/controls/OrbitControls";

export default class Scene {
  gameObjects = {};
  loopAction = (objs) => {};

  constructor({ canvasContainer, sceneSizes }) {
    // Для использования внутри класса добавляем параметры к this
    this.sceneSizes = sceneSizes;
    this.fps = 30;
    this.then = Date.now();
    this.interval = 1000 / this.fps;

    this.initRenderer(canvasContainer); // создание рендерера
    this.initScene(); // создание сцены
    this.initCamera(); // создание камеры
    this.render(); // Запускаем рендеринг

    const startLoop = () => {
      requestAnimationFrame(startLoop);

      this.loopAction(this.gameObjects);

      var now = Date.now();
      var delta = now - this.then;
      if (delta > this.interval) {
        this.then = now - (delta % this.interval);
        this.orbit.update();
        this.render();
      }
    };

    startLoop();
  }

  resizeScene(width, height) {
    this.sceneSizes.width = width;
    this.sceneSizes.height = height;

    this.camera.aspect = this.sceneSizes.width / this.sceneSizes.height;
    this.camera.updateProjectionMatrix();

    this.renderer.setSize(this.sceneSizes.width, this.sceneSizes.height);
    this.render();
  }

  initRenderer(canvasContainer) {
    // Создаём редерер (по умолчанию будет использован WebGL2)
    // antialias отвечает за сглаживание объектов
    this.renderer = new THREE.WebGLRenderer({ antialias: true });

    //Задаём размеры рендерера
    this.renderer.setSize(this.sceneSizes.width, this.sceneSizes.height);

    //Добавляем рендерер в узел-контейнер, который мы прокинули извне
    canvasContainer.appendChild(this.renderer.domElement);
  }

  initScene() {
    // Создаём объект сцены
    this.scene = new THREE.Scene();

    // Задаём цвет фона
    this.scene.background = new THREE.Color("black");
  }

  initCamera() {
    // Создаём ортографическую камеру (Идеально подходит для 2d)
    this.camera = new THREE.PerspectiveCamera(
      90,
      this.sceneSizes.width / this.sceneSizes.height,
      0.1,
      10000
    );

    this.orbit = new OrbitControls(this.camera, this.renderer.domElement);

    // Позиционируем камеру в пространстве
    this.camera.position.set(
      0, // Позиция по x
      0, // Позиция по y
      5 // Позиция по z
    );
    this.orbit.update();
  }

  render() {
    // Выполняем рендеринг сцены (нужно запускать для отображения изменений)
    this.renderer.render(this.scene, this.camera);
  }

  removeObject(name) {
    if (this.gameObjects.hasOwnProperty(name)) {
      this.scene.remove(this.gameObjects[name]);
      delete this.gameObjects[name];
    }
  }

  addObject(name, obj) {
    if (!this.gameObjects.hasOwnProperty(name)) {
      this.gameObjects[name] = obj;
      this.scene.add(this.gameObjects[name]);
    }
  }
}
