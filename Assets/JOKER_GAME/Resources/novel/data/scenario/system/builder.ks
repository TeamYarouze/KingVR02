;ビルダーでシナリオごとに必ず呼び出されるシステム系のKSファイル

;メッセージウィンドウを非表示にする
[macro name="tb_show_message_window"]
	[layopt  layer="message0"  visible="true"  ]
[endmacro]

;メッセージウィンドウを表示する
[macro name="tb_hide_message_window"]
	[layopt  layer="message0"  visible="false"  ]
[endmacro]

[macro name="_tb_system_call"]
	[call storage=%storage ]
[endmacro]

[macro name="tb_image_show"]
	[backlay]
	[image storage=%storage layer=1 page=back visible=true y=%y x=%x width=%width height=%height ]
	[trans time=%time|1000 method=%method]
	[wt]
[endmacro]
	
[macro name="tb_image_hide"]
	[backlay]
	[freeimage layer=1 page=back]
	[trans time=%time|1000 method=%method]
	[wt]
[endmacro]

;生ティラノ用のマーカー
[macro name="tb_start_tyrano_code"]
[endmacro]

[macro name="_tb_end_tyrano_code"]
[endmacro]

[macro name="lr"]
[l][r]
[endmacro]


