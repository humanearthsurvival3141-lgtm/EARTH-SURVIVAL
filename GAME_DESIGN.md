
# EARTH SURVIVAL — Game Design Document

## 1. Game Overview

**Title:** EARTH SURVIVAL  
**Genre:** Open-world survival and exploration  
**Engine:** Unity  
**Programming Language:** C#  
**Target Platform:** Android  
**Camera:** Third-person  
**Mode:** Single-player prototype  
**Development Status:** Early development

## 2. Game Vision

EARTH SURVIVAL is a peaceful survival adventure where players explore a large natural world, gather resources, find shelter, manage their basic needs, and complete missions.

The initial goal is to create a small, playable prototype before expanding the world.

## 3. World Design

The planned world includes:

- Forests with trees and collectible resources
- Villages with houses and shelters
- Cities with buildings and exploration areas
- Mountains with varied terrain
- Rivers or water sources, where appropriate
- Day and night cycles as a future feature
- Weather effects as a planned feature

The first prototype will use a small test area to keep development manageable.

## 4. Player Character

The player will be able to:

- Walk and run
- Jump where appropriate
- Explore the environment
- Collect resources
- Interact with designated objects
- Find or use shelter
- Complete survival missions

Player movement and animation must be tested in Unity before release.

## 5. Survival Systems

### Health
Health represents the player's physical condition. It may decrease when survival needs are neglected.

### Hunger
Hunger decreases over time. Food items can restore hunger.

### Thirst
Thirst decreases over time. Safe water items can restore thirst.

### Stamina
Stamina represents energy used by movement or other actions. Stamina costs and regeneration will be balanced during testing.

### Shelter
Shelter is intended to provide a safe place to rest and may become important during weather or night-time challenges.

## 6. Resource Collection

Planned collectible resources include:

- Food
- Water
- Wood
- Stone
- Medicine

Each item should have a clear purpose. Resource collection must be connected to the inventory system.

## 7. Inventory System

The inventory should:

- Track item types and quantities
- Prevent invalid item quantities
- Handle limited capacity safely
- Allow appropriate items to be used
- Support optional inventory clearing during a retry

The inventory UI will be implemented after the underlying inventory logic works.

## 8. Mission System

Missions may include:

- Collect a specified amount of food
- Find water
- Gather wood or stone
- Reach a shelter
- Reach a checkpoint
- Survive until a timer expires or an objective is completed

A mission can be completed or failed. A failed mission should allow the player to retry.

## 9. Timer and Progression

The prototype will include:

- A configurable countdown timer
- Timer pause and reset functionality
- Mission failure when time expires
- Mission completion when objectives are met
- Next-level unlocking after success
- Retry and checkpoint functionality

The timer and mission manager must be tested together to avoid duplicate events or incorrect resets.

## 10. Mobile Controls

The Android interface is planned to include:

- Virtual movement joystick
- Jump button
- Run button
- Interaction or collection button when needed
- Survival-stat displays
- Mission timer
- Pause and retry controls

Controls should remain readable on different screen sizes.

## 11. Save System

The game may save:

- Unlocked level
- Selected gameplay progress
- Settings
- Checkpoint information, where supported

Save and load behavior must be tested before release. Sensitive information should not be stored in plain-text saves.

## 12. Audio and Visual Direction

The intended visual style is immersive natural exploration.

Planned audio includes:

- Ambient forest sounds
- Wind and weather effects
- Footsteps
- Resource collection sounds
- UI feedback

These are design goals, not confirmed implemented features.

## 13. Design Restrictions

The initial game concept excludes:

- Guns and weapons
- Combat
- Player-versus-player gameplay
- Item marketplace
- Guaranteed income or financial rewards

The focus is peaceful survival and exploration.

## 14. Development Roadmap

1. Organize and validate the Unity project.
2. Build a playable player controller.
3. Create a small test environment.
4. Implement survival statistics.
5. Implement inventory and collection.
6. Add missions, timer, retry, and checkpoints.
7. Add mobile UI.
8. Implement and test saving.
9. Optimize and test on Android.
10. Prepare a build only after core tests pass.

## 15. Future Possibilities

Additional environments, crafting, weather, expanded missions, and other features may be considered after the prototype is stable.

Pi Network integration or in-game rewards are not part of the initial prototype. Any future integration will depend on technical feasibility, platform requirements, security, and applicable rules.

## 16. Current Status

This document describes the intended game design. A feature listed here is not considered complete until it has been implemented and tested.

## 17. Success Criteria

The first playable prototype should allow the player to:

- Start and control the character
- Explore a test environment
- Collect resources
- Manage health, hunger, and thirst
- Complete or fail a mission
- Retry after failure
- Unlock the next level
- Run on a supported Android test device
