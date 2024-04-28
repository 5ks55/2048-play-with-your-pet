using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct TileData
{
    public int x;
    public int y;
    public int tileNumber;
    public TileState tileState;
}


public class TileBoard : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private TileState[] tileStates;

    private TileGrid grid;
    private List<Tile> tiles;
    private bool waiting;
    [SerializeField] private Player player;

    public bool dataSaved = false;


    private void Awake()
    {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>(16); 
    }

    public void ClearBoard()
    {
        foreach (var cell in grid.cells)
        {
            cell.tile = null;
        }

        foreach (var tile in tiles)
        {
            Destroy(tile.gameObject);
        }

        tiles.Clear();
    }
    public void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }

    private void Update()
    {
        if (!waiting)
        {
            if (player.GetSwipeUp()) 
            {
                MoveTiles(Vector2Int.up, 0, 1, 1, 1);
            }
            else if (player.GetSwipeDown()) 
            {
                MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
            }
            else if (player.GetSwipeLeft()) 
            {
                MoveTiles(Vector2Int.left, 1, 1, 0, 1);
            }
            else if (player.GetSwipeRight()) 
            {
                MoveTiles(Vector2Int.right, grid.width - 2, -1, 0, 1);
            }
            player.SetSwipeReset();
        } 

        SaveData();
    }

    private void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        bool changed = false;

        for (int x = startX; x >= 0 && x < grid.width; x += incrementX) 
        {
            for (int y = startY; y >= 0 && y < grid.height; y += incrementY) 
            {
                TileCell cell = grid.GetCell(x, y);

                if (cell.occupied)
                {
                    changed |= MoveTile(cell.tile, direction);
                }
            }
        }

        if (changed)
        {
            StartCoroutine(WaitForChanges());
        }
    }

    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);

        while (adjacent != null)
        {
            if (adjacent.occupied)
            {
                if (CanMerge(tile, adjacent.tile))
                {
                    Merge(tile, adjacent.tile);
                    return true;
                }

                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null)
        {
            tile.MoveTo(newCell);
            return true;
        }

        return false;
    }

    private bool CanMerge(Tile a, Tile b)
    {
        return a.number == b.number && !b.locked;
    }
    private void Merge(Tile a, Tile b)
    {
        tiles.Remove(a);
        a.Merge(b.cell);

        int index = Mathf.Clamp(IndexOf(b.state) + 1, 0, tileStates.Length - 1);
        int number = b.number * 2;

        b.SetState(tileStates[index], number);
        gameManager.IncreaseScore(number);
    }

    private int IndexOf(TileState state)
    {
        for (int i = 0; i < tileStates.Length; i++) 
        {
            if (state == tileStates[i])
            {
                return i;
            }
        }
        return -1;
    }
    private IEnumerator WaitForChanges()
    {
        waiting = true;

        yield return new WaitForSeconds(0.1f);

        waiting = false;

        foreach (var tile in tiles)
        {
            tile.locked = false;
        }

        if(tiles.Count != grid.size)
        {
            CreateTile();
        }
        
        if (CheckForGameOver())
        {
            gameManager.GameOver();
        }
    }

    private bool CheckForGameOver()
    {
        if(tiles.Count != grid.size)
        {
            return false;
        }

        foreach (var tile in tiles)
        {
            TileCell up = grid.GetAdjacentCell(tile.cell, Vector2Int.up);
            TileCell down = grid.GetAdjacentCell(tile.cell, Vector2Int.down);
            TileCell left = grid.GetAdjacentCell(tile.cell, Vector2Int.left);
            TileCell right = grid.GetAdjacentCell(tile.cell, Vector2Int.right);

            if(up != null && CanMerge(tile, up.tile))
            {
                return false;
            } 
            
            if(down != null && CanMerge(tile, down.tile))
            {
                return false;
            }    
            
            if(left != null && CanMerge(tile, left.tile))
            {
                return false;
            }  
            
            if(right != null && CanMerge(tile, right.tile))
            {
                return false;
            }
        }

        return true;
    }

    public void SaveData()
    {
        List<TileData> tileDataList = new List<TileData>();

        foreach (Tile tile in tiles)
        {
            TileData data = new TileData
            {
                x = tile.cell.coordinates.x,
                y = tile.cell.coordinates.y,
                tileNumber = tile.number,
                tileState = tile.state
            };

            tileDataList.Add(data);
        }

        string json = JsonUtility.ToJson(new SaveData { tiles = tileDataList });
        PlayerPrefs.SetString("tileData", json);

        dataSaved = true;
        SaveDataIndicator();
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("tileData"))
        {
            string json = PlayerPrefs.GetString("tileData");
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            foreach (var data in saveData.tiles)
            {
                Tile tile = Instantiate(tilePrefab, grid.transform);
                tile.SetState(data.tileState, data.tileNumber);
                tile.Spawn(grid.GetCell(data.x, data.y));
                tiles.Add(tile);
            }
        }
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("tileData");
        dataSaved = false;
        SaveDataIndicator();
    }

    private void SaveDataIndicator()
    {
        PlayerPrefs.SetInt("dataSaved", dataSaved ? 1 : 0);
    }

    public void LoadDataIndicator()
    {
        int savedIndicator = PlayerPrefs.GetInt("dataSaved", 0);
        dataSaved = savedIndicator == 1;
    }
}

[System.Serializable]
public class SaveData
{
    public List<TileData> tiles;
}
