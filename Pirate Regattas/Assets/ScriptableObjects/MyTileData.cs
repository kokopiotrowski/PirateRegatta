using UnityEngine;
using UnityEngine.Tilemaps;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class MyTileData : ScriptableObject
    {
        public TileBase[] tiles;

        public bool swimmable;
        public bool isWin;
        public int moveMultiplier;
        public float randomEventChance;

    }
}
