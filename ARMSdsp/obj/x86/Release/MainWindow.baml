   M S B A M L   `   `   `  ���� B  >ARMSdsp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SDSPSDSP  1clr-namespace:SDSP.PropertiesSDSP.Properties  F BGUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH3clr-namespace:GUIControls.Tree;assembly=GUIControlsGUIControls.Tree >.clr-namespace:GUIControls;assembly=GUIControlsGUIControls     SDSP.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     $localclr-namespace:SDSP     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4
propertiesclr-namespace:SDSP.Properties     Dtree3clr-namespace:GUIControls.Tree;assembly=GUIControls     =ui.clr-namespace:GUIControls;assembly=GUIControls     -     � Title$,  &Просмотр данных СДСП��$
��1024��$	��481��$	��800��$	��300��$��Stretch=�$��Stretch=����#CED4DF$��Segoe UI)�$
��11pt(� � Icon$/ )/ARMSdsp;component/Resources/MAINICON.ico�� J� SnapsToDevicePixels . &   System.Windows.Media.TextOptions   TextFormattingMode$ Display=� � WindowStartupLocation$ CenterOwner=� � WindowState$ 	Maximized=� � 	Resources   SDSP.Properties.Settings   Settings   BooleanToVisibilityConverter �� 
TargetType �� Property	 �� Value��%�   &	        &	      '�� 
       �� �� " ��.���  � ��$	 Normal$���  � ��$	 Stretch=���  � ��$	 Stretch=���  � ��$	 Right=���  � ��$	 4 0 4 0q�.��� $��Stretch=�$��Stretch=�
 � RowDefinitions
 �� $
��Auto ��� $
��100* � � ColumnDefinitions �� .+��� -   $��border_page��$��1��$��1��$��1��$��1��$��0,0,0,0q����#DEE5EFa� $��0��$��0��$��0q�$��Stretch=�$��Stretch=�  GradientBrushForPanel#��[ .+���  �� Orientation$ Vertical=�.+�� 
 �� $
��Auto ��� $
��Auto ��� $
��Auto �$��0�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+�  GUIControls.DbSettings  $��0��$��2��$��0��$��4,2,2,2q�$��Center=� � ToolTip$/ )Настройки подключения�� � DataContext ��  �� Source# [  �� $��0��$��2��$��1��$��3��$��Stretch=�$��Stretch=�{�.   �� ResizeDirection$ Columns=�  CustomSplitterStyle#��[ �� $��0��$��2��$��2��$��3,0,10,0q�$
��Left=�$��Center=�$��Candara)�$��24(�$��Italic$�$
��Bold"�
������� �� TextAlignment$
 Left=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 1Q�   ShadowDepth$ 1Q�   Color$ 
WhiteSmoke���� $Z�TАРМ «Система дистанционного съёма показаний»���� $��0��$��3��$��5 4 5 0q�$��Stretch=�$	��Top=�$��Candara)�$��12(�$��Italic$�$��Light"�$ Right=��� $<�6© 2013, инженер отдела сбыта ОЭС���� $��1��$��3��$��5 0 5 3q�$��Stretch=�$��Bottom=�$��Candara)�$��12(�$��Italic$�$��Light"�$ Right=��� $,�&Трус Михаил Петрович��� -    �Name$ ToolBarGrid��{��� *IsConnected+ �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��100* �
 �� $
��Auto �.+��� $��0��$��0��$��10��$	��5 0q�$��Stretch=�$	��Top=����#DEE2EA$	��0.5q��� $��0��$��0��$��10��$	��5 2q�$��Stretch=�$	��Top=����#A0A0A0$	��0.5q��� $��0��$��2,0,2,0q�$��Center=�$ ��Подразделение��+  %GUIControls.Tree.ComboBoxWithTreeView  -     Name$ DepartamentsTree��$��1��$	��100��$��2,6,2,4q��� $��2��$��3��$��Stretch=�$��Stretch=�$ Columns=�#��[ �� $��3��$��2,0,2,0q�$��Center=�$��Профиль���� -    ��Name$ cmbProfiles��$��4��$	��100��$��2,6,2,4q� �� 
IsReadOnly .  �� IsTextSearchEnabled .   �� SelectedIndex$ 0���� $��5��$��3��$��Stretch=�$��Stretch=�$ Columns=�#��[ �� $��6��$��2,0,2,0q�$��Center=�$��Период��   GUIControls.DropDownButton  -     Name$ 
DatePeriod��$��7��$��2,6,2,4q� �� Text$# выберите период��"  GUIControls.PopupControlBase   DropDownContent � $��2q�
 �� �� �� ��  �� �� .+��� $��0��$��0��$3��-/ARMSdsp;component/Resources/start_period.png���� $��0��$��1��$	��2 2q�$ Center=��� $!�начало периода��(  "System.Windows.Controls.DatePicker  -     Name$ datePickerStart��$��1��$��0��$��2��$	��2 2q�$��Stretch=�    FirstDayOfWeek$  Monday=�!   SelectedDateFormat$! Short=��� $��2��$��0��$1��+/ARMSdsp;component/Resources/end_period.png���� $��2��$��1��$	��2 2q�$ Center=��� $�конец периода��  -   $ datePickerEnd��$��3��$��0��$��2��$	��2 2q�$��Stretch=�$  Monday=�$! Short=��� -	   " ��Name$" 	btnExport��$��9��$��3��$	��100��$��0,6,5,4q�$��Right=�$��Stretch=����� *CommandExportData+.���� $��2 0 2 0q�$ 
Horizontal=�.+��� $��Resources/Excel-icon.png���� �� $�Экспорт��   $ 8Q�#   	Direction$	# -90Q�$   RenderingBias$$ Performance=�$ 1Q��� -
   $��border_content��$��1��$��1��$��1��$��1��$��10 10q�$��Stretch=�$��Stretch=�#��� ��   $ 10Q�$	# -90Q�$$ Performance=�$ 2Q��� $��1��$��1��$��15 15q�$
��Auto=�$
��Auto=�.��� -   $��
ReportGrid��$��Stretch=�$��Stretch=