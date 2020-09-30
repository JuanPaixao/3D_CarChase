using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopPool : MonoBehaviour
{
    public Stack<GameObject> copCars;

    public GameObject cop;
    public int copInitialQuantity;

    private void Start()
    {
        copCars = new Stack<GameObject>();
        CreateAllCars();
    }
    public void CreateAllCars()
    {
        for (int i = 0; i < copInitialQuantity; i++)
        {
            GameObject police = GameObject.Instantiate(this.cop, this.transform);
            CopAI cop = police.GetComponent<CopAI>();
            cop.copPool = this;
            police.SetActive(false);
            this.copCars.Push(police);
        }
    }
    public void ReturnCop(GameObject cop)
    {
        cop.SetActive(false);
        this.copCars.Push(cop);
    }
    public GameObject CreateCop()
    {
        return this.copCars.Pop();
    }
    public bool HasCops()
    {
        return this.copCars.Count > 0;
    }
}
