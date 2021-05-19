using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GameScripts
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private Tilemap map;

        [SerializeField] private List<MyTileData> tileDatas;

        private Dictionary<TileBase, MyTileData> dataFromTiles;


        private void Awake()
        {
            dataFromTiles = new Dictionary<TileBase, MyTileData>();

            foreach (var tileData in tileDatas)
            {
                foreach (var tile in tileData.tiles)
                {
                    dataFromTiles.Add(tile, tileData);
                }
            }
        }
        public bool CheckIfSwimmable(Vector2 position)
        {
            Vector3Int gridPosition = map.WorldToCell(position);
            TileBase tile = map.GetTile(gridPosition);
            return dataFromTiles[tile].swimmable;
        }

        public bool CheckIfWin(Vector2 position)
        {
            Vector3Int gridPosition = map.WorldToCell(position);
            TileBase tile = map.GetTile(gridPosition);
            return dataFromTiles[tile].isWin;
            
        }
    }
}
