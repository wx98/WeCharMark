   M S B A M L   `   `   `  ����
  Client    Client.SubjectManage   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  课程管理��5   	   $	��300��6   $	��550��6#    � WindowStartupLocation$ CenterScreen=�6/   .��5      �  � RowDefinitions 5      �� 5   
   $��* �6   �� 5   
   $��30 �6   .+�5
      &   System.Windows.Controls.DataGrid  -   5          Name$ mGrid��5
      $��0��6      AutoGenerateColumns .     SelectionMode$ Single=�6G      
IsReadOnly .    Columns 5   
   0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��69   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 50  6E   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6!   �� *sID+6E     5      $ 课程��6>   
 6!   �� *
sSubject+6>   5      �� 5       �� Orientation$ 
Horizontal=�6   $��1��6*   .+�5   
   �� $��课程��6   $��50��6   $��Center=�6)   $��10,0,0,0q�6E   �� 5   
   -   5         ��Name$ 
txtSubject��5      $	��150��6&   $��Center=�62   �� 5   
   -   5         ��Name$ btAdd��5      $��增加��6#   $��50��60   $��10,0,0,0q�6;   $��Center=�6M   6h   �� 5   
   -   5        $ btUpdate��5      $��更新��6#   $��50��61   $��10,0,0,0q�6<   $��Center=�6N   6j   �� 5   
   -   5        $ btDelete��5      $��删除��6#   $��50��61   $��10,0,0,0q�6<   $��Center=�6N   6j   5      5      5      