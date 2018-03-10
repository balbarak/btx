using Foundation;
using Btx.Mobile.iOS.CustomRenders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;
using Btx.Mobile.iOS.Extensions;
using CoreGraphics;

[assembly: ExportRenderer(typeof(Page), typeof(KeyboardRender))]
namespace Btx.Mobile.iOS.CustomRenders
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public class KeyboardRender : PageRenderer
    {
        NSObject _keyboardShowObserver;
        NSObject _keyboardHideObserver;
        NSObject _keyboardChangeFrameObserver;
        private bool _pageWasShiftedUp;
        private double _activeViewBottom;
        private bool _isKeyboardShown;

        public KeyboardRender() { }

        public new static void Init()
        {
            var now = DateTime.Now;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var page = Element as ContentPage;

            if (page != null)
            {
                var contentScrollView = page.Content as ScrollView;

                if (contentScrollView != null)
                    return;

                RegisterForKeyboardNotifications();
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            UnregisterForKeyboardNotifications();
        }

        void RegisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver == null)
                _keyboardShowObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardShow);
            if (_keyboardHideObserver == null)
                _keyboardHideObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardHide);

            if (_keyboardChangeFrameObserver == null)
                _keyboardChangeFrameObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillChangeFrameNotification, OnChangeFrame);
        }

        void UnregisterForKeyboardNotifications()
        {
            _isKeyboardShown = false;
            if (_keyboardShowObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardShowObserver);
                _keyboardShowObserver.Dispose();
                _keyboardShowObserver = null;
            }

            if (_keyboardHideObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardHideObserver);
                _keyboardHideObserver.Dispose();
                _keyboardHideObserver = null;
            }
        }

        protected virtual void OnKeyboardShow(NSNotification notification)
        {
            if (!IsViewLoaded || _isKeyboardShown)
                return;

            _isKeyboardShown = true;
            var activeView = View.FindFirstResponder();

            if (activeView == null)
                return;

            var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
            var isOverlapping = activeView.IsKeyboardOverlapping(View, keyboardFrame);

            if (!isOverlapping)
                return;

            if (isOverlapping)
            {
                _activeViewBottom = activeView.GetViewRelativeBottom(View);
                ShiftPageUp(keyboardFrame.Height, _activeViewBottom);
            }
        }

        private void OnKeyboardHide(NSNotification notification)
        {
            if (!IsViewLoaded)
                return;

            _isKeyboardShown = false;
            var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);

            if (_pageWasShiftedUp)
            {
                ShiftPageDown(keyboardFrame.Height, _activeViewBottom);
            }
        }


        private void OnChangeFrame(NSNotification notification)
        {
            var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
            var keyboardHeight = keyboardFrame.Height;

            var activeView = View.FindFirstResponder();

            if (activeView == null)
                return;

            Debug.WriteLine($"Active View: {activeView}");
            var pageFrame = Element.Bounds;

            _activeViewBottom = activeView.GetViewRelativeBottom(View);

            var newHeight = pageFrame.Height + CalculateShiftByAmount(pageFrame.Height, keyboardHeight, _activeViewBottom);

            Debug.WriteLine($"New Height: {newHeight}");

            Element.LayoutTo(new Rectangle(pageFrame.X, pageFrame.Y,
              pageFrame.Width, newHeight));
            

        }

        private void ShiftPageUp(nfloat keyboardHeight, double activeViewBottom)
        {
            var pageFrame = Element.Bounds;

            var newHeight = pageFrame.Height + CalculateShiftByAmount(pageFrame.Height, keyboardHeight, activeViewBottom);

            Element.LayoutTo(new Rectangle(pageFrame.X, pageFrame.Y,
               pageFrame.Width, newHeight));

            _pageWasShiftedUp = true;
        }

        private void ShiftPageDown(nfloat keyboardHeight, double activeViewBottom)
        {
            var pageFrame = Element.Bounds;

            var newHeight = activeViewBottom;

            Element.LayoutTo(new Rectangle(pageFrame.X, pageFrame.Y,
             pageFrame.Width, newHeight));

            _pageWasShiftedUp = false;
        }

        private double CalculateShiftByAmount(double pageHeight, nfloat keyboardHeight, double activeViewBottom)
        {
            return (pageHeight - activeViewBottom) - keyboardHeight;
        }
    }

    public static class ViewExtensions
    {

        public static UIView FindFirstResponder(this UIView view)
        {
            if (view.IsFirstResponder)
            {
                return view;
            }
            foreach (UIView subView in view.Subviews)
            {
                var firstResponder = subView.FindFirstResponder();
                if (firstResponder != null)
                    return firstResponder;
            }
            return null;
        }

        public static double GetViewRelativeBottom(this UIView view, UIView rootView)
        {
            var viewRelativeCoordinates = rootView.ConvertPointFromView(view.Frame.Location, view);
            var activeViewRoundedY = Math.Round(viewRelativeCoordinates.Y, 2);

            return activeViewRoundedY + view.Frame.Height;
        }

        public static bool IsKeyboardOverlapping(this UIView activeView, UIView rootView, CGRect keyboardFrame)
        {
            var activeViewBottom = activeView.GetViewRelativeBottom(rootView);
            var pageHeight = rootView.Frame.Height;
            var keyboardHeight = keyboardFrame.Height;

            var isOverlapping = activeViewBottom >= (pageHeight - keyboardHeight);

            return isOverlapping;
        }
    }
}