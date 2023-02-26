using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMapManager
{
    public static List<GameMap> ActiveMaps;

    static GameMapManager()
    {
        ActiveMaps = new List<GameMap>();
    }

    public static void LoadMap(GameMap map)
    {
        // Unload all the currently active maps, except for the given one if it is active
        foreach (GameMap m in ActiveMaps)
        {
            if(m != map)
                m.UnloadMap();
        }

        // Load the new map
        if(!map.IsLoaded)
            map.LoadMap();

        // Clear the active map list and add the new loaded map
        ActiveMaps.Clear();
        ActiveMaps.Add(map);
    }

    public static void LoadMaps(List<GameMap> maps)
    {
        List<GameMap> MapsToUnload = new List<GameMap>();
        // Unload any active maps not in the new list
        foreach(GameMap m in ActiveMaps)
        {
            if (!maps.Contains(m))
                MapsToUnload.Add(m);
        }

        foreach(GameMap m in MapsToUnload)
        {
            ActiveMaps.Remove(m);
            m.UnloadMap();
        }

        // Load any new maps that aren't already loaded
        foreach(GameMap m in maps)
        {
            if(!m.IsLoaded)
            {
                m.LoadMap();
                ActiveMaps.Add(m);
            }
        }
    }
}
