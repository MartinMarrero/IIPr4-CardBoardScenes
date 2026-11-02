# IIPr4 - Escenas Cardboard en Unity

Proyecto de realidad virtual orientado a Google Cardboard. La escena permite
interactuar con el entorno mediante la mirada: mantener la vista sobre un
objeto activa su comportamiento y mirar al suelo permite desplazarse por la
escena.

## Descargar y ejecutar

- **APK para Android:** [build/Cardboard.apk](build/Cardboard.apk)
- **Demostración:** ![Ejecución de la escena](gifs/testrun.gif)

Instala el APK en un dispositivo Android compatible con Google Cardboard e
introduce el teléfono en el visor. La cámara se controla con la orientación de
la cabeza y la interacción se realiza manteniendo la mirada durante el tiempo
configurado.

## Mecánicas

- **Interacción por mirada:** `GazeInteractor` lanza un rayo desde la cámara
  para detectar objetos interactuables a una distancia máxima configurable.
  Cuando el usuario mantiene la mirada sobre un objetivo durante
  `gazeDuration`, se ejecuta la acción correspondiente.
  - **Recolectables:** se atraen hacia el jugador y desaparecen al llegar a él.
  - **Imán:** recoge todos los recolectables activos de la escena.
  - **Suelo:** fija un punto de destino y mueve al jugador hasta él.
- **Movimiento por puntos del suelo:** al mirar una superficie con la etiqueta
  `Ground`, el jugador se desplaza hacia el punto exacto indicado por el rayo,
  manteniendo su altura actual.
- **Control de cámara en el editor:** mientras se mantiene pulsado el botón
  derecho del ratón, `EditorCameraLook` permite probar la orientación de la
  cámara con el ratón sin utilizar un dispositivo Cardboard.

## Scripts principales

- [`scripts/GazeInteractor.cs`](scripts/GazeInteractor.cs): detecta los
  objetivos mediante raycast, controla el temporizador de mirada y mueve al
  jugador.
- [`scripts/GazeCollectible.cs`](scripts/GazeCollectible.cs): registra los
  recolectables activos, los atrae hacia el jugador y los desactiva al
  recogerse.
- [`scripts/GazeMagnet.cs`](scripts/GazeMagnet.cs): activa la recolección
  simultánea de todos los objetos recolectables.
- [`scripts/EditorCameraLook.cs`](scripts/EditorCameraLook.cs): permite
  controlar la rotación de la cámara con el ratón durante las pruebas en el
  editor.

## Configuración

Los parámetros principales se pueden ajustar desde el Inspector de Unity:

- `gazeDuration`: tiempo necesario para activar un objetivo.
- `maxDistance`: distancia máxima del raycast de interacción.
- `moveSpeed`: velocidad de desplazamiento del jugador.
- `attractionSpeed`: velocidad a la que los recolectables se acercan al
  jugador.

Los objetos del suelo deben utilizar la etiqueta `Ground`. El objeto que
gestiona la interacción necesita referencias a la raíz del jugador
(`playerRoot`) y a la cámara que emite el rayo.