   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UI.ViewUI.View  5          UI.View.AddOrderItemView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:UI.View     5      -   5        $��AddOrderItemPage��5        N� Title$  AddOrderItemView��5	      .��5      �� .+�5   
   �� $��Add Order Item View��6   
�������$��14(�6F   $
��Bold"�6T    �� Padding$ 	5,10,0,10q�6f   � 5   
    � RowDefinitions 5      �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��Enter quantity:��6(    �� TextAlignment$ Center=�6@   $
��Bold"�6W   $��Italic$�6i   $ 	5,10,5,10q�6|   �� 5      -   5        $��txtQty��5   &   $��1��6   $��	5,10,5,10q�64   �� 5      -   5        $��2��5      $��Submit��6;   
���Gc��$��14(�6`   $
��Bold"�6n   $��	0,10,0,10q�6�   $��0q�6�   5      �� 5   
   $��Stock Items List��6   
�������$
��Bold"�6C   $ 5,5,0,5q�6U   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	itemsList��5         
IsReadOnly$
 True��6?      AutoGenerateColumns$ False��6Q      RowHeaderWidth$ 0Q�6m   ��6'   �� 	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$	
 SKU��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6I   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *Id+6I     5      $

 Item��6%   $ * 6L    63   �� *Name+6L     5      $
 Price��6%   $ * 6N    64   �� *Price+6N     5       $
 Stock Level��6%   $ * 6V    6:   �� *	InStock+6V   5"      �� 5#   
   -   5        $
��Exit��5#      
�������$��14(�6D   $
��Bold"�6R   $��	0,10,0,10q�6d   $��0q�6x   5$      5%      