using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum State {
        Idle,
        Walking,
        Attacking,
        Menuing,
    };

    private State state = State.Idle;

    //Public functions
    public State GetState(){
        return state;
    }
    public void SetState(State newState){
        state = newState;
    }
}
