   M S B A M L   `   `   `  ����=  9UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UI.ViewUI.View  5          UI.View.OrdersView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:UI.View     5        N� Title$  
OrdersView��5	      .��5      �� $��Stretch=�6   $��Stretch=�6/   .+�5   
   �� $��
Order List��6   $��Stretch=�6&   
������� �� TextAlignment$
 Left=�6Y   $��14(�6n   $
��Bold"�6|    �� Padding$ 	5,10,0,10q�6�   �� 5   
   -   5        $��1��5      $��Go to Add Order Page��69   $	��200��6X   
���/���$
��Bold"�6}   $��Right=�6�   $��0,5,10,5q�6�   $��0q�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	orderList��5         
IsReadOnly$
 True��6&      AutoGenerateColumns$ False��6P      RowHeaderWidth$ 0Q�6l   ��68   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6H   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 61   �� *Id+6H     5      $ DateTime��6%   $	 * 6T   
 67   �� *
DateTime+6T     5      $ Total Items��6%   $	 * 6Y   
 6:   �� *
TotalItems+6Y     5      $ Total Price��6%   $	 * 6T   
 6:   �� *Total+6T     5      $ State��6%   $	 * 6R   
 64   �� *	StateEnum+6R   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Action��6)   $	 * 69      CellTemplate 5      �� 5      .R�5      �� -   5        $��Details��5   %   $��0q�6V   5      5        5      $ Action��6*   $	 * 6:    5      �� 5      .R�5      �� -   5        $��Delete��5   %   $��0q�6K   5       5"      5$      5%      5&      