using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Manabound.Card
{
   public class CardPresenter : MonoBehaviour, CardController.ICardView
   {
      [SerializeField] private TextMeshPro nameText;
      [SerializeField] private TextMeshPro valueText;
      [SerializeField] private MeshRenderer meshRenderer;
      [SerializeField] private Shader cardShader;
      [SerializeField] private GameObject glow;

      private static readonly int IconTexture = Shader.PropertyToID("IconTexture");
      private static readonly int CardColor1 = Shader.PropertyToID("CardColor");

      private Material CardMaterial => meshRenderer.material;

      public string Name
      {
         get => nameText.text;
         set => nameText.text = value;
      }

      public string Value
      {
         get => valueText.text;
         set => valueText.text = value;
      }

      public Sprite CardSprite
      {
         set => CardMaterial.SetTexture(IconTexture, value.texture);
      }

      public Color CardColor
      {
         set => CardMaterial.SetColor(CardColor1, value);
      }

      public bool Glow
      {
         set => glow.SetActive(value);
      }

      public void Awake()
      {
         meshRenderer.material = new Material(cardShader);
      }

      public Sequence FlipCard(float time)
      {
         var seq = DOTween.Sequence();
         seq.Append(transform.DORotate(Vector3.up * 180, time));
         return seq;
      }
   }
}
