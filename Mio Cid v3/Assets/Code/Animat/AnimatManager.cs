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

        public Animinfo animinfo;

        public Animation(string name, Sprite[] frames, Animinfo animinfo)
        {
            this.name = name;
            this.frames = frames;
            this.animinfo = animinfo;
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

            Animinfo animinfo = LoadAnimInfo(animationDir);

            if (animinfo == null)
            {
                animinfo = new Animinfo("Default", 100f);
            }

            List<Sprite> animationSprites = new List<Sprite>();

            foreach (string animationFile in animationFiles)
            {
                Sprite sprite =
                    IMG2Sprite
                        .instance
                        .LoadNewSprite(animationFile, animinfo.ppu);
                animationSprites.Add (sprite);
            }

            AddAnimation(Path.GetFileName(animationDir),
            animationSprites.ToArray(),
            animinfo);
        }
    }

    public void AddAnimation(string name, Sprite[] animation, Animinfo animinfo)
    {
        animations.Add(new Animation(name, animation, animinfo));
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

    private Animinfo LoadAnimInfo(string animationDir)
    {
        string animInfoPath = animationDir + "/animinfo.json";

        if (File.Exists(animInfoPath))
        {
            string json = File.ReadAllText(animInfoPath);

            return JsonUtility.FromJson<Animinfo>(json);
        }
        return null;
    }
}
