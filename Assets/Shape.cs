using Assets;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class Shape
    {
        private System.Random rand = new System.Random(DateTime.Now.Millisecond);
        public List<Cube> Cubes { get; set; }
        public int ShapeIndex { get; set; }
        public int RotationIndex { get; set; }
        public Color Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        Dictionary<string, Vector3> Rotator { get; set; }

        public GameObject GO;

        public Shape()
        {
            GO = new GameObject("Shape");
            CreateRotationMatrix();
            SetRandomColor();
            SetShape();

            X = 0;
            Y = 20;
        }

        public void SetPostion()
        {
            this.Cubes.ForEach((x) =>
            {
                x.SetPosition(this.X, this.Y);
            });
        }

        public void Rotate()
        {
            foreach (var cube in Cubes)
            {
                var rotation = Rotator[$"{cube.XOffset},{cube.YOffset}"];

                cube.XOffset = (int)rotation.x;
                cube.YOffset = (int)rotation.y;
            }
        }

        public void SetRotationIndex(bool clockWise)
        {
            if (clockWise)
                RotationIndex += 1;
            else
                RotationIndex -= 1;

            if (RotationIndex > 3)
                RotationIndex = 0;

            if (RotationIndex < 0)
                RotationIndex = 3;
        }

        private void SetRandomColor()
        {
            switch (rand.Next(0, 5))
            {
                case 0:
                    Color = Color.red;
                    break;
                case 1:
                    Color = Color.green;
                    break;
                case 2:
                    Color = Color.yellow;
                    break;
                case 3:
                    Color = Color.magenta;
                    break;
                case 4:
                    Color = Color.cyan;
                    break;
            }
        }

        private void SetShape()
        {

            Cubes = new List<Cube>();

            if (this.ShapeIndex == 0)
                this.ShapeIndex = rand.Next(1, 6);

            switch ((Shapes)ShapeIndex)
            {
                case Shapes.Line:
                    Cubes.Add(new Cube(1, 0, -1, Color));
                    Cubes.Add(new Cube(2, 0, 0, Color));
                    Cubes.Add(new Cube(3, 0, 1, Color));
                    Cubes.Add(new Cube(4, 0, 2, Color));
                    break;
                case Shapes.Tee:
                    Cubes.Add(new Cube(1, 0, -1, Color));
                    Cubes.Add(new Cube(2, 0, 0, Color));
                    Cubes.Add(new Cube(3, 0, 1, Color));
                    Cubes.Add(new Cube(4, 1, 0, Color));
                    break;
                case Shapes.Squiggly:
                    Cubes.Add(new Cube(1, 0, 0, Color));
                    Cubes.Add(new Cube(2, 1, 0, Color));
                    Cubes.Add(new Cube(3, 0, 1, Color));
                    Cubes.Add(new Cube(4, -1, 1, Color));
                    break;
                case Shapes.El:
                    Cubes.Add(new Cube(1, 0, -1, Color));
                    Cubes.Add(new Cube(2, 0, 0, Color));
                    Cubes.Add(new Cube(3, 0, 1, Color));
                    Cubes.Add(new Cube(4, -1, 1, Color));
                    break;
                case Shapes.Block:
                    Cubes.Add(new Cube(1, 0, 0, Color));
                    Cubes.Add(new Cube(2, 0, 1, Color));
                    Cubes.Add(new Cube(3, 1, 0, Color));
                    Cubes.Add(new Cube(4, 1, 1, Color));
                    break;
            }

            foreach (var cube in Cubes)
            {
                cube.GO.transform.SetParent(this.GO.transform);
            }
        }

        private void CreateRotationMatrix()
        {
            Rotator = new Dictionary<string, Vector3>();

            Rotator["0,0"] = new Vector3(1, 0);
            Rotator["1,0"] = new Vector3(1, 1);
            Rotator["1,1"] = new Vector3(0, 1);
            Rotator["0,1"] = new Vector3(0, 0);

            Rotator["-1,-1"] = new Vector3(2, -1);
            Rotator["2,-1"] = new Vector3(2, 2);
            Rotator["2,2"] = new Vector3(-1, 2);
            Rotator["-1,2"] = new Vector3(-1, -1);

            Rotator["0,-1"] = new Vector3(2, 0);
            Rotator["2,0"] = new Vector3(1, 2);
            Rotator["1,2"] = new Vector3(-1, 1);
            Rotator["-1,1"] = new Vector3(0, -1);

            Rotator["1,-1"] = new Vector3(2, 1);
            Rotator["2,1"] = new Vector3(0, 2);
            Rotator["0,2"] = new Vector3(-1, 0);
            Rotator["-1,0"] = new Vector3(1, -1);
        }
    }
}
