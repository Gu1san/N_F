# N&F

## 🕹️ Overview
**N&F** is a 2D platformer game developed as a study project. It features classic mechanics, challenging players to solve puzzles and navigate levels filled with obstacles.

## ✨ Key Features
- **Player Movement**:  
  - Double jump.  
  - Dash.  
  - Wall jump.  
  - Switching between two characters (black and white) with unique interactions.  
  - Collaborative mechanics between characters.
 
<!--Vídeo mostrando movimentos do player-->
https://github.com/user-attachments/assets/94f6b3bb-0e71-44e1-8cb5-9770a5df0579

- **Interactive Level Elements**:  
  - Moving platforms.  
  - Buttons that activate/deactivate doors and mechanisms.  
  - Doors requiring keys to unlock.  
  - Breakable walls.  
<!--Vídeo mostrando as interações dos players-->
https://github.com/user-attachments/assets/73372fa0-588a-4568-95f4-bd53003c1531

<!--Vídeo mostrando as interações dos players-->
https://github.com/user-attachments/assets/514bfdd7-5ecd-4e5b-af55-5f3c7f9d0824

<!--Vídeo mostrando mecânica de destrancar porta com chave-->
https://github.com/user-attachments/assets/5ce698c6-2148-4d06-8550-5b3f0ca708bf

## 🔧 Technologies and Solutions
- **Interactive objects**: Objects like doors and moving platforms inherits from an abstract class with Activate, Deactivate and Reset functions.
- **Observer pattern**: Every interactable object uses the Observer pattern to reset to default state with the level.
- **Character interactions**: With Unity's Layer Collision Matrix, each character has its own layer to detect specific objects.

## 🚀 How to Play
1. Clone this repository:  
   ```bash
   git clone https://github.com/username/N_F.git
   ```
2. Open the project in Unity (version 2022.3.38 or later).
3. Load the SampleScene and hit Play.
4. Use WASD to control the characters, Q to change characters, Shift to dash ant Space to grab specific walls with white player.

## 📂 Download the Game  


## 💡 Learnings and Highlights  
- **Collision Optimization**: Layer based collisions, predictive collision to break walls seamlessly.
- **2D Camera controll**: Set camera limits and switch between them.
  
## 📖 Next Steps  
- Adding new levels and challenges.  
- Enhancing visuals with custom shaders and tilesets.  
- Introducing enemies or other interactive elements. 

## 👨‍💻 About the Developer  
This project is part of my portfolio as a game developer. I specialize in creating interactive experiences using Unity and C#, focusing on innovative mechanics and polished gameplay.

