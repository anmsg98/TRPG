using System; /* Array.Clear */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class FindRoute : MonoBehaviour
{
    public static FindRoute instance { get; set; }
    
    public GameObject grid;
    public Transform parent;
    
    private const int MAX = 45;

    private int[,] map = new int [MAX, MAX];
    private int[,] move;
    private int[,] visit;
    
    private int[] dr = new int[4]{ 0, -1, 0, 1 };
    private int[] dc = new int[4]{ -1, 0, 1, 0 };
    int rp, wp;
    
    public float followtime;
    void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        LoadMap();
    }

    void Update()
    {
    }

    void LoadMap()
    {
        TextAsset text = Resources.Load<TextAsset>("MapInfo/Map");
        StringReader sr = new StringReader(text.text);
        string line;
        line = sr.ReadLine();
        int length = line.Length;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                map[i, j] = Convert.ToInt32(line[j].ToString());
            }
            line = sr.ReadLine();
        }
        
        sr.Close();
    }
    
    public void FindDis(Vector2Int currentPos, int moveDistance)
    {
        DestroyGrid();
        move = new int[moveDistance * 2 + 1, moveDistance * 2 + 1];
        visit = new int[moveDistance * 2 + 1, moveDistance * 2 + 1];
        
        Vector2Int[] queue = new Vector2Int[MAX * MAX];
        Vector2Int locToindex = new Vector2Int(currentPos.x + (MAX - 1) / 2, currentPos.y + (MAX - 1) / 2);
        
        for (int i = 0; i < moveDistance * 2 + 1; i++)
        {
            for (int j = 0; j < moveDistance * 2 + 1; j++)
            {
                if (locToindex.x - moveDistance + i >= 0 && locToindex.x - moveDistance + i < MAX && 
                    locToindex.y - moveDistance + j >= 0 && locToindex.y - moveDistance + j < MAX)
                {
                    move[i, j] = map[locToindex.x - moveDistance + i, locToindex.y - moveDistance + j];
                }
            }
        }

        BFS(moveDistance, moveDistance, queue, moveDistance);

        Vector2 pos = new Vector2((10f / 45f * (float)currentPos.y), (10f / 45f * (float)currentPos.x));
        for (int i = 0; i < moveDistance * 2 + 1; i++)
        {
            for (int j = 0; j < moveDistance * 2 + 1; j++)
            {
                if (move[i, j] > 0 && move[i, j] <= moveDistance + 1)
                {
                    float r = pos.x + (float)(j - moveDistance) * (10f / 45f);
                    float c = pos.y + (float)(moveDistance - i) * (10f / 45f);
                    GameObject obj = Instantiate(grid, new Vector3(r, -0.1f, c), Quaternion.identity);
                    obj.GetComponent<Grid>().currentLoc =
                        new Vector2Int(currentPos.x + moveDistance - i, currentPos.y + j - moveDistance);
                    obj.transform.parent = parent;
                    obj.name = "Grid";
                }
            }
        }
    }

    void BFS(int r, int c, Vector2Int[] queue, int moveDistance)
    {
        wp = rp = 0;

        queue[wp].x = r;
        queue[wp++].y = c;

        visit[r, c] = 1;

        while (wp > rp)
        {
            Vector2Int outo = queue[rp++];

            for (int i = 0; i < 4; i++)
            {
                int nr = Math.Clamp(outo.x + dr[i], 0, moveDistance * 2);
                int nc = Math.Clamp(outo.y + dc[i], 0, moveDistance * 2);

                if (move[nr, nc] != 0 && visit[nr,nc] == 0)
                {
                    queue[wp].x = nr;
                    queue[wp++].y = nc;

                    visit[nr, nc] = 1;

                    move[nr, nc] = move[outo.x, outo.y] + 1;
                }
            }
        }
    }
    
    private Vector3 velocity = Vector3.zero;
    

    void DestroyGrid()
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject) ;
        }
    }
    
    public IEnumerator FollowCamera()
    {
        global::FollowCamera.instance.moveOn = true;
        yield return new WaitForSeconds(followtime);
        global::FollowCamera.instance.moveOn = false;
    }
    
}