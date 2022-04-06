using System.Collections;
using System.Collections.Generic;

public class Character
{
    public string name;
    public int level;
    public float levelProgress;
    public Inventory inventory;

    public Character(string name, int level, float levelProgress){
        this.name = name;
        this.level = level;
        this.levelProgress = levelProgress;
    }
}
