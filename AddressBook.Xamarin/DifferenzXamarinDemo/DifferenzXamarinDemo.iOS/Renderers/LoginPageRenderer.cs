﻿using System;
using System.Linq;
using DifferenzXamarinDemo.iOS.Renderers;
using DifferenzXamarinDemo.ViewModels;
using DifferenzXamarinDemo.Views;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace DifferenzXamarinDemo.iOS.Renderers
{
    public class LoginPageRenderer : PageRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            LoginPageViewModel.OnPresenter += LoginViewModel_OnPresenter;
        }

        private void LoginViewModel_OnPresenter(object sender, EventArgs e)
        {
            var facebookViewController = ((OAuth2Authenticator)sender).GetUI();
            facebookViewController.View.Subviews.First().Frame =
                new CoreGraphics.CGRect(
                    0,
                    facebookViewController.View.Subviews.Last().Bounds.Height + 20,
                    facebookViewController.View.Subviews.First().Bounds.Width,
                    facebookViewController.View.Subviews.First().Bounds.Height
                );

            var rootViewController = UIKit.UIApplication.SharedApplication.KeyWindow.RootViewController;
            rootViewController.PresentViewController(facebookViewController, true, null);
        }

    }
}

