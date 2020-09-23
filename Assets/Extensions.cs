using UnityEngine;

namespace Assets
{
    public static class Extensions
    {
        public static GameObject SetPosition(this GameObject gameObject, Vector3 position)
        {
            gameObject.transform.position = position;
            return gameObject;
        }

        public static GameObject SetColor(this GameObject gameObject, Color color)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
            return gameObject;
        }

        public static GameObject AddParent(this GameObject child, GameObject parent)
        {
            child.transform.SetParent(parent.transform);
            return child;
        }
    }
}
