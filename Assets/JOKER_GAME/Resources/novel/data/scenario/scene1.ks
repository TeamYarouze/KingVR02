[_tb_system_call storage=system/_scene1.ks]

*Start
[tb_start_tyrano_code]

[cm]

[iscript]
f.preload_images_bg = ["data/bgimage/mapImage.gif","data/bgimage/bg01.jpg"];

[endscript]
[preload storage=&f.preload_images_bg]

[iscript]

var sentenceSave
if(JSON.parse(localStorage.getItem("sentenceSaveSet"))==""||JSON.parse(localStorage.getItem("sentenceSaveSet"))==null){
sentenceSave = {title01:0,title02:0,title03:0,title04:0,title05:0,title06:0,title07:0,title08:0};
localStorage.setItem("sentenceSaveSet" , JSON.stringify(sentenceSave));
}
[endscript]

[macro name="hide_ok_button"]

[anim name="bt_ok" x=500 y=250 opacity=0 time=600]
[wa]

[endmacro]
[_tb_end_tyrano_code]

[tb_start_tyrano_code]
*title

[if exp="sf.yourlastname !=null "]
@jump target=*sbm_fullname
[endif]

*show_input_yourlastname
[back  storage="relastname.jpg"  time="1000"  ]

[edit left=150 top=200 name="sf.yourlastname"]

[locate x=150 y=250]
[button target="*sbm_lastname" name=button_name_kettei graphic="kettei.gif" ]


[s]

*sbm_lastname

[commit]
[cm]

[if exp="sf.yourlastname==''"]


[_tb_end_tyrano_code]

[tb_show_message_window  ]
プリンセス、ご苗字が未入力です。[p]


[tb_start_tyrano_code]
@jump target=*show_input_yourlastname
[endif]
[_tb_end_tyrano_code]

[back  storage="rename.jpg"  time="1000"  ]
[tb_show_message_window  ]

[emb exp=sf.yourlastname]さん、次はお名前を教えてください。[p]


[tb_hide_message_window  ]
[tb_start_tyrano_code]

*show_input_yourname

[edit left=150 top=200 name="sf.yourname"]

[locate x=150 y=250]
[button target="*sbm_name" name=button_name_kettei graphic="kettei.gif" ]


[s]

*sbm_name

[commit]
[cm]

[if exp="sf.yourname==''"]
[_tb_end_tyrano_code]

[tb_show_message_window  ]
お名前が未入力です。[p]


[tb_start_tyrano_code]
@jump target=*show_input_yourname
[endif]

*sbm_fullname
[back  storage="welcom.jpg"  time="1000"  ]
[eval exp="sf.nakaguro = '・'"]
[eval exp="sf.yourfullname = sf.yourname + sf.nakaguro + sf.yourlastname"]

[_tb_end_tyrano_code]

[tb_show_message_window  ]
ようこそ、[emb exp=sf.yourfullname]さん。[p]


[tb_hide_message_window  ]
[jump  storage="prologue.ks"  target=""  ]
[s  ]
