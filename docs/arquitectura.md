# Arquitectura Técnica — Sistema Solar en RA

## Visión general del sistema

```
Capa de presentación (XR)
├── Unity 2022.3.62f1
├── AR Foundation 5.x + ARCore XR Plugin
└── UI/UX en Unity Canvas (World Space)

Capa de lógica de negocio (Scripts C#)
├── Gestores de AR (ARPlaneManager, ARTrackedImageManager, etc.)
├── Controllers de interacción
└── Servicios de datos (UnityWebRequest)

Capa de datos (si aplica)
├── API REST (JSON)
├── Local Storage (PlayerPrefs / JSON files)
└── Assets de Unity (Prefabs, Modelos 3D)
```

## Componentes principales

| Componente | Propósito | Script asociado |
|-----------|---------|----------------|
|MotorUnity |2022.3.62f1 (LTS) |(no aplica, es el motor) |
|Tracking AR |AR Foundation 5.x + ARCore XR Plugin |AR/PlacementAR.cs, AR/PausarAR.cs |
|UI |Unity UI (uGUI) + TextMeshPro |UI/UIManager.cs, UI/PlanetInfoPanel.cs, UI/BCerrar.cs, Core/PlanetSelector.cs, Quiz/QuizManager.cs |
|Narración |Text-to-Speech nativo de Android (android.speech.tts.TextToSpeech, vía JNI) |Narrator/NarratorController.cs, Narrator/TTSInitListener.cs |
|Persistencia |PlayerPrefs (mejores puntajes del quiz) |Quiz/QuizManager.cs |
|Input |Input Manager (legacy) — Input.touchCount, Input.GetTouch() |Core/PlanetTouch.cs, Core/PinchZoomController.cs, AR/PlacementAR.cs |

## Diagrama de escena

```
Scripts/
├── AR/                  → Colocación y control del tracking AR
│   ├── PlacementAR.cs
│   └── PausarAR.cs
├── Core/                → Lógica central de selección y navegación
│   ├── Planet.cs
│   ├── PlanetSelector.cs
│   ├── PlanetFocusController.cs
│   ├── PlanetTouch.cs
│   └── PinchZoomController.cs
├── UI/                  → Paneles y flujo de interfaz
│   ├── UIManager.cs
│   ├── PlanetInfoPanel.cs
│   └── BCerrar.cs
├── Quiz/                → Sistema de evaluación
│   ├── QuizManager.cs
│   └── QuizQuestion.cs
├── Narrator/             → Texto a voz
│   ├── NarratorController.cs
│   └── TTSInitListener.cs
└── Earth/, Jupiter/, Mars/ (+ Deimos/, Phobos/), Mercury/,
    Neptuno/, Saturno/ (+ RINGS/), Urano/, Venus/, SUN/
                          → scripts de órbita y rotación agrupados por cuerpo
                            celeste (decisión final: se mantienen así en vez de
                            consolidarse en una carpeta compartida, ver nota
                            abajo)
```

## Decisiones de diseño

| Decisión | Alternativa descartada | Por qué se eligió |
|---------|----------------------|------------------|
|TTS nativo de Android (android.speech.tts) para el narrador |Servicio de TTS en la nube (Google Cloud TTS / Amazon Polly) |No requiere internet, sin costo por caracter/petición, sin latencia de red — relevante en contextos de uso (aula, exterior) con conectividad no confiable |
|Seguimiento continuo de cámara en LateUpdate(), recalculado cada frame |Animación de una sola vez (corrutina) hacia una posición fija al enfocar un planeta |Los planetas siguen orbitando después del enfoque; una animación de una sola vez pierde al objetivo apenas termina de reproducirse |
|Distancia de enfoque dinámica según Planet.visualRadius |Distancia de enfoque fija para todos los cuerpos |Con un valor fijo, la cámara terminaba dentro de la geometría de objetos grandes como el Sol |
|Colocación del sistema solar exactamente en el punto tocado |Desplazar la colocación hacia adelante de la cámara (offset), probado en una iteración anterior |El offset generaba una posición menos predecible para el usuario; se priorizó que el punto de colocación sea siempre el que el usuario tocó, literalmente |
|Cambio de planeta solo mediante botones (Anterior/Siguiente) |Cambio de planeta por toque directo sobre el modelo 3D |El zoom del enfoque agranda los colisionadores de todos los planetas (incluidos los no enfocados), causando selecciones erróneas al tocar cualquier punto de la pantalla; los botones no dependen de raycast físico y no tienen ese problema |
|Doble tap reservado exclusivamente para abrir información |Un solo tap para seleccionar y otro para ver información |Reducir la ambigüedad de qué gesto hace qué; un solo tap no hace nada, evitando toques accidentales |
|Sprite Swap para estados de botones (normal/highlighted/pressed/feedback) |Color Tint (transición por defecto de Unity Button) |El diseño visual usa bordes con color fijo "horneado" en el sprite (glow neón); un tinte de color los deformaría |
|PlayerPrefs para persistir el mejor puntaje del quiz |Backend remoto con base de datos |Fuera del alcance del proyecto; no se requiere sincronizar puntajes entre dispositivos |
|Scripting Backend IL2CPP |Mono |Mejor rendimiento en tiempo de ejecución en dispositivo, a cambio de compilación más lenta — tradeoff aceptable dado que se compila con poca frecuencia|

## Métricas de rendimiento

| Métrica | Objetivo | Obtenido |
|---------|---------|---------|
| FPS promedio | ≥30 (AR) | ≈ 405 FPS|
| Draw Calls | ≤100 | 5|
| SUS Score | ≥65 | Pendiente|
| Tiempo de inicio | ≤5s | 4s|

*Actualizar con datos reales al finalizar el proyecto*
