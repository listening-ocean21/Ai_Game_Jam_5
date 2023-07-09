using AnttiStarterKit.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Manager<InventoryManager>
{
    float width = 720;
    float height = 800;
    float padding = 10;

    /// <summary>
    /// 由外界传入, 具体有哪些食物需要随机选择
    /// </summary>
    public FoodType[] foodArray;

    /// <summary>
    /// 背包中的食物
    /// </summary>
    public List<Food> foodInInventory;

    // For Debug
    public GameObject debugPrefab;


    const int blockNumX = 3;
    const int blockNumY = 3;

    void Start()
    {
        RectTransform inventoryRectTransform = gameObject.GetComponent<RectTransform>();
        width = inventoryRectTransform.rect.width;
        height = inventoryRectTransform.rect.height;
    }

    void Update()
    {
        // 测试用代码
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("GenerateFoods");
            GenerateFoods(7);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("DropAllFoods");
            DropAllFoods();
        }


    }

    /// <summary>
    /// 背包中生成食物
    /// </summary>
    /// <param name="generateFoodNum"></param>
    public void GenerateFoods(int generateFoodNum)
    {

        // 先随机生成不同类型的食物
        for (int i = 0; i < generateFoodNum; i++)
        {
            // 从foodArray中随机获得要生成的食物的编号
            int foodID = Random.Range(0, foodArray.Length - 1);

            // FoodType foodType = foodArray[foodID]; // TODO: 根据编号确定食物

            // TODO: 实例化对应的食物
            Food food = Instantiate(debugPrefab, gameObject.transform).GetComponent<Food>();

            // 将物体添加入当前包裹的食物列表
            foodInInventory.Add(food);
        }
        // 重新排布所有的食物
        List<Food> foodNotPacked = foodInInventory;
        foodNotPacked = ReArrangeAllFoods(foodNotPacked, false);
        ReArrangeAllFoods(foodNotPacked, true);
    }

    public List<Food> ReArrangeAllFoods(List<Food> Foods , bool isFlip)
    {
        Foods.Sort((leftFood, rightFood) =>
        {
            return (rightFood.width * rightFood.height).CompareTo(leftFood.width * leftFood.height);
        }
        );
        RectanglePacker packer = new RectanglePacker(new Vector2(width, height));
        List<Vector2> rectangles = new List<Vector2>();
        foreach(var food in Foods)
        {
            rectangles.Add(new Vector2(food.width+padding, food.height+padding));
        }


        List<Food> foodNotPacked = new List<Food>();
        for (int i = 0; i < Foods.Count; i++)
        {
            Food food = Foods[i];

            var rectangle = rectangles[i];
            Vector2? position = packer.Pack(rectangle);

            Vector2 foodPosition;
            if (position != null)
            {
                Vector2 pos = (Vector2)position;
                foodPosition = isFlip ? new Vector2(width - pos.x - food.width, height - pos.y - food.height) : pos;
            }
            else
            {
                foodNotPacked.Add(Foods[i]);
                continue;
            }

            // 设置食物的位置
            RectTransform foodTransform = food.gameObject.GetComponent<RectTransform>();
            Vector2 rectPadding = new Vector2(padding, padding);
            rectPadding = isFlip ? -rectPadding : rectPadding;
            foodTransform.anchoredPosition = foodPosition + rectPadding;
        }
        return foodNotPacked;
    }

    public void ReArrangeAllFoodsUniform()
    {
        foodInInventory.Sort((leftFood, rightFood) =>
        {
            return (leftFood.width * leftFood.height).CompareTo(rightFood.width * rightFood.height);
        });
        float blockWidth = width / blockNumX;
        float blockHeight = height / blockNumY;
        Vector2 blockSize = new Vector2(blockWidth, blockHeight);
        for (int i = 0; i < foodInInventory.Count; i++)
        {
            Vector2Int pos = IndexTo2D(i, blockNumX);
            Vector2 tempPos = new Vector2(pos.x * blockWidth + padding, pos.y * blockHeight + padding);

            // 设置食物的位置
            Food food = foodInInventory[i];
            RectTransform foodTransform = food.gameObject.GetComponent<RectTransform>();
            foodTransform.anchoredPosition = tempPos;
        }

    }

    Vector2Int IndexTo2D(int index, int width)
    {
        Vector2Int pos = new Vector2Int(0, 0);
        pos.y = index / width;
        pos.x = index % width;
        return pos;
    }

    /// <summary>
    /// 清空所有的食物
    /// </summary>
    public void DropAllFoods()
    {
        for (int i = 0; i < foodInInventory.Count; i++)
        {
            Destroy(foodInInventory[i].gameObject);
        }
        foodInInventory.Clear();
    }

    /// <summary>
    /// 食物离开
    /// </summary>
    /// <param name="food"></param>
    public void FoodLeave(Food food)
    {
        if (food != null && foodInInventory.Contains(food))
        {
            foodInInventory.Remove(food);
        }
    }
}

/// <summary>
/// 装箱算法，目的是尽可能塞东西
/// </summary>
public class RectanglePacker
{
    private struct FreeArea
    {
        public Vector2 Position;
        public Vector2 Size;
    }

    private List<FreeArea> freeAreas;
    private Vector2 backpackSize;

    public RectanglePacker(Vector2 backpackSize)
    {
        this.backpackSize = backpackSize;
        freeAreas = new List<FreeArea> { new FreeArea { Position = Vector2.zero, Size = backpackSize } };
    }

    public Vector2? Pack(Vector2 rectangleSize)
    {
        float minLeftArea = float.MaxValue;
        int id = -1;
        for (int i = 0; i < freeAreas.Count; i++)
        {

            if (freeAreas[i].Size.x >= rectangleSize.x && freeAreas[i].Size.y >= rectangleSize.y)
            {
                float leftArea = (freeAreas[i].Size.x - rectangleSize.x) * (freeAreas[i].Size.y - rectangleSize.y);
                if(leftArea < minLeftArea)
                {
                    id = i;
                    minLeftArea = leftArea;
                }
            }
        }

        if(id == -1)
        {
            return null;
        }
        else
        {
            Vector2 position = freeAreas[id].Position;
            SplitFreeArea(id, rectangleSize);
            return position;
        }
    }

    private void SplitFreeArea(int index, Vector2 rectangleSize)
    {
        FreeArea area = freeAreas[index];

        freeAreas.RemoveAt(index);

        // Split the remaining free area into up to two smaller free areas (right and below)
        if (area.Size.x > rectangleSize.x)
        {
            freeAreas.Add(new FreeArea
            {
                Position = new Vector2(area.Position.x + rectangleSize.x, area.Position.y),
                Size = new Vector2(area.Size.x - rectangleSize.x, rectangleSize.y)
            });
        }

        if (area.Size.y > rectangleSize.y)
        {
            freeAreas.Add(new FreeArea
            {
                Position = new Vector2(area.Position.x, area.Position.y + rectangleSize.y),
                Size = new Vector2(area.Size.x, area.Size.y - rectangleSize.y)
            });
        }
    }
}


