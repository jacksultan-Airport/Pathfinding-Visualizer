using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Testing : MonoBehaviour {
    
    [SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    [SerializeField] private PathfindingVisual pathfindingVisual;
   
    private Pathfinding pathfinding;

    private void Start() {
        //                            height and width of the cells
        pathfinding = new Pathfinding(30, 30);
        pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
    }

    private void Update() {
        //left mouse click event
        if (Input.GetMouseButtonDown(0)) {

            // Gets mouse position from the camera perspective and mouse x,y position, was coded in utiliti
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            //Passes that value to the pathfinding algorithm 
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            // then we call calculated list of best nodes with least f cost. 
            // Find Path(0,0,x,y) 0, 0 starting point, x,y value from mouse world position
            List<PathNode> path = pathfinding.FindPath(5, 5, x, y);
            //Draws line if click
            //Goes through the list of nodes then draws lines
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
            
        }

        //Right click event
        //Sets unwalkable nodes
        if (Input.GetMouseButtonDown(1)) {
            // Gets mouse position from the camera perspective and mouse x,y position, was coded in utiliti
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            //Sets unwalkable nodes
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }
    }

}
