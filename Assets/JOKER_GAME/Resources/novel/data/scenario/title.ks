;ぶっとび王様

[_tb_system_call storage=system/_title.ks]

*title

;○背景（王様の夢の中）
[back time="1000" method="crossfade" storage="BG_black.jpg"]
[tb_hide_message_window]

[chara_show name="セリフ1"]
[chara_hide name="セリフ1" wait=100 ]
[chara_show name="セリフ2"]
[chara_hide name="セリフ2" wait=false ]
[tb_show_message_window]

[playse storage="se01.mp3"]

#
ガバァッ！[p]

;○背景（王様の部屋）
[back time="100" method="crossfade" storage="BG_001.jpg"]

[chara_show name="宰相顔"]
[chara_show name="宰相"]

#さいしょう
「だいおうさまーーーーーーーーーーぁっ！！」[p]

#
[chara_right name="王様裸横小" wait=false]

#だいおうさま
「あぁあぁっ。なにをするぅっ」[p]

#
[chara_hide name="宰相顔" wait=false ]
[chara_hide name="宰相" wait=false ]
[chara_left name="宰相小" wait=false]
[chara_left_face name="宰相顔小" wait=false]

#さいしょう
「もう　トナーリノおうこくにいく　おじかんですよ」[p]

#
[chara_mod name="王様裸横小" layer=5 storage="chara/1/King06aa.png"]

#だいおうさま
「まだ　ねむいよぅ」[p]

[chara_mod name="宰相顔" layer=8 storage="chara/2/BU_Saisyouab.png"]

#さいしょう
「えいっ！」[p]

#
[chara_mod name="王様裸横小" layer=5 storage="chara/1/King08aa.png"]

#だいおうさま
「ん？」[p]

#
[chara_mod name="王様裸横小" layer=5 storage="chara/1/King01aa.png"]

#だいおうさま
「んんん？」[p]

#さいしょう
「いきますよー！　ポチッとな」[p]

#
[chara_hide name="宰相顔小" layer=8 name="宰相顔小" wait=false ]
[chara_hide name="宰相小" layer=7 name="宰相小" wait=false]
[chara_hide name="王様裸横小1" layer=5 name="王様小1" wait=false]

[chara_right name ="王様横小2" wait=false]
[anim layer=5 left="-10000" top="-2500" time="4000"]

[playse storage="se02.mp3"]

#だいおうさま
「ぎょえーぇっ」[p]

;○背景（王様の玉座アップCG）
[back time="100" method="crossfade" storage="CG_001.jpg"]

;//下記の雰囲気がつたわるような演出入れたい
;//王様がベッドから急に飛び出して、玉座へ座ってしまう。
;//ベッドからは、大きな銀のバネが見えている。

[chara_mod name="宰相顔" layer=8 storage="chara/2/BU_Saisyouae.png"]

[playse storage="se03.mp3"]

#さいしょう
「さすがは　かいはつだいじん」[p]
「ぎょくざへの　めいちゅうりつも　ばつぐんだ」[p]

#だいおうさま
「ワシのベッドに　なにをしたんだ？」[p]

#さいしょう
「だいおうさまが　あさねぼうばかりするから」[p]
「ベッドを　すりかえておいたのです」[p]

[chara_mod name="宰相顔" layer=8 storage="chara/2/BU_Saisyouae.png"]

#さいしょう
「そのなも　ぎょくざまで　とばしたろーくん　です！」[p]

#だいおうさま
「ワシのベッドを　かえせーぇ」[p]

#さいしょう
「もう　じかんがありません。　わたくしが　このままおしていきます」[p]

#
[tb_hide_message_window  ]
[chara_mod name="宰相顔" storage="chara/none.gif" ]
[chara_hide layer=7 name="宰相" wait=false ]

;○背景（お城の廊下）
[back time="3000" method="crossfade" storage="BG_002.jpg"]

[chara_show name="宰相" layer=7 left=0]

#さいしょう
「おぉぉぉーーーーーーーーっ！！」[p]

#だいおうさま
「うわ〜〜〜っ！！　どこへいくのだーぁっ」[p]

