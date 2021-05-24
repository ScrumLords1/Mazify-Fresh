using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum Wall
{

    Left = 4,
    Right = 2,
    Up = 1,
    Down = 3,

    Visited = 128,
}

public struct Position
{
    public int X;
    public int Y;
}

public struct WallDown
{
    public Position Position;
    public Wall Shared;
}



public class MazeGenerator
{
    //Returning the opposite wall of node
    private static Wall GetOppositeWall(Wall wall)
    {
        switch (wall) 
        {
            case Wall.Right: return Wall.Left;
            case Wall.Left: return Wall.Right;
            case Wall.Up: return Wall.Down;
            case Wall.Down: return Wall.Up;

            default: return Wall.Right;
        }
    }
    private static Wall[,] BackTrack(Wall[,] Mazify, int width, int height)
    {
        //Declare and create stack
        var ranNum = new System.Random(/*seed*/);
        var posStack = new Stack<Position>();
        var position = new Position {X = ranNum.Next(0, width), Y = ranNum.Next(0, height)};

        // Sets initial node to visited
        Mazify[position.X, position.Y] |= Wall.Visited;
        posStack.Push(position);

        //Start traversing the stack
        while(posStack.Count > 0)
        {
            //Start @ current node then find neighbouring node
            var current = posStack.Pop();
            var neighbours = UnvisitedNode(current, Mazify, width, height);

            //Keep track of unvisited nodes
            if(neighbours.Count > 0)
            {
                posStack.Push(current); //Current position onto stack

                var ranIndex = ranNum.Next(0, neighbours.Count);
                var ranNeighbour = neighbours[ranIndex];

              
                var nPosition = ranNeighbour.Position;

                //Remove shared wall current node
                Mazify[current.X, current.Y] &= ~ranNeighbour.Shared;
                Mazify[nPosition.X, nPosition.Y] &= ~GetOppositeWall(ranNeighbour.Shared);
                Mazify[nPosition.X, nPosition.Y] |= Wall.Visited; //mark node as visited

                //Neightbor position to the stack
                posStack.Push(nPosition);
            }
        }
        
        return Mazify; 
    }

    private static List<WallDown> UnvisitedNode(Position p, Wall[,] Mazify, int width, int height)
    {
        var list = new List<WallDown>();

        //Check Left Wall 
                if (p.X > 0)
                {
                    if (!Mazify[p.X - 1, p.Y].HasFlag(Wall.Visited))
                    {
                        list.Add(new WallDown { Position = new Position { X = p.X - 1, Y = p.Y }, Shared = Wall.Left });
                    }
                }
              
        //Check Bottom Walls (Down)
            if (p.Y > 0)
                {
                    if (!Mazify[p.X, p.Y - 1].HasFlag(Wall.Visited))
                    {
                        list.Add(new WallDown { Position = new Position { X = p.X, Y = p.Y - 1 }, Shared = Wall.Down });
                    }
                }
              
        //Check Top Wall (Up)
            if (p.Y < height - 1)
                {
                    if (!Mazify[p.X, p.Y + 1].HasFlag(Wall.Visited))
                    {
                        list.Add(new WallDown { Position = new Position { X = p.X, Y = p.Y + 1 }, Shared = Wall.Up });
                    }
               
                }
                

        //Check Right Wall
            if (p.X < width - 1)
                {
                    if (!Mazify[p.X + 1, p.Y].HasFlag(Wall.Visited))
                    {
                        list.Add(new WallDown { Position = new Position { X = p.X + 1, Y = p.Y }, Shared = Wall.Right });
                    }
              
                }

        return list;
    }
    
    public static Wall[,] Generate(int width, int height)
    {
        Wall[,] Mazify = new Wall[width, height];

        Wall initial = Wall.Right | Wall.Left | Wall.Up | Wall.Down;

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                Mazify[i, j] = initial;

            }
        }



        return BackTrack(Mazify, width, height);
    }


}





