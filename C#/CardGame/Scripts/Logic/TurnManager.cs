using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

// this class will take care of switching turns and counting down time until the turn expires
public class TurnManager : MonoBehaviour {

    // private RopeTimer timer;
    public Player whoGoesFirst;
    public Player whoGoesSecond;
    public Button endTurnButton;
    public Dice dice;
    public CameraControl cameraControler;

    // for Singleton Pattern
    public static TurnManager Instance;

    private Player _whoseTurn;
    public Player whoseTurn
    {
        get
        {
            return _whoseTurn;
        }

        set
        {
            _whoseTurn = value;
            //timer.StartTimer();

            GlobalSettings.Instance.EnableEndTurnButtonOnStart(_whoseTurn);
            _whoseTurn.PArea.ControlsON = true;
            TurnMaker tm = whoseTurn.GetComponent<TurnMaker>();
            // player`s method OnTurnStart() will be called in tm.OnTurnStart();
            tm.OnTurnStart();
            if (tm is PlayerTurnMaker)
            {
                whoseTurn.HighlightPlayableCards();
            }
            // remove highlights for opponent.
            whoseTurn.otherPlayer.HighlightPlayableCards(true);
            if (whoseTurn.tag == "LowPlayer")
            {
                cameraControler.MToP1();
            }
            else {
                cameraControler.MToP2();
            }
        }
    }

    void Awake()
    {
        Instance = this;
        //timer = GetComponent<RopeTimer>();
    }

    void Start()
    {
       OnGameStart();
    }

    public void OnGameStart()
    {
        //Debug.Log("In TurnManager.OnGameStart()");
        dice.diceCamera.enabled = false;
        endTurnButton.interactable = false;
        CardLogic.CardsCreatedThisGame.Clear();
        CreatureLogic.CreaturesCreatedThisGame.Clear();

        foreach (Player p in Player.Players)
        {

            //p.ManaThisTurn = 0;
            //p.ManaLeft = 0;
            p.LoadCharacterInfoFromAsset();
            p.TransmitInfoAboutPlayerToVisual();
            p.PArea.PDeck.CardsInDeck = p.deck.cards.Count;
            // move both portraits to the center
            p.PArea.Portrait.transform.position = p.PArea.InitialPortraitPosition.position;
        }

        Sequence s = DOTween.Sequence();
        s.Append(Player.Players[0].PArea.Portrait.transform.DOMove(Player.Players[0].PArea.PortraitPosition.position, 1f).SetEase(Ease.InQuad));
        s.Insert(0f, Player.Players[1].PArea.Portrait.transform.DOMove(Player.Players[1].PArea.PortraitPosition.position, 1f).SetEase(Ease.InQuad));
        s.PrependInterval(3f);
        s.OnComplete(() =>
            {
                // determine who starts the game.
                int rnd = Random.Range(0,2);  // 2 is exclusive boundary
                // Debug.Log(Player.Players.Length);
                whoGoesFirst = Player.Players[rnd];
                // Debug.Log(whoGoesFirst);
                whoGoesSecond = whoGoesFirst.otherPlayer;
                // Debug.Log(whoGoesSecond);
         
                // draw 4 cards for first player and 5 for second player
                //int initDraw = 4;
                /*for (int i = 0; i < initDraw; i++)
                {            
                    // second player draws a card
                    whoGoesSecond.DrawACard(true);
                    // first player draws a card
                    whoGoesFirst.DrawACard(true);
                }*/
                // add one more card to second player`s hand
                // whoGoesSecond.DrawACard(true);
                //new GivePlayerACoinCommand(null, whoGoesSecond).AddToQueue();
                // whoGoesSecond.DrawACoin();

                new StartATurnCommand(whoGoesFirst).AddToQueue();
            });
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EndTurn();
    }*/

    public void EndTurn()
    {
        // stop timer
        // timer.StopTimer();
        // send all commands in the end of current player`s turn

        //RollTheDice

        //disable endturnbutton
        //Debug.Log(whoseTurn.commandsWaitinglist);
        endTurnButton.interactable = false;
        whoseTurn.PArea.ControlsON = false;
        if (whoseTurn.tag == "LowPlayer")
        {
            cameraControler.P1ToM();
        }
        else {
            cameraControler.P2ToM();
        }

        new RollDiceCommand(dice).AddToQueue();
        new EndTurnCommand(whoseTurn,whoGoesFirst,whoGoesSecond,dice).AddToQueue();
        //whoseTurn.OnTurnEnd(dicenum);
       
    }

    public void StopTheTimer()
    {
        //timer.StopTimer();
    }

}

