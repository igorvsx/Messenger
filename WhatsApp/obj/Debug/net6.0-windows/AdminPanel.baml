   M S B A M L   `   `   `  ����C  ?WhatsApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WhatsAppWhatsApp  5   	       WhatsApp.AdminPanel   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WhatsApp     5   	     � Title$  
AdminPanel��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      $��2* �6   �� 5       � ColumnDefinitions 5   
   �� 5      $��2* �6   �� 5      �� 5      $��2* �6   .+�5   
   �� $*��$Входящие сообщения:��6   $��20(�6/   $��5q�6=   $��Center=�6H   $��Center=�6c   $��0��6�   �� 5   
   -   5         ��Name$ MessagesLbx��5      $��1��6'   �� 5   
   $��Пользователи��6   $��20(�6(   $��5q�66   $��Center=�6A   $��Center=�6\   $��0��6y   $��1��6�   �� 5   
   -   5        $ 	ClientLbx��5      $��1��6%   $��1��62   �� 5   
   -   5        $ LogLbx��5      $��1��6"   $��2��6/   �� 5   
   -   5         ��Name$ 
MessageTxt��5      $��2��6&   $��15q�63   �� 5   
   $��Логи��6   $��20(�6    $��5q�6.   $��Center=�69   $��Center=�6T   $��0��6q   $��2��6~   �� 5   
   -   5         ��Name$ 
SendMsgBtn��5      $��Отправить��6%   $��16(�69   $��1��6G   $��2��6W   $��2��6k   6x   5      5      