﻿using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Microsoft.Maui.Controls
{
	public partial class View
	{
		public static void MapIsInAccessibleTree(IViewHandler handler, View view)
		{
			// If the user hasn't set IsInAccessibleTree then just don't do anything
			if (!view.IsSet(AutomationProperties.IsInAccessibleTreeProperty))
				return;

			var Control = handler.NativeView as UIView;

			// iOS sets the default value for IsAccessibilityElement late in the layout cycle
			// But if we set it to false ourselves then that causes it to act like it's false

			// from the docs:
			// https://developer.apple.com/documentation/objectivec/nsobject/1615141-isaccessibilityelement
			// The default value for this property is false unless the receiver is a standard UIKit control,
			// in which case the value is true.
			//
			// So we just base the default on that logic				
			var _defaultIsAccessibilityElement = Control.IsAccessibilityElement || Control is UIControl;

			Control.IsAccessibilityElement = (bool)((bool?)view.GetValue(AutomationProperties.IsInAccessibleTreeProperty) ?? _defaultIsAccessibilityElement);
		}

		public static void MapExcludedWithChildren(IViewHandler handler, View view)
		{
			if (!view.IsSet(AutomationProperties.ExcludedWithChildrenProperty))
				return;

			var Control = handler.NativeView as UIView;

			var _defaultAccessibilityElementsHidden = Control.AccessibilityElementsHidden || Control is UIControl;
			Control.AccessibilityElementsHidden = (bool) ((bool?)view.GetValue(AutomationProperties.ExcludedWithChildrenProperty) ?? _defaultAccessibilityElementsHidden);
		}
	}
}