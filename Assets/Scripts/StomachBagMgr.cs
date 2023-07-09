using FoodManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class StomachBagMgr : MonoBehaviour
{
    
    public static StomachBagMgr inst = null;

    public static StomachBagMgr Inst
    {
        get
        {
            if (inst == null)
            {
                inst = new StomachBagMgr();
            }

            return inst;
        }
        
    }
    
    
    //背包格子的宽高
    public int bagRow = 6;
    public int bagColumn = 7;
    public int cellSize = 100;
    public int foodSize = 4;
    
    //public Vector3 bagLeftBottomPos = new Vector3();

    public GameObject stomachBagGrid;
    
    public List<List<StomachGridInfo>> stomachGrids = new List<List<StomachGridInfo>>();

    //格子类，记录格子是否激活，格子装入的食物，与该格子关联的其它格子
    public class StomachGridInfo
    {
        public bool isActive = false;
        public FoodType foodType = FoodType.None;
        public List<Vector2> relatedGridXY = new List<Vector2>();
    }
    
    public void InitStomachGrids()
    {
        for (int i = 0; i < bagRow; i++)
        {
            List<StomachGridInfo> rowInfos = new List<StomachGridInfo>();
            for (int j = 0; j < bagColumn; j++)
            {
                StomachGridInfo gridInfo = new StomachGridInfo();
                rowInfos.Add(gridInfo);
            }
            stomachGrids.Add(rowInfos);
        }
    }

    public void SetStomachBagSize(int row, int column)
    {
        // for (int i = 0; i < row; i++)
        // {
        //     for (int j = 0; j < column; j++)
        //     {
        //         stomachGrids[i][j].isActive = true;
        //     }
        // }
        
        for (int i = row; i >= 0; i--)
        {
            for (int j = 0; j < column; j++)
            {
                stomachGrids[i][j].isActive = true;
            }
        }
    }


    //判断是否能放进此处的格子，食物拖拽调用，传入食物左下角的trans，以及食物的形状
    public bool CheckPutFood(Vector3 position, uint[,] foodShape, FoodType foodType)
    {
        Vector2 originGridXY = TransToGridXY(position);
        if (originGridXY.x < 0 || originGridXY.x >= bagColumn || originGridXY.y < 0 || originGridXY.y >= bagRow)
        {
            return false;
        } 
        

        for (int i = 0; i < foodSize; i++)
        {
            for (int j = 0; j < foodSize; j++)
            {
                //判断越界
                if (originGridXY.x + j < 0 || originGridXY.x + j >= bagColumn || originGridXY.y + i < 0 || originGridXY.y + i >= bagRow)
                {
                    return false;
                }
                if (IsGridOccupied(new Vector2(originGridXY.x + j, originGridXY.y + i)) && foodShape[i, j] == 1) return false;
            }
        }
        
        return true;
    }

    public void PutFood(Vector3 position, uint[,] foodShape, FoodType foodType)
    {
        if (CheckPutFood(position, foodShape, foodType))
        {
            Vector2 originGridXY = TransToGridXY(position);
            List<Vector2> relatedGridXY = new List<Vector2>();
            
            

            for (int i = 0; i < foodSize; i++)
            {
                for (int j = 0; j < foodSize; j++)
                {
                    if (foodShape[i, j] == 1)
                    {
                        stomachGrids[(int) originGridXY.x + j][(int) originGridXY.y + i].foodType = foodType;
                        
                        relatedGridXY.Add(new Vector2(originGridXY.x + j, originGridXY.y + i));
                    }
                }
            }
            
            for (int i = 0; i < foodSize; i++)
            {
                for (int j = 0; j < foodSize; j++)
                {
                    if (foodShape[i, j] == 1)
                    {
                        stomachGrids[(int)originGridXY.x + j][(int)originGridXY.y + i].relatedGridXY = relatedGridXY;
                    }
                }
            }
        }
    }

    public void ConsumeOneColumn()
    {
        for (int i = 0; i < bagRow; i++)
        {
            stomachGrids[i][0].foodType = FoodType.None;
            
            //todo：只有自己,数值面板
            if (stomachGrids[i][0].relatedGridXY.Count == 1)
            {
                
            }
            else
            {
                foreach (var relatedGridXY in stomachGrids[i][0].relatedGridXY)
                {
                    StomachGridInfo relatedGridInfo = GetGridInfo(relatedGridXY);
                    
                    var whereRemove = relatedGridInfo.relatedGridXY.FirstOrDefault(t => (int)t.x == 0 && (int)t.y == i);
                    relatedGridInfo.relatedGridXY.Remove(whereRemove);
                }
            }
            stomachGrids[i][0].relatedGridXY.Clear();
            
        }
    }

    public void moveOneColumn()
    {
        for (int i = 0; i < bagRow; i++)
        {
            for (int j = 0; j < bagColumn - 1; j++)
            {
                stomachGrids[i][j] = stomachGrids[i][j + 1];
                
                for (int k = 0; k < stomachGrids[i][j].relatedGridXY.Count; k++)
                {
                    stomachGrids[i][j].relatedGridXY[k] = new Vector2(stomachGrids[i][j].relatedGridXY[k].x - 1, stomachGrids[i][j].relatedGridXY[k].y);
                }
            }
        }
        
    }
    
    public StomachGridInfo GetGridInfo(Vector2 gridXY)
    {
        return stomachGrids[(int) gridXY.y][(int) gridXY.x];
    }
    
    public Vector2 TransToGridXY(Vector3 position)
    {
        Vector2 gridXY = new Vector2();
        Vector3 bagLeftBottomPos = stomachBagGrid.transform.position;
        gridXY.x = (int) ((position.x - bagLeftBottomPos.x) / cellSize);
        gridXY.y = (int) ((position.y - bagLeftBottomPos.y) / cellSize);
        return gridXY;
    }
    
    public Vector2 LocalTransToGridXY(Vector3 position)
    {
        Vector2 gridXY = new Vector2();
        Vector3 bagLeftBottomPos = stomachBagGrid.transform.localPosition;
        gridXY.x = (int) ((position.x - bagLeftBottomPos.x) / cellSize);
        gridXY.y = (int) ((position.y - bagLeftBottomPos.y) / cellSize);
        return gridXY;
    }

    //该格子是否被占用
    public bool IsGridOccupied(Vector2 gridXY)
    {
        if (stomachGrids[(int) gridXY.y][(int) gridXY.x].foodType != FoodType.None && stomachGrids[(int) gridXY.y][(int) gridXY.x].isActive)
        {
            return true;
        }
        return false;
    }
    

    public void PrintAll()
    {
        for (int i = 0; i < bagRow; i++)
        {
            for (int j = 0; j < bagColumn; j++)
            {
                print("x:" + j + " y:" + i + " foodType:" + stomachGrids[i][j].isActive);

            }
        }
    }

    void Awake()
    {
        //InitStomachGrids();
        //SetStomachBagSize(5,5);
    }

    void Update()
    {
        
    }
}
