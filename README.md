# Cube Text Animation with Unity ECS/DOTS

This project is a text visualization system developed using Unity's Entity Component System (ECS) and Data-Oriented Technology Stack (DOTS). It animates and displays text in 3D using thousands of cubes.

## 🎮 Features

- **ECS/DOTS Optimization**: High-performance cube management
- **Dynamic Text Generation**: Creates 3D text using cubes
- **Animated Movement**: Synchronized wave-like motion of cubes
- **Customizable Parameters**:
  - Number of cubes
  - Letter spacing
  - Movement speed and distance
  - Text scale

## 🛠️ Technologies Used

- Unity 2022.3 or later
- Unity Entity Component System (ECS)
- Unity DOTS (Data-Oriented Technology Stack)
- Burst Compiler

## 📂 System Components

### Components
- `CubeComponent`: Core properties of cube entities
- `CubeSpawnerData`: Cube generation parameters

### Systems
- `CubeSpawnSystem`: Positions cubes to form text
- `CubeMovementSystem`: Animates the cubes
- `CubeSystemGroup`: Manages and sequences systems

## 🚀 Setup

1. Open **Unity Hub**
2. Click **"Add"** and select the project folder
3. Once the project opens in **Unity Editor**:
   - Open the main scene from the `Scenes` folder
   - Locate the **CubeSpawner** GameObject in the Hierarchy
   - Adjust the parameters in the **Inspector** as needed

## ⚙️ Configuration

Customize the following settings via the **CubeSpawner** GameObject:

```csharp
public class CubeSpawnerAuthoring : MonoBehaviour
{
    public string ShapeText = "CEM";     // Text to display
    public int NumberOfCubes = 50000;    // Total number of cubes
    public float LetterSpacing = 3f;     // Spacing between letters
    public float LetterScale = 1f;       // Letter size
}
```

## 🎯 Performance

- Efficient management of thousands of cubes using **ECS and DOTS**
- Optimized performance with **Burst Compiler**
- High FPS with **parallel processing support**
