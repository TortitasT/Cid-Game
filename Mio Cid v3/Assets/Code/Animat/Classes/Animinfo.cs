using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Animinfo
{
    public string name;

    public float ppu;

    public Animinfo(string name, float ppu)
    {
        this.name = name;
        this.ppu = ppu;
    }
}
