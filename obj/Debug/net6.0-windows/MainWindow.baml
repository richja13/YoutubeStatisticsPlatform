   M S B A M L   `   `   `  ����N  JChannelInfoPlatform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:ChannelInfoPlatformChannelInfoPlatform  5   	   M+clr-namespace:ChannelInfoPlatform.ViewModelChannelInfoPlatform.ViewModel  5   	   $    ChannelInfoPlatform.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:ChannelInfoPlatform     5   	   A	viewmodel+clr-namespace:ChannelInfoPlatform.ViewModel     5   	     � Title$  
MainWindow��5	   	   $	��600��6   $
��1000��6)    � WindowStyle$
 None=�5
   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      1  +ChannelInfoPlatform.ViewModel.MainViewModel  5   
   .��5   
   �� ��5   
   �� 5       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,2&�63   .��5      �  � Color$ #823038��6    � Offset$	 0.0Q�6/   � 5      $ #DEBA6F��6   $ 1Q�6/   5       J� Clip 5   
   �� 5      	 �� RadiusX$	 45Q�6    
 �� RadiusY$
 45Q�6-    �� Rect$ 0,0,1000,600�6:   ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5       � RowDefinitions 5!      �� 5"      $��80 �6    �� 5#      .+�5%      �� $��	0,15,15,0q�6A   $��1��6T   $	��Top=�6d   $��Right=�6|   $��0q�6�   ���#AEECEF
������ $��23(�6�   ��6   �� *CloseApplicationCommand+.��6�   X6�   �� 5&      $��2��6   ���#823038$��0,0,2,0q�6<   ._�5'      �� .+�5(      �� $��	0,20,0,35q�6    $��Pixel History��63   $��Center=�6H   $��Center=�6c   
�������$��23(�6�   	� 5*       d� 	IsChecked$
 TrueY�6F      
MenuButton#��[  6W   $
��Home��6{   $��50��6�   
�������$��14(�6�   ��6"   �� *HomeViewCommand+6�   	� 5+      #��[  6L   $��
Statistics��6p   $��50��6�   
�������$��14(�6�   ��6"   �� *StatisticsViewCommand+6�   	� 5,      #��[  6H   $��Videos��6l   $��50��6}   
�������$��14(�6�   ��6"   �� *VideosViewCommand+6�   5-      5.      �� 5/        TextBoxTheme#��[ 6   $	��250��6<   $��40��6H   $��Center=�6T   $
��left=�6o   $��5q�6�   $��1��6�   �� 51      $��1��6   $��1��6*   $��10q�6:   ��6F   �� *CurrentView+53      54      55      