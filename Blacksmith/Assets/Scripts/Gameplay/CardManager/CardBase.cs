using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class CardBase
{
    private int id;
    public int Id => id;

    private bool faceUp;
    public bool FaceUp;


    public CardBase()
    {
        id = CardIdFactory.GetUniqueId();
    }
}