[chara_mod name="宰相顔" layer=8 storage="chara/2/BU_Saisyouab.png"]

#さいしょう
「かいはつだいじんの　ところですーーーっ！！」[p]

#
[tb_hide_message_window  ]
[chara_hide name="宰相顔" layer=8 wait=false ]
[chara_hide name="宰相" layer=7 wait=false ]
[wait time=1000]

;○背景（発射場）
[back time="3000" method="crossfade" storage="BG_003.jpg"]

[chara_show name="宰相"]

#さいしょう
「はぁ、はぁ、はぁ……　つきました……」[p]

[chara_mod name="宰相顔" layer=8 storage="chara/2/BU_Saisyouab.png"]

#さいしょう
「かいはつだいじーーーーん」[p]

#
[chara_hide name="宰相顔" wait=false ]
[chara_hide name="宰相" wait=false ]

#？？？
「いないよ～ん」[p]

#
[chara_left name ="宰相小" wait=false]
[chara_left_face name ="宰相顔小" wait=false]
[chara_right name ="開発副大臣小" top="10"]
[chara_right_face name ="開発副大臣顔小" top="10"]

#？？？
「おじいちゃん　ダウンしちゃってて」[p]

[chara_mod name="開発副大臣顔小" storage="chara/none.gif" ]

#かいはつふくだいじん
「かいはつふくだいじんの　わたしがかわりに」[p]

[chara_mod name="宰相顔小" storage="chara/2/BU_Saisyouac.png" ]

#さいしょう
「だいおうさまを　トナリーノおうこくへ　おつれしなさい」[p]

[chara_mod name="開発副大臣顔小" storage="chara/3/BU_Kaifukuad.png" ]

#かいはつふくだいじん
「へいへい……おうさま　また　ねぼーしたでしょ～！？」[p]

#だいおうさま
「じかんのほうが　ワシより　はやかっただけじゃ」[p]

[chara_mod name="開発副大臣顔小" storage="chara/none.gif" ]

#かいはつふくだいじん
「いいな～。　ルカリン　ぜぇんぜん　ねてないんだ～よ」[p]

[chara_mod name="宰相顔小" storage="chara/none.gif" ]

#さいしょう
「じかんがないので　はやく　マシンの　せつめいをしなさい」[p]

[chara_mod name="開発副大臣顔小" storage="chara/3/BU_Kaifukuad.png" ]

#かいはつふくだいじん
「ほーいほい」[p]

[chara_mod name="開発副大臣顔小" storage="chara/none.gif" ]

#かいはつふくだいじん
「おうさま～。　いどうするじょ～」[p]

[tb_hide_message_window  ]
[chara_hide layer=7 name="宰相小"  wait=false]
[chara_hide layer=5 name="開発副大臣小"  wait=false]

;○背景（発射装置の中）
[back time="1000" method="crossfade" storage="BG_black.jpg"]

[playse storage="se04.mp3"]

#だいおうさま
「な、なんじゃ、ここは」[p]

[chara_show name="開発副大臣小" left="180" wait=false]

#かいはつふくだいじん
「はっしゃそうちの　なかだよ」[p]

#

;○背景（発射装置の中）
[back time="1000" method="crossfade" storage="BG_004.jpg"]

;//SE　下記文言はＳＥにあわせ手変更する
[playse storage="se05.mp3"]
ガゴーン、カンカラ、カンカラ、ゴウンゴウン[p]

[anim name="開発副大臣小" left = 1050 wait=false]

#さいしょうのこえ
「ん？　なんですか　このおと」[p]

#だいおうさま
「なんで、わしからはなれる？」[p]

#かいはつふくだいじん
「あ！　ごっめーん！！」[p]
「せつめいのまえに　マシン　うごかしちった～」[p]

#さいしょうのこえ
「なっ、なんですと！」[p]

#
;//SE　下記文言はＳＥにあわせ手変更する
[playse storage="se06.mp3"]
ドンガラガッシャーン[p]

#かいはつふくだいじん
「あ！」[p]

#だいおうさま
「な、なんじゃー！？　だいじょうぶなのかぁ？？」[p]

