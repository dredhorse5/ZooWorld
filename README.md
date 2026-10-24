# Zoo World

Unity test assignment: a small 3D zoo where prey and predators spawn, move, collide and form a food chain. The focus is animal architecture and extensibility, not content.

- **Unity:** 6000.6.7f1
- **Stack:** VContainer, uGUI + TextMeshPro, Rigidbody physics, ScriptableObject configs
- **Entry scene:** `Assets/ZooWorld/Scenes/SampleScene.unity`

## Run

1. Open `Assets/ZooWorld/Scenes/SampleScene.unity`.
2. Press **Play**. There is no player input; animals spawn and move automatically.

## Configs

| Asset | Responsibility |
|---|---|
| `Animals/Frog.asset`, `Animals/Snake.asset` (`AnimalConfig`) | animal role (`Prey` / `Predators`), prefab, and which movement config to use |
| `Configs/AnimalSpawnerConfig.asset` | spawn interval and the weighted list of `AnimalConfig`s to spawn |
| `Configs/FoodChainRulesConfig.asset` | role-to-role food-chain rules (`Eat`, `Dead`, `Nothing`, `NotImplemented`) |
| `Configs/Movement/JumpMovementConfig.asset` | frog: inherited `Speed`, `jumpForce`, `minJumpDelay` / `maxJumpDelay`, `jumpTurnAngle` |
| `Configs/Movement/LinearMovementConfig.asset` | snake: inherited `Speed`, `WallTurnAngle` |
| `Configs/Movement/RandomMovementConfig.asset` | extra/legacy random strategy: inherited `Speed`, `SteerAngle`, `NoiseFrequency` |
| `Configs/TastyFeedbackConfig.asset` | prefab and offset used by `TastyFeedbackService` for the “Tasty!” label |

Config chain: `AnimalSpawnerConfig.Animals[] → AnimalConfig.Prefab + AnimalConfig.MovementConfig → BaseMovementConfig.Create() → IMovement`. `AnimalConfig.AnimalType` is what `FoodChainRulesConfig` uses for rule lookup. All configs are ScriptableObjects created via `Create → Animal/...`.

## Implemented

- **Spawning** — `AnimalSpawner` spawns animals on a configurable interval from `AnimalSpawnerConfig` (weighted list).
- **Movement strategies**
  - `JumpMovement` — frog hops on a timer; FSM with `Grounded` / `Jumping` / `SeeingWall`.
  - `LinearMovement` — snake moves in a straight line at constant speed and turns when an obstacle is detected.
  - `RandomMovement` — an additional Perlin-noise steering strategy, kept in the project next to the other movements.
- **Food chain** — `FoodChainRulesConfig` + `FoodChainService`:
  - prey ↔ prey: no rule, animals push each other by physics;
  - predator + prey: prey dies, predator gets “Tasty!” feedback;
  - predator + predator: one survives, one dies.
- **Counters** — `ZooStatistics` counts dead prey/predators; `ZooHudView` renders them with uGUI/TextMeshPro.
- **DI** — VContainer. `AnimalFactory` instantiates prefabs through `IObjectResolver`, so components on prefabs get their dependencies injected.
- **Event-driven feedback** — `FoodChainService → IAnimalDeathService → IAnimalEvents → ZooStatistics / TastyFeedbackService / ZooHudView`.

## Architecture in one minute

- `IMovement` is a strategy. A `BaseMovementConfig` ScriptableObject creates its concrete movement via `Create()`, and `BaseMovement<T>` stores the config and the `IMotor`.
- `IMotor` / `AnimalMotor` is the physics actuator: move, jump, rotate, brake, ground/obstacle probes. Movement strategies do not know Rigidbody details.
- `AnimalFactory` is the composition root for one animal: instantiate the prefab through VContainer, set `Animal` on all `AnimalComponent`s, initialize `Animal` and `AnimalMotor`.
- Food chain is data + service: `FoodChainRulesConfig` stores role rules; `FoodChainService` resolves a rule and calls `IAnimalDeathService.Kill`.
- Death is idempotent (`Animal.TryKill`), then exactly one `AnimalDiedEvent` is published before the object is destroyed. Stats, Tasty feedback and the HUD subscribe to it; `FoodChainService` knows nothing about UI.
- `JumpMovement` has a small FSM (`Grounded → Jumping → Grounded`, `Grounded → SeeingWall → Grounded`) so “jump only from the ground” and “turn away from a wall” are explicit.

## Extending

- **New animal:** create an `AnimalConfig` (role, prefab, movement config), add `Animal`, `AnimalMotor` and `AnimalCollision` to its prefab, put the config into `AnimalSpawnerConfig.Animals`, add food-chain rules if needed.
- **New movement:** implement `BaseMovement<T>` + a config with `Create()`; assign that config to an animal.
- **New rule:** add an entry to `FoodChainRulesConfig` (from role → to role → Eat / Dead / Nothing).
- **New feedback:** subscribe to `IAnimalEvents.AnimalDied`; do not modify `FoodChainService`.
