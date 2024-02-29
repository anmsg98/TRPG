using System; /* Array.Clear */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SearchPlane : MonoBehaviour
{
    public GameObject grid;
    
    private const int MAX = 45;
    private const int dis = 2;

    public Vector2Int currentLoc;
    private int[,] map = new int [MAX, MAX];
    private int[,] move;
    private int[,] visit;
    
    private int[] dr = new int[4]{ 0, -1, 0, 1 };
    private int[] dc = new int[4]{ -1, 0, 1, 0 };
    int rp, wp;
    
    void Start()
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
        
        FindRoute();
    }

    void FindRoute()
    {
        move = new int[dis * 2 + 1, dis * 2 + 1];
        visit = new int[dis * 2 + 1, dis * 2 + 1];
        
        Vector2Int[] queue = new Vector2Int[MAX * MAX];
        Vector2Int locToindex = new Vector2Int(currentLoc.x + (MAX - 1) / 2, currentLoc.y + (MAX - 1) / 2);
        
        print("locToindex : " + locToindex);
        for (int i = 0; i < dis * 2 + 1; i++)
        {
            for (int j = 0; j < dis * 2 + 1; j++)
            {
                if (locToindex.x - dis + i >= 0 && locToindex.x - dis + i < MAX && 
                    locToindex.y - dis + j >= 0 && locToindex.y - dis + j < MAX)
                {
                    move[i, j] = map[locToindex.x - dis + i, locToindex.y - dis + j];
                }
            }
        }

        BFS(dis, dis, queue);

        Vector2 pos = new Vector2((10f / 45f * (float)currentLoc.y), (10f / 45f * (float)currentLoc.x));
        for (int i = 0; i < dis * 2 + 1; i++)
        {
            for (int j = 0; j < dis * 2 + 1; j++)
            {
                if (move[i, j] > 0 && move[i, j] <= dis + 1)
                {
                    float r = pos.x + (float)(j - dis) * (10f / 45f);
                    float c = pos.y + (float)(dis - i) * (10f / 45f);
                    GameObject obj = Instantiate(grid, new Vector3(r, -0.1f, c), Quaternion.identity);
                }
            }
        }
    }

    void BFS(int r, int c, Vector2Int[] queue)
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
                int nr = Math.Clamp(outo.x + dr[i], 0, 4);
                int nc = Math.Clamp(outo.y + dc[i], 0, 4);

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
}