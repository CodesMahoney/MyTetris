using UnityEngine;
using Assets;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class GameState : MonoBehaviour
{
    public int Level = 1;
    public GameObject Background;
    public Shape ActiveShape = null;
    public float GameRate;
    public Canvas Canvas;
    public bool CheckingCollision = false;
    private List<Shape> StaticPieces = new List<Shape>();

    void Start()
    {
        Background.SetColor(Color.blue);
        InvokeRepeating("ExecutePass", 1f, .5f);
    }

    void Update()
    {
        if (!CheckingCollision)
        {
            if (ActiveShape == null)
            {
                ActiveShape = new Shape();
            }

            if (Input.GetKeyDown("d"))
            {
                if (!CheckCollision(-1, 0))
                {
                    ActiveShape.X -= 1;
                }
            }

            if (Input.GetKeyDown("a"))
            {
                if (!CheckCollision(1, 0))
                {
                    ActiveShape.X += 1;
                }
            }

            if (Input.GetKeyDown("s"))
            {
                for (var i = 0; ActiveShape.Y + i >= 0; i--)
                {
                    if (CheckCollision(0, i - 1))
                    {
                        ActiveShape.Y += i;
                        continue;
                    }
                }
            }

            if (Input.GetKeyDown("e"))
            {
                ActiveShape.Rotate();
            }

            if (Input.GetKeyDown("q"))
            {
                ActiveShape.Rotate();
            }

            ActiveShape.SetPostion();
        }
    }

    public void ExecutePass() 
    {
        if (!CheckingCollision)
        {
            if (CheckCollision(0, -1))
            {
                LockShape();
            }
            else
            {
                ActiveShape.Y -= 1;
            }
        }
    }

    public Check

    public bool CheckCollision(int x, int y)
    {
        var timer = new Stopwatch();
        timer.Start();

        var collision = false;

        var allStaticCubes = new List<Cube>();
       
        StaticPieces.Select(shapes => shapes.Cubes).ToList().ForEach((cubes) => {
            allStaticCubes.AddRange(cubes);
        });

        var allStaticCubePositions = allStaticCubes.Select(cube => cube.GO.transform.position);
        var activeCubePositions = ActiveShape.Cubes.Select(cube => cube.GO.transform.position);

        if (x != 0)
        {
            allStaticCubePositions = allStaticCubePositions.Where(pos => pos.x >= ActiveShape.X - 1 && pos.x <= pos.x + 2);
        }

        foreach (var staticPos in allStaticCubePositions)
        {
            foreach( var activePos in activeCubePositions)
            {
                if (activePos.x + x == staticPos.x && activePos.y + y == staticPos.y)
                {
                    collision = true;
                }
            };
        };

        if (ActiveShape.Cubes.Any(cube => cube.GO.transform.position.y + y < 0))
        {
            collision = true;
        }

        timer.Stop();
        UnityEngine.Debug.Log("Timer: " + timer.Elapsed.TotalMilliseconds);

        CheckingCollision = false;

        return collision;
    }

    public void LockShape()
    {
        StaticPieces.Add(ActiveShape);
        ActiveShape = null;
    }
}
