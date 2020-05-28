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
        public List<string> ImageList { get; set; }



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

                // initiate player bet
                ((Global)this.Context.ApplicationInstance).playerBet = 0d;

                // Hide all buttons
                btnDeal.Visible = false;
                btnHit.Visible = false;
                btnStand.Visible = false;

                // initiate welcome message
                lblMainMessage.Visible = true;
                lblMainMessage.Text = "Welcome To Black Jack. \nPlease Place Your Bets";

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

                // endplay status
                bool endPlay = false;

                // dealer turn status
                bool dealerTurn = false;
            }

            if (IsPostBack)
            {

                // hide welcome message
                lblMainMessage.Visible = false;
                lblMainMessage.Text = "";
            }

            // Initiate PlayerMoney
            lblBank.Text = $"Player Bank: ${((Global)this.Context.ApplicationInstance).playerMoney.ToString()}";

            //

        }

        protected void btnDeal_OnClick(object sender, EventArgs e)
        {
            // Starts Dealing

            // Dealer gets one card initialy
            ((Global)this.Context.ApplicationInstance).dealerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
            lblCardValueDealer.Text = dealerHandValue();
            imgClosedCard.Visible = true;


            // Player gets two cards
            ((Global)this.Context.ApplicationInstance).playerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
            ((Global)this.Context.ApplicationInstance).playerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
            lblCardValuePlayer.Text = playerHandValue();
                


            btnHit.Visible = true;
            btnStand.Visible = true;
            btnDeal.Visible = false;
        }

        protected void btnHit_OnClick(object sender, EventArgs e)
        {
            // player gets one extra card
            ((Global)this.Context.ApplicationInstance).playerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
            lblCardValuePlayer.Text = playerHandValue();
        }


        protected void btnStand_OnClick(object sender, EventArgs e)
        {
            // Dealer gets one card initialy
            ((Global)this.Context.ApplicationInstance).dealerDeck.draw(((Global)this.Context.ApplicationInstance).playingDeck);
            lblCardValueDealer.Text = dealerHandValue();
            imgClosedCard.Visible = false;
        }

        protected void DropDownListMoney_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (double.Parse(DropDownListMoney.SelectedValue) > ((Global) this.Context.ApplicationInstance).playerMoney)
            {
                lblMainMessage.Text =
                    $"You don't have enough money! Please bet less than ${double.Parse(DropDownListMoney.SelectedValue)}!";
            }
            else
            {
                btnDeal.Visible = true;
                ((Global)this.Context.ApplicationInstance).playerBet += double.Parse(DropDownListMoney.SelectedValue);
                ((Global)this.Context.ApplicationInstance).playerMoney -= double.Parse(DropDownListMoney.SelectedValue);
                lblBetValue.Text = $"Player Bet: ${((Global)this.Context.ApplicationInstance).playerBet.ToString()}";
                lblBank.Text = $"Player Bank: ${((Global)this.Context.ApplicationInstance).playerMoney.ToString()}";
                imgChip.Visible = true;
                DropDownListMoney.Visible = false;
            }
            

        }

        protected int VerifyWinner(int dealerCardValue, int playerCardValue)
        {
            // return 1 if winner is dealer
            // return 2 if winner is player

            // if player value card over 21
            if (((Global)this.Context.ApplicationInstance).playerDeck.cardsValue() > 21)
            {
                lblMainMessage.Text = $"Player Busts! Dealer Wins!";
                return 1;
            }

            // 

            return 0;
        }

        protected void terminatePlay()
        {

        }

        protected bool playerOutOfMoney()
        {
            return ((Global)this.Context.ApplicationInstance).playerMoney <= 0? true : false;
        }

        protected string dealerHandValue()
        {
            return $"Dealer Hand: {((Global)this.Context.ApplicationInstance).dealerDeck.cardsValue().ToString()}";
        }

        protected string playerHandValue()
        {
            return $"Player Hand: {((Global)this.Context.ApplicationInstance).playerDeck.cardsValue().ToString()}";
        }
    }
}