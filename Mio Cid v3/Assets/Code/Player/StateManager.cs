using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance = null;

    public enum State
    {
        Idle,
        Talking,
        Menuing,
    };

    private State state = State.Idle;

    //Public functions
    public State GetState()
    {
        return state;
    }
    public void SetState(State newState)
    {
        state = newState;
    }
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
