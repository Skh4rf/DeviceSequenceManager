﻿#pragma checksum "..\..\..\AddDeviceUserControl\AddDeviceUserControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5BFA5A119CE93D9C3AE06D3134AB58B0037594BD9F2B59B87EDDAE34DCDBA09D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DeviceSequenceManager;
using DeviceSequenceManager.MVVM;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DeviceSequenceManager {
    
    
    /// <summary>
    /// AddDeviceUserControl
    /// </summary>
    public partial class AddDeviceUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\AddDeviceUserControl\AddDeviceUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DeviceSequenceManager.AddDeviceUserControl userControl;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\AddDeviceUserControl\AddDeviceUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost AddDeviceDialog;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\AddDeviceUserControl\AddDeviceUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxIP;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\AddDeviceUserControl\AddDeviceUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxPort;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\AddDeviceUserControl\AddDeviceUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxSocket;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\AddDeviceUserControl\AddDeviceUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxDeviceType;
        
        #line default
        #line hidden
        
        
        #line 201 "..\..\..\AddDeviceUserControl\AddDeviceUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ButtonAccept;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ChromaProcedureManager;component/adddeviceusercontrol/adddeviceusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddDeviceUserControl\AddDeviceUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.userControl = ((DeviceSequenceManager.AddDeviceUserControl)(target));
            return;
            case 2:
            this.AddDeviceDialog = ((MaterialDesignThemes.Wpf.DialogHost)(target));
            return;
            case 3:
            this.TextBoxIP = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TextBoxPort = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.ComboBoxSocket = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.ComboBoxDeviceType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.ButtonAccept = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

