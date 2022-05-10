using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModManager : MonoBehaviour
{
    public static ModManager instance;

    private Dictionary<string, string> mods = new Dictionary<string, string>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy (gameObject);
        }

        ScanMods();
    }

    private void ScanMods()
    {
        string[] scannedMods =
            Directory.GetDirectories(Application.dataPath + "/Mods");

        foreach (string modDir in scannedMods)
        {
            string modName = Path.GetFileName(modDir);
            mods.Add (modName, modDir);

            Debug.Log("Found " + modName + " in " + modDir);
        }
    }

    public string[] GetAnimations()
    {
        List<string> animations = new List<string>();

        foreach (string modName in mods.Keys)
        {
            string modDir = mods[modName];

            string[] modAnimations = new string[] { };
            if (Directory.Exists(modDir + "/Animations"))
            {
                modAnimations =
                    Directory.GetDirectories(modDir + "/Animations");
            }

            foreach (string animationDir in modAnimations)
            {
                animations.Add (animationDir);
            }
        }

        return animations.ToArray();
    }
}
