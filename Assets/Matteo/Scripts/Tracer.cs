using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Tracer
{
    public bool added = false;
    RootTileController previousTile;
    RootTileController currentTile;
    List<RootTileController> availableConnections = new List<RootTileController>();

    public Tracer(RootTileController startingTile, RootTileController previousTile)
    {
        currentTile = startingTile;
        this.previousTile = previousTile;
        RootMap.Instance().tracersToBeAdded.Add(this);
    }

    /* if this returns false, delete tracer*/
    public bool Tick()
    {

        availableConnections = currentTile.connectedTiles;
        bool allConnectionsAreFlagged = true;
        foreach (RootTileController connectedTile in availableConnections)
        {
            allConnectionsAreFlagged = allConnectionsAreFlagged && connectedTile.tileData.isConnectedToTree;
        }
        //I came back to a root that has been already flagged
        if (allConnectionsAreFlagged)
        {
            //RemoveThisTracer();
            return false;

        }


        //WaitForSeconds
        switch (availableConnections.Count)
        {
            //I should never have 0, but idk, bug happen
            case 0:
                return false;
            //I'm at the end of a root, or is the tree origin
            case 1:

                //bool isConnectedOnlyToTree = availableConnections[0].tileData.tileState == RootTileData.TileState.TreeOrigin;
                bool isThisTree = currentTile.tileData.tileState == RootTileData.TileState.TreeOrigin;
                if (!isThisTree)
                {
                    FlagAsConnected(currentTile);
                    //RemoveThisTracer();
                    return false;
                }
                else
                {
                    AdvanceTracer();
                }

                break;

            case 2:
                AdvanceTracer();
                break;


            default:

                // AdvanceTracer();
                //this line is to avoid spawning another tracer from the same one
                availableConnections.Remove(previousTile);
                foreach (RootTileController nextTile in availableConnections)
                {

                    if (nextTile.tileData.isConnectedToTree == true) continue;
                    Tracer newTracer = new Tracer(nextTile, currentTile);
                    RootMap.Instance().tracersToBeAdded.Add(newTracer);



                }
                break;

        }

        return true;

    }
    //this will continue if has a previous, otherwise it will move just to the only available connection
    private void AdvanceTracer()
    {
        //it shouldn't give an error if previousTile is null
        availableConnections.Remove(previousTile);
        FlagAsConnected(currentTile);
        previousTile = currentTile;
        currentTile = availableConnections[0];
    }



    public void FlagAsConnected(RootTileController controller)
    {
        if (currentTile.tileData.isConnectedToTree == true) return;
        bool isPlayer1 = currentTile.tileData.rootOwner == Player.PlayerID.Player1;
        GameEvent eventToRaise = isPlayer1 ? currentTile.eventsSO.player1IncreaseScoreEvent : currentTile.eventsSO.player2IncreaseScoreEvent;
        eventToRaise.Raise();
        currentTile.tileData.isConnectedToTree = true;
        currentTile.spawner.SpawnDebug(currentTile);
    }


}
