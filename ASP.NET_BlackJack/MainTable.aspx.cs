using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NET_BlackJack.Services;


namespace ASP.NET_BlackJack
{
    public partial class MainTable : System.Web.UI.Page
    {
        public List<int> MoneyList;
        public List<int> DepositMoneyList;
        public List<string> ImageList { get; set; }
        bool endPlay;
        bool dealerTurn;


        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute only once when page is load
            if (!IsPostBack)
            {
                // Create our playing deck
                ((Global)this.Context.ApplicationInstance).playingDeck = new Deck();

                // Create Full Deck
                ((Global)this.Context.ApplicationInstance).playingDeck.createFullDeck();

                // Shuffle Deck
                ((Global)this.Context.ApplicationInstance).playingDeck.shuffle();

                // Create player deck
                ((Global)this.Context.ApplicationInstance).playerDeck = new Deck();

                // Create Dealer Deck
                ((Global)this.Context.ApplicationInstance).dealerDeck = new Deck();

                // initiate player money
                ((Global)this.Context.ApplicationInstance).playerMoney = 500d;

                // Initiate PlayerMoney
                lblBank.Text = $"Player Bank: ${((Global)this.Context.ApplicationInstance).playerMoney.ToString()}";
                lblBank.Visible = false;

                // initiate player bet
                ((Global)this.Context.ApplicationInstance).playerBet = 0d;

                // Hide all buttons
                btnDeal.Visible = false;
                btnHit.Visible = false;
                btnStand.Visible = false;

                // initiate welcome message
                showMainMessage($"Welcome To Black Jack. You Have ${((Global)this.Context.ApplicationInstance).playerMoney} In Your Bank. Please Place Your Bets.");

                // DropDownlist data
                MoneyList = new List<int>();
                this.MoneyList.Add(5);
                this.MoneyList.Add(10);
                this.MoneyList.Add(25);
                this.MoneyList.Add(50);
                this.MoneyList.Add(100);
                DropDownListMoney.DataSource = this.MoneyList;
                DropDownListMoney.DataBind();
                DropDownListMoney.Items.Insert(0, new ListItem("Place Your Bets"));

                // DropDownlist deposit money data
                DepositMoneyList = new List<int>();
                this.DepositMoneyList.Add(100);
                this.DepositMoneyList.Add(200);
                this.DepositMoneyList.Add(300);
                this.DepositMoneyList.Add(400);
                this.DepositMoneyList.Add(500);
                DropDownListDeposit.DataSource = this.DepositMoneyList;
                DropDownListDeposit.DataBind();
                DropDownListDeposit.Items.Insert(0, new ListItem("Deposit Money"));

                // endplay status
                endPlay = false;

                // dealer turn status
                dealerTurn = false;
            }

            if (IsPostBack)
            {

                lblBank.Text = $"Player Bank: ${((Global)this.Context.ApplicationInstance).playerMoney.ToString()}";

            }

        }

        protected void btnDeal_OnClick(object sender, EventArgs e)
        {
            // hide welcome message
            lblMainMessage.Visible = false;
            lblMainMessage.Text = "";

            // Starts Dealing

            // Dealer gets one card initialy
            ((Global)this.Context.ApplicationInstance).dealerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
            lblCardValueDealer.Text = dealerHandValueString();
            imgClosedCard.Visible = true;


            // Player gets two cards
            ((Global)this.Context.ApplicationInstance).playerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
            ((Global)this.Context.ApplicationInstance).playerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
            lblCardValuePlayer.Text = playerHandValueString();


            lblBank.Visible = true;
            btnHit.Visible = true;
            btnStand.Visible = true;
            btnDeal.Visible = false;
            lblCardValueDealer.Visible = true;
            lblCardValuePlayer.Visible = true;

            verifyNaturalBlackJack();

        }

        protected void btnHit_OnClick(object sender, EventArgs e)
        {


            // player gets one extra card
            ((Global)this.Context.ApplicationInstance).playerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
            lblCardValuePlayer.Text = playerHandValueString();

            verifyBlackJack();
            verifyPlayerBust();

        }


