
# EARTH SURVIVAL — Testing Checklist

Use this checklist to test the prototype in Unity. Mark an item complete only after testing it.

## 1. Project Setup

- [ ] Unity project opens successfully.
- [ ] All required C# scripts compile.
- [ ] No unexplained errors appear in the Console.
- [ ] Required scenes are configured.
- [ ] Required scripts are attached to the correct GameObjects.

## 2. Player Movement

- [ ] Player spawns at the correct position.
- [ ] Player can move forward, backward, left, and right.
- [ ] Player rotation follows movement direction.
- [ ] Camera follows the player correctly.
- [ ] Player does not fall through the ground.
- [ ] Jumping works when enabled.
- [ ] Movement works with Android touch controls.

## 3. Survival Statistics

- [ ] Health starts at the configured value.
- [ ] Hunger decreases over time.
- [ ] Thirst decreases over time.
- [ ] Health decreases when hunger reaches zero.
- [ ] Health decreases when thirst reaches zero.
- [ ] Statistics remain within their configured limits.
- [ ] Restoring health works.
- [ ] Restoring hunger and thirst works.
- [ ] Death is triggered only once.

## 4. Collectible Items

- [ ] Player can collect food.
- [ ] Player can collect water.
- [ ] Food restores the configured survival statistics.
- [ ] Water restores the configured survival statistics.
- [ ] Collected items disappear when configured.
- [ ] Non-player objects cannot collect player-only items accidentally.

## 5. Inventory

- [ ] Collected resources are added to the inventory.
- [ ] Item quantities update correctly.
- [ ] Inventory capacity is respected.
- [ ] Inventory handles full capacity safely.
- [ ] Clearing inventory works when enabled.
- [ ] Inventory is preserved when clearing is disabled.

## 6. Missions and Timer

- [ ] Mission timer starts correctly.
- [ ] Timer counts down accurately.
- [ ] Timer can be paused.
- [ ] Timer can be reset.
- [ ] Timer expiry triggers mission failure.
- [ ] Mission success triggers only once.
- [ ] Completing a mission unlocks the next level.
- [ ] Retrying resets mission state and restarts the timer.

## 7. Checkpoints and Retry

- [ ] Player returns to the assigned checkpoint.
- [ ] Player position and rotation are restored correctly.
- [ ] Survival statistics reset as intended.
- [ ] Inventory behavior follows the configured retry option.
- [ ] Repeated retry requests do not cause unexpected behavior.
- [ ] Scene reload works when required.

## 8. Saving and Loading

- [ ] Save operation completes successfully.
- [ ] Saved progress loads correctly.
- [ ] Invalid or missing save data is handled safely.
- [ ] Resetting progress behaves as intended.

## 9. Android Testing

- [ ] Android build completes successfully.
- [ ] Game launches on a physical device.
- [ ] Touch controls respond correctly.
- [ ] UI elements fit the screen.
- [ ] Game remains playable during longer sessions.
- [ ] Performance is acceptable on the target device.
- [ ] Pause and resume behavior is checked.

## 10. Release Readiness

- [ ] Critical bugs are fixed.
- [ ] All required scenes are included in the build.
- [ ] Privacy policy matches the actual game's data practices.
- [ ] Third-party assets and code have appropriate permissions.
- [ ] App icon and store assets are prepared.
- [ ] Build has been tested before distribution.

## Test Record

- **Unity version:**
- **Device:**
- **Android version:**
- **Build date:**
- **Tester:**
- **Known issues:**

## Important

These are planned test cases, not evidence that the game has passed testing. Record actual results during Unity testing.
