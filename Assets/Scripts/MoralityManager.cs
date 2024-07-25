using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoralityManager : MonoBehaviour
{
    public static int morality;
    public static int GreetMorality;
    public static int AttackMorality;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        morality = 0;
        GreetMorality = 4;
        AttackMorality = -2;
    }

    public static void ModifyMorality(int Value)
    {
        MoralityManager.morality += Value;
    }

    public static int SendMorality()
    {
        return MoralityManager.morality;
    }

    public static string PrintMorality()
    {
        return MoralityManager.morality.ToString();
    }


}
