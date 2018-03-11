//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

//namespace ZzaDashboard
//{
//    public static class ViewModelLocator
//    {

//        //Use 'propa' snippet to create dependency objects 
//        public static bool GetAutoWireViewModel(DependencyObject obj)
//        {
//            return (bool)obj.GetValue(AutoWireViewModelProperty);
//        }

//        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
//        {
//            obj.SetValue(AutoWireViewModelProperty, value);
//        }

//        // Using a DependencyProperty as the backing store for AutoWireViewModel.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty AutoWireViewModelProperty =
//            DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

//        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//        {
//            if (DesignerProperties.GetIsInDesignMode(d))
//                return;

//            //get current view type/name
//            var viewType = d.GetType();
//            var viewTypeName = viewType.FullName;

//            //then get type of view model (just append model to name b/c it follows convention)
//            var viewModelTypeName = viewTypeName + "Model";
//            var viewModelType = Type.GetType(viewModelTypeName);

//            var viewModel = Activator.CreateInstance(viewModelType);
//            ((FrameworkElement)d).DataContext = viewModel;  
//        }
//    }
//}
