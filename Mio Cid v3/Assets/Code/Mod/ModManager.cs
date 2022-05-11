using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ModManager : MonoBehaviour
{
    public static ModManager instance;

    public struct Mod
    {
        public Modinfo modinfo;

        public string dir;

        public Mod(Modinfo modinfo, string dir)
        {
            this.modinfo = modinfo;
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
            Directory.GetDirectories(Application.streamingAssetsPath + "/Mods");

        foreach (string modDir in scannedMods)
        {
            Modinfo modInfo = LoadModInfo(modDir);

            if (modInfo != null)
            {
                if (modInfo.enabled)
                {
                    mods.Add(new Mod(modInfo, modDir));
                }
            }
        }

        SortMods();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        foreach (Mod mod in mods)
        {
            AlertManager.Instance.Alert("Loaded " + mod.modinfo.name);
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.5f);

        AlertManager.Instance.Alert("Loaded all mods!");
    }

    public string[] GetAnimations()
    {
        List<string> animations = new List<string>();

        foreach (Mod mod in mods)
        {
            if (Directory.Exists(mod.dir + "/Animations"))
            {
                string[] modAnimations =
                    Directory.GetDirectories(mod.dir + "/Animations");

                foreach (string animationDir in modAnimations)
                {
                    string duplicate = GetDuplicate(animationDir, animations);
                    if (duplicate != null)
                    {
                        animations.Remove (duplicate);
                        Debug
                            .Log("Removed duplicate animation: " +
                            duplicate +
                            " because " +
                            animationDir +
                            " is a duplicate.");
                    }

                    animations.Add (animationDir);
                }
            }
        }

        return animations.ToArray();
    }

    private void AddMod(Modinfo modinfo, string dir)
    {
        mods.Add(new Mod(modinfo, dir));
    }

    private Modinfo LoadModInfo(string modDir)
    {
        if (File.Exists(modDir + "/Modinfo.json"))
        {
            string modInfoPath = modDir + "/Modinfo.json";

            string modInfoJson = File.ReadAllText(modInfoPath);

            Modinfo modInfo = JsonUtility.FromJson<Modinfo>(modInfoJson);

            return modInfo;
        }

        return null;
    }

    private void SortMods()
    {
        mods.Sort((a, b) => a.modinfo.order.CompareTo(b.modinfo.order));
    }

    private string GetDuplicate(string animationDir, List<string> animations)
    {
        foreach (string storedAnimationDir in animations)
        {
            string storedAnimationDirName =
                new DirectoryInfo(storedAnimationDir).Name;
            string animationDirName = new DirectoryInfo(animationDir).Name;

            if (storedAnimationDirName == animationDirName)
            {
                return storedAnimationDir;
            }
        }
        return null;
    }
}
