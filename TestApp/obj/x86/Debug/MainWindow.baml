   M S B A M L   `   `   `  ����B  >TestApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TestAppTestApp  5   	   F BGUIControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:GUIControls;assembly=GUIControlsGUIControls 5   	   7 clr-namespace:TestApp.PropertiesTestApp.Properties  5   	   H3clr-namespace:GUIControls.Tree;assembly=GUIControlsGUIControls.Tree 5   	       TestApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'localclr-namespace:TestApp     5   	   =my.clr-namespace:GUIControls;assembly=GUIControls     5   	   7
properties clr-namespace:TestApp.Properties     5   	   Dtree3clr-namespace:GUIControls.Tree;assembly=GUIControls     5   	     � Title$  
MainWindow��5   	   $	��887��5	   	   $	��489��5
   	    � 	Resources 5      !  TestApp.Properties.Settings   Settings��%   &	          5   
   6   5      .��5      � .+�5   
      GUIControls.DropDownButton  -   5        $��dropDownButton1��5      $	��345��5      $��20��5      $��	12,12,0,0q�5      $
��Left=�5      $	��Top=�5        GUIControls.DbSettings  5   
   -   5        $��dbSettings1��5      $��
378,38,0,0q�5      $
��Left=�5      $	��Top=�5       � ToolTip$/ )Настройки подключения��5       � DataContext 5      ��  �� Source# [  5      �� 5   
   -   5        $��	comboBox1��5      $	��308��5      $��28��5       $��	12,38,0,0q�5!      $
��Left=�5"      $	��Top=�5#      &   System.Windows.Controls.Calendar  5$   
   -   5        $��	calendar1��5$      $	��205��5%      $	��172��5&      $��
428,12,0,0q�5'      $
��Left=�5(      $	��Top=�5)        MyCalendarStyle#��[ 5*      +  %GUIControls.Tree.ComboBoxWithTreeView  5+   
   -   5        $��comboBoxWithTreeView1��5+   $   $	��120��5,   $   $��23��5-   $   $��	12,72,0,0q�5.   $   $
��Left=�5/   $   $	��Top=�50   $   ,  &GUIControls.Tree.TreeViewWithRowSelect  51   
   $	��332��6%   $	��312��52   %   $��
49,114,0,0q�53   %   $
��Left=�54   %   $	��Top=�55   %     TreeViewWithRowSelectStyle#��[ 56   %   .@�57      Q� $��Первый��6    Q� 
IsExpanded . .@�58      Q� $��Первый 1��6   Q� 59      $��Первый 2��6    . .@�5:      Q� $��Один��6#   Q� 5;      $��Два��6#   Q� 5<      $��Три��6#   5=      Q� 5>      $K��EПервый с длинннннннннннным названием��6   Q� 5?      $��Первый 4��6   5@      Q� 5A      $��Второй��6   Q� 5B      $��Третий��6    . .@�5C      Q� $��Один��6   Q� 5D      $��Два��6   Q� 5E      $��Три��6    . .@�5F      Q� $��Три��6#   Q� 5G      $F��@Дваааааааааааааааааааааааааааааа��6#   Q� 5H      $��Один��6#   5I      Q� 5J      $��Четыре��6   5K      5L      �� 5M   
   -   5        $��button1��5M   ^   $��Button��6   $��30��6"   $
��Left=�6.   $��466,219,0,0q�6I   $	��Top=�6m   $	��130��6�   6�   �� 5N   
   -   5        $��button2��5N   ^   $��Button��6   $��36��6"   $
��Left=�6.   $��466,270,0,0q�6I   $	��Top=�6m   $	��134��6�   5O      5P      