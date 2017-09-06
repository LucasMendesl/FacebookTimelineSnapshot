using System;
using System.Net;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using FacebookTimelineSnapshot.Request;

namespace FacebookTimelineSnapshot
{
    class Program
    {
        static CookieContainer cookieJar = new CookieContainer();
        static FacebookRequest request = new FacebookRequest(cookieJar);

        static void Main(string[] args)
        {
            IHtmlDocument mainDom = request.Get(Consts.BaseUrl).Html;
            IElement loginForm = mainDom.GetElementById("login_form");

            FacebookLogin loginModel = GetFacebookLogin(loginForm);
            FacebookResponse loginResponse = request.Post(loginForm.GetAttribute("action"), loginModel, false);
            
            if (!loginResponse.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("Usuário e/ou senha inválidos!");
            }
        }
        
        static FacebookLogin GetFacebookLogin(IElement el)
        {
            return new FacebookLogin();
        }
    }
}
