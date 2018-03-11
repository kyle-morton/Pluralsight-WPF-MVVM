//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Interactivity;

//namespace ZzaDashboard
//{
//    public class ShowNotificationMessageBehavior : Behavior<ContentControl>
//    {

//        #region DEP PROPS

//        public string Message
//        {
//            get { return (string)GetValue(MessageProperty); }
//            set { SetValue(MessageProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty MessageProperty =
//            DependencyProperty.Register("Message", typeof(string), typeof(ShowNotificationMessageBehavior), new PropertyMetadata(null, OnMessageChanged));

//        #endregion

//        #region EVENTS

//        private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//        {
//            //cast incoming behaviro to ShowNotificationMessageBehavior
//            var behavior = ((ShowNotificationMessageBehavior)d);
//            behavior.AssociatedObject.Content = e.NewValue;
//            behavior.AssociatedObject.Visibility = Visibility.Visible;
//        }

//        /// <summary>
//        /// on associating this class w/ incoming behavior, 
//        /// attach click handler to dismiss message
//        /// </summary>
//        protected override void OnAttached()
//        {
//            AssociatedObject.MouseLeftButtonDown += (s, e) =>
//            {
//                AssociatedObject.Visibility = Visibility.Collapsed;
//            };
//        }

//        #endregion


//    }
//}
