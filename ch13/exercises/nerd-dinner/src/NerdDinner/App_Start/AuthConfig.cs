using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using NerdDinner.Models;
using System.Web.Configuration;

namespace NerdDinner
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            var microsoftClientId = WebConfigurationManager.AppSettings["microsoftClientId"];
            var microsoftClientSecret = WebConfigurationManager.AppSettings["microsoftClientSecret"];
            if (!string.IsNullOrEmpty(microsoftClientId) && !string.IsNullOrEmpty(microsoftClientSecret))
            {
                OAuthWebSecurity.RegisterMicrosoftClient(
                    clientId: microsoftClientId,
                    clientSecret: microsoftClientSecret);
            }

            var twitterConsumerKey = WebConfigurationManager.AppSettings["twitterConsumerKey"];
            var twitterConsumerSecret = WebConfigurationManager.AppSettings["twitterConsumerSecret"];
            if (!string.IsNullOrEmpty(twitterConsumerKey) && !string.IsNullOrEmpty(twitterConsumerSecret))
            {
                OAuthWebSecurity.RegisterTwitterClient(
                    consumerKey: twitterConsumerKey,
                    consumerSecret: twitterConsumerSecret);
            }

            var facebookAppId = WebConfigurationManager.AppSettings["facebookAppId"];
            var facebookAppSecret = WebConfigurationManager.AppSettings["facebookAppSecret"];
            if (!string.IsNullOrEmpty(facebookAppId) && !string.IsNullOrEmpty(facebookAppSecret))
            {
                OAuthWebSecurity.RegisterFacebookClient(
                    appId: facebookAppId,
                    appSecret: facebookAppSecret);
            }

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
