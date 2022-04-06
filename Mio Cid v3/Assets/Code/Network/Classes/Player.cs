using System.Collections;
using System.Collections.Generic;

public class Player
{
    public string id;
    public Character character;
    public Vector2Data pos;

    public Player(string id, Character character, Vector2Data pos){
        this.id = id;
        this.character = character;
        this.pos = pos;
    }
}
