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
        private Rectangle _currentRectangle;

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

            if (_keyboardChangeFrameObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_keyboardChangeFrameObserver);
                _keyboardChangeFrameObserver.Dispose();
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
            if (!IsViewLoaded)
                return;

            var pageFrame = Element.Bounds;
            var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
            var keyboardHeight = keyboardFrame.Height;
           

            var newY = CalculateShiftByAmount(pageFrame.Height, keyboardHeight, _activeViewBottom);

            Debug.WriteLine($"Change Active Bottom: {_activeViewBottom}");
            Debug.WriteLine($"Chante Y: {newY}");

            Element.LayoutTo(new Rectangle(pageFrame.X, newY,
               pageFrame.Width, pageFrame.Height));


        }

        private void ShiftPageUp(nfloat keyboardHeight, double activeViewBottom)
        {
            var pageFrame = Element.Bounds;

            var newY = pageFrame.Y + CalculateShiftByAmount(pageFrame.Height, keyboardHeight, activeViewBottom);

            Element.LayoutTo(new Rectangle(pageFrame.X, newY,
                pageFrame.Width, pageFrame.Height));

            Debug.WriteLine($"Shift Active Bottom: {activeViewBottom}");
            Debug.WriteLine($"Shift Up Y: {newY}");

            _pageWasShiftedUp = true;
        }

        private void ShiftPageDown(nfloat keyboardHeight, double activeViewBottom)
        {
            var pageFrame = Element.Bounds;

            var newY = pageFrame.Y - CalculateShiftByAmount(pageFrame.Height, keyboardHeight, activeViewBottom);

            Element.LayoutTo(new Rectangle(pageFrame.X, newY,
                pageFrame.Width, pageFrame.Height));

            _pageWasShiftedUp = false;
        }

        private double CalculateShiftByAmount(double pageHeight, nfloat keyboardHeight, double activeViewBottom)
        {
            return (pageHeight - activeViewBottom) - keyboardHeight;
        }
    }
}