   M S B A M L   `   `   `  ���� F  BGUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GUIControls.TreeGUIControls.Tree  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0894    .GUIControls.Tree.LeftMarginMultiplierConverter   
leftMargin     Length  HoverItemColor  ListItemHoverFill �� Color  ListItemSelectedFill   ListItemSelectedInactiveFill  ListItemSelectedHoverFill  SelectedItemForegroundColor  TreeViewItemFocusVisual �� Property �� Value J� Opacity �� StrokeDashArray  ExpandCollapseToggleStyle �� 
TargetType �� 
TargetType �� Padding	 A�Name
 �� Triggers P� Property P� Value �� 
TargetName ]� 
Conditions �� Property �� Value "	 TreeViewWithRowSelectItemStyle �� Path �� RelativeSource �� AncestorType ��Name �� CornerRadius J� SnapsToDevicePixels �� 	Converter � ColumnDefinitions d�Name �� 	ClickMode d� 	IsChecked ��Name ��Name P� 
SourceName �� 
SourceName  �� BasedOn 
 TreeViewWithRowSelectStyle,   &GUIControls.Tree.TreeViewWithRowSelect�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     %myclr-namespace:GUIControls.Tree  %�  &	        &	      &	 !     &	 -     &	 B     &	 W     &	 l     &	 �     &	 �     &		 <    7  7   7  7  7  7  7  7  7  'Q� �    7 	 &	
 �       $  18Q���	#FFA2BCDC���� # �  �� $ 	#FF6180A5���� $ 	#FFC2C7D1���� $ 	#FF7497C0���� $ 	#FFFAFAFA���� .���  � �� �� .R��� ._��� $��0,0,0,0q�$ 0Q�
���   �$	 1 2R�$��5���� " d�.���  � |� .  ��  � ��$ 16����  � ��$ 16����  � �� �� " d�.R��� $��16��$��16�����	#00FFFFFF$��1q�$ 0q�._�� .+�A� $	 
ExpandPath��$��10��$��10��$��0q�$��Center=�$��Center=�#��z��F� $
��Plus��$��10��$��10��$��Center=�$��Center=�#��z����B� $��M5,2L5,8C�F� $��Minus��$��10��$��10��$��Center=�$��Center=�#��z����B� $��M2,5L8,5C�
 P�  �@J�IsMouseOver . . ��� $ 
ExpandPath�� � �� �	#FF18E4FF�� $
 Plus�� � �� �	#FF18E4FF�� $ Minus�� � �� �	#FF18E4FF�� $ 
ExpandPath�� � ��$ 12���� $ 
ExpandPath�� � ��$ 12���� $
 Plus�� � �� ��@KL @B �@KL @T� 	�� $ Minus�� � �� ��@B @KL �@T� @KL 	P�  �@d�	IsChecked$
 TrueY�. ��� $
 Plus�� � y�$ Hidden=�]�  ��  �@d�	IsChecked$
 TrueY���  �@J�IsMouseOver . ./��� $ 
ExpandPath�� � �� �	#FF18E4FF�� $ 
ExpandPath�� � ��$ 12���� $ 
ExpandPath�� � ��$ 12���� $ Minus�� � �� ��@B @KL �@T� @KL 	�� " Q�.���  � �� �� $  HorizontalContentAlignment� �� " ����  � �� �� $ VerticalContentAlignment� �� " ����  � ��
 ���� ��  � ��$ 1,0,0,0q���  �@�FocusVisualStyle8   ��  � �� �� " Q�.R��� .+��� $ Bd��$��0q�$��Stretch=�#��z��#��z��$	��1.0q�$ 2��# z�� . ._�� ���� 8   �� *TemplatedParent+ �� $��18 ��� .+�d� $ Expander��$ Press=�8�� 8��  �� $ 
IsExpanded� �� *TemplatedParent+�� $ PART_Header��$��1��$��0,2,2,2q�#��z��$��Header���� $ 	ItemsHost��
 P�  �@Q�
IsExpanded .  . ��� $ 	ItemsHost�� � y�$ 	Collapsed=�P�  �@��HasItems .  . ��� $ Expander�� � y�$ Hidden=�]�  ��  � �� .  ��  � ��$
 Auto��./��� $ PART_Header�� � ��$ 75��]�  ��  � �� .  ��  � ��$
 Auto��./��� $ PART_Header�� � ��$ 19��P� $ Bd�� �@J�IsMouseOver . . ��� $ Bd�� � �� �	#FFFFFFFF�� $ Bd�� � �� �	#FFAAAAAA�� $ Bd�� � ��$	 1.0q�P�  �@Q�
IsSelected . . ��� $ Bd��8   � ���� $ Bd�� � �� �	#FF84acdd�� $ Expander��8   � ���� 8   � ��]�  ��  �@Q�
IsSelected . ��  �@��IsSelectionActive .  ./��� $ Bd��8   � ���� $ Bd�� � �� �	#FFd9d9d9��  � ��
 �   �]�  ��  �@Q�
IsSelected . �� $ Bd�� �@J�IsMouseOver . ./��� $ Bd��8   � ���� $ Bd�� � �� �	#FF7da2ce��  � ��
 �����P�  � {� .  . ��� # � �� � ���� 8    " Q��� "  .���  �@�OverridesDefaultStyle . ��  � ��$ 50����  � ��$ 30����  � ��$
 Auto=���  � ��$
 Auto=���  � �� �� "  .R��� $��0q�$��Stretch=�$��Stretch=�#��z��#��z��#��z��$ 5��# z�� . ._��� ��.  |�.  $��4q�.���� 