#さいしょうのこえ
「だいおうさまーーーっ！！」[p]

;//宰相の立ち絵が登場

[chara_center name ="宰相小" wait=false]

#さいしょう
「か、かいはつだいじんっ！！」[p]

[chara_hide layer=9 name="宰相小" wait=false]
[chara_show name="宰相小" layer=7 wait=false]

#さいしょう
「だいおうさまは？？　だいおうさまは　ぶじか？」[p]

#かいはつふくだいじん
「ゲーッ　やっばいかも～ぉ」[p]
「さっきの　カンじゃなくて　ネジとんだ　おとだったっぽい！！」[p]

#だいおうさま・さいしょう
「えーーーぇっ！！」[p]

#かいはつふくだいじん
「もうしゅっぱつさせるしかない！」[p]
「さいしょう、あぶないからそとへ」[p]

[tb_hide_message_window]

;//ふたりの立ち絵消える。
[chara_hide layer=7 name="宰相小" wait=false]
[chara_hide name="開発副大臣小" wait=false]

;//SE　下記文言はＳＥにあわせて変更する
[playse storage="se07.mp3"]
ガゴーン、ゴッ、ゴッゴッゴッ、ゴゴゴーーーーーーー[p]

;○背景（発射装置の中）
[back time="100" method="crossfade" storage="BG_black.jpg"]
[chara_show name="Door" wait=false]

#だいおうさま
「ここは　どこだあぁ？」[p]

#かいはつふくだいじん
「たいほうでいうと　ひをつけるところ」[p]

#だいおうさま
「た、たいほうっ？」[p]

#かいはつふくだいじん
「そうそう　たのしそーでしょぉ？」[p]

#だいおうさま
「だ、だんだん　あがっていくぞ」[p]

#かいはつふくだいじん
「おしろの　てっぺんまで　おつれいたしま～す」[p]

#だいおうさま
「えぇえっ。　そんなたかくぅ？？」[p]

#かいはつふくだいじん
「トリのように　そらをとばせて　あげちゃうよ～★」[p]

#だいおうさま
「いやじゃ　いやじゃ！！」[p]

#かいはつふくだいじん
「あ！　ついたみたい～　とびら　あけちゃうねっ」[p]

[chara_hide name="Door" wait=false]

#
;○背景（ドアの向こう）
[back storage="BG_Road01.jpg"]

#だいおうさま
「せ、せんろぉ……」[p]



;//SE　ジェットコースターで上がってく感じの音
[playse storage="se08.mp3"]
ガッタン、ガッタン[p]

#だいおうさま
「え－－－－－－－－－－－－－！！」[p]

#
[playse storage="se09.mp3"]
[back storage="BG_Road02.jpg"]

#だいおうさま
「せんろが　きれておるぞ！」[p]
「だいじょうぶなのかぁっ？？」[p]

#かいはつふくだいじん
「ぎょくざごと　とばしちゃうよ！！」[p]

#だいおうさま
「ワ、ワシ　ここから　ぎょくざでとぶの？」[p]

#かいはつふくだいじん
「うんそう」[p]

#かいはつふくだいじん
「とおくで　そうさできるよーに　なってるから」[p]
「これもって　ガンバって　おうさまを　たすけてあげてちょ」[p]

#さいしょう
「どうやって　そうさするんです？」[p]

#かいはつふくだいじん
「そっちは　あとあと！！」[p]
「もう　しゅっぱつしちゃう　みたいだから」[p]

#だいおうさま
「あんぜんなのか？」[p]

#かいはつふくだいじん
「ゆうきが　あれば　なんでもできる！」[p]

#
[back storage="BG_Road03.jpg"]

#だいおうさま
「そんなバカな」[p]

#かいはつふくだいじん
「となりのくにまで　レッツゴー！！」[p]


;//SE　大砲を撃つような音
[playse storage="se10.mp3"]
ズドーーーーーーーーーーーンッ[p]

[back storage="BG_white.jpg"]

#だいおうさま
「うわ～～～～～～～～～～～～ぁっ！！」[p]

#
[scene file="Stage000" ]