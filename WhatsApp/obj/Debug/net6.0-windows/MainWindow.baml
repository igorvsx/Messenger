   M S B A M L   `   `   `  ����C  ?WhatsApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WhatsAppWhatsApp  5   	       WhatsApp.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WhatsApp     5   	     � Title$  
MainWindow��5
   	   $	��380��6   $	��600��6)   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��2* �6   .+�5   
   �� $9��3Добро пожаловать в ватсап 2.0��6   $��Center=�69   $��Center=�6V   $��20(�6q   $��0��6   �� 5   
   -   5         ��Name$ UsernameBox��5      $��1��6'   $��25q�64   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$4 .Введите имя пользователя��6@   �� 5   
   -   5         ��Name$ 
NewChatBtn��5      $��2��6%   $��Создать чат��62   $��25q�6H   6T   �� 5   
   -   5        $ LoginBtn��5      $��3��6#   $E��?Подключиться к существующему чату��60   $��25q�6\   6h   �� 5   
   -   5        $ IPBox��5      $��4��6!   $��25q�6.   $+ %Введите IP адрес чата��6:   $��Center=�6q   5      5      