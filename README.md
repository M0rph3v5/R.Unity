# R.Unity
_Get strong typed, autocompleted resources like scenes, tags and layers in C# Unity projects_

## Why use this?

It makes your code that uses resources:
- **Fully typed**, less guessing what a method will return
- **Compile time checked**, no more incorrect strings that make your application crash at runtime
- **Autocompleted**, never have to guess that scene name again

Currently you type:
```csharp
string sceneName = "Level1";
LayerMask layer = LayerMask.NameToLayer("TransparentFX");
GameObject go = FindGameObjectsWithTag("Bullets"); // seriously, never use FindGameObjectsWithTag though
Animator.Play("IdleState");
```

With R.Unity it becomes:
```csharp
string sceneName = R.Scene.Level1;
LayerMask layer = R.Layer.TransparentFXMask;
GameObject go = FindGameObjectsWithTag(R.Tag.Bullets); // seriously, never use FindGameObjectsWithTag though
Animator.Play(R.Animator.PlayerAnimator.IdleState);
```

## How to use this?

Right now it doesn't auto compile everytime you add a new animation, scene or a tag yet. You can manually trigger R.Unity by running:

`Tools > R.cs > Rebuild`

# openUPM

[![openupm](https://img.shields.io/npm/v/com.m0rph3v5.runity?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.m0rph3v5.runity/)

This repository has UPM support. Install it with:

```
npm install -g openupm-cli
openupm add com.m0rph3v5.runity
```
