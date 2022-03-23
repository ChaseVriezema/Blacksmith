using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardIdFactory
{
    private static int prevId = 0;

    public static int GetUniqueId()
    {
        prevId++;
        return prevId;
    }
        
}
