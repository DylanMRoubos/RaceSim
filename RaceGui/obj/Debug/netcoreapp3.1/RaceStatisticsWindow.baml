   M S B A M L   `   `   `  ����B  >RaceGui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:RaceGuiRaceGui  5   	   @ <Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null,"clr-namespace:Model;assembly=ModelModel 5   	   "    RaceGui.RaceStatisticsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:RaceGui     5   	   4Model"clr-namespace:Model;assembly=Model     5   	     � Title$  RaceStatisticsWindow��5	   	   $	��600��6&   $
��1000��63    � DataContext 5
        RaceGui.RaceDataContext  5   
   .��5      � .+�5   
   �� -   5        $��SectionTimes��5      $��330,74,330,23q�6'   $
{�True��6>   ��6O   �� *SectionTimesList+ �� View 5      �� 5      .P�5      �� $
��Name��6%    �� Width$	 100��63    �� DisplayMemberBinding 6?   �� *Name+�� 5      $��Section��6%   $	 100��66    6B   �� *Section.SectionType+�� 5      $
��Time��6%   $	 100��63    6?   �� *Time+5      5      �� 5   
   -   5        $��DistanceDriven��5      $��50,74,711,23q�6)   $
{�True��6?   ��6P   �� *DistanceDrivenList+ 5      �� 5      .P�5      �� $
��Name��6%   $	 100��63    6?   �� *Name+�� 5      $��Distance��6%   $	 100��67    6C   �� *
Distance+5      5      �� 5!   
   -   5        $��BrokenDownAmount��5!      $��723,74,49,23q�6+   $
{�True��6A   ��6R   �� *BrokenDownAmountList+ 5"      �� 5#      .P�5$      �� $
��Name��6%   $	 100��63    6?   �� *Name+�� 5%      $��Broken Amount��6%   $	 100��6<    6H   �� *BrokenDownAmount+5&      5(      �� 5)   
   $��Drivers distance driven��6   $
��Left=�62   $��
104,43,0,0q�6M   $	��Top=�6a   �� 5*   
   $��Drivers section times��6   $��Center=�60   $��0,43,0,0q�6M   $	��Top=�6_   �� 5+   
   $ ��Drivers broken down amount��6   $
��Left=�65   $��
755,43,0,0q�6P   $	��Top=�6d   5-      5.      