   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UI.ViewUI.View  5          UI.View.OrderDetailsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:UI.View     5        N� Title$  OrderDetailsView��5	      .��5      �� .+�5   
   �� $��Order Details View��6   
������� �� TextAlignment$
 Left=�6E   $��14(�6Z   $
��Bold"�6h    �� Padding$ 	5,10,0,10q�6z   �� 5   
   $��Order Information��6   
�������$
��Bold"�6D   $ 5,5,0,5q�6V   � 5   
   -   5         �Name$ orderInfoLine��5       � ShowGridLines$
 True��6&    � RowDefinitions 5      �� 5      6"   �� 5      6"    � ColumnDefinitions 5      �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   �� 5      $��* �6#   6/   .+�5      �� $��0��6   $��0��6(   $��Id��65   $ Center=�6?   $
��Bold"�6V   $ 0,5,0,5q�6h   �� 5      $��1��6   $��0��6(   $��DateTime��65   $ Center=�6E   $
��Bold"�6\   $ 0,5,0,5q�6n   �� 5      $��2��6   $��0��6(   $��Total Items��65   $ Center=�6H   $
��Bold"�6_   $ 0,5,0,5q�6q   �� 5      $��3��6   $��0��6(   $��Total Price��65   $ Center=�6H   $
��Bold"�6_   $ 0,5,0,5q�6q   �� 5      $��4��6   $��0��6(   $��State��65   $ Center=�6B   $
��Bold"�6Y   $ 0,5,0,5q�6k   �� 5       $��5��6   $��0��6(   $��Action��65   $ Center=�6C   $
��Bold"�6Z   $ 0,5,0,5q�6l   �� 5!      $��0��6   $��1��6(   $ Center=�6I   $ 0,5,0,5q�6a   ��65   �� *Id+6a   �� 5"      $��1��6   $��1��6(   $ Center=�6O   $ 0,5,0,5q�6g   ��65   �� *
DateTime+6g   �� 5#      $��2��6   $��1��6(   $ Center=�6Q   $ 0,5,0,5q�6i   ��65   �� *
TotalItems+6i   �� 5$      $��3��6   $��1��6(   $ Center=�6L   $ 0,5,0,5q�6d   ��65   �� *Total+6d   �� 5%      $��4��6   $��1��6(   $ Center=�6P   $ 0,5,0,5q�6h   ��65   �� *	StateEnum+6h   �� 5'      -   5        $��
btnProcess��5'   H   $��5��6   $��1��6%   $��Process��6Z   $y�Hidden=�6l   $��0,5,0,5q�6�   $��0q�6�   5(      �� 5)   
   $��Order Items��6   
�������$
��Bold"�6>   $ 5,5,0,5q�6P   &   System.Windows.Controls.DataGrid  5*   
   -   5          Name$ orderItemList��5*         
IsReadOnly$
 True��6*   	   AutoGenerateColumns$	 False��6T   
   RowHeaderWidth$
 0Q�6p   ��6<   ��    Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Header$	 SKU��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 62   �� *StockItemId+6R     5-      $
 Item��6%   $ * 6S    63   �� *Description+6S     5.      $ Price��6%   $ * 6N    64   �� *Price+6N     5/      $ Quantity��6%   $ * 6T    67   �� *
Quantity+6T     50      $ Total��6%   $ * 6N    64   �� *Total+6N   52      �� 53   
   -   5        $��Orders��53      
�������$��14(�6F   $
��Bold"�6T   $��	0,10,0,10q�6f   $��0q�6z   54      55      