        protected void btnStand_OnClick(object sender, EventArgs e)
        {
            //// Dealer gets one card initialy
            //((Global)this.Context.ApplicationInstance).dealerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
            //lblCardValueDealer.Text = dealerHandValueString();

            // dealer keep drawing until above 17
            while (((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue() < 17)
            {
                ((Global)this.Context.ApplicationInstance).dealerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
                lblCardValueDealer.Text = dealerHandValueString();

            }
            imgClosedCard.Visible = false;

            // hide hit and stand button
            btnHit.Visible = false;
            btnStand.Visible = false;

            verifyDealerBust();
            verifyWinner();


        }

        protected void DropDownListMoney_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (double.Parse(DropDownListMoney.SelectedValue) > ((Global)this.Context.ApplicationInstance).playerMoney)
            {
                showMainMessage($"You don't have enough money! Please bet less than ${double.Parse(DropDownListMoney.SelectedValue)}!");
            }
            else
            {
                btnDeal.Visible = true;
                ((Global)this.Context.ApplicationInstance).playerBet += double.Parse(DropDownListMoney.SelectedValue);
                ((Global)this.Context.ApplicationInstance).playerMoney -= double.Parse(DropDownListMoney.SelectedValue);
                lblBetValue.Text = $"Player Bet: ${((Global)this.Context.ApplicationInstance).playerBet.ToString()}";

                lblBank.Visible = false;
                imgChip.Visible = true;
                DropDownListMoney.SelectedIndex = 0;
                DropDownListMoney.Visible = false;
                lblMainMessage.Visible = false;
                lblBetValue.Visible = true;
            }

        }

        protected int verifyPlayerBust()
        {
            // Verify if player over 21
            if (((Global)this.Context.ApplicationInstance).playerDeck.cardsValue() > 21)
            {
                showMainMessage($"Player Busts! Dealer Wins!");
                endofPlayVisibility();
                endPlay = true;
                return 1;
            }
            else
            {
                return 0;
            }



        }

        protected int verifyDealerBust()
        {
            // Verify if dealer over 21
            if (((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue() > 21)
            {
                showMainMessage($"Dealer Busts! Player Wins ${((Global)this.Context.ApplicationInstance).playerBet}!");
                ((Global)this.Context.ApplicationInstance).playerMoney += (((Global)this.Context.ApplicationInstance).playerBet * 2);
                endofPlayVisibility();
                endPlay = true;
                return 1;
            }
            else
            {
                return 0;
            }
        }

        protected void verifyWinner()
        {
            if (((Global)this.Context.ApplicationInstance).playerDeck.cardsValue() > ((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue() && ((Global)this.Context.ApplicationInstance).playerDeck.cardsValue() <= 21)
            {
                // player wins
                showMainMessage($"Player Wins ${((Global)this.Context.ApplicationInstance).playerBet}!");
                ((Global)this.Context.ApplicationInstance).playerMoney += (((Global)this.Context.ApplicationInstance).playerBet * 2);
                endofPlayVisibility();
                endPlay = true;
            }
            else if (((Global)this.Context.ApplicationInstance).playerDeck.cardsValue() < ((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue() && ((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue() <= 21)
            {
                // Dealer wins
                showMainMessage($"Dealer Wins!");
                endofPlayVisibility();
                endPlay = true;
            }
            else if (((Global)this.Context.ApplicationInstance).playerDeck.cardsValue() == ((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue())
            {
                // Tie / Push
                showMainMessage($"Player And Dealer Have Same Card Value! Push");
                ((Global)this.Context.ApplicationInstance).playerMoney += ((Global)this.Context.ApplicationInstance).playerBet;
                endofPlayVisibility();
                endPlay = true;
            }
        }

        protected void verifyBlackJack()
        {
            if (((Global)this.Context.ApplicationInstance).playerDeck.cardsValue() == 21 && ((Global)this.Context.ApplicationInstance).playerDeck.Cards.Count > 2)   // if player got a blackjack/21 with more than 2 cards
            {
                // hide button hit and stand
                btnHit.Visible = false;
                btnStand.Visible = false;

                // hide Closed Card
                imgClosedCard.Visible = false;

                // dealer keep drawing until above 17
                while (((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue() < 17)
                {
                    ((Global)this.Context.ApplicationInstance).dealerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
                    lblCardValueDealer.Text = dealerHandValueString();
                }

                // check if dealer also has black jack
                if (((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue() == ((Global)this.Context.ApplicationInstance).playerDeck.cardsValue()) // Tie
                {
                    // check who has less card (Black Jack) wins
                    if (((Global)this.Context.ApplicationInstance).dealerDeck.Cards.Count > ((Global)this.Context.ApplicationInstance).playerDeck.Cards.Count)
                    {
                        showMainMessage($"Dealer Has 21 With Less Card! Dealer Wins!");
                        endofPlayVisibility();
                        endPlay = true;
                    }
                    else if (((Global)this.Context.ApplicationInstance).dealerDeck.Cards.Count < ((Global)this.Context.ApplicationInstance).playerDeck.Cards.Count)
                    {
                        showMainMessage($"Player Has 21 With Less Card! Player Wins ${((Global)this.Context.ApplicationInstance).playerBet}");
                        ((Global)this.Context.ApplicationInstance).playerMoney += (((Global)this.Context.ApplicationInstance).playerBet * 2);
                        endofPlayVisibility();
                        endPlay = true;
                    }
                    else
                    {
                        showMainMessage($"Player And Dealer Have 21! Push");
                        ((Global)this.Context.ApplicationInstance).playerMoney += ((Global)this.Context.ApplicationInstance).playerBet;
                        endofPlayVisibility();
                        endPlay = true;
                    }
                }
                else
                {
                    showMainMessage($"Player Has 21! Player Wins ${((Global)this.Context.ApplicationInstance).playerBet}");
                    ((Global)this.Context.ApplicationInstance).playerMoney += (((Global)this.Context.ApplicationInstance).playerBet * 2);
                    endofPlayVisibility();
                    endPlay = true;
                }


            }
        }

        protected void verifyNaturalBlackJack()
        {
            if (((Global)this.Context.ApplicationInstance).playerDeck.cardsValue() == 21 && ((Global)this.Context.ApplicationInstance).playerDeck.Cards.Count == 2)   // if player got a blackjack/21 with only 2 cards
            {
                // hide button hit and stand
                btnHit.Visible = false;
                btnStand.Visible = false;

                // hide Closed Card
                imgClosedCard.Visible = false;

                // dealer keep drawing until above 17
                while (((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue() < 17)
                {
                    ((Global)this.Context.ApplicationInstance).dealerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
                    lblCardValueDealer.Text = dealerHandValueString();
                }

                // check if dealer also has black jack
                if (((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue() == ((Global)this.Context.ApplicationInstance).playerDeck.cardsValue()) // Dealer wins
                {
                    showMainMessage($"Dealer Has Black Jack! Dealer Wins!");
                    endofPlayVisibility();
                    endPlay = true;
                }
                else
                {
                    showMainMessage($"Player Has Black Jack! Player Wins ${((Global)this.Context.ApplicationInstance).playerBet * 1.5}");
                    ((Global)this.Context.ApplicationInstance).playerMoney += (((Global)this.Context.ApplicationInstance).playerBet * 2.5);
                    endofPlayVisibility();
                    endPlay = true;
                }
            }
        }

        protected void showMainMessage(string message)
        {
            lblMainMessage.Text = message;
            lblMainMessage.Visible = true;
        }

        protected void endofPlayVisibility()
        {
            btnPlayAgain.Visible = true;
            btnQuitPlay.Visible = true;
            btnHit.Visible = false;
            btnStand.Visible = false;
            imgChip.Visible = false;
            lblBetValue.Visible = false;





        }

        protected bool playerOutOfMoney()
        {
            return ((Global)this.Context.ApplicationInstance).playerMoney <= 0 ? true : false;
        }

        protected string dealerHandValueString()
        {
            return $"Dealer Hand: {((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue().ToString()}";
        }

        protected int dealerHandValueInt()
        {
            return ((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue();
        }

        protected string playerHandValueString()
        {
            return $"Player Hand: {((Global)this.Context.ApplicationInstance).playerDeck.cardsValue().ToString()}";
        }

        protected int playerHandValueInt()
        {
            return ((Global)this.Context.ApplicationInstance).playerDeck.cardsValue();
        }


        protected void btnPlayAgain_Click(object sender, EventArgs e)
        {

            // put all playing decks to initial deck
            ((Global)this.Context.ApplicationInstance).playerDeck.moveAllCardsToDeck(((Global)this.Context.ApplicationInstance).playingDeck);
            ((Global)this.Context.ApplicationInstance).dealerDeck.moveAllCardsToDeck(((Global)this.Context.ApplicationInstance).playingDeck);

            // hide this button
            btnPlayAgain.Visible = false;

            DropDownListMoney.Visible = true;
            lblBetValue.Visible = true;
            lblMainMessage.Text = "Please Place Your Bets Again";
            ((Global)this.Context.ApplicationInstance).playerBet = 0d;
            lblCardValuePlayer.Visible = false;
            lblCardValueDealer.Visible = false;
            lblBetValue.Visible = false;
            imgClosedCard.Visible = false;
            btnQuitPlay.Visible = false;

            // check if player still have money
            if (playerOutOfMoney())
            {
                showMainMessage($"You Have $ {((Global)this.Context.ApplicationInstance).playerMoney} In Your Bank. Would You Like To Deposit More Money and Play Again?");
                DropDownListMoney.Visible = false;
                lblBank.Visible = false;

                btnYes.Visible = true;
                btnNo.Visible = true;
            }
        }

        protected void btnQuitPlay_Click(object sender, EventArgs e)
        {
            // put all playing decks to initial deck
            ((Global)this.Context.ApplicationInstance).playerDeck.moveAllCardsToDeck(((Global)this.Context.ApplicationInstance).playingDeck);
            ((Global)this.Context.ApplicationInstance).dealerDeck.moveAllCardsToDeck(((Global)this.Context.ApplicationInstance).playingDeck);

            // hide this button
            btnPlayAgain.Visible = false;

            DropDownListMoney.Visible = false;
            lblBetValue.Visible = false;
            lblCardValuePlayer.Visible = false;
            lblCardValueDealer.Visible = false;
            lblBetValue.Visible = false;
            imgClosedCard.Visible = false;
            lblBank.Visible = false;
            btnQuitPlay.Visible = false;

            showMainMessage($"Thank You For Playing BlackJack. You're Taking Home ${((Global)this.Context.ApplicationInstance).playerMoney}. Good Bye!");
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            DropDownListDeposit.Visible = true;
            btnYes.Visible = false;
            btnNo.Visible = false;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            showMainMessage($"Thank You For Playing BlackJack. Good Bye!");
            btnYes.Visible = false;
            btnNo.Visible = false;
        }

        protected void DropDownListDeposit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Global)this.Context.ApplicationInstance).playerMoney += double.Parse(DropDownListDeposit.SelectedValue);
            btnPlayAgain_Click(new object(), new EventArgs());
            DropDownListDeposit.Visible = false;
        }

        
    }
}