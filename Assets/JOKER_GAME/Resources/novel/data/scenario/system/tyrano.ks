
*start

;○背景変更マクロ　storage と time を指定する

[macro name="back_no_window"]
    
    ;@layopt layer=message0 visible=false
    [backlay]
    [image layer=base page=back storage=%storage]
    [trans layer="base" method=%method|crossfade children=false time=%time|2000]
    [wt]
    ;@layopt layer=message0 visible=true

    [endmacro]



;○背景変更マクロ　テキストウィンドウのOUT、IN　storage と time を指定する
[macro name="back"]
    
    [tb_hide_message_window ]
    
    ;@layopt layer=message0 visible=false
    [backlay]
    [image layer=base page=back storage=%storage]
    [trans layer="base" method=%method|crossfade children=false time=%time|2000]
    [wt]
    ;@layopt layer=message0 visible=true
    
    [tb_show_message_window]

    [endmacro]

;========================================
;以下プラグインからコピー
;========================================

;○キャラクターにアニメーションをつける

[macro name="chara_shake"]

    [iscript]
    
    tf.swing_1 = mp.swing;
    tf.swing_2 = mp.swing*2*-1;
    
    [endscript]
    
    [keyframe name="shake"]
    [frame p=0% x="0" ]
    [frame p=25% x="&tf.swing_1" ]
    [frame p=75% x="&tf.swing_2" ]
    [frame p=100% x="&tf.swing_1" ]
    [endkeyframe]

    [kanim name="%name" keyframe="shake" count=%count|5 time=%time|500]

[endmacro]

;○背景点滅
[macro name="back_chikachika"]
	[backlay]
	[image name="effect" layer=1 left=0 top=0 storage=white.jpg page=back visible=true]
	[trans time=60]
	[wt]
	[backlay]
	[freeimage name="effect" layer=1 page=back]
	[trans time=40]
	[wt]
	[backlay]
	[image name="effect" layer=1 left=0 top=0 storage=white.jpg page=back visible=true]
	[trans time=60]
	[wt]
	[backlay]
	[freeimage name="effect" layer=1 page=back]
	[trans time=40]
	[wt]
[endmacro]

;========================================
;以下自作
;========================================

;○時間経過マクロ
[macro name="time_course"]

    [tb_hide_message_window ]

    [back_no_window time="1000" method="crossfade" storage="BG_black.jpg" ]
    [back_no_window time="1000" method="clip" storage="BG_white.jpg" ]
    
[endmacro]


;○主人公名マクロ
[macro name="yourname"]

    #
    [iscript]
    $(".chara_name_area").text(sf.yourname);
    [endscript]
    
[endmacro]


;○小さいキャラ中心表示マクロ
[macro name="chara_center"]
    
    [chara_show name="%name" layer=9 left="550" top="10" wait=false]

    [endmacro]

[macro name="chara_center_face"]

    [chara_show name="%name" layer=10 left="550" top="10" wait=false]

    [endmacro]


;○２キャラ表示マクロ左
[macro name="chara_left"]
    
    [chara_show name="%name" layer=7 left="7" top="10" wait=false]

    [endmacro]

[macro name="chara_left_face"]

    [chara_show name="%name" layer=8 left="7" top="10" wait=false]

    [endmacro]


;○２キャラ表示マクロ右
[macro name="chara_right"]

    [chara_show name="%name" layer=5 left="940" top="10" wait=false]

    [endmacro]

[macro name="chara_right_face"]

    [chara_show name="%name" layer=6 left="940" top="10" wait=false]

    [endmacro]

[return]
