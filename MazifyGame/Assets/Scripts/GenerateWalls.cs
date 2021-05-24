using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RenderingWalls : MonoBehaviour
{
    [SerializeField]
    [Range(1, 50)]
    private int width = 20;

    [SerializeField]
    [Range(1, 50)]
    private int height = 20;

    [SerializeField]
    private float size = 0.5f;

    [SerializeField]
    private Transform wallFab = null;

    [SerializeField]
    private Transform floorFab = null;

    // Start is called before the first frame update
    void Start()
    {
        var Mazify = MazeGenerator.Generate(width, height);

        Draw(Mazify);

    }
    // LAyout the maze
    private void Draw(Wall[,] Mazify)
    {
        //instantiating the floor plane
        var floor = Instantiate(floorFab, transform);
        floor.localScale = new Vector3(width, 1, height);

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var cell = Mazify[i, j];
                var position = new Vector3(-width / 2 + i, 0, -height /2  + j); //centers the maze

                if (cell.HasFlag(Wall.Up))
                {
                    var wallTop = Instantiate(wallFab, transform) as Transform;
                    wallTop.position = position + new Vector3(0, 0, size / 2);
                    wallTop.localScale = new Vector3(size, wallTop.localScale.y, wallTop.localScale.z);
                }

                if (cell.HasFlag(Wall.Left))
                {
                    var wallLeft = Instantiate(wallFab, transform) as Transform;
                    wallLeft.position = position + new Vector3(-size / 2, 0, 0);
                    wallLeft.localScale = new Vector3(size, wallLeft.localScale.y, wallLeft.localScale.z);
                    wallLeft.eulerAngles = new Vector3(0, 90, 0);
                }

                if(i == width - 1)
                {
                    if (cell.HasFlag(Wall.Right))
                    {
                        var wallRight = Instantiate(wallFab, transform) as Transform;
                        wallRight.position = position + new Vector3(+size / 2, 0, 0);
                        wallRight.localScale = new Vector3(size, wallRight.localScale.y, wallRight.localScale.z);
                        wallRight.eulerAngles = new Vector3(0, 90, 0);
                    }
                }

                if(j == 0)
                {
                    if (cell.HasFlag(Wall.Down))
                    {
                        var wallBottom = Instantiate(wallFab, transform) as Transform;
                        wallBottom.position = position + new Vector3(0, 0, -size /2);
                        wallBottom.localScale = new Vector3(size, wallBottom.localScale.y, wallBottom.localScale.z);
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

}
