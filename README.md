# XR_project3

玩法說明

1. 以傳統五子棋為基礎，此遊戲採用四子棋規則，勝利方式：一條四子連線(橫、豎、斜皆可)
2. 遊戲為雙人連線對戰模式，目前固定玩家1為黃色，玩家2為藍色(先進入遊戲的玩家為玩家1)
3. 大棋盤：
	3.1 在3D的大棋盤中，所有棋子都將投影到六個面，所以實際上是在六個2D的棋盤上下棋
	3.2 大棋盤在雙方玩家的正中間，若沒有棋子靠近是不會顯示的
	3.3 當有棋子靠近時，會有黃色的小方塊做提示，代表可以下棋的位置
4. 小棋盤：
	4.1 為了方便玩家看清整個局勢，玩家會各有一個小棋盤，小棋盤顯示的就是六個面目前的局勢
	4.2 小棋盤上方有一顆白色的球，代表大棋盤的正上方
	4.3 玩家可以抓住白色的球，轉動小棋盤
	4.4 玩家只能在自己的回合轉動小棋盤
	4.5 玩家只能沿著一個軸轉動小棋盤
5. 回合制：
	5.1 第一回合由玩家1開始
	5.2 場地的兩邊顯示剩餘的秒數以及目前是誰的回合
	5.3 每回合有30秒的下棋時間，剩餘10秒時會有滴答聲提示
	5.4 我方的回合：My turn; 敵方的回合：Enemy's turn
	5.5 不是自己的回合無法拿棋子以及轉動小棋盤
	5.6 若手上拿著棋子，且時間已經到了，手中的棋子會直接消失
6. 棋子池
	6.1 雙方玩家有各自的棋子池，從池中選出想要的棋子來使用
	6.2 棋子有三種類型，棋子上方有說明顏色以及顏色的數量
	6.3 為了遊戲的平衡性，加入了第三種顏色(紅色)，可利用紅色阻撓對方
	6.4 紅色連成四子的話，會顯示平手
7. 互動選單
	7.1 用來嘲諷對手，增加互動趣味性	
	7.2 目前有8種對話語音可選擇
8. 遊戲結束後，勝利的一方可以看到由棋子排成的笑臉，輸的一方則是哭臉
9. 當中若發生大棋盤中的棋子自己轉動的情況，請以小棋盤為主QAQ
	
操作說明

1. 使用HTC VIVE or HTC VIVE pro
2. grip鍵用來抓放棋子、轉動小棋盤
3. touchpad用來傳送位置(射線為綠色才可傳送，紅色則否)
4. trigger鍵可以開啟/關閉互動選單
	4.1 開啟互動選單後，在touchpad上滑動來選擇要使用的互動對話
	4.2 按下touchpad做確認
