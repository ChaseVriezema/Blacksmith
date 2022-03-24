using UnityEngine;
using TMPro;
using DG.Tweening;

public class PlayingCardPresenter : MonoBehaviour
{

      [HideInInspector] public bool FaceUp;
      [SerializeField] private TextMeshPro nameText;
      [SerializeField] private TextMeshPro valueText;
      [SerializeField] private GameObject glow;

      private static readonly string iconTexture = "Sprite";
      private static readonly string cardColor = "BGColor";

      private MeshRenderer meshRenderer;
      private MaterialPropertyBlock propertyBlock;

      private int cardId;

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
         set 
         {
            propertyBlock.SetTexture(iconTexture, value.texture);
            meshRenderer.SetPropertyBlock(propertyBlock);
         }
      }

      public Color CardColor
      {
         set 
         {
            propertyBlock.SetColor(cardColor, value);
            meshRenderer.SetPropertyBlock(propertyBlock);
         }
      }

      public bool Glow
      {
         set => glow.SetActive(value);
      }

      public void Awake()
      {
         meshRenderer = this.GetComponent<MeshRenderer>();
         propertyBlock = new MaterialPropertyBlock();
         meshRenderer.SetPropertyBlock(propertyBlock);
      }

      public void Init(PlayingCard card, bool faceUp)
      {
          FaceUp = faceUp;
          Name = card.CardValue.ToString();
          Value = "";
          CardSprite = GameSettings.Instance.Settings.SuitIcons[(int)card.CardSuit];
          CardColor = GameSettings.Instance.Settings.SuitColors[(int)card.CardSuit];
          cardId = card.Id;
      }

      public Sequence FlipCard(float time, bool faceUp)
      {
         FaceUp = faceUp;
         var seq = DOTween.Sequence();
         seq.Append(transform.DORotate(faceUp ? Vector3.zero : Vector3.up * 180, time));
         return seq;
      }
}
