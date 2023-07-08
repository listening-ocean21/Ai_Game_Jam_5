using AnttiStarterKit.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum FoodType
{

}

public class Food : MonoBehaviour
{

}

public class InventoryManager : Manager<InventoryManager>
{
    
    public int width;
    public int height;
    public FoodType[] foodArray;
    public List<Food> foodInInventory;

    void Start()
    {
        
    }

    void Update()
    {
        // �����ô���
        //if(Input.GetKeyDown(KeyCode.G))
        //{
        //    Debug.Log("GenerateFoods");
        //    GenerateFoods(5);
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Debug.Log("DropAllFoods");
        //    DropAllFoods();
        //}

    }

    public void GenerateFoods(int generateFoodNum)
    {

        // ��������ɲ�ͬ���͵�ʳ��
        for(int i = 0; i < generateFoodNum; i++)
        {
            // ��foodArray��������Ҫ���ɵ�ʳ��ı��
            int foodID = Random.Range(0, foodArray.Length - 1);
            FoodType foodType = foodArray[foodID]; // ���ݱ��ȷ��ʳ��

            // TODO: �Ӷ�����л�ȡ��Ӧ��ʳ��
            Food food = new Food();

            // ����������뵱ǰ������ʳ���б�
            foodInInventory.Add(food);
        }
        // �����Ų����е�ʳ��
        ReArrangeAllFoods();
    }

    public void ReArrangeAllFoods()
    {
        for(int i = 0; i < foodInInventory.Count; i++)
        {
            Food food = foodInInventory[i];
            // �ڷ�Χ���������ʳ���λ��
            // TODO: �п��ܻᳬ��������Χ, ��Ҫ�����Ų�
            Vector2 foodPosition = new Vector2(Random.Range(0.0f, (float)width), Random.Range(0.0f, (float)height)); // TODO: �п��ܻᳬ���߽�

            // ����ʳ���λ��
            food.transform.position = foodPosition;

            // ��ʳ��ĸ���������Ϊ����
            food.transform.parent = Instance.transform;
        }
    }

    public void DropAllFoods()
    {
        for (int i = 0; i < foodInInventory.Count; i++)
        {
            // ɾ��ʳ��
            Destroy(foodInInventory[i].gameObject);
        }
        foodInInventory.Clear();
    }

    public void FoodLeave(Food food)
    {
        if(food != null && foodInInventory.Contains(food))
        {
            foodInInventory.Remove(food);
        }
    }
}
