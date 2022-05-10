using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AnimatManager : MonoBehaviour
{
    [Serializable]
    public struct Animation
    {
        public string name;

        public Sprite[] frames;

        public Animation(string name, Sprite[] frames)
        {
            this.name = name;
            this.frames = frames;
        }
    }

    public List<Animation> animations;

    public static AnimatManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy (gameObject);
        }
    }

    private void Start()
    {
        LoadModAnimations();
    }

    private void LoadModAnimations()
    {
        string[] animationsToLoad = ModManager.instance.GetAnimations();

        foreach (string animationDir in animationsToLoad)
        {
            Debug.Log("Loading animation " + animationDir);

            string[] animationFiles = Directory.GetFiles(animationDir, "*.png");

            List<Sprite> animationSprites = new List<Sprite>();

            foreach (string animationFile in animationFiles)
            {
                Sprite sprite =
                    IMG2Sprite.instance.LoadNewSprite(animationFile, 100f);
                animationSprites.Add (sprite);
            }

            AddAnimation(Path.GetFileName(animationDir),
            animationSprites.ToArray());
        }
    }

    public void AddAnimation(string name, Sprite[] animation)
    {
        animations.Add(new Animation(name, animation));
    }

    public Sprite[] GetAnimation(string name)
    {
        foreach (Animation animation in animations)
        {
            if (animation.name == name)
            {
                return animation.frames;
            }
        }
        return null;
    }
}
