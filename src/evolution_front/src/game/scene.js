import * as THREE from "three";

export default class Scene {
  constructor({
    canvasContainer,
    sceneSizes,
    rectSizes,
    color,
    colorChangeHandler,
  }) {
    // Для использования внутри класса добавляем параметры к this
    this.sceneSizes = sceneSizes;
    this.colorChangeHandler = colorChangeHandler;

    this.initRenderer(canvasContainer); // создание рендерера
    this.initScene(); // создание сцены
    this.initCamera(); // создание камеры
    // this.renderRect(rectSizes, color); // Добавляем квадрат на сцену
    this.renderCube();
    this.render(); // Запускаем рендеринг
  }

  resizeScene(width, height) {
    this.sceneSizes.width = width;
    this.sceneSizes.height = height;

    this.cube.position.x = this.sceneSizes.width / 2;
    this.cube.position.y = -this.sceneSizes.height / 2;

    // Позиционируем камеру в пространстве
    this.camera.position.set(
      this.sceneSizes.width / 2, // Позиция по x
      this.sceneSizes.height / -2, // Позиция по y
      this.camera.position.z // Позиция по z
    );
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
    this.scene.background = new THREE.Color("white");
  }

  initCamera() {
    // Создаём ортографическую камеру (Идеально подходит для 2d)
    this.camera = new THREE.PerspectiveCamera(
      75,
      this.sceneSizes.width / this.sceneSizes.height,
      0.1,
      1000
    );

    // Позиционируем камеру в пространстве
    this.camera.position.set(
      this.sceneSizes.width / 2, // Позиция по x
      this.sceneSizes.height / -2, // Позиция по y
      5 // Позиция по z
    );
  }

  render() {
    // Выполняем рендеринг сцены (нужно запускать для отображения изменений)
    this.renderer.render(this.scene, this.camera);
  }

  renderCube() {
    const geometry = new THREE.BoxGeometry(1, 1, 1);
    const material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
    this.cube = new THREE.Mesh(geometry, material);

    //Позиционируем куб в пространстве
    this.cube.position.x = this.sceneSizes.width / 2;
    this.cube.position.y = -this.sceneSizes.height / 2;

    var animate = function(obj) {
      obj.cube.rotation.x += 0.01;
      obj.cube.rotation.y += 0.01;// calc elapsed time since last loop
      obj.renderer.render(obj.scene, obj.camera);
      return () => {
        requestAnimationFrame(animate(obj));
      }
    }

    animate(this)();

    this.scene.add(this.cube);
  }

  renderRect({ width, height }, color) {
    // Создаём геометрию - квадрат с высотой "height" и шириной "width"
    const geometry = new THREE.PlaneGeometry(width, height);

    // Создаём материал с цветом "color"
    const material = new THREE.MeshBasicMaterial({ color });

    // Создаём сетку - квадрат
    this.rect = new THREE.Mesh(geometry, material);

    //Позиционируем квадрат в пространстве
    this.rect.position.x = this.sceneSizes.width / 2;
    this.rect.position.y = -this.sceneSizes.height / 2;

    this.scene.add(this.rect);
  }

  // Служит для изменения цвета квадрат
  rectColorChange(color) {
    // Меняем цвет квадрата
    this.rect.material.color.set(color);

    // Запускаем рендеринг (отобразится квадрат с новым цветом)
    this.render();
  }
}
