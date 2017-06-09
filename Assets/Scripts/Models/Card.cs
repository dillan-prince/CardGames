using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardGames.Utility;
using UnityEngine;

namespace CardGames.Models
{
    /// <summary>
    /// Class representing a playing card.
    /// </summary>
    public class Card : MonoBehaviour
    {
        /// <summary>
        /// Name of the card.
        /// </summary>
        /// <example>Ace</example>
        public string Name;

        /// <summary>
        /// Suit of the card.
        /// </summary>
        /// <example>Spades</example>
        public string Suit;

        /// <summary>
        /// Value associated with the card, or 10 if card is a face card.
        /// </summary>
        public int Value;

        /// <summary>
        /// Dual value associated with the card if the card is a face card.
        /// </summary>
        /// <example>If card is a Jack, the royal value is 11. If the card is a Queen, the royal value is 12. If the card is a King, the royal value is 13.</example>
        public int? RoyalValue;

        /// <summary>
        /// Sprite list of possible card faces.
        /// </summary>
        public Sprite[] Faces;

        /// <summary>
        /// Sprite representing the back side of the card.
        /// </summary>
        public Sprite Back;

        /// <summary>
        /// Index associated with the card, used to set the appropriate face.
        /// </summary>
        public int Index;

        /// <summary>
        /// Unity component used to animate the flip of the card.
        /// </summary>
        public AnimationCurve flipCurve;

        /// <summary>
        /// Unity component used to animate a card moving.
        /// </summary>
        public AnimationCurve moveCurve;

        /// <summary>
        /// Unity component used to change the displayed face.
        /// </summary>
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
        }

        #region Public Methods
        /// <summary>
        /// Constructor used to set the card's Name, Suit, Index, and Value. Royal Value is also set if the card is a face card. The card is initialized face down.
        /// </summary>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="suit"><see cref="Suit"/></param>
        /// <param name="index"><see cref="Index"/></param>
        public Card Init(string name, string suit, int index)
        {
            Name = name;
            Suit = suit;
            Index = index;

            _spriteRenderer.sprite = Back;

            // Set value of the card based on its name
            switch (Name)
            {
                case CardInformation.Names.Ace:
                    Value = CardInformation.Values.Ace;
                    break;
                case CardInformation.Names.Two:
                    Value = CardInformation.Values.Two;
                    break;
                case CardInformation.Names.Three:
                    Value = CardInformation.Values.Three;
                    break;
                case CardInformation.Names.Four:
                    Value = CardInformation.Values.Four;
                    break;
                case CardInformation.Names.Five:
                    Value = CardInformation.Values.Five;
                    break;
                case CardInformation.Names.Six:
                    Value = CardInformation.Values.Six;
                    break;
                case CardInformation.Names.Seven:
                    Value = CardInformation.Values.Seven;
                    break;
                case CardInformation.Names.Eight:
                    Value = CardInformation.Values.Eight;
                    break;
                case CardInformation.Names.Nine:
                    Value = CardInformation.Values.Nine;
                    break;
                case CardInformation.Names.Ten:
                    Value = CardInformation.Values.Ten;
                    break;
                case CardInformation.Names.Jack:
                    Value = CardInformation.Values.Ten;
                    RoyalValue = CardInformation.Values.Jack;
                    break;
                case CardInformation.Names.Queen:
                    Value = CardInformation.Values.Ten;
                    RoyalValue = CardInformation.Values.Queen;
                    break;
                case CardInformation.Names.King:
                    Value = CardInformation.Values.Ten;
                    RoyalValue = CardInformation.Values.King;
                    break;
                default:
                    throw new ArgumentException(string.Format("Card constructor called with invalid card name: {0}", name));
            }

            return this;
        }

        /// <summary>
        /// Flips the card to show its face.
        /// </summary>
        public void ShowFront()
        {
            if (_spriteRenderer.sprite == Back)
                AnimateFlip(Back, Faces[Index]);
        }

        /// <summary>
        /// Flips the card to show its back.
        /// </summary>
        public void ShowBack()
        {
            if (_spriteRenderer.sprite != Back)
                AnimateFlip(Faces[Index], Back);
        }

        /// <summary>
        /// Moves the card to a new position.
        /// </summary>
        /// <param name="newPosition">Final position of the card after moving.</param>
        public void Move(Vector3 newPosition)
        {
            StartCoroutine(Cr_AnimateMove(newPosition));
        }
        #endregion

        #region Private Methods
        private void AnimateFlip(Sprite startImage, Sprite endImage)
        {
            StopCoroutine(Cr_Flip(startImage, endImage));
            StartCoroutine(Cr_Flip(startImage, endImage));
        }

        /// <summary>
        /// Coroutine used to animate a card flipping along its vertical axis.
        /// </summary>
        /// <param name="startImage"></param>
        /// <param name="endImage"></param>
        /// <param name="cardInex"></param>
        /// <returns></returns>
        private IEnumerator Cr_Flip(Sprite startImage, Sprite endImage)
        {
            _spriteRenderer.sprite = startImage;

            float duration = 0.2f;
            float time = 0f;

            while (time < 1.2f)
            {
                float scale = flipCurve.Evaluate(time);
                time += Time.deltaTime / duration;

                Vector3 localScale = transform.localScale;
                localScale.x = scale;
                transform.localScale = localScale;

                if (time >= 0.5f)
                    _spriteRenderer.sprite = endImage;

                yield return new WaitForFixedUpdate();
            }
        }

        /// <summary>
        /// Coroutine used to animate the card moving from its current position to a new one.
        /// </summary>
        /// <param name="newPosition">Final position of the card after moving.</param>
        /// <returns></returns>
        private IEnumerator Cr_AnimateMove(Vector3 newPosition)
        {
            float time = 0f;

            Vector3 originalPosition = transform.localPosition;
            Vector3 difference = newPosition - originalPosition;
            float tolerance = 1e-4f;
            while (Math.Abs(transform.localPosition.x - newPosition.x) > tolerance
                 || Math.Abs(transform.localPosition.y - newPosition.y) > tolerance
                 || Math.Abs(transform.localPosition.z - newPosition.z) > tolerance)
            {
                transform.localPosition = originalPosition + difference * moveCurve.Evaluate(time);
                time += Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }
        }
        #endregion
    }
}
