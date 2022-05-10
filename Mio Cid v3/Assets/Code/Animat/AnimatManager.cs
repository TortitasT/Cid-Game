using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatManager : MonoBehaviour
{
    public static AnimatManager instance;

    [SerializeField]
    private Dictionary<string, Sprite[]>
        animations = new Dictionary<string, Sprite[]>();

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
        string[] animationsToLoad = ModManager.instance.GetAnimations();

        foreach (string animationDir in animationsToLoad)
        {
            Debug.Log("Loading animation " + animationDir);
        }
    }

    public void AddAnimation(string name, Sprite[] animation)
    {
        animations.Add (name, animation);
    }

    public Sprite[] GetAnimation(string name)
    {
        return animations[name];
    }
}
