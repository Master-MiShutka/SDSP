   M S B A M L   `   `   `  ���� F  BGUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:GUIControlsGUIControls  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TextBoxBorder  �� EndPoint � MappingMode �� 
StartPoint � Color � Offset  TextBox_MouseOver  TextBox_Focused  TextBox_DisabledBorder �� Color  TextBox_DisabledBackground  DefaultWatermarkTemplate"    GUIControls.WatermarkTextBox �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name �� CornerRadius J� Opacity ��Name J� SnapsToDevicePixels ��Name    	Watermark    WatermarkTemplate J� IsHitTestVisible �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName P� Property P� Value�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     #localclr-namespace:GUIControls  %   &	        &	 �     &	 �     &	 i    &	 |    &	 �    '   �    7   7  7  7  7  7  �� $
  0,20&�$ Absolute=�$	 0,0&�.��� $ #ABADB3��$
 0.05Q�� $ #E2E3EA��$
 0.07Q�� $ #E3E9EF��$ 1Q��� $	  0,1&�$	 0,0&�.��� $ #5794BF��$
 0.05Q�� $ #B7D5EA��$
 0.07Q�� $ #C7E2F1��$ 1Q��� $	  0,1&�$	 0,0&�.��� $ #3D7BAD��$
 0.05Q�� $ #A4C9E3��$
 0.07Q�� $ #B7D9ED��$ 1Q��� $ #ADB2B5���� $ #F4F4F4���� .R��� 
�������|�.  ���� �� "   .���  � ��# � ����  � ��# � ����  � ��8   ��  � ��$ 1q���  � ��$ 3q���  �@J�	AllowDrop . ��  �@�FocusVisualStyle Z� ��  �@��PanningMode$ VerticalFirst=���  �@��IsFlicksEnabled .  ��  �@  WatermarkTemplate8  ��  � �� �� "	   .R�� .+��� $
 Border��#��z��#��z��$ 1��#��z���� $
 MouseOverVisual��$ 0Q�#��z��8�� $ 1���� $
 FocusVisual��$ 0Q�#��z��8�� $ 1���� $ PART_ContentHost��# z �� $ PART_WatermarkHost��#��z #��z #��z��#��z�� .  #��z��$y�	Collapsed=� ]�  ��  �@J�	IsFocused .  ��  � ��$  ��/��� $ PART_WatermarkHost�� � y�$ Visible=�P�  �@J�IsMouseOver . . ��� $ MouseOverVisual�� �@J�Opacity$ 1Q�P�  �@J�	IsFocused . . ��� $ FocusVisual�� �@J�Opacity$ 1Q�P�  � {� .  . ��� $ Border��8   � ���� $ Border��8   � ���� # � �� � ��