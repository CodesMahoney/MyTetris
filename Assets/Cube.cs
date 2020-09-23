using UnityEngine;

namespace Assets
{
    public class Cube
    {
        public GameObject GO { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public Vector3 Position => GO.transform.position;

        public Cube(int Id, int XOffset, int YOffset, Color color)
        {
            this.XOffset = XOffset;
            this.YOffset = YOffset;

            this.GO = GameObject.CreatePrimitive(PrimitiveType.Cube).SetPosition(new Vector3(XOffset, YOffset)).SetColor(color);
        }

        public void SetPosition(int x, int y)
        {
            GO.transform.position = new Vector3(x + XOffset, y + YOffset);
        }
    }
}
