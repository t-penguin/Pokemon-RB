using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapEditor
{
    private static BoundsInt ViewAreaBase = new BoundsInt(-9, -9, 0, 20, 18, 1);
    private static int GameMapMask = 1 << LayerMask.NameToLayer("Game Maps");

    // Clears the tilemap and then replicates the tiles that are in view onto the tilemap
    public static void SetTilemapToView(Tilemap tilemap, Vector2Int center)
    {
        tilemap.ClearAllTiles();

        // Set the view area bound using the position and base view area
        BoundsInt viewArea = new BoundsInt(center.x - 10, center.y - 9, 0, 20, 18, 1);

        List<Tilemap> mapsInView = GetTilemapsInView(new Vector2Int((center.x - 1) / 2, center.y / 2));

        foreach(Tilemap map in mapsInView)
        {
            for (int i = viewArea.xMin; i < viewArea.xMax; i++)
            {
                for (int j = viewArea.yMin; j < viewArea.yMax; j++)
                {
                    Vector3Int tilePosition = new Vector3Int(i, j);
                    TileBase tile = map.GetTile(tilePosition);
                    // Only set the tile if it isn't null, prevents overriding
                    if (tile != null)  
                        tilemap.SetTile(tilePosition, tile);
                }
            }
        }
    }

    // Returns a list of all the tilemaps from the gamemaps visible in the game view
    public static List<Tilemap> GetTilemapsInView(Vector2Int playerPosition)
    {
        List<GameObject> gameMapsInView = new List<GameObject>();

        /* Check the top-left and bottom-right of the view for an active game map
         * Add the game map to the maps in view if it's not already there */
        Vector2 topLeft = new Vector2(playerPosition.x - 4, playerPosition.y + 4);
        Vector2 bottomRight = new Vector2(playerPosition.x + 5, playerPosition.y - 4);

        // Check top-left
        RaycastHit2D hit;
        hit = Physics2D.Linecast(topLeft, topLeft, GameMapMask);
        if (hit.transform != null)
            gameMapsInView.Add(hit.transform.gameObject);

        // Check bottom-right
        hit = Physics2D.Linecast(bottomRight, bottomRight, GameMapMask);
        GameObject map;
        if(hit.transform != null)
        {
            map = hit.transform.gameObject;
            if (!gameMapsInView.Contains(map))
                gameMapsInView.Add(map);
        }

        // Check middle
        hit = Physics2D.Linecast(playerPosition, playerPosition, GameMapMask);
        if(hit.transform != null)
        {
            map = hit.transform.gameObject;
            if (!gameMapsInView.Contains(map))
                gameMapsInView.Add(map);
        }

        List<Tilemap> tilemaps = new List<Tilemap>();

        /* Get tile maps from each game map
         * Tile maps should be ordered in the hierarchy by their sorting layer
         * Layers further back should be higher up in the hierarchy */
        foreach (GameObject gameMap in gameMapsInView)
        {
            foreach (Tilemap tilemap in gameMap.GetComponentsInChildren<Tilemap>())
                tilemaps.Add(tilemap);
        }

        return tilemaps;
    }
}