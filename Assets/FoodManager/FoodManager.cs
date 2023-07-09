using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace FoodManager
{
	public enum FoodType
	{
		Apple = 0,
		Banana = 1,
		Sugarcane = 2, //甘蔗
		Grape = 3,     //葡萄
		Cucumber = 4,   //黄瓜

		Steak = 5,     //肉排
		Drumstick = 6, //鸡腿
		Fish = 7,
		BeefHead = 8,   //牛头
		RoastedWholeLamb = 9, //烤全羊

		WhiteCarnation = 10,      //白康乃馨
		YellowChrysanthemum = 11, //黄菊花
		RedRose = 12 ,             //红玫瑰
		None = 13
	}
	public struct FOOD
	{
		public FoodType name;
		public List<int> shapes;
		public List<int> attr;  // 生命力，精神力，适应力
		public List<int> level;
	};

	public class CFood
	{
		Dictionary<int, FOOD> map;
		public void init()
		{

			// A #0, Fruit_A
			FOOD Fruit_A;

			Fruit_A.name = FoodType.Apple;

			Fruit_A.shapes = new List<int>();
			Fruit_A.shapes.Add(1);

			Fruit_A.attr = new List<int>();
			Fruit_A.attr.Add(1);
			Fruit_A.attr.Add(0);
			Fruit_A.attr.Add(0);

			Fruit_A.level = new List<int>();
			Fruit_A.level.Add(1);
			Fruit_A.level.Add(2);
			Fruit_A.level.Add(3);

			map.Add(0, Fruit_A);

			// =================================
			// B #1, Fruit_B
			FOOD Fruit_B;
			Fruit_B.name = FoodType.Banana;

			Fruit_B.shapes = new List<int>();
			Fruit_B.shapes.Add(4);
			Fruit_B.shapes.Add(5);
			Fruit_B.shapes.Add(7);

			Fruit_B.attr = new List<int>();
			Fruit_B.attr.Add(5);
			Fruit_B.attr.Add(-2);
			Fruit_B.attr.Add(0);

			Fruit_B.level = new List<int>();
			Fruit_B.level.Add(1);
			Fruit_B.level.Add(2);
			Fruit_B.level.Add(3);
			Fruit_B.level.Add(4);

			map.Add(1, Fruit_B);

			// =================================
			// C #2, Fruit_C
			FOOD Fruit_C;

			Fruit_C.name = FoodType.Sugarcane;

			Fruit_C.shapes = new List<int>();
			Fruit_C.shapes.Add(8);
			Fruit_C.shapes.Add(10);
			Fruit_C.shapes.Add(14);

			Fruit_C.attr = new List<int>();
			Fruit_C.attr.Add(6);
			Fruit_C.attr.Add(-1);
			Fruit_C.attr.Add(-1);

			Fruit_C.level = new List<int>();
			Fruit_C.level.Add(3);
			Fruit_C.level.Add(4);
			Fruit_C.level.Add(5);

			map.Add(2, Fruit_C);

			// =================================
			// D #3, Fruit_D
			FOOD Fruit_D;

			Fruit_D.name = FoodType.Grape;

			Fruit_D.shapes = new List<int>();
			Fruit_D.shapes.Add(9);
			Fruit_D.shapes.Add(11);
			Fruit_D.shapes.Add(13);
			Fruit_D.shapes.Add(15);
			Fruit_D.shapes.Add(16);

			Fruit_D.attr = new List<int>();
			Fruit_D.attr.Add(7);
			Fruit_D.attr.Add(0);
			Fruit_D.attr.Add(-2);

			Fruit_D.level = new List<int>();
			Fruit_D.level.Add(3);
			Fruit_D.level.Add(4);
			Fruit_D.level.Add(5);

			map.Add(3, Fruit_D);

			// =================================
			// E #4, Fruit_E
			FOOD Fruit_E;

			Fruit_E.name = FoodType.Cucumber;

			Fruit_E.shapes = new List<int>();
			Fruit_E.shapes.Add(6);
			Fruit_E.shapes.Add(7);
			Fruit_E.shapes.Add(14);
			Fruit_E.shapes.Add(16);

			Fruit_E.attr = new List<int>();
			Fruit_E.attr.Add(4);
			Fruit_E.attr.Add(0);
			Fruit_E.attr.Add(0);

			Fruit_E.level = new List<int>();
			Fruit_E.level.Add(4);
			Fruit_E.level.Add(5);

			map.Add(4, Fruit_E);

			// =================================
			// M_1 #5, Meat_M_1
			FOOD Meat_M_1;

			Meat_M_1.name = FoodType.Steak;

			Meat_M_1.shapes = new List<int>();
			Meat_M_1.shapes.Add(2);
			Meat_M_1.shapes.Add(4);
			Meat_M_1.shapes.Add(5);

			Meat_M_1.attr = new List<int>();
			Meat_M_1.attr.Add(0);
			Meat_M_1.attr.Add(2);
			Meat_M_1.attr.Add(0);

			Meat_M_1.level = new List<int>();
			Meat_M_1.level.Add(1);
			Meat_M_1.level.Add(2);
			Meat_M_1.level.Add(3);

			map.Add(5, Meat_M_1);

			// =================================
			// M_2 #6, Meat_M_2
			FOOD Meat_M_2;

			Meat_M_2.name = FoodType.Drumstick;

			Meat_M_2.shapes = new List<int>();
			Meat_M_2.shapes.Add(2);
			Meat_M_2.shapes.Add(3);
			Meat_M_2.shapes.Add(6);
			Meat_M_2.shapes.Add(8);

			Meat_M_2.attr = new List<int>();
			Meat_M_2.attr.Add(1);
			Meat_M_2.attr.Add(3);
			Meat_M_2.attr.Add(0);

			Meat_M_2.level = new List<int>();
			Meat_M_2.level.Add(2);
			Meat_M_2.level.Add(3);
			Meat_M_2.level.Add(4);

			map.Add(6, Meat_M_2);

			// =================================
			// M_3 #7, Meat_M_3
			FOOD Meat_M_3;

			Meat_M_3.name = FoodType.Fish;

			Meat_M_3.shapes = new List<int>();
			Meat_M_3.shapes.Add(2);
			Meat_M_3.shapes.Add(9);
			Meat_M_3.shapes.Add(10);
			Meat_M_3.shapes.Add(12);
			Meat_M_3.shapes.Add(14);
			Meat_M_3.shapes.Add(15);

			Meat_M_3.attr = new List<int>();
			Meat_M_3.attr.Add(0);
			Meat_M_3.attr.Add(4);
			Meat_M_3.attr.Add(-1);

			Meat_M_3.level = new List<int>();
			Meat_M_3.level.Add(1);
			Meat_M_3.level.Add(2);
			Meat_M_3.level.Add(3);
			Meat_M_3.level.Add(4);
			Meat_M_3.level.Add(5);

			map.Add(7, Meat_M_3);

			// =================================
			// M_4 #8, Meat_M_4
			FOOD Meat_M_4;

			Meat_M_4.name = FoodType.BeefHead;

			Meat_M_4.shapes = new List<int>();
			Meat_M_4.shapes.Add(7);
			Meat_M_4.shapes.Add(11);
			Meat_M_4.shapes.Add(12);
			Meat_M_4.shapes.Add(15);

			Meat_M_4.attr = new List<int>();
			Meat_M_4.attr.Add(-1);
			Meat_M_4.attr.Add(6);
			Meat_M_4.attr.Add(-2);

			Meat_M_4.level = new List<int>();
			Meat_M_4.level.Add(3);
			Meat_M_4.level.Add(4);
			Meat_M_4.level.Add(5);

			map.Add(8, Meat_M_4);

			// =================================
			// M_5 #9, Meat_M_5
			FOOD Meat_M_5;

			Meat_M_5.name = FoodType.RoastedWholeLamb;

			Meat_M_5.shapes = new List<int>();
			Meat_M_5.shapes.Add(12);
			Meat_M_5.shapes.Add(15);
			Meat_M_5.shapes.Add(16);

			Meat_M_5.attr = new List<int>();
			Meat_M_5.attr.Add(-1);
			Meat_M_5.attr.Add(5);
			Meat_M_5.attr.Add(0);

			Meat_M_5.level = new List<int>();
			Meat_M_5.level.Add(4);
			Meat_M_5.level.Add(5);

			map.Add(9, Meat_M_5);

			// =================================
			// F_1 #10, Flower_F_1
			FOOD Flower_F_1;

			Flower_F_1.name = FoodType.WhiteCarnation;

			Flower_F_1.shapes = new List<int>();
			Flower_F_1.shapes.Add(2);
			Flower_F_1.shapes.Add(8);
			Flower_F_1.shapes.Add(10);
			Flower_F_1.shapes.Add(16);

			Flower_F_1.attr = new List<int>();
			Flower_F_1.attr.Add(0);
			Flower_F_1.attr.Add(0);
			Flower_F_1.attr.Add(1);

			Flower_F_1.level = new List<int>();
			Flower_F_1.level.Add(1);
			Flower_F_1.level.Add(3);

			map.Add(10, Flower_F_1);

			// =================================
			// F_2 #11, Flower_F_2
			FOOD Flower_F_2;

			Flower_F_2.name = FoodType.YellowChrysanthemum;

			Flower_F_2.shapes = new List<int>();
			Flower_F_2.shapes.Add(1);
			Flower_F_2.shapes.Add(12);
			Flower_F_2.shapes.Add(15);

			Flower_F_2.attr = new List<int>();
			Flower_F_2.attr.Add(-1);
			Flower_F_2.attr.Add(0);
			Flower_F_2.attr.Add(2);

			Flower_F_2.level = new List<int>();
			Flower_F_2.level.Add(2);
			Flower_F_2.level.Add(4);

			map.Add(11, Flower_F_2);

			// =================================
			// F_3 #12, Flower_F_3
			FOOD Flower_F_3;

			Flower_F_3.name = FoodType.RedRose;

			Flower_F_3.shapes = new List<int>();
			Flower_F_3.shapes.Add(3);
			Flower_F_3.shapes.Add(6);
			Flower_F_3.shapes.Add(7);

			Flower_F_3.attr = new List<int>();
			Flower_F_3.attr.Add(0);
			Flower_F_3.attr.Add(-1);
			Flower_F_3.attr.Add(3);

			Flower_F_3.level = new List<int>();
			Flower_F_3.level.Add(1);
			Flower_F_3.level.Add(2);
			Flower_F_3.level.Add(3);
			Flower_F_3.level.Add(4);

			map.Add(12, Flower_F_3);
		}

		public Dictionary<int, FOOD> getFoodList()
        {
			return map;
        }

		//获取形状的AABB包围盒
		public Vector2Int getShapeAABB(int vShape)
		{
			if (vShape == 1)
			{
				return new Vector2Int(1, 1);

			}
			else if (vShape == 2)
			{
				return new Vector2Int(2, 1);
			}
			else if (vShape == 3)
			{
				return new Vector2Int(2, 2);
			}
			else if (vShape == 4)
			{
				return new Vector2Int(2, 2);
			}
			else if (vShape == 5)
			{
				return new Vector2Int(2, 2);
			}
			else if (vShape == 6)
			{
				return new Vector2Int(3, 2);
			}
			else if (vShape == 7)
			{
				return new Vector2Int(2, 3);
			}
			else if (vShape == 8)
			{
				return new Vector2Int(1, 3);
			}
			else if (vShape == 9)
			{
				return new Vector2Int(2, 3);
			}
			else if (vShape == 10)
			{
				return new Vector2Int(1, 4);
			}
			else if (vShape == 11)
			{
				return new Vector2Int(2, 3);
			}
			else if (vShape == 12)
			{
				return new Vector2Int(2, 2);
			}
			else if (vShape == 13)
			{
				return new Vector2Int(2, 3);
			}
			else if (vShape == 14)
			{
				return new Vector2Int(3, 2);
			}
			else if (vShape == 15)
			{
				return new Vector2Int(4, 2);
			}
			else if (vShape == 16)
			{
				return new Vector2Int(3, 3);
			}
			else
				return new Vector2Int(0, 0);
		}
		public int[] GenerateUniqueRandom(int minValue, int maxValue, int n)
		{
			if (n > maxValue - minValue + 1)
				n = maxValue - minValue + 1;

			int maxIndex = maxValue - minValue + 2;// 索引数组上限
			int[] indexArr = new int[maxIndex];
			for (int i = 0; i < maxIndex; i++)
			{
				indexArr[i] = minValue - 1;
				minValue++;
			}

			Random ran = new Random();
			int[] randNum = new int[n];
			int index;
			for (int j = 0; j < n; j++)
			{
				index = ran.Next(1, maxIndex - 1);// 生成一个随机数作为索引
				randNum[j] = indexArr[index];
				indexArr[index] = indexArr[maxIndex - 1];
				maxIndex--; //索引上限减 1
			}
			return randNum;
		}

		//全局随机生产食物
		public Vector2Int[] ProduceFood(uint level)
		{
			int numOfFood = 0;
			//根据关卡数生产食物
			if(level == 1)
            {
				numOfFood = 3;
			}
			else if (level == 2)
			{
				numOfFood = 4;
			}
			else if (level == 3)
			{
				numOfFood = 5;
			}
			else if (level == 4)
			{
				numOfFood = 6;
			}
			else if (level == 5)
			{
				numOfFood = 7;
			}
            else
            {
				numOfFood = 0;
			}

			int[] foods;
			bool qualifiedFlag = true;
			//循环
			do
			{
				foods = GenerateUniqueRandom(0, 12, numOfFood);
				
				foreach (int food in foods)
				{
					FOOD Food = map[food];
					if (!Food.level.Contains(numOfFood))
					{
						qualifiedFlag = false;
						break;
					}

					qualifiedFlag = true;
				}

			} while (!qualifiedFlag);

			//随机确定食物的具体形状
			foreach (int food in foods)
			{
				FOOD Food = map[food];
				int shapeCount = Food.shapes.Count;
				int[] foodShapeIndex = GenerateUniqueRandom(0, shapeCount - 1, 1);
				int foodShape = Food.shapes[foodShapeIndex[0]];

			}
		}



		//public uint[,] GetFoodShape(FoodType foodType)
		//{
		//	FOOD Food = map[(int)foodType];
		//	if (!Food.shapes)
		//	{
		//	}
		//}
	}

	class execute
	{
		static void Main(string[] args)
		{
			CFood foodList = new CFood();
			foodList.init();
			Dictionary<int, FOOD> mapList = foodList.getFoodList();
		}
	}

}
