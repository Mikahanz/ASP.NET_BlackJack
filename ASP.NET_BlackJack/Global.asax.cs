using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using ASP.NET_BlackJack.Services;

namespace ASP.NET_BlackJack
{
    public class Global : HttpApplication
    {
        public Deck playingDeck;

        public Deck playerDeck;

        public Deck dealerDeck;

        public double playerMoney;

        public double playerBet;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}