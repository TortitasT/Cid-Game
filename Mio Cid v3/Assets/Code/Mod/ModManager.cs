using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModManager : MonoBehaviour
{
    public static ModManager instance;

    // private Dictionary<string, string> mods = new Dictionary<string, string>();
    public struct Mod
    {
        public string name;

        public string dir;

        public Mod(string name, string dir)
        {
            this.name = name;
            this.dir = dir;
        }
    }

    public List<Mod> mods = new List<Mod>();

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
            AddMod (modName, modDir);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        foreach (Mod mod in mods)
        {
            AlertManager.Instance.Alert("Load " + mod.name);
        }
        yield return new WaitForSeconds(0.5f);

        AlertManager.Instance.Alert("Loaded all mods!");
    }

    public string[] GetAnimations()
    {
        List<string> animations = new List<string>();

        foreach (Mod mod in mods)
        {
            string modDir = mod.name;

            string[] modAnimations = new string[] { };
            if (Directory.Exists(mod.dir + "/Animations"))
            {
                modAnimations =
                    Directory.GetDirectories(mod.dir + "/Animations");
            }

            foreach (string animationDir in modAnimations)
            {
                animations.Add (animationDir);
            }
        }

        return animations.ToArray();
    }

    private void AddMod(string name, string dir)
    {
        mods.Add(new Mod(name, dir));
    }
}
