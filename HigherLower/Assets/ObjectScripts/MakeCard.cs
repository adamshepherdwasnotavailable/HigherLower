using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MakeCard : MonoBehaviour
{

    public Texture2D clubsTexture;
    public Texture2D heartsTexture;
    public Texture2D diamondsTexture;
    public Texture2D spadesTexture;

    public Texture2D kingTexture;
    public Texture2D queenTexture;
    public Texture2D jackTexture;
    public Texture2D jokerTexture;

    public GameObject textLabelPrefab;
    public GameObject jokerTextPrefab;


    public Card.Suit CardSuit
    {
        get
        {
            return _cardSuit;
        }
        set
        {
            _cardSuit = value;
        }
    }

    [SerializeField] private Card.Suit _cardSuit;

    public int Pip
    {
        get
        {
            return _pip;
        }
        set
        {
            _pip = value;
        }
    }
    [Range(1, 14)]
    [SerializeField] private int _pip;

    public float pipSize = 0.15f;
    public float topBottomPadding = 0.09f;
    public float leftRightPadding = 0.25f;
    public float labelCornerDistanceX = 0.13f;
    public float labelCornerDistanceY = 0.12f;
    public float labelSizeX = 0.15f;
    public float labelSizeY = 0.21f;
    public float labelProportionSuit = 0.18f;

    private Texture2D currentCardTexture;
    private Color currentCardColor;

    // playing cards pips follow two grids, a 3 x 3, and a 5x3 (and a combination of the two)
    private Dictionary<int, PipPattern> patterns = new Dictionary<int, PipPattern>
    {
        {2, new PipPattern(0, 2) },
        {3, new PipPattern(0, 3) },
        {4, new PipPattern(2, 0) },
        {5, new PipPattern(2, 1) },
        {6, new PipPattern(3, 0) },
        {7, new PipPattern(3, 1) },
        {8, new PipPattern(3, 2) },
        {9, new PipPattern(4, 1) },
        {10, new PipPattern(4, 2) },
    };

    // rules:
    // left and right columns evenly distributed top to bottom
    // middle column always work outward from middle:
    // if evens on left and right, put in middle, else offset upward by 1 (above imaginary middle)
    // if two in middle, put down 3, and remove the centre
    // top half or 2/3 are the right way up, others are upside down.



    // Start is called before the first frame update
    void Start()
    {
        UpdateCardUI();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void UpdateSuitColorAndTexture()
    {
        switch (CardSuit)
        {
            case Card.Suit.Clubs:
                currentCardColor = Color.black;
                currentCardTexture = clubsTexture;
                break;
            case Card.Suit.Spades:
                currentCardColor = Color.black;
                currentCardTexture = spadesTexture;
                break;
            case Card.Suit.Hearts:
                currentCardColor = Color.red;
                currentCardTexture = heartsTexture;
                break;
            case Card.Suit.Diamonds:
                currentCardColor = Color.red;
                currentCardTexture = diamondsTexture;
                break;
        }
    }

    public void UpdateCardUI()
    {
        // Update the values to match deck
        CardSuit = Game.deck.currentCard.suit;
        Pip = Game.deck.currentCard.pips;

        // First, clear any current graphics on the card
        DestroyAllChildren(gameObject);

        // check that suit color and texture correct
        UpdateSuitColorAndTexture();

        // get the required pattern
        if (Pip == Math.Clamp(Pip, 2, 10)) {
            UpdateNonPictureCard();
        }
        else
        {
            if (Pip == 1)
            {
                // Ace - make pip in middle and big
                AddPip(1, 0.5f, 0.5f, false, 3.0f);
            }
            else if (Pip == 11)
            {
                // draw a jack/queen/king in the middle
                AddCardPicture(jackTexture);
            }
            else if (Pip == 12)
            {
                // draw a jack/queen/king in the middle
                AddCardPicture(queenTexture);
            }
            else if (Pip == 13)
            {
                // draw a jack/queen/king in the middle
                AddCardPicture(kingTexture);
            }
        }

        if (Pip == 14)
        {
            // joker - special case
            AddJokerLabels();
            AddCardPicture(jokerTexture);

        }
        else
        {
            UpdateCardCorners();
        }
    }

    private void UpdateCardCorners()
    {
        string cardLabel = Pip.ToString();

        if (Pip == 1)
        {
            cardLabel = "A";
        }
        else if (Pip == 11)
        {
            cardLabel = "J";
        }
        else if (Pip == 12)
        {
            cardLabel = "Q";
        }
        else if (Pip == 13)
        {
            cardLabel = "K";
        }

        // need to write a number, or A, J, Q, K
        // Undernearth need the suit
        AddCardLabel(0 + labelCornerDistanceX, 0 + labelCornerDistanceY, cardLabel, true);
        AddCardLabel(1 - labelCornerDistanceX, 0 + labelCornerDistanceY, cardLabel, true);
        AddCardLabel(0 + labelCornerDistanceX, 1 - labelCornerDistanceY, cardLabel);
        AddCardLabel(1 - labelCornerDistanceX, 1 - labelCornerDistanceY, cardLabel);
    }

    private void UpdateNonPictureCard()
    {
        PipPattern pattern = patterns[Pip];

        float verticalUsableSpace = 1 - pipSize - 2 * topBottomPadding;
        float horizontalUsableSpace = 1 - pipSize - 2 * leftRightPadding;

        int pipNum = 1;

        float horizontalGaps = horizontalUsableSpace / 2;

        // add left and right columns pips
        if (pattern.leftRightCount == 1)
        {
            // should never happen, but just put in middle
            pipNum = AddLeftRightPips(pipNum, pipSize / 2 + topBottomPadding + verticalUsableSpace / 2, horizontalUsableSpace);
        }
        else if (pattern.leftRightCount > 1)
        {
            float verticalGaps = verticalUsableSpace / (pattern.leftRightCount - 1);

            for (int i = 0; i < pattern.leftRightCount; i++)
            {
                pipNum = AddLeftRightPips(pipNum, pipSize / 2 + topBottomPadding + verticalGaps * i, horizontalUsableSpace, flipped: i + 1 <= pattern.leftRightCount / 2);
            }
        }

        // Gaps between the 5 Pip spaces
        float middlePipGaps = verticalUsableSpace / 4;

        // add middle pips
        if (pattern.middleCount == 1)
        {
            float offset = 0.0f;
            if (pattern.leftRightCount % 2 == 1)
            {
                // odd pips, put Pip one above middle
                offset = middlePipGaps;
            }

            AddPip(pipNum, leftRightPadding + horizontalGaps + pipSize / 2, topBottomPadding + pipSize / 2 + verticalUsableSpace / 2 + offset);
        }
        else if (pattern.middleCount == 2)
        {
            if (pattern.leftRightCount == 0)
            {
                // spread out top and bottom
                AddPip(pipNum, leftRightPadding + horizontalGaps + pipSize / 2, topBottomPadding + pipSize / 2, true);
                pipNum++;
                AddPip(pipNum, leftRightPadding + horizontalGaps + pipSize / 2, topBottomPadding + pipSize / 2 + verticalUsableSpace);
            }
            else
            {
                // keep centred
                AddPip(pipNum, leftRightPadding + horizontalGaps + pipSize / 2, topBottomPadding + pipSize / 2 + verticalUsableSpace / 2 - middlePipGaps, true);
                pipNum++;
                AddPip(pipNum, leftRightPadding + horizontalGaps + pipSize / 2, topBottomPadding + pipSize / 2 + verticalUsableSpace / 2 + middlePipGaps);
            }
        }
        else if (pattern.middleCount == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                AddPip(pipNum, leftRightPadding + horizontalGaps + pipSize / 2, topBottomPadding + pipSize / 2 + middlePipGaps * 2 * i, flipped: i + 1 <= pattern.middleCount / 2);
                pipNum++;
            }
        }
    }

    private int AddLeftRightPips(int pipNum, float yLoc, float gapBetweenLRColumns = 0.0f, bool flipped = false)
    {
        AddPip(pipNum, leftRightPadding + pipSize / 2, yLoc, flipped);
        pipNum++;
        AddPip(pipNum, leftRightPadding + gapBetweenLRColumns + pipSize / 2, yLoc, flipped);
        pipNum++;

        return pipNum;
    }

    private void DestroyAllChildren(GameObject go)
    {
        foreach (Transform transform in go.transform)
        {
            Destroy(transform.gameObject);
        }
    }

    private void AddPip(int pipNum, float xLoc, float yLoc, bool flipped = false, float sizeMultiplier = 1.0f)
    {
        GameObject pipObj = new GameObject("Pip" + pipNum.ToString());

        RectTransform trans = pipObj.AddComponent<RectTransform>();
        trans.transform.SetParent(gameObject.transform);
        trans.localScale = Vector3.one;
        trans.anchorMin = new Vector2(xLoc - pipSize * sizeMultiplier / 2, yLoc - pipSize * sizeMultiplier / 2);
        trans.anchorMax = new Vector2(xLoc + pipSize * sizeMultiplier / 2, yLoc + pipSize * sizeMultiplier / 2);
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.offsetMax = Vector2.zero;
        trans.offsetMin = Vector2.zero;

        if (flipped)
        {
            trans.Rotate(new Vector3(180, 0, 0));
        }

        Image pipImage = pipObj.AddComponent<Image>();
        pipImage.preserveAspect = true;
        Sprite pipSprite = Sprite.Create(currentCardTexture, new Rect(0, 0, currentCardTexture.width, currentCardTexture.height), Vector2.zero);
        pipImage.sprite = pipSprite;
    }

    private void AddCardLabel(float xLoc, float yLoc, string cardName, bool flipped = false)
    {
        // first create empty group to hold pip and text
        GameObject labelHolder = new GameObject("LabelHolder");

        RectTransform trans = labelHolder.AddComponent<RectTransform>();
        trans.transform.SetParent(gameObject.transform);
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.anchorMin = new Vector2(xLoc - labelSizeX / 2, yLoc - labelSizeY / 2);
        trans.anchorMax = new Vector2(xLoc + labelSizeX / 2, yLoc + labelSizeY / 2);
        trans.offsetMax = Vector2.zero;
        trans.offsetMin = Vector2.zero;

        // now create pip
        GameObject pipObj = new GameObject("Pip");

        trans = pipObj.AddComponent<RectTransform>();
        trans.transform.SetParent(labelHolder.transform);
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.anchorMin = new Vector2(0, 0);
        trans.anchorMax = new Vector2(1, labelProportionSuit);
        trans.offsetMax = Vector2.zero;
        trans.offsetMin = Vector2.zero;

        Image pipImage = pipObj.AddComponent<Image>();
        pipImage.preserveAspect = true;
        Sprite pipSprite = Sprite.Create(currentCardTexture, new Rect(0, 0, currentCardTexture.width, currentCardTexture.height), Vector2.zero);
        pipImage.sprite = pipSprite;

        // now create text
        GameObject textLabel = Instantiate(textLabelPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        trans = textLabel.GetComponent<RectTransform>();
        trans.transform.SetParent(labelHolder.transform);
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.anchorMin = new Vector2(0, labelProportionSuit);
        trans.anchorMax = new Vector2(1, 1);
        trans.offsetMax = Vector2.zero;
        trans.offsetMin = Vector2.zero;

        TextMeshProUGUI textMeshPro = textLabel.GetComponent<TextMeshProUGUI>();
        if (textMeshPro != null)
        {
            textMeshPro.text = cardName;
            textMeshPro.color = currentCardColor;
        }

        if (flipped)
        {
            labelHolder.transform.Rotate(new Vector3(0, 0, 180));
        }

    }

    private void AddCardPicture(Texture2D texture)
    {
        GameObject picObj = new GameObject("Picture");

        RectTransform trans = picObj.AddComponent<RectTransform>();
        trans.transform.SetParent(gameObject.transform);
        trans.localScale = Vector3.one;
        trans.anchorMin = new Vector2(0.2f, 0.2f);
        trans.anchorMax = new Vector2(0.8f, 0.8f);
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.offsetMax = Vector2.zero;
        trans.offsetMin = Vector2.zero;

        Image pipImage = picObj.AddComponent<Image>();
        pipImage.preserveAspect = true;
        Sprite pipSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        pipImage.sprite = pipSprite;

    }

    private void AddJokerLabels()
    {
        // now create text
        GameObject textLabel = Instantiate(jokerTextPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        RectTransform trans = textLabel.GetComponent<RectTransform>();
        trans.transform.SetParent(gameObject.transform);
        trans.localScale = Vector3.one;
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.anchorMin = new Vector2(0, 0);
        trans.anchorMax = new Vector2(1, 1);
        trans.offsetMax = Vector2.zero;
        trans.offsetMin = Vector2.zero;
    }
}
