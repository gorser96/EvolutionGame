import * as THREE from "three";
import { OrbitControls } from "three/examples/jsm/controls/OrbitControls";
import { default as Ammo } from "ammo.js/builds/ammo";

export default class Scene {
  gameObjects = {};
  ammoObj = {};
  loopAction = (objs) => {};
  ammoPromise = {};
  tmpTrans = {};

  constructor({ canvasContainer, sceneSizes }) {
    // Для использования внутри класса добавляем параметры к this
    this.sceneSizes = sceneSizes;
    this.fps = 30;
    this.then = Date.now();
    this.interval = 1000 / this.fps;

    this.ammoPromise = Ammo().then((ammoObj) => {
      this.ammoObj = ammoObj;
      this.tmpTrans = new this.ammoObj.btTransform();
      this.initPhysics();
    });

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
        if (this.physicsWorld) {
          this.physicsWorld.stepSimulation(delta, 10);

          for (let i = 0; i < Object.keys(this.gameObjects).length; i++) {
            const objName = Object.keys(this.gameObjects)[i];
            let objAmmo =
              this.gameObjects[objName].obj.userData &&
              this.gameObjects[objName].obj.userData.physicsBody;
            if (objAmmo) {
              let ms = objAmmo.getMotionState();
              if (ms) {
                ms.getWorldTransform(this.tmpTrans);
                let p = this.tmpTrans.getOrigin();
                let q = this.tmpTrans.getRotation();
                this.gameObjects[objName].obj.position.set(p.x(), p.y(), p.z());
                this.gameObjects[objName].obj.quaternion.set(
                  q.x(),
                  q.y(),
                  q.z(),
                  q.w()
                );
              }
            }
          }
        }
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

  initPhysics() {
    this.collisionConfiguration =
      new this.ammoObj.btDefaultCollisionConfiguration();
    this.dispatcher = new this.ammoObj.btCollisionDispatcher(
      this.collisionConfiguration
    );
    this.overlappingPairCache = new this.ammoObj.btDbvtBroadphase();
    this.solver = new this.ammoObj.btSequentialImpulseConstraintSolver();

    this.physicsWorld = new this.ammoObj.btDiscreteDynamicsWorld(
      this.dispatcher,
      this.overlappingPairCache,
      this.solver,
      this.collisionConfiguration
    );
    this.physicsWorld.setGravity(new this.ammoObj.btVector3(0, -1, 0));
  }

  initScene() {
    // Создаём объект сцены
    this.scene = new THREE.Scene();

    // Задаём цвет фона
    this.scene.background = new THREE.Color("gray");
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

  async getAmmo() {
    await this.ammoPromise;
    return this.ammoObj;
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
      this.scene.add(this.gameObjects[name]["obj"]);
      if (this.gameObjects[name].hasOwnProperty("physics")) {
        this.physicsWorld.addRigidBody(this.gameObjects[name]["physics"]);
      }
    }
  }
}
