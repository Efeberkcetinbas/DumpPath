using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Extensions
{
    public static void Shuffle<T>(this T[] array,int shuffleAccuracy)
    {
        for (int i = 0; i < shuffleAccuracy; i++)
        {
            int randomIndex=Random.Range(0,array.Length);

            T temp=array[randomIndex];
            array[randomIndex]=array[0];
            array[0]=temp;

        }
    }
    //Fisher-Yates Shuffle

    public static void Shuffle<T>(this List<T> list,int shuffleAccuracy)
    {
        for (int i = 0; i < shuffleAccuracy; i++)
        {
            int randomIndex=Random.Range(0,list.Count);

            T temp=list[randomIndex];
            list[randomIndex]=list[0];
            list[0]=temp;

        }
    }

    
}
