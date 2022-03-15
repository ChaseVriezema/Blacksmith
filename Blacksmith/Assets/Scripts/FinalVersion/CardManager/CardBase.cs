using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class CardBase
{
    private int id;
    public int Id => id;

    public CardBase()
    {
        id = CardIdFactory.GetUniqueId();
    }
}
