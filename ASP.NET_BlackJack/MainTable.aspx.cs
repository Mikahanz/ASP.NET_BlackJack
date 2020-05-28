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

        protected void Page_Init(object sender, EventArgs e)
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
            ((Global) this.Context.ApplicationInstance).playerMoney = 500d;

            // Hide all buttons
            btnDeal.Visible = false;
            btnHit.Visible = false;
            btnStand.Visible = false;

            // initiate welcome message
            lblMainMessage.Visible = true;
            lblMainMessage.Text = "Welcome To Black Jack. Please Place Your Bets";

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

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // hide welcome message
                lblMainMessage.Visible = false;
                lblMainMessage.Text = "";


            }

            

            
            // Initiate PlayerMoney
            lblBank.Text = $"${((Global) this.Context.ApplicationInstance).playerMoney}";

            //

        }

        protected void btnDeal_OnClick(object sender, EventArgs e)
        {

        }

        protected void btnHit_OnClick(object sender, EventArgs e)
        {

        }


        protected void btnStand_OnClick(object sender, EventArgs e)
        {

        }

        protected void DropDownListMoney_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeal.Visible = true;
            
        }
    }
}