using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public static class BattleTransition
{
    private static TileBase BlackTile;

    static BattleTransition()
    {
        BlackTile = Resources.Load<TileBase>("Tiles/Black Tile");
    }

    public static IEnumerator StartBattleTransition(int transitionIndex, Tilemap tilemap, Vector2Int position, BattleStateManager battle)
    {
        TilemapRenderer renderer = tilemap.GetComponent<TilemapRenderer>();
        renderer.sortingOrder = -1;

        // Replicate the tiles that are in view
        Vector2Int center = position * 2;
        center.x++;
        TilemapEditor.SetTilemapToView(tilemap, center);

        // Flash the screen several times if the battle is wild
        if (battle.BattleType == BattleType.WILD_BATTLE)
            yield return battle.StartCoroutine(FlashScreen(battle.BattleFlash));

        // Start the animation depending on the animation type

        float waitTime;

        switch (transitionIndex)
        {
            default:
            case 0:
                yield return battle.StartCoroutine(DoubleCircle(tilemap, center));
                waitTime = 84 / 60f;
                break;
            case 1:
                yield return battle.StartCoroutine(SingleCircle(tilemap, center));
                waitTime = 84 / 60f;
                break;
            case 2:
                yield return battle.StartCoroutine(HorizontalStripes(tilemap, center));
                waitTime = 82 / 60f;
                break;
            case 3:
                yield return battle.StartCoroutine(VerticalStripes(tilemap, center));
                waitTime = 82 / 60f;
                break;
            case 4:
                yield return battle.StartCoroutine(InwardSpiral(tilemap, center));
                waitTime = 94 / 60f;
                break;
            case 5:
                yield return battle.StartCoroutine(OutwardSpiral(tilemap, center));
                waitTime = 94 / 60f;
                break;
            case 6:
                yield return battle.StartCoroutine(Shrink(tilemap, center));
                waitTime = 108 / 60f;
                break;
            case 7:
                yield return battle.StartCoroutine(Split(tilemap, center));
                waitTime = 108 / 60f;
                break;
        }

        renderer.sortingOrder = 1;
        yield return new WaitForSeconds(waitTime);

        tilemap.ClearAllTiles();
    }

    #region Transitions

    private static IEnumerator FlashScreen(Image image)
    {
        // dark, bright, dark, bright, dark, bright, normal
        for (int i = 0; i < 6; i++)
        {
            Color color = i % 2 == 0 ? Color.black : Color.white;
            for(int j = 1; j <= 8; j++)
            {
                color.a = j < 5 ? 0.24f * j : 0.24f * (8 - j);
                image.color = color;
                yield return new WaitForSeconds(1 / 30f);
            }
        }
    }

    /* Battle Type: Wild
     * Dungeon Map: No
     * Player Level + 3 > Enemy Level */
    private static IEnumerator DoubleCircle(Tilemap tilemap, Vector2Int center)
    {
        List<Vector3Int> tilePositions = new List<Vector3Int>();

        // Counter-clockwise from the horizontal
        for (int i = 1; i < 31; i++)
        {
            float slope;
            tilePositions.Clear();

            // Vertical line (tan does not exist)
            if (i == 15)
                slope = 25;
            else
            {
                float theta = Mathf.PI * i / 30f;
                float tangent = Mathf.Tan(theta);
                slope = tangent;
            }

            bool firstHalf = i <= 15;
            // Top right & Bottom Right
            float xBound = 10;
            float yBound = firstHalf ? 9 : -10;

            tilePositions.AddRange(GetTilesBelowLine(tilemap, slope, xBound, yBound, center));

            // Bottom left & Top left
            xBound = -10;
            yBound = firstHalf ? -10 : 9;

            tilePositions.AddRange(GetTilesAboveLine(tilemap, slope, xBound, yBound, center));

            foreach (Vector3Int tp in tilePositions)
                tilemap.SetTile(tp, BlackTile);

            yield return new WaitForSeconds(1 / 60f);
        }
    }

    /* Battle Type: Wild
     * Dungeon Map: No
     * Player Level + 3 <= Enemy Level */
    private static IEnumerator SingleCircle(Tilemap tilemap, Vector2Int center)
    {
        List<Vector3Int> tilePositions = new List<Vector3Int>();

        // Counter-clockwise from the right horizontal
        for (int i = 1; i < 61; i++)
        {
            float slope;
            tilePositions.Clear();

            if (i == 15 || i == 45)
                slope = 25;
            else
            {
                float theta = 2 * Mathf.PI * i / 60f;
                float tangent = Mathf.Tan(theta);
                slope = tangent;
            }

            bool top = i <= 30;
            float yBound = top ? 9 : -10;

            bool right = i <= 15 || i > 45;
            float xBound = right ? 10 : -10;

            if(right)
                tilePositions.AddRange(GetTilesBelowLine(tilemap, slope, xBound, yBound, center));
            else
                tilePositions.AddRange(GetTilesAboveLine(tilemap, slope, xBound, yBound, center));

            foreach (Vector3Int tp in tilePositions)
                tilemap.SetTile(tp, BlackTile);

            yield return new WaitForSeconds(1 / 60f);
        }
    }

    /* Battle Type: Wild
     * Dungeon Map: Yes
     * Player Level + 3 > Enemy Level */
    private static IEnumerator HorizontalStripes(Tilemap tilemap, Vector2Int center)
    {
        List<Vector3Int> tilePositions = new List<Vector3Int>();

        // Alternating left-to-right and right-to-left stripes
        for (int i = 1; i < 63; i++)
        {
            int xThreshold = (int)(20 * (i / 62f) - 11);
            for (int y = -8; y < 9; y += 2)
                tilePositions.Add(new Vector3Int(center.x + xThreshold, center.y + y));

            xThreshold = (int)(-20 * (i / 62f) + 10);
            for (int y = -9; y < 8; y += 2)
                tilePositions.Add(new Vector3Int(center.x + xThreshold, center.y + y));

            foreach (Vector3Int tp in tilePositions)
                tilemap.SetTile(tp, BlackTile);

            yield return new WaitForSeconds(1 / 60f);
        }
    }

    /* Battle Type: Wild
     * Dungeon Map: Yes
     * Player Level + 3 <= Enemy Level */
    private static IEnumerator VerticalStripes(Tilemap tilemap, Vector2Int center)
    {
        List<Vector3Int> tilePositions = new List<Vector3Int>();

        // Alternating top-to-bottom and bottom-to-top stripes
        for (int i = 1; i < 55; i++)
        {
            int yThreshold = (int)(18 * (i / 54f) - 10);
            for (int x = -9; x < 10; x += 2)
                tilePositions.Add(new Vector3Int(center.x + x, center.y + yThreshold));

            yThreshold = (int)(-18 * (i / 54f) + 9);
            for (int x = -10; x < 9; x += 2)
                tilePositions.Add(new Vector3Int(center.x + x, center.y + yThreshold));

            foreach (Vector3Int tp in tilePositions)
                tilemap.SetTile(tp, BlackTile);

            yield return new WaitForSeconds(1 / 60f);
        }
    }

    /* Battle Type: Trainer
     * Dungeon Map: No
     * Player Level + 3 > Enemy Level */
    private static IEnumerator InwardSpiral(Tilemap tilemap, Vector2Int center)
    {
        List<Vector3Int> tilePositions = new List<Vector3Int>();

        int previousSteps = 0;

        Vector2Int change = new Vector2Int(0, -1);
        Vector2Int previousPosition = new Vector2Int(-10, 9);
        Vector2Int nextCorner = new Vector2Int(-10, -9);
        int cornersHit = 0;
        Vector2Int size = new Vector2Int(19, 18);

        // Counter-clockwise inward spiral starting from the top left
        for (int i = 1; i < 153; i++)
        {
            tilePositions.Clear();
            float totalSteps = 20 * 18 * i / 152;
            int steps = (int)totalSteps - previousSteps;
            previousSteps = (int)totalSteps;

            for(int j = 0; j < steps; j++)
            {
                Vector2Int currentPosition = previousPosition + change;
                tilePositions.Add(new Vector3Int(currentPosition.x + center.x, currentPosition.y + center.y));
                previousPosition = currentPosition;

                if(currentPosition == nextCorner)
                {
                    cornersHit++;
                    if(cornersHit % 2 == 0)
                    {
                        size.x--;
                        size.y--;
                    }

                    if(change.x == 0)
                    {
                        change.x = change.y < 0 ? 1 : -1;
                        change.y = 0;
                        nextCorner.x += size.x * change.x;
                    }
                    else
                    {
                        change.y = change.x > 0 ? 1 : -1;
                        change.x = 0;
                        nextCorner.y += size.y * change.y;
                    }
                }
            }

            foreach (Vector3Int tp in tilePositions)
                tilemap.SetTile(tp, BlackTile);

            yield return new WaitForSeconds(1 / 60f);
        }
    }

    /* Battle Type: Trainer
     * Dungeon Map: No
     * Player Level + 3 <= Enemy Level */
    private static IEnumerator OutwardSpiral(Tilemap tilemap, Vector2Int center)
    {
        List<Vector3Int> tilePositions = new List<Vector3Int>();

        int previousSteps = 0;

        Vector2Int change = new Vector2Int(-1, 0);
        Vector2Int previousPosition = new Vector2Int(1, -1);
        Vector2Int nextCorner = new Vector2Int(-1, -1);
        int cornersHit = 0;
        Vector2Int size = new Vector2Int(1, 1);
        // Counter-clockwise outward spiral starting from the top right of the 4 middle tiles
        for (int i = 1; i < 123; i++)
        {
            tilePositions.Clear();
            float totalSteps = 20 * 19 * i / 122;
            int steps = (int)totalSteps - previousSteps;
            previousSteps = (int)totalSteps;

            for (int j = 0; j < steps; j++)
            {
                Vector2Int currentPosition = previousPosition + change;
                tilePositions.Add(new Vector3Int(currentPosition.x + center.x, currentPosition.y + center.y));
                previousPosition = currentPosition;

                if (currentPosition == nextCorner)
                {
                    cornersHit++;
                    if (cornersHit % 2 == 0)
                    {
                        size.x++;
                        size.y++;
                    }

                    if (change.x == 0)
                    {
                        change.x = change.y < 0 ? 1 : -1;
                        change.y = 0;
                        nextCorner.x += size.x * change.x;
                    }
                    else
                    {
                        change.y = change.x > 0 ? 1 : -1;
                        change.x = 0;
                        nextCorner.y += size.y * change.y;
                    }
                }
            }

            foreach (Vector3Int tp in tilePositions)
                tilemap.SetTile(tp, BlackTile);

            yield return new WaitForSeconds(1 / 60f);
        }
    }

    /* Battle Type: Trainer
     * Dungeon Map: Yes
     * Player Level + 3 > Enemy Level */
    private static IEnumerator Shrink(Tilemap tilemap, Vector2Int center)
    {
        List<Vector3Int> tilePositions = new List<Vector3Int>();

        Vector2Int topLeft = new Vector2Int(-10, 8);
        Vector2Int bottomRight = new Vector2Int(9, -9);
        Vector2Int size = new Vector2Int(9, 8);

        // 4 panel split screen that is brought inward
        for (int i = 1; i < 51; i++)
        {
            if ((i - 2) % 6 == 0)
            {
                // Top left section
                Vector2Int corner = new Vector2Int(center.x - size.x - 1, center.y);
                BoundsInt captureBounds = new BoundsInt(corner.x, corner.y + 1, 0, size.x, size.y, 1);
                BoundsInt placeBounds = new BoundsInt(corner.x + 1, corner.y, 0, size.x, size.y, 1);
                TileBase[] tileBlock = tilemap.GetTilesBlock(captureBounds);
                tilemap.SetTilesBlock(placeBounds, tileBlock);

                // Bottom left section
                corner = new Vector2Int(center.x - size.x - 1, center.y - size.y - 1);
                captureBounds = new BoundsInt(corner.x, corner.y, 0, size.x, size.y, 1);
                placeBounds = new BoundsInt(corner.x + 1, corner.y + 1, 0, size.x, size.y, 1);
                tileBlock = tilemap.GetTilesBlock(captureBounds);
                tilemap.SetTilesBlock(placeBounds, tileBlock);

                // Top right section
                corner = new Vector2Int(center.x, center.y);
                captureBounds = new BoundsInt(corner.x + 1, corner.y + 1, 0, size.x, size.y, 1);
                placeBounds = new BoundsInt(corner.x, corner.y, 0, size.x, size.y, 1);
                tileBlock = tilemap.GetTilesBlock(captureBounds);
                tilemap.SetTilesBlock(placeBounds, tileBlock);

                // Bottom right section
                corner = new Vector2Int(center.x, center.y - size.y - 1);
                captureBounds = new BoundsInt(corner.x + 1, corner.y, 0, size.x, size.y, 1);
                placeBounds = new BoundsInt(corner.x, corner.y + 1, 0, size.x, size.y, 1);
                tileBlock = tilemap.GetTilesBlock(captureBounds);
                tilemap.SetTilesBlock(placeBounds, tileBlock);

                size.x--;
                size.y--;

                for (int x = topLeft.x; x <= bottomRight.x; x++)
                {
                    for (int y = bottomRight.y; y <= topLeft.y; y++)
                    {
                        if (x == topLeft.x || x == bottomRight.x || y == topLeft.y || y == bottomRight.y)
                            tilePositions.Add(new Vector3Int(center.x + x, center.y + y));
                    }
                }

                topLeft.x++;
                topLeft.y--;
                bottomRight.x--;
                bottomRight.y++;

                foreach (Vector3Int tp in tilePositions)
                    tilemap.SetTile(tp, BlackTile);
            }
            yield return new WaitForSeconds(1 / 60f);
        }

        yield return new WaitForSeconds(4 / 60f);
    }

    /* Battle Type: Trainer
     * Dungeon Map: Yes
     * Player Level + 3 <= Enemy Level */
    private static IEnumerator Split(Tilemap tilemap, Vector2Int center)
    {
        List<Vector3Int> tilePositions = new List<Vector3Int>();
        Vector2Int size = new Vector2Int(9, 8);
        int iterations = 0;

        // 4 panel split screen that is brought outward
        for (int i = 1; i < 53; i++)
        {
            Vector2Int corner;
            BoundsInt captureBounds;
            BoundsInt placeBounds;
            TileBase[] tileBlock;

            if ((i - 2) % 6 == 0)
            {
                // Bottom left block
                corner = new Vector2Int(center.x - 10, center.y - 9);
                captureBounds = new BoundsInt(corner.x + 1, corner.y + 1, 0, size.x, size.y, 1);
                placeBounds = new BoundsInt(corner.x, corner.y, 0, size.x, size.y, 1);
                tileBlock = tilemap.GetTilesBlock(captureBounds);
                tilemap.SetTilesBlock(placeBounds, tileBlock);

                // Bottom right block
                corner = new Vector2Int(center.x + 9 - size.x, center.y - 9);
                captureBounds = new BoundsInt(corner.x, corner.y, 0, size.x, size.y, 1);
                placeBounds = new BoundsInt(corner.x + 1, corner.y, 0, size.x, size.y, 1);
                tileBlock = tilemap.GetTilesBlock(captureBounds);
                tilemap.SetTilesBlock(placeBounds, tileBlock);

                // Black tiles
                for (int x = -10; x < 10; x++)
                    tilePositions.Add(new Vector3Int(center.x + x, center.y - iterations - 1));

                for (int y = -9; y < 0; y++)
                {
                    tilePositions.Add(new Vector3Int(center.x + iterations, center.y + y));
                    tilePositions.Add(new Vector3Int(center.x - iterations - 1, center.y + y));
                }

                foreach (Vector3Int tp in tilePositions)
                    tilemap.SetTile(tp, BlackTile);
            }
            else if ((i - 2) % 6 == 2)
            {
                // Top left block
                corner = new Vector2Int(center.x - 10, center.y + 8 - size.y);
                captureBounds = new BoundsInt(corner.x + 1, corner.y, 0, size.x, size.y, 1);
                placeBounds = new BoundsInt(corner.x, corner.y + 1, 0, size.x, size.y, 1);
                tileBlock = tilemap.GetTilesBlock(captureBounds);
                tilemap.SetTilesBlock(placeBounds, tileBlock);

                // Top right block
                corner = new Vector2Int(center.x + 9 - size.x, center.y + 8 - size.y);
                captureBounds = new BoundsInt(corner.x, corner.y, 0, size.x, size.y, 1);
                placeBounds = new BoundsInt(corner.x + 1, corner.y + 1, 0, size.x, size.y, 1);
                tileBlock = tilemap.GetTilesBlock(captureBounds);
                tilemap.SetTilesBlock(placeBounds, tileBlock);

                size.x--;
                size.y--;

                // Black tiles
                for (int x = -10; x < 10; x++)
                    tilePositions.Add(new Vector3Int(center.x + x, center.y + iterations));

                for (int y = 0; y < 9; y++)
                {
                    tilePositions.Add(new Vector3Int(center.x + iterations, center.y + y));
                    tilePositions.Add(new Vector3Int(center.x - iterations - 1, center.y + y));
                }

                iterations++;

                foreach (Vector3Int tp in tilePositions)
                    tilemap.SetTile(tp, BlackTile);

            }

            yield return new WaitForSeconds(1 / 60f);
        }
        
        yield return new WaitForSeconds(2 / 60f);
    }

    #endregion

    private static List<Vector3Int> GetTilesBelowLine(Tilemap tilemap, float slope, float xBound, float yBound, Vector2Int center)
    {
        List<Vector3Int> tiles = new List<Vector3Int>();
        int yIncrement = 1;
        if (yBound < 0)
            yIncrement *= -1;

        for (int i = 0; i < xBound; i++)
        {
            for (int j = 0; Mathf.Abs(j) < Mathf.Abs(yBound); j += yIncrement)
            {
                float xTileCenter = i + 0.5f;
                float yTileCenter = j + 0.5f;

                float threshold = xTileCenter * slope;
                if(yTileCenter <= threshold)
                {
                    Vector3Int tilePosition = new Vector3Int(center.x + i, center.y + j);
                    tiles.Add(tilePosition);
                }
            }
        }

        return tiles;
    }

    private static List<Vector3Int> GetTilesAboveLine(Tilemap tilemap, float slope, float xBound, float yBound, Vector2Int center)
    {
        List<Vector3Int> tiles = new List<Vector3Int>();
        bool posY = yBound > 0;
        int yIncrement = posY ? 1 : -1;

        for (int i = 0; i > xBound - 1; i--)
        {
            for (int j = posY ? 0 : -1; Mathf.Abs(j) < Mathf.Abs(yBound); j += yIncrement)
            {
                float xTileCenter = i + 0.5f;
                float yTileCenter = j + 0.5f;

                float threshold = xTileCenter * slope;
                if (yTileCenter >= threshold)
                {
                    Vector3Int tilePosition = new Vector3Int(center.x + i, center.y + j);
                    tiles.Add(tilePosition);
                }
            }
        }

        return tiles;
    }
}