using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Btx.Mobile.CustomRenders
{
    public class ScrollListViewRender : ListView
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListView"/> class.
        /// </summary>
        public ScrollListViewRender() : base(ListViewCachingStrategy.RetainElement)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListView"/> class.
        /// </summary>
        /// <param name="strategy">The caching strategy to use.</param>
        public ScrollListViewRender(ListViewCachingStrategy strategy) : base(strategy)
        {
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Event that is raised after a scroll completes.
        /// </summary>
        public event EventHandler<ScrolledEventArgs> Scrolled;

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to be called after a scroll completes.
        /// </summary>
        /// <param name="args">The scroll event arguments.</param>
        public void OnScrolled(ScrolledEventArgs args)
        {
            Scrolled?.Invoke(this, args);
        }

        #endregion
    }
}
