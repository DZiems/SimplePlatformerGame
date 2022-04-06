using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


enum CheckpointManagerStyle
{
    ByMostRecent,
    ByFurthest
}

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] CheckpointManagerStyle _style;

    private Checkpoint[] _checkpoints;
    private Checkpoint _mostRecentCheckpoint;


    private void Start()
    {
        //GetComponentsInChildren will get children in their order in the editor
        _checkpoints = GetComponentsInChildren<Checkpoint>();

        foreach (var checkpoint in _checkpoints)
            checkpoint.OnPassed += SetMostRecentCheckpoint;
    }

    //warning: assumes checkpoint order in the editor determines which checkpoint is furthest, not the literal distance.
    private Checkpoint GetFurthestPassedCheckpoint()
    {
        return _checkpoints.LastOrDefault(t => t.Passed);
    }

    private void SetMostRecentCheckpoint(Checkpoint checkpoint)
    {
        _mostRecentCheckpoint = checkpoint;
    }

    public Checkpoint GetPlayerCheckpoint() {
        switch (_style)
        {
            case CheckpointManagerStyle.ByFurthest:
                return GetFurthestPassedCheckpoint();
            case CheckpointManagerStyle.ByMostRecent:
                return _mostRecentCheckpoint;
            default:
                return GetFurthestPassedCheckpoint();
        }
    }
}
