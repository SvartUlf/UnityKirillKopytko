using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.Progress;

public class TestScript : MonoBehaviour
{
    private int[] _userScore = new int[5] { 10, 20, 30, 40, 50 };
    private int[] _enemyHealth = new int[5] { 40, 20, 60, 100, 80 };
    private List<int> _itemCost = new List<int> { 10,20,30,10,80,90};
    private int[] _enemiesInWave = new int[5] { 2,3,4,5,6 };
    private int[] _waveReward = new int[5] { 20,30,40,50,60 };
    private int[] _unsorted = new int[5] { 1, 7, 2, 4, 3 };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Task1();
        Task2();
        Task3();
        Task4();
        Task5();
    } 

    private void Task1()
    {
        var totalScore = 0;
        foreach (int currentScore in _userScore)
        {
            totalScore += currentScore;
        }
        var middleScore = totalScore / _userScore.Length;
        Debug.Log("Твой счет:" + middleScore);
        if (middleScore >= 50)
        {
            Debug.Log("Ты победил");
        }
        else
        {
            Debug.Log("Ты проиграл");
        }
    }

    private void Task2()
    {
        var maxEnemyHealth = 0;
        foreach (int currentEnemyHealth in _enemyHealth)
        {
            if (currentEnemyHealth > maxEnemyHealth)
            {
                maxEnemyHealth = currentEnemyHealth;
            }
        }
        if (maxEnemyHealth < 50)
        {
            Debug.Log("Обычный враг со здоровьем:" + maxEnemyHealth);
        }

        else if (maxEnemyHealth < 100)
        {
            Debug.Log("Элитный враг со здоровьем:" + maxEnemyHealth);
        }
        else if (maxEnemyHealth >= 100)
        {
            Debug.Log("Босс со здоровьем:" + maxEnemyHealth);
        }
    }

    private void Task3()
    {
        int rareItemsCount = 0;
        int totalInventoryCost = 0;
        foreach (int itemCost in _itemCost)
        {
            totalInventoryCost += itemCost;
            if (itemCost >= 70)
            {
                rareItemsCount++;
            }
        }
        Debug.Log("Стоимость инвентаря:" + totalInventoryCost);
        if (rareItemsCount >= 2)
        {
            Debug.Log("Богатый Игрок!!! число редких предметов:" + rareItemsCount);
        }
        else
        {
            Debug.Log("Бедный Игрок((( число редких предметов:" + rareItemsCount);
        }
    }

    private void Task4()
    {
        var totalEnemies = 0;
        var totalReward = 0;
        for (int i = 0; i < _enemiesInWave.Length; i++)
        {
            if (_enemiesInWave[i] < 5)
            {
                Debug.Log("Легкая волна, врагов:" + _enemiesInWave[i]);
            }
            else if (_enemiesInWave[i] < 10)
            {
                Debug.Log("Средняя волна волна, врагов:" + _enemiesInWave[i]);
            }
            else
            {
                Debug.Log("Сложная волна волна, врагов:" + _enemiesInWave[i]);
            }
            totalEnemies += _enemiesInWave[i];
            totalReward += _waveReward[i];
        }

        if (totalEnemies < 30)
        {
            Debug.Log("Легкий уровень, врагов:" + totalEnemies);
        }
        else
        {
            Debug.Log("Тяжелый уровень, врагов:" + totalEnemies);
        }
        Debug.Log("Награда за уровень:" + totalReward);
    }

    private void Task5()
    {
        var unsortedResult = String.Join(",", _unsorted);

        Debug.Log("Элементы не сортированого массива:" + unsortedResult);
        Array.Sort(_unsorted);
        var sortedResult = String.Join(",", _unsorted);
        Debug.Log("Элементы сортированого массива:" + sortedResult);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
