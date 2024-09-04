using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventList : MonoBehaviour
{
    public void EndTurn()
    {
        BuildManager.Instance.ChangeUI(BuildState.TurnEnd);
    }
    // TEST EVENT
    public void Event0_Choice0_hello()
    {
        Debug.Log("HELLO~");
        EndTurn();
    }
}
