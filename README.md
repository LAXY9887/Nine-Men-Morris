# Nine-Men-Morris

本頁面紀錄由兩位作者共同開發之九子棋遊戲的成果展示。本遊戲使用 Unity 進行開發。

<div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/icon%20design.png" width=350 height=350>
</div>

## 作者

企劃/程式：Angelcemept

美術/動畫：黃勇勝

## 更新

2024.05.11 建立 Github 頁面, 輸出遊戲 Demo 執行檔。遊戲仍在開發中。

## 工作事項

- [ ] 完善所有功能
- [ ] 上架 Steam 平台
- [ ] 更新連線遊戲功能

## 遊戲設計

遊戲標題：九子棋 (Nine Men’s Morris)

核心玩法：依照九子其規則

遊戲體驗：

 1. 有背景音樂, 下棋音效
 3. 有動態效果演出
 4. 可以跟不同難度的電腦對戰
 5. 可以與網路上的玩家連線對戰, 並記錄戰績和排行

遊戲規則：

1. 兩名玩家執黑/白棋輪流行動, 每人 9 顆棋子。(黑棋先手)雙方輪流將棋子擺在一 7 x 7 棋盤 (圖一)的交點，或是移動棋子。

   <div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/board%20v2.png" width=350 height=350>
   </div>
   
   <p align='center'>圖一、棋盤</p>
   
2. 玩家的目標為將自己的三顆棋子連成 “磨” (Mill) (圖二), 當作成 Mill 時可以移除對手的一顆棋子。

   <div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/Explaination.png" width=350 height=350>
   </div>

   <p align='center'>圖二、磨 (Mill)</p>
   
3. 第一階段, 雙方輪流下棋並嘗試吃掉對手的棋子，或是布局阻礙對手。當雙方用完手上的棋子, 且遊戲尚可以進行，則進入第二階段。

4. 第二階段, 玩家可以移動自己的棋子到相鄰的空交點。並嘗試吃掉對手的棋子，或是將對手的路線堵死。

*※ 遊戲勝利條件為：對手剩下不到3顆棋子或是無路可走。*

*※ 不能移除對手Mill中的棋子, 除非別無選擇。*

*※ 當一次形成 1 個以上的Mill也只能移除一顆棋子。*

特殊規則：
1. 當一方只剩下 3 顆棋子，在第二階段時可以移動至任意地方。(可以到處飛)
2. 當一方只剩下 3 顆棋子而且另一方只剩下 4 顆棋子，戰況將陷入膠著，因此當達成這個條件時將倒數 10 次移動，用盡移動數後仍無法決勝，則平手。

## 美術/動畫設計

<div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/main%20title%20v2.png">
</div>
   
<p align='center'>圖三、遊戲封面</p>

<div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/mainGame%20v2.png">
</div>
   
<p align='center'>圖四、遊戲主畫面</p>

<div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/menus%20pannel.png">
</div>
   
<p align='center'>圖五、遊戲設定選單</p>

<div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/menu%20rules%20design.png" width=500 height=300>
</div>
   
<p align='center'>圖六、遊戲規則選單</p>

<div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/menu%20credit%20design.png" width=500 height=300>
</div>
   
<p align='center'>圖七、製作人員選單</p>

## 動態特效演示

1. 落子、回合提示

<div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/performance-b1.gif" width=800 height=450>
</div>

2. 連線、吃子提示

<div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/performance-b2.gif" width=800 height=450>
</div>

3. 進入第二階段提示

<div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/performace-b3.gif" width=800 height=450>
</div>

4. 遊戲結束提示

<div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/performance-b4.gif" width=800 height=450>
</div>

## 遊戲核心

在正式使用 Unity 進行開發之前，我先使用 C# 寫了一個能在 Console 運行的文字介面，先完善遊戲規則和流程然後才建立 Unity 專案並加上美術素材和動態特效。
在 `GameCore_v1`中的`GameCore_v1.exe`是遊戲執行檔，可以用終端機開啟遊玩。另有提供遊戲核心程式碼`GameCore_v1_CS_project`資料夾內。

<div align='center'>
     <img src="https://github.com/LAXY9887/Nine-Men-Morris/blob/main/Image%20Assets/game%20core.png">
</div>
   
<p align='center'>圖八、遊戲核心</p>
