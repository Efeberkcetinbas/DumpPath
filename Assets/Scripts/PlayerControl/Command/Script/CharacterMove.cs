using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]
    private List<Move> commandList = new List<Move>();
    private int index;

    #region path drawing
    //path drawing
    private float verticalOffset = 0.1f;
    private Vector3 startPoint;
    private PathDraw pathDrawer;

    private void Start()
    {
        startPoint = this.transform.position;
        startPoint.y = verticalOffset;
        pathDrawer = this.GetComponent<PathDraw>();
    }
    #endregion

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnFalseMove,OnFalseMove);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnFalseMove,OnFalseMove);

    }

    public void AddCommand(Move command)
    {
        if (index < commandList.Count)
            commandList.RemoveRange(index, commandList.Count - index);

        commandList.Add(command);
        //command.Execute();
        index++;

        UpdateLine();
    }

    public void UndoCommand()
    {
        if (commandList.Count == 0)
            return;
        if (index > 0)
        {
            EventManager.Broadcast(GameEvent.OnUndoBegin);
            commandList[index - 1].Undo();
            index--;
            EventManager.Broadcast(GameEvent.OnUndoEnd);
        }
        UpdateLine();
    }

    private void OnFalseMove()
    {
        //Bug Fixed with Index=0;
        index=0;
        commandList.Clear();
    }

    public void RedoCommand()
    {
        if (commandList.Count == 0)
            return;

        if (index < commandList.Count)
        {
            Debug.Log("UNDO");
            index++;
            commandList[index - 1].Execute();
        }
        
        UpdateLine();
    }

    public void UpdateLine()
    {
        if (pathDrawer == null)
            return;

        List<Vector3> positions = new List<Vector3>();
        positions.Add(startPoint);

        for (int i = 0; i < index; i++)
        {
            Vector3 newPosition = commandList[i].GetMove() + positions[i];
            newPosition.y = verticalOffset; // used to keep it near the ground
            positions.Add(newPosition);
        }

        //pathDrawer.UpdateLine(positions);
    }
}



