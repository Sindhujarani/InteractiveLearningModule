# InteractiveLearningModule
Project Overview

An interactive Unity desktop application combining three core learning systems:


Task 1: Object Interaction (click to identify objects)
Task 2: Drag and Drop (match objects to correct slots)
Task 3: Quiz System (MCQ quiz based on the objects)


Built using Unity 2022.3 LTS | C# | TextMeshPro | No VR headset required


How to Run


Open Unity Hub
Click Open Project → select the InteractiveLearningModule folder
Open the SampleScene from the Scenes folder
Press the Play button in the Unity Editor



Game Flow

START
  ↓
Task 1 — Click each object
(Color changes + Glow effect + Sound plays + Name appears)
  ↓
Task 2 — Drag each object to its correct slot
(Correct → snaps into place | Wrong → returns to original position)
  ↓
All 5 objects placed correctly → 2 second delay → Quiz starts automatically
  ↓
Task 3 — Answer 5 MCQ questions one at a time
(Correct/Wrong feedback per question + Score tracking)
  ↓
Final Result Screen
(Score displayed + Play Again button to restart quiz)


Task 1 — Object Interaction

Objects in scene: Apple, Box, Bottle, Stone, Chair

When you click an object:


Its color changes to a unique highlight color
A glow/pulse animation plays (scales up and emits light)
The object's name appears in the UI text at the top
The matching audio clip plays


Scripts used: ObjectClick.cs

Skills demonstrated:


OnMouseDown() for click detection (built-in Unity raycast)
AudioSource.Play() for audio playback
TextMeshProUGUI for UI text updates
Coroutines for smooth glow animation
Material emission for glow effect



Task 2 — Drag and Drop System

Layout: 5 objects on the left | 5 labeled slots on the right (scrambled order)

How it works:


Click and hold any object to start dragging
Drag it toward any slot on the right side
The moment it touches the correct slot → instantly snaps into position → shows "Correct!"
If dropped on a wrong slot or empty space → returns to original position → shows "Try Again!"
Messages auto-clear after 1.5 seconds
Once correctly placed, the object is locked and cannot be dragged again


Scripts used: DragDrop.cs

Skills demonstrated:


OnMouseDown / OnMouseDrag / OnMouseUp for input handling
Camera.main.ScreenToWorldPoint() for world-space dragging
OnTriggerEnter() for collider-based slot detection
Static counter (correctlyPlacedCount) shared across all 5 object instances
Position reset logic for wrong placement
WaitForSeconds coroutine for quiz transition delay



Task 3 — Quiz System

Triggered automatically when all 5 objects are correctly placed (2 second delay)

Quiz features:


5 questions shown one at a time
4 multiple choice options per question
Answer buttons disabled after selection (prevents double clicking)
Correct answer → green feedback
Wrong answer → red feedback + correct answer revealed
Score tracked and displayed live ("Score: X/5")
Next button advances to the following question
Final result screen shows total score + performance message
Play Again button restarts the quiz from Question 1


Scripts used: QuizManager.cs

Skills demonstrated:


List<Question> data structure for storing quiz data
Dynamic button listener assignment via onClick.AddListener()
RemoveAllListeners() to prevent stacked events across questions
SetActive(true/false) for panel show/hide management
Singleton pattern (QuizManager.Instance) for cross-script access
Game flow logic (Task 2 → Task 3 automatic transition)



Project Structure

Assets/
├── Audio/
│   ├── Apple_Sound
│   ├── Box_Sound
│   ├── Bottle_Sound
│   ├── Stone_Sound
│   └── Chair_Sound
├── Materials/
│   ├── Mat_Apple
│   ├── Mat_Box
│   ├── Mat_Bottle
│   ├── Mat_Stone
│   └── Mat_Chair
├── Prefabs/
├── Scenes/
│   └── SampleScene
└── Scripts/
    ├── ObjectClick.cs
    ├── DragDrop.cs
    └── QuizManager.cs


Scripts Overview

ObjectClick.cs

Handles Task 1 — Attached to each of the 5 clickable objects.


Detects mouse click via OnMouseDown()
Changes material color to assigned clickColor
Plays attached AudioSource clip
Updates UI name text
Runs GlowPulse() coroutine for scale + emission animation
ResetObject() method restores original color, glow, and scale


DragDrop.cs

Handles Task 2 — Attached to each of the 5 draggable objects.


OnMouseDrag() converts screen mouse position to world position
OnTriggerEnter() detects slot collision and checks name match
Snaps to correct slot instantly on contact
Resets to originalPosition on wrong slot or empty drop
Static correctlyPlacedCount tracks total correctly placed objects
Triggers quiz start via QuizManager.Instance.StartQuiz() after 2 second delay


QuizManager.cs

Handles Task 3 — Attached to a dedicated QuizManager GameObject.


Singleton pattern for global access
SetupQuestions() populates 5 hardcoded questions at runtime
ShowQuestion() dynamically updates UI elements for each question
OnAnswerSelected() handles correct/wrong logic and score
StartQuiz() hides Task 1/2 elements and shows QuizPanel
RestartQuiz() resets quiz back to Question 1



Quiz Questions

#QuestionCorrect Answer1Which object is a fruit?Apple2Which object can hold liquid?Bottle3Which object is used for sitting?Chair4Which object is a heavy natural rock?Stone5Which object is used for storage?Box


Design Decisions

Single Scene Architecture
All three tasks run in one scene. This avoids scene-transition complexity, keeps data in memory without needing persistent managers or PlayerPrefs, and matches the task's intended continuous flow: interaction → matching → quiz.

Static Counter in DragDrop
correctlyPlacedCount is a static variable so all 5 DragDrop instances share one counter without needing a separate manager script. Clean, minimal, and reusable.

Singleton Pattern for QuizManager
QuizManager.Instance allows DragDrop to trigger the quiz without a direct Inspector reference, keeping scripts loosely coupled.

Code-Driven Button Listeners
Quiz answer buttons use onClick.AddListener() in code rather than Inspector connections. This makes the same 4 buttons reusable across all 5 questions dynamically — a more scalable approach than 5 separate button sets.


Evaluation Criteria Coverage

CriteriaHow it's addressedCode structure3 focused scripts, each handling one task onlyNaming conventionsClear names: ObjectClick, DragDrop, QuizManager, correctSlotName, originalPositionLogic handlingTrigger detection, state flags, coroutines, countersUI workflowPanels show/hide cleanly, feedback auto-clears, score updates liveReusabilitySame script works for all 5 objects — only Inspector fields differBug-free executionEdge cases handled: empty-space drops, double-click prevention, listener cleanup


Developer Notes


Built and tested on Unity 2022.3.10f1
No external packages required beyond TextMeshPro (included with Unity)
All audio files recorded and imported as MP3
No VR headset required — runs entirely in Unity Game view on PC


