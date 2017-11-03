# Magic-adventure_Side-Scrolling-Game
<pre>
Magic adventure is a side-scrolling shooting game. 
I also practice concept of object-oriented programming on this project.

Language:Visual C++ </pre>
# Illustration:
<pre>
一開始會有首頁，按下start後遊戲開始。遊戲畫面有兩個區塊，上部是遊戲區，下部是狀態區。Z鍵是普通攻擊，x鍵是強力攻擊。
打倒怪物會獲得經驗值和掉落物。當經驗值條集滿時可以升等，改變自機的彈幕類型，
並且在戰鬥中會遇到不同屬性的區域，會改變子彈的路徑(如有浮力區、引力區、反彈區等等)。
</pre>
#New_version_UI:

![image](https://github.com/marsii1017/Magic-adventure_Side-Scrolling-Game/blob/master/Resources/magic_adventure.PNG)

Old_version_UI_:

<img src="https://github.com/marsii1017/Magic-adventure_Side-Scrolling-Game/blob/master/Resources/UI-1.jpg" width="500"> 

# Architecture:
![image](https://github.com/marsii1017/Magic-adventure_Side-Scrolling-Game/blob/master/Resources/Architecture%20of%20project.PNG)

# analysis:
<pre>
(i)系統區:
		先初始化系統(載圖)。
有一個main function，程式執行後先到這個副程式，顯示開始畫面，等待玩家按下start。
		Start之後，跳到game 初始化，開始遊戲。
		有一個系統副程式，偵測到遊戲結束(玩家血歸零)，關閉基本timer，顯示遊戲結束，等到玩家按下menu按鈕，跳回main。

	

(ii)基本timer:
		重複跑遊戲中所有的副程式。1/1000秒執行一次。

(iii)繪圖:
		使用Form1.Paint function。其下有很多副程式(比較乾淨)，分別負責不同物件的繪圖，方法是把指標丟過去這些副程式。
		每個繪圖副程式會偵測每個物件是否有被啟動，決定要不要把它畫出來。

(iv)碰撞:
		用迴圈去跑會發生碰撞關係的物件。先計算各物件的有效範圍，再判斷兩物件是否也碰撞，若有執行結果。

(v)物件:
(a)player: 射擊、初始化player子彈、邊界偵測、狀態。
(b)monster: monster初始化、射擊、初始化monster子彈、邊界偵測、狀態、行動(AI)。
(c)bullet:邊界偵測、行動、刪除子彈。
(d)zone: zone初始化、邊界偵測、區域效果影響(effect)狀態、行動。
(e)item: item初始化、邊界偵測、行動。

(v-ii)說明:
(a)	射擊:針對monster和player，決定射擊的時機(玩家:鍵盤；怪物:時間)
(b)	初始化子彈:設定彈幕的形式。
(c)	邊界偵測:當物件超出螢幕範圍就把他無效化。
(d)	狀態:觀測玩家和怪物的狀態做出一些反應。
(e)	行動:主要是改變位置。而boss在這裡有寫一點AI。
(f)	刪除子彈:因為無效化子彈要改變的參數比較多，故合併。(其實其他的物件應該也要比照辦理，但因為其他物件的參數影響不大就沒有)
(g)	區域效果影響:偵測子彈是否進入區域內，並改變子彈的軌跡(速度)
   
   (vi)操控:
偵測鍵盤的keydown進行角色控制。由於keydown事件一瞬間只能偵測一個鍵，所以透過操作變數達到目的。
例如:方向鍵”上”按下bool變數control[1]=trueif control[1]==true
自機向上移動。

  (vii)腳本timer:
		關卡的腳本，依時間產生怪物和區域。
	
(viii)展示(display):
		吃到寶石的時候暫時出現＂＋１００＂的字樣，負責那一類的功能。


</pre>
