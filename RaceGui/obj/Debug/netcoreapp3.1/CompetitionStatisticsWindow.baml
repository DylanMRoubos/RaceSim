   M S B A M L   `   `   `  ����B  >RaceGui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:RaceGuiRaceGui  5   	   @ <Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null,"clr-namespace:Model;assembly=ModelModel 5   	       RaceGui.StatisticsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:RaceGui     5   	   4Model"clr-namespace:Model;assembly=Model     5   	     � Title$  StatisticsWindow��5	   	   $	��453��6"   $	��286��6/    � DataContext 5
      $  RaceGui.CompetitionDataContext  5   
   .��5      � .+�5   
   �� -   5        $��SectionTimes��5      $��32,57,32,40q�6'   $
{�True��6<   ��6M   �� *DriverPointsList+ �� View 5      �� 5      .P�5      �� $
��Name��6%    �� Width$	 100��63    �� DisplayMemberBinding 6?   �� *Name+�� 5      $��Points��6%   $	 100��65    6A   �� *Points+5      5      �� 5   
   $��Competition:��6   $
��Left=�6'   $��	32,10,0,0q�6B   $	��Top=�6U   �� 5   
   $
��Left=�64   $��
148,13,0,0q�6O   $	��Top=�6c   ��6   �� *CompetitionName+6c   �� 5   
   $��Best Driver:��6   $
��Left=�6'   $��	32,31,0,0q�6B   $	��Top=�6U   �� 5   
   $
��Left=�6/   $��
148,28,0,0q�6J   $	��Top=�6^   ��6   �� *
BestDriver+6^   5      5      