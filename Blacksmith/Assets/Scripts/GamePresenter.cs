using System.Collections;
using System.Collections.Generic;
using Manabound.Card;
using UnityEngine;
using UnityEngine.Serialization;

public class GamePresenter : MonoBehaviour
{
   [SerializeField] private HandPresenter handPresenter;
   [SerializeField] private CardPresenter cardPresenter;
}
