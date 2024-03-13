using System; /* Array.Clear */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Numerics;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class FindRoute : MonoBehaviour
{
    public static FindRoute instance { get; set; }

    public GameObject enemy;
    public GameObject[] grid;
    public Transform gridParent;
    public Transform enemyParent;
    
    private const int MAX = 45;

    public int[,] map = new int [MAX, MAX];
    private int[,] move;
    private int[,] visit;
    
    private int[] dr = new int[4]{ 0, -1, 0, 1 };
    private int[] dc = new int[4]{ -1, 0, 1, 0 };
    int rp, wp;
    
    public float followtime;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        LoadMap();
    }

    void Update()
    {
    }

    void LoadMap()
    {
        int cnt = 0;
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
                if (map[i, j] == 0)
                {
                    GameObject obj = Instantiate(enemy,
                        new Vector3((j - (length - 1) / 2) * 0.2222f, 0f, ((length - 1) / 2 - i) * 0.2222f),
                        Quaternion.identity);
                    obj.transform.GetComponent<Enemy>().currentPos = new Vector2Int(((length - 1) / 2 - i),(j - (length - 1) / 2));
                    obj.transform.GetComponent<Enemy>().idx = cnt;
                    obj.name = "Enemy";
                    obj.transform.parent = enemyParent;
                    cnt++;
                }
            }
            line = sr.ReadLine();
        }
        sr.Close();
        
    }

    public void FindDis(Vector2Int currentPos, int moveDistance, string Entity, Transform transform)
    {
        DestroyGrid();
        move = new int[moveDistance * 2 + 1, moveDistance * 2 + 1];
        visit = new int[moveDistance * 2 + 1, moveDistance * 2 + 1];
        
        Vector2Int[] queue = new Vector2Int[MAX * MAX];
        Vector2Int locToindex = new Vector2Int((MAX - 1) / 2 - currentPos.x, currentPos.y + (MAX - 1) / 2);
        
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

        move[moveDistance, moveDistance] = 1;
        BFS(moveDistance, moveDistance, queue, moveDistance);

        move[moveDistance, moveDistance] = 2;
        Vector2 pos = new Vector2((10f / 45f * (float)currentPos.y), (10f / 45f * (float)currentPos.x));
        
        
        
        if (Entity == "Enemy")
        {
            transform.GetComponent<Enemy>().newPos =  transform.GetComponent<Enemy>().currentPos;
            transform.GetComponent<Enemy>().min = 99f;
        }

        for (int i = 0; i < moveDistance * 2 + 1; i++)
        {
            for (int j = 0; j < moveDistance * 2 + 1; j++)
            {
                if (move[i, j] > 1 && move[i, j] <= moveDistance + 1)
                {
                    float r = pos.x + (float)(j - moveDistance) * (10f / 45f);
                    float c = pos.y + (float)(moveDistance - i) * (10f / 45f);
                    if (Entity == "Player")
                    {
                        GameObject obj = Instantiate(grid[0], new Vector3(r, -0.1f, c), Quaternion.identity);
                        obj.GetComponent<Grid>().currentLoc =
                            new Vector2Int(currentPos.x + moveDistance - i, currentPos.y + j - moveDistance);
                        obj.GetComponent<Grid>().distance = move[i, j];
                        obj.transform.parent = gridParent;
                        obj.name = "Grid";
                    }
                    else
                    {
                        GameObject obj = Instantiate(grid[1], new Vector3(r, -0.1f, c), Quaternion.identity);
                        obj.GetComponent<Grid>().currentLoc =
                            new Vector2Int(currentPos.x + moveDistance - i, currentPos.y + j - moveDistance);
                        obj.GetComponent<Grid>().distance = move[i, j];
                        obj.transform.parent = gridParent;
                        obj.name = "EnemyGrid";
                        transform.GetComponent<Enemy>().newPos = new Vector2Int(currentPos.x + moveDistance - i,
                            currentPos.y + j - moveDistance);
                        float dis = Vector2Int.Distance(transform.GetComponent<Enemy>().newPos,
                            new Vector2Int(0, 0));
                        if (transform.GetComponent<Enemy>().min > dis)
                        {
                            transform.GetComponent<Enemy>().currentPos = transform.GetComponent<Enemy>().newPos;
                            transform.GetComponent<Enemy>().min = dis;
                        }
                    }
                }
            }
        }

        if (Entity == "Enemy")
        {
            transform.GetComponent<Enemy>().m_targetPos = new Vector3(transform.GetComponent<Enemy>().currentPos.y * 0.2222f, 0f, transform.GetComponent<Enemy>().currentPos.x * 0.2222f);
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
        foreach (Transform child in gridParent)
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