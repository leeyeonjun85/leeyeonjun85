
/////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////
// 게임설정

let dy = 0.7; // 화면 이동 속도
let quiz_interval = 40; // 퀴즈 간격
let quiz_nember = 100; // 퀴즈 갯수





/////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////
// 사용 설명서 작성
let text_start_line = 60; // 내용 시작점
let text_line_interval = 36; // 내용 줄간격

let text_0 = `[사용설명]`; // 제목
let text_1 = `• 화면 하단의 난이도(easy~hard) 설정`; // 내용1
let text_2 = `• 'Play'로 게임시작`;
let text_3 = `• 숫자버튼으로 답입력, 'OK'로 제출`;
let text_4 = `• 화면 중간의 초록 막대는 제한시간`;
let text_5 = ``;
let text_6 = ``;
let text_7 = ``;
let text_8 = `                            leeyeonjun85@gmail.com`;
let text_9 = `                                             by 이연준 v1.0`;



/////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////



let canvas = document.getElementById('canvas');
let ctx = canvas.getContext('2d');
let canvasW = 400;
let canvasH = 400;

let reply_number = 0;
let score = 0;
let x = 0;
let y = 0;
let random_number1 = 0;
let random_number2 = 0;
let random_number = 0;
let random_number_list1 = [];
let random_number_list = [];
let timer = $('#timer').val();
let dy_count = 0;

let img1 = new Image();
    img1.src = 'https://lh3.googleusercontent.com/fife/AAbDypBJmLtKPnvM7SEgrTirzZFn4JuFbQSQqZGiE5l8bDy-fEDdkKnvS--lrFK9g8BA_8Bbr1P0a-64izHFLri2DawKwT4FRBIj6kWavRfWbCshz9Nneq70vO5LBg7ld5mLLaPxXa27xmJUY8vvMdGFsnIOOqg6fSa2gQRofaXVKSnyLjybbk3lJUJ71czfgJOGboA7G0rjLbbcGYhb6_TVnD3g8oOJjJ2ZISW8GswhbW3xshWVY3xfnq7o8pewScCkoocg5jYdzEN9g5KFVNA8Eqpzh5BcVlv9PkIZ0VqK30ksY--4FgEThh2eO6w0UBP-TaIEsRG8fnOSXH4gimjXCJmqzZmxZ2dIMTGgX-_PVnq8ttn_PItgzNFPZYZyyS7D_RAO_2uC5zsgBq18tgTf5-7U9rZGTUVttzPWEdEXhOhnj8y5csdCD7-eWF4xpMw8RAi3AnH4dmWRr7mWogZdP9dW_4z39tppr9mCNkH7FtI18nenr1-IE565XKcxcFdXRG_kkPhplwGUHUauE6w-nylwB5PbHsB7crPe-DAGgkEubhA3vhbYJTCK0y7q-jLv9gKX45em0wReEP449JFVd8mPITinZVGU85vT7_5kxeKScAKDZBpXaigDfEG-CQ3Q8TrxRpY9ClzqCkIcvnJXrfTUv8SVuP1yqyFFKrmbOpOCKYpl16n7fGf4TjG_YE97tKZ-IqmL6Ug55L5BsKqXfgRXb5OrOjIwlFRWzOO6C8c2KJIaFpS52HA_2-5WBdNwbSQzNiA2f3QM31e9wgzbyrwB4AUESEq0eYEYghI1a5Hg0obFmQmfe8v_333H0WjL3BgYSC7xTn3oXKqQM9sEy4xoWCRurJYH2-_euIH2rmTHv2DHOGhmEEAEEfsVn3vUvzhm_qi8VNhXqsZK7h1rTsIuyTY4kazpWQBPsH2eEz9gY4pxb0QT8yYaQ28aOVxpjtlzpsz1EStFq2xwC0f1K9HErSpjbc3i7TKProntAnFNUMWbdfTuWxF4l-NrNZ1sLf8TyzwH8Gve85DhrxfFKFyuArrIHtP6shJW67tJrNO0QkiOvilInbHo-GzA4EavM761AXTDAgF6V1BAy0pko4cz9BT_qfF2ATsCH2VALFK4FCEu1z05937A-1Gd5RxiTZ6xL1xd8lpZ7KsomZC7C66KmAMmSQmue1uSqw4iTtNwr5IJ7EaiZEU1P-pznnxW_rrgKrT0VO5ReITnnm_KJLb6hEXfo3qUykuZjGOfIwe6zBUO5vV2JAyqnl7h_5v17KzSjuCRnuox7ZdLsiwsEcsX8mJ4WqlreA8029dM31n96dUD2SuCt3KhUDgkEB8zv2b3xxUGnAmmqm0eDJS8kAFCImbYUmeA7EjnHjOe1MswqLzE6rupsYOdiyvjo6thvEdAdOeH1pZWrb-bkvezpw97_0k7MFkx0o1yqw6AlM7EUZNgPosw6-Rd-yzAmA-bsZwgmgWGMQ6tqZOKiuOheAO84-_jYKyjWgHvo5kUm34n-N5g-nJCCzXXO9dE3k7QeIPFX-52upI08Y17_QJIkOx8cN4RZ28pyiMXOeU4XUnk_ITmp-3at5u9Kwl07l5k=w1872-h757';
let img2 = new Image();
    img2.src = 'https://lh3.googleusercontent.com/fife/AAbDypAYk-QRkulJjmhn8jugZLSEyi25BWsTKnI2PSrfah-Qju1Nsi_7kbwt1RXXaQcBB1pUrZ_70voUKGQjEEEkW7BgVPEASqYhTODOpkoPo3TMTCFRryRSQN5kM1gD2_dS-dTPfUWdh6U3VTgWcGMDISX9cXM5NcWpNLYdejdfwsvQOaQGoioB35pOvcksgjB087RYQgtF8jznSmzLXcgvff5yXSndgov020HqxQiWw9T-KdiF2ZmWDH6iGOC7TX1NSvaXzw4WzK3GgejgVPUhKo-mCDGPXMsGqwZKTV9_T49dY2ZKy2skJwZGJlxKWDQUt_r34V5lwMpBtqgLcCfK3F5KEzPuX7UPeUFKAf9TJW5-kxEOOS8nEQSXez939imfiUOckWtxlnLQnxUG9dTmT_E5lTpgMYhlXeP0g7WaWOiV34abFeIApB7-FXopa9xCPQDKIorNLgmAQjDSI_I8pxHUGUwXEklFxPfYkfjvEDpngxoqBZunyXW8xs-f7wRwYVjzq6z5ScVXJunZ1FwP_8g1nI_RYG_8tY3sWeMpNMphdR50mfCaSdqVfPs_UccwD9zr9vkKFCZp-r00HaiQFSCmXERDGyl6MdcgdhDSoRFMvN5sYYuYGuwr_yNq65cye-e5XiAXWmVtzRxM8gNxSNhjCjCU7qFZ8f3iJR0GvgvFJFR10Bya7gZSi2VCq5nOZQFKu6HvxNwl27g-mSP8ZfJSS8xX6Dh2Rpp4y7OoCt3ti7resJdbp8f0efQSrLrr8zOjQtVwXsqt--nn--TzGMDDMtRwCPS5nGWXFFsSJqlfj_uPPSdJv38ozvZ1YdTsfilgpWDWxUowenypQwJj0d14Sn2rY03LEepknODfppfx7t4bkymMbtQ56tNd-F-PGGIhCt5LrQUWYPuyRmMmk0GQNmbs4CqXGOS8I8D2eLpGrAY42qYSHdv4uWLQWB3euQtUiZyRq3Zm5C1pvqhnDvGfjexkaKRnheo3in6vt3tRCB39hw9wh0w5aA8mNxPZnHUIQBRmeRYa3r6jfCx9r54gb4C6JHo7zwJHne3-Lv0xQ2A9pG4SueFGoCq4a1GmpMfvKODJCa0RtKUiBu7PabGJEPMkbAsaldem4VIDdMMC_cTmmgwUZGwlvt8evbxHNBzsP0pOAtVrYFccNnO84RLZ9kgycOTfT6SvEBxE5FHWZZwyNmGJ-HP4QNWnR7P9XY41KyTvk7qqa56xpipiMn4OHg8EypyYFdPOwxWBkvw_WrR1W1Wd4cRht_LQQov9Ipxcu7mKsWsuxS67QU_WQ8_j0rQCTSIgwEVFeoMbJZ74owLUKngzc1znBvE1q80GgoglftvINV0Q2Km1_1RDlTmmDVuwUYTob7fDgOz8_8E4eo2nFbgzuNnUus9Fhy0d2znvpTSPBbbqvubvWYLdG1FPP33V1SYJHVPDXBUws_alPTIgmWn1xLq5p7W2U7IhQ5Fd-piGihKZVuFMuJkVmGihIdvz6jV-eDoU7O_7BN06FLDbTLM_8wHFHyw7MG-NWhvjsV13VjKuy53XX4iQbmm_ygmfHgx146jQUyInEFNkMhEbiCo9NHz1AYbA89dM=w1872-h757';
let img3 = new Image();
    img3.src = 'https://lh3.googleusercontent.com/fife/AAbDypBDkyawMLy5nbEto7lfuJtCOqco5BjbBvaa0TBFnYe5GAtyNJOPTJClehi1DCIw85sCgSuhlK9oaOyKaP_X9dzO_ScNKEZi1obl30k3xUvbG2xOiwxfhxPtUxdn5O7jFhCkoiQxiqkn_t1CktQ5pYpHaq1mPgCqnsrjnC6-jW4aEWUKpW3Ltk04bfFj39rczw4cSO8p-05yJ_3T86jqkFCffzeOyOUnhwdjZDqe-3SbEn8h5e2xQ-wNPZQX9HCW7vqrMKpoWD6RsXo_z1ssiB0W43Dh_TFsm38wPpNf4lF-eYpB1wvzXbG6UQLTwvUUyBu-nGDgX1_0AYfqvDGX7Y4ZCQkOjrj1J-gCARNLEqqHs63ECrA54M0lfTwwL3cTHGoafMqNxhf1T6uirEQGfpgWa4iKqJiDy26ZG5Ntmz1-Dp_mGp3pN-MvOjthXcAOxD13zoTIlIa_IbmRVfYVEMOjdnM0ehQLfWIfWjKzw0uhD3kkVJ9Ps9s5OwicbGoW92BVJHVegg-IbuE1oOY3k_LrLB_jpaq5LtQj66wOThWG9sHqm8uxQ9Ap_RbvJEHnlHG7LPwWG0x6TGwea2OwF3lJ35-bTgaOu1IQYE0R6IB3TxJHUUWZa5qvkKKIWaPX4vYLC7lc05Kbcn7dZgXUMt1aMMSTnfen1ii6g_66XOeM0bqZSC0rx5WAvsM_ZZi0nb8FMMeiQJOqBtQrCPoTrPpMOYlJgssB7mK4_48EDQiyQwi6lvo8hU6xNfTi5qn9V9T7vr9rAnAH_AZfXXDWMjdd07ZNoBt4jNh5uqK2Angwj5U8INMp2k9Ygnkowq-mVoz4m7XvC54dsWWGAAPxkzSUUEFuPQ2iavaiWN9SydL5WZOT2KCs0GgqbTNzt25IU4DS9yysZirq6u2FdMG_D89aL0CdYPnifyWNGiOLJFNWrxPQ7RqZUWaNAkWIGjhoPW8xSu15lUOjA-IHtd6GRhJGCbCAIukQVmDDhheQ0BBnsgtSy4jW9oYmzVDgmZHQnFXfOGXPRZZ7eGq7XLubN9d1tcf3Jnkyl1NVefqhtiYO0J9PUMfxuBCv62Ndm6IInkOJ3NoT8c4puvPZbeDQi1vSQwC3njPcajeNdaP1qy4DdwObiEnhmhy63_W19Yc0lspdWwtmkMg9g0GN-WTypqOmA9kVYM9wwRUrJjBI56w1JlnxnFIlM6ooO95UU8oZg61UE6U1GGOvAmCPatqUPEoXXLr3yzbpG_adDiOD5G1PqX0Bbs64wc5lGb18aDTZ8vIL0bkF1NHRJW_0laG9PfhaZl_cD774cD45tJn4n62IcQIxn9l2ybYERWH-383kWlMw4vRNpehp4QTf0KPIck-BIYGqBDqTDdrc2eBuwwoqkE3kn3L1ZSRjROXbANZN9jEjp1yK8r861eyPkPOJN-nW5lxF5jVRzsVqmrcppXtHW207JMzlkdcXPCvfNoa6DB-suoPcuUAfh9TGLJ7sduplz9DS94fbgtzX_-UPN86jozrQVev-1eKSoJ3OKN0WZvM84UacxNj0L9GrNmobQWreAlh0jFk53nH-DB5R3wKRMx3zvoY1GKdVF6hgudEL=w1872-h757';
let img4 = new Image();
    img4.src = 'https://lh3.googleusercontent.com/fife/AAbDypAbOtxxBlcENrtKDj9PbZi-rFljpyJfvjgH0AlffLJnb_SxQ2VkUaf2Pp3U0nD1QDoCw_9HklglYaYtKp5StKoewPwSotCg61kCYhj8aPq00CxXkfVnnguZBsUzMrrXm63LmYEBQu56G41Cs7mTWHunQpXHVMvO0z-BtQlSV6w06bC-ym6ipRc3mfGTZJG_nqvbUnx6IDIXC__JHU9Hp2i_zpyCcILpGn0Up40sm4c7JbH0QVzo6zYQhyCeMXP-4bSFPyh2S2vaMSZaFBGhglvkSozXED_KJuC57eOef3qsNV5dphVl2CmyAfTzrgUUdQeKB0F7WO4rd2sSEXONSAEisuxn9JCqF6c30ooQcacMBBrJCx7F1IqqgalBLGXritLM08k6Hl4xIS6X0FEmdTM9OTXnSW23q3TtZ25DztvmRfJpLdfFPEYNtA57NjHDv-GQLyD5rHJ_lFa4Xc6RZvDgzqnOxAqnp5dqxB7oHoXzjNdDzou6MHFgf_byElM_nYmMv5bTGBd0HLW7Bh4D-5QJYyOMl4hUsjrR0Kj7gljf4GVWdHvIxz3qc4SjFIcw1g8XS4dU7G9WysfnyBFJSYKsmtlMb5MLNP0oTosiwYIHzYXXm7V9IsmLDXssoh8JyiAWMZk7IeCMHKjQr4BeNcgTYpPmuLUoeRiJAgQMHfWIBkUK8jQnBfKTcWwwMQTgzOU4OlSLi_EBeumah5rGhvF96OCwxQNxbJE_5xBBbwKdah_ojTjxVhYnzo_DMAbvYNmeFdx7Yezf7-8W97MhWp9C49Jj8qLhj1Jlqi0O6eK_cEmiSJfm_Jsapjk1l0qmeYWzdcM8hNSEyLTFi4iJZamXkPgDXjv90_2CR3WBde8XpviRVrMykk0d81dmslRFoLsFAyqnWlhwz99AzfUHxsT1Jt_m4EQNa8xFcTiPTvPZFzbstxjMGkU8IHps0U-wyMz2cgNV3LWvnGn0JKygArsw5vgiSQSFPAVgElnkxjkeSEglJUhL7CQn344fkh3kGT_7ffMiDeKg0atp8FdvdH8VMMaetdpEOtBsPbzWKiyXTjh0qPH3-hcLUgxakG4nUmya0xeEJyTtmhDgMKlPgfit5Oc29PznXWPGX0d9N-kNg6AFUSu-ievhbJe0Beugrz3jvvYA1oqs4ygH0utJ8r2VGXZw167j2Pm0lyU4l_QtUBIZnEBdgK_FwXzbhr4OkhR6_833CE3gtXqTmdSVIPeiMIWYTHKeLFBTfApI0tpnTfDeMuP7eunCgedp2drkglFiPjwcxUPZCwDvxk7n3LTQlFRHNdtluLV3sP0bg0M0YmFBfNZUNMB1NmjPgTVO5JVU-cRmpYO6JwAaengNoKW4gAj_kV-3KqP4HO6n1hdpnNY8ShMgZg3_cIIbdtDybN79tMIwig_Rhhxdv_xoxSSHChEv4HIal6Qrp1NAbSdZyJ4hn0p-bjlxxS4KoeLEOuVLrpF1VSkZi3QAGmoLH018lC3_S2xmF7BD3UE0Y3cLCj5jJGZyZw95T5Ns2NpZ3C25CRQM8bfJMUxBemaWnPoJR7dCTWxtYEdxA-8dy9SHfbGnzo7sm5_wtH9vFMrU=w1872-h757';
let img5 = new Image();
    img5.src = 'https://lh3.googleusercontent.com/fife/AAbDypCGys_c9E4WPC3yUtzGaHrjSEzC4TKSNsDneLr6GRv06X8wrDFzBy_2Dcl5nafUw_OwbfnytjLw1Uv-hWrpWYRhGSEM9O0ZtnQAm-mramKU9sc0e480NXbnrlyutxdyTrOm3lkzzpJy7cCfnJKOs9AzPJ9rpeIi6g_RSTgc-wnuobHgdO3FrQFjoO8DsCoKviTfH5cfC2fw0TatGZmoj0tlCNMI_vFOYkpu4AcRkBJZEQobsMxAngX7r_9PY4aXEMnBwMmCrhGlDv45A3nm261WPJOci1UT6f3dfSev_Ik5EBkZ4gXkLZYsf6_Ax0Z0n7LUNmkGTm_hsh1920gWWWvWOk2Jkjfopk243_3RzjxYztlS9vV1cAAmHxBtJ8_gzbIE9F4eoLhqfrJ7luF-W7Gy0WIGnINkm8VgMA1C7cozKn7GVx8GT7RsnpsOLh6GAtuVGH6HLEkT5S7k8SVgYkR98lJ30AQTJac60N7_1NH5a3WLsVrGGEWAGoB4EG_y3ZFKIVaRb-UtscN-NkvJOJnTVbG7Y8bBppUi0vyN-a7P95sd9Y4uAyGFmI7vwvJmmz-tyW8JGcMC8ctq3cE286bpENWRTnRd4TN_2UlU_APi5tn-dQshtk0uoXPOY5GVRUhUffkFQo1fEFdqc6yItKJFLorLzLqg_LieH1D9YgXz_RQKq3LIAErChF0gptCXMvz3c-3u6SzVQRx4L4fslXbYNIZqxeLLaxnFAEWanoQoPatCHm55J-Ai183NRI_WaDtosISlxva48k-f7xJmJG2PthvRr8TN74uOF21ArgMD9pv8NmKSKiXSshTqDXYvN2uD5Tdn8JZwT9j3A_p3eibsWsByG2XiyIgJlEsUT-5JRNhAphoY0_U9-DfftQeoZEV8L5RRj2zvlUzdYvluwgZN5YKdpgQ8-KG7oo_JI4gXU4OAPF-LG3grSHFS1LSpGYaUIlZdBwS1h-bbQOpawSvA6JmJxrrmcykXjiUDSq1b7sIx-0iKeXkchD9CsgTXSViODsxlXkE6LNwniOSmVEp78ZxLi3iyMY9dNWLBs5j8bD0TurUP-rABjJ3cFGuJ2yFp9_SdVM0m0ZdBDm_u14h1L8tIEq9wuBYgY-B7l7GfvoR8Z-Qjj9o695Xg0g1JAbtPSLV8pPRcUmhmjkm8tfQeDZZrMM5kRMFHjEEEJv3cvVyKub5ghtk2bJf5b2ZmfDRyISdFUUdn_4_bHB47B0xsXjrH84bcwvF62cpgtJs6UA-wxbpkarQnXBc_BzfiJFOrHJL4mGH_x5zF_hRqjzH2J3aw8RMPUXUWNgjZEvPQ8NcvW8JBLzCgD3RmzK6qBBP9HN1HlQ7H6I_EX-dPlcVvRGhMvFecxFaFtsXGZs4Yx86wfwhp2ceqNrU6JC9CdFbiB9u05Y9cyCvYGUhP3oavxmwH1iQFf7ezqTLuq1fwFMzXqKH9w9GovdYlVHlzFEuQ8vqH4nlKvBmiwa4FLnRLRLpOa2J6GFKGjpt4sqxuqMhomcDIYLl6eygzchChs6SfKATrPpCZjwl8FFDO309nq5rr5Fo6v5v9hX4tsV3vRg0SpJ8mt8A_qvlMl73O=w1872-h757';
let img6 = new Image();
    img6.src = 'https://lh3.googleusercontent.com/fife/AAbDypBLaMr12eGl3Rmq1cBTAq8CgGBCzaD4z44X2zbAgKJhEWuERvV1j_7UJ9gYTHgXGQNflTxrIk0FS0-V36b1zvp_34g9rvRGa2tWzsAAag-bC_NBQ82yIvu5YsuIujWmh0CB9d21stp_ppR4vgMS_zcTcgk5R63pTwayXdkpLFJnWwvhG6C9iCS2Xv72RVlNLXGttvRoyHVZoP1zqnZuP8xy3Pd-uI0v0hf2FPk7WR4yy4TA1r64U8HXnEj7YZcnsWIGk09Ms--EhblKIozoZ-Rx9hNUFrCldbaUtX8PIy3LggAaEzpX9szY-F2rvSYYe80E3cYW6wjivPpemC03dzPwkLOPZU2lT3MEAY0Uys6Qvo30MtTGLSKEakcxWJZRsocKUQhqZ-Blq7ThbqNqogRnB8LSzvuO0JForMTDsyFt1jLQ-YcDTVFa0frQmDrJ-m90bK873vSZS9hrPF2ZWucI5CT7xVvxGQmTukLgkFQ95jBaZ934P6RAjcY4r5Kv7e0RxYmOT77PE8T00HYXEdQP_fSxPY6vtsD-eFNa9FanVqTWxytn4gZImpjoiomsZfHo3BBrNHWGyHVXCjmKDtGJUVULOusFlULgbwQV33tr4YktSko-e5-Zu2qwG09EfSJnXwPEDGA4EkwXbXg3Z2pjeKI5C65tHlwsNhtDbpztIUvXv8ZhzPnhBRIHlQpe76JxwEd9LPsd37C6ZUGIjq43yhjnhA_LUOglICKkDov-GNG051T1mDIbEAvO02tn6BnsZwSO0C5yBq-cIilgVpWZKMisLPop4p47tz-KFN6EZCH-5GDqD9ukmRLpFG4kAz11exnqRscghDIALigNWWAcv7MxTDv8C0yJY1xruRvvDhM20JohsRM3dMTph7GKibOvBJpU_F0p_M0R_7uhUFOBRl6mrf58wmcOhtIWeei5t3QhLaQ1rrUp1DLyrWG9Kw8iLw6FgH-_MXajFPl_2LxY1RsWXbAIcPZh1S_H8sNDmbuDcnn8IZcAjiMt_NiZI30NeFXhBc2cMz2eVxQI46p4chn0_niPQObNd5-lEg_WLnR2BOqi_70USzHZl_TiG0LOqi1-OG2vOXnV4nmKgZIIuRzSX5mlMC-yoZ93mlou7h0Wgx-1hVyON7AweR-W4yB9x_RqWl_3l5SCW5UT-KITgFWXoVA9jOluuYJho-_w8SqTKEOiEsWAYDxLhJYY85xwPQrtqRzE6zDa8hl4OX5SGXAau5-FS1kY34KfFYgNPoeQwAS48ov3T-UnCSX6LyEPV7FeO5v2h_ND4BHPKM0-ChCYLEX4lRqDCTsFSibbPc8wtIgWF_Aiq70yYtyBTnElpcJE48Mk6fJ4NkIMTGKX9zFGToYdAnsPmtncMH_0DDUO8ZSlqShVC7Qd1hGQoxx0L445gK9N0mEfvTMbMmv9V2xtXDNU_bTt3omDS4tfOOQRSUt7rsdJCfsFShzNYnbJE66__2iVeGa7k_CwcCdSGJdQmO0mh_Q9GHZq1yL99z-rY1buZfku3VQylCgG9LHyO3yiMjm6UGs1yDjrTS7vQcM77Rv7Ie8qq3HpNJMVolIqGR-iVg7375zU_f3B=w1872-h757';
let img7 = new Image();
    img7.src = 'https://lh3.googleusercontent.com/fife/AAbDypArQ4B3SN-qV-FvMcwhnuIvpOVg34ksA7gE2EXUW0TzE3cCLACmmHPsI1mIcCr_r3nAMyhDrVfhStuwxL9xS1Pk-1iUUyVgFhiIUs5Ou-u3FnevlAc-N5I9v0etNeMCstwuOHzpoxahu9lAEc77N4791kqIxxnv-Ub4DEsvevhqX1RvaTLFvaTlNkqfAMhE6m3_NFsEzluBD8MP3ivF4nRjAK_nZuUWHjcnmM7y6eVRzxHjoO053JTrYUu5o2L0PeOnKibansh7dBAtIWGQYkh6zrsd0W-Wx8RhQUcDsCn8PNjhgbmAgUw7Ad7EvRRfAd14rSo-LvPXWoOefvc5q4YxZwQB3TSivZnymuEMMH4NO-NNDkHByeKgNoEKJq0tokLjNy9duqQxnACveYCWUgEH1EEDnvE7Movo5ZxNk61GTpM8YU6hAB4V-ByD96i5NV56_7RFDOcsbcW_MLfa5BOAQJ0PUTohjSzj-1F7uqoG1cJpSnxY0CQoUbiAOLsYG9drrMjCTUILq6M0zGimFs_A9_1hN2uOcgcc5HwVQuzePzML8OxSHT4aGqwZ_AYxIrSr-TVM5qbjag2vtYcLIODnZWvH1vF9FGh4s9VzwTC94jmNUltcFmyqOyZo_W6k9mNtyXgHcrqMewrAULkFaozAbEPhbGD2aNslCJsVLihMgslvkm7P5AHDmbN1zXuiU0yBFwqehQff49CHasM22Dj-zLZ6_T4FOCFbHi2zkchTlphTXsQl4OXTmutGb1T5uGA4CwkfqSXPwRC9mXXtQ2Bb2ih918j0F6RNeUIIEqenDIpJbimElYe2cIQzqmsLhMkXUR7Lz-0_B52ox8vidAcDglHZ3q6RecexkAaI6kupHqkkHNGO9UT-bE7oZ2lExg_HexfBRZ6q1JxEkLNUKGN9xDff4IRWWD8T7_SVb8Oag7rgmYLkFxPwIGhSrghc7JFUTeLzg4DjfAQnbuUqq0ZcAodJD1_tcGVUbAy7cu-qxmTUqHyZu6TX5HrdHkAoWok63Mq_L4VcA71pP92-s3yIHTAZUNSaOBGqt5zC_AaKDH-T6-1G31dvqtSSbUKWBRS4gOUArqK39pbR8vTIA4zEeYrmoPSgxUHfy563kAO0qwaa--HlU1Y1tX43basJjhoExjW4sLj0jf-taU8zDXivIbOzLJohoOFrKEOzNuVU-Z54WxxBd9WsGKQ01f5L476NQnM4u_jUnGwGtie51vGcyv_VdGQQ69rxvCrkFGnR9A8Q7p3AhhwN5DpKk72J4ZLlY_b5ABPSM9vHhxebDVBWUNQbIKBS33h3mq-CkIhQHW-LJA-1CWLA3-WME2jGZRz1OYpbYJ6_Vo5lLIZVlZU4WWwoJuVfCIiblP9CWC-RXIlnc1of57AkP1j_VU-iqEooHW2N29MpfH4-iH0ATBCw6QSUjhgmEAW8eLwEbG5mOwH7motR0o0VFrep0DVKjK_qehiURIBUWuHA1o_BEtWiU79i2wZwL40Qmpzi0waClCQ71o1ZmIJQG7rSplp47vBj2MPbvMby6nxXlntjN3dx92l9AK59yzKlKyqJXuFJnPJG15uAdVLvRdWdw8EW=w1872-h757';
let img8 = new Image();
    img8.src = 'https://lh3.googleusercontent.com/fife/AAbDypDfQPHu6-3BC3dRTR2G2EF_JeidUQOo_jNMMvRcSidIuq_WvFQ0CQW1dPleEF0YQTlcWdH2mM10neeNYx3qdZS3dePjKxg7vQwJtUBwPutMg_OcopFHAdbl6W5ce2XryoKa8aok-TIvzqgvSHwfBwak4XmopxtvaeUMtXT5JIguNCk1Mq3Ng1a2jGazdjp4gQzvqkIkuq67cfuhllBii5XjciHGo22ihrw1Uys8nyITTUySFCT__I99qHP8uejQvzi7nGrtrU1QFmbbnBTWgb_VeY7_yxFHudPCKFG2JvkQ1NmBLrgLERIr8fWj1cyVp9qVCUNgyr3rT2p4hdkk-mnsnxCqBnDis2zQGzunQHLtCjfxw4ZDLs5CGvwZTfuCF7RVCP7QEI18VfMi77SjMAmmKEaWs13tNAJzCpPE5Mp6zDQkuksOFsyKLuovidDeGZHbasBny1oUNFbBerc9lK3OnnKEb1uYfRW_ge_OF6KVEPnx14JTgFJV1ErDtME1O1HwKpjK7jLpVc5y16B21DQ_2WlVEr0TzlyUJdnB37B9JUMl832NCkGH7vnQB5-51k7ulak3lAkkLyQowwhcxwGtOnE9QhkUpFJnQxeSn_AS937rk-aac1fkM9dZnk1d-OeJRgblou3gj30hArgDlK4QxaKea-DKDTuGEbV0WoBtSOPZ5vqnJYdhTx7s14VmnFN92KOzKIBYf_3LGSc5AcsdnoNAZO9dHIIje3TfyrTQZ3-fYGlkuU4VrbpQsMkxiDoSTrAfJEzRftlOjQ3d7WIBVkKI1hnz_Pj3ztmvPcbSZNgClCXCy_5Bxzjqjox81dSIq_SMmgR3HF6_l2bx3LiS3QsUTW0TphZpyUt0zzHtm5RWkGI4NZgLvyu4Y2xoiWyzSm0f9PVhlvwY1BE9C5KtKcr3v47mgRiPSdHurYMYjjBXP5XWcU_f-oApsHKtgbvxtBhQMwmlKpdef8BXgaOs6EfTfpF4IUAQ4Hbeg1OGhB-i-4omvCKP3-8rL8pn-RTyKxuiCiWTuWGxUkZZyTnRWsxk0rg2uSWuOCtv8SBV1XDLY-eDlk-NjnDHzXcB4CuVs_7xu-XIbNatWjB_bPmFQhdT5PZSuZBz0OMVg4sRamD5lOS-D1Abt_oX9OG_znkWzIi-Tkc1mZtQDO3VzvvIkh97TUSNmHQR5nCJXJlBAI0102NsZ92DlsQYmN1N-joAYJVHTM0GkS3OqE75tRbjovoQYBedHSETO3GX135NnCg_KPzE8un3J9Kh3K7gUR0VM8B7aRrFJPIyIvSu5OkL4FotZAViQus0BleA-2BE4BZgA9TY11yhhXLF7sen2LyOTRSD2HoOkPbF2m41ErV2F--rA13fzC76xMhflDbaQ7R0azBhEQvJSwQKkD-wa7RWI7wLr1G0rjI8vnsBRHY10zeBU_R0n68LrJgzI95I-ybCbvwxxXKEF2whdfMT6qZ3CErp-it0jEJaS8oqVjaHgYQTk5epERNFbfySO2Ng141mLVxRW6meQ6hYcknBrY_1-noV6n-JN1jNCnt-xOe2r7wppOM49iiBDKsZbek4t2NWUpFkQ61LvmmlstEs=w1872-h757';
let img9 = new Image();
    img9.src = 'https://lh3.googleusercontent.com/fife/AAbDypAEU7ORGIx19sv-ZJrnaAcyo3U_tfYsB73NQ3y1k9mvkpSyMcFMU08w2Y10RzMAjN6mWA08Qed0vUnEBy1wXVOZLuT-VXTKmxFi8eY9zlP3-cj2glMBrpXRB5nVRKR6Gy2WLnDwTnQY_LxqnFzx_Zix3pWYWyO18ymRh0qfIPObAZg0XstmCOgngPtX_JtDx2xMDsN0RXjSWGRqNH-972glNnJdUJ-F99Rhm9JO-4iuxzTFPoRMBtykkkV3HMV_MNH5uHsBdCsj-jQxbHwNS8TwcYJjJRZkcbmSI5hniitZwTYhgKvatpUtcCVRuA_u6PwSX11rd7lUyaqFFho-mW6iVrXJm7faQZb9i-U-01qMd5JPv7CqDDJ1tYuKDIQclJeiofPnUoYXzyRPW9sZbMy3I1wojRsB7X7i-HLtFVtEuqU6W1BuylPld9WN8mmECRP9XBGKHtwy1mcMOofP77vgXAWV1F2TridH1PtBCIq61eVfgXDSGOiLZTf-961RktqdNMPsCQxlYRA4Kl-X5XfE7Qu0vndtFxcYJU-xWxP6nhoPoOBs0VpCZmIQ-1B6aIZpecPglhE_xirRZKt0xdZvVqLaZheANtJZyQMOfab7FtUkvuexfDhDWDJ_tqQbeVDe9hENjCQcho6KwhbmVwgp_oE3JUoGwXS0orH9H7aAGrtHu4BBC9dQrul7EiErzRxtgYBrqSg3Vr5Wi5ud19BCgWwxLZwfw6FOVgnoFFbEwo3MOcSJHyikOTRsNs5cB4QczqSbBpoiIuTHehnrybLKt48IZbq_4uAKtEYBUgPNGcu0tKFDJBgHdvGkLmqHbojylcgT1LSk_fcILYDJkfVYLjmt-0oIR7D9RKcl_h6GfeYb3d0zasdr3tmF3QiU_J2B3YlHU247uQB54xHIM1BfnYblffmTkeKa91ZHQhspbNbILo2Fz8HGa9jDlwm0DXLEtyMHwpn3SItaS2R8460Svpn89tvgZTTA8HMh9pMkQ08mbZ4hR5sTBuFoSGkFkN3Zb-D5Kwcl-Llo7GOjFnhqgwmc6eLyEEryL4lNGRl3jUUL2iWAo1fqBgGy6UJdbfCR5-D8LeijnpJxoF_EziZOeviSEibIdVkfKDO7VHCdg9Ou21OtoUvY1YWozpLo48u8gApWcdmv4iIW0XxXDRkwzS-ENgUHLA0bbQsGnoP9nIZs91_cRhgfB4rGdEvBlFnzV3RdjVn8DMPjYI8NSFRg555bT2lp7wkPe-0qiXc6zbQM4kqx5PcCMYSZn2XzBQ6-hz9oJhATgd9qYlJ3u3MckXPb8T2lKljUR1GkJ6yh3XqiXi4TOUo-kR9eVFTdmVwIdg-7Kq8xQAexTFqwMfYI_ebkdne5b17pQw4kYkPtKifc_oH9y2UyvCbCBCtHlAo7oSkG_3PN5lTawCfgw7tcgXuP3YXrgfp42uvVOKZ0Z74GKEvMgpk0s8l6Ry_hDyksLlghM55IDor28_8DTCfIHDw9PxUC367riJ_E5R7H3rqVsMR5CrTEI-b2w77UGyByqQl6d9EHsYLrq9XTKJiv4SKIE7XW7AuV6wvNYQT9nXILKd2h-0JaJSt67Msz=w1872-h757';
let img1_gra = new Image();
    img1_gra.src = 'https://lh3.googleusercontent.com/fife/AAbDypDot-6BbzbondyG9fa0FIEiaK5vxCNF0AOf5a99Mbjf0UIWmopJ4AW-juGA9w1IBe0CjbVbIKO5drFHjBx0vTn4EMoEe1OYWnd1JKZGFXfJ-nVQauBaXPgHVy3xqn4uSxkXPWGW7JPeYVuOJHWIrpEvoQ7DHxYS9Qc4UrKUSZyewi7i3kCwmmEBW3fbH_3dgewYz3J0RLpDktcXmxqlrTWsMW3UdxvGyDT63zbZVuoVYcRNjA0Xq-7MSd-Mj1bkirzpBk1xjcXqq_b1sqhUqDO9dKqXUkiyMlltyJntm1wMB8WG42w6MZGlUiEaPBM8hicHzVAXekl5RJl0oZy6x9iimcUHM_CIkU-qdG_DtUEGMusk6vYXfK88LVaEVDGCOs4k5HiPVuKJMitREC26kdh4pAD2ebwWD5Rc-De8NEK8MelW1U52cpuaYa_4gmYSrYxr2DSPsool_S0ABpA7kdqqpvwE7onPb-UR6RdvisYorjgT0Gaj9mqY3d--_TIzYjGNF2HJkckItpbrEp0nC0dq_o0ker-VcLsRnhv1dqcNr7eitBN2fo97BBPRT3kkYYPpkX_ic1TLP0MXS4g_C3w7Df9M-SZGHJWVBdb7lFYRcBFKAlYM-k6Tmk2Lhcv6p0m56cfm5e_Lalx2KMKPlQNUpGUybk3xEsWD7daeup6G4ufkxGB4lpgqO0M3qOppps2NEo_wusqWXwaeeJmjs6zyXbFt47Hp9VWQDKNj0Uy6hQgl6en8e8GemRpHIbA3D6P3cl95zEvu-v9ivKR0APIt522ScXPHr3R6xzKzRlrv-8j0kFkqjE33m07E4xaFDnC8u0rViuo2n_qT0FcRFy3sozjM5miuhHFuc_MdMgyKO9dtu3pMnTG2_a--AKU2-mJIrm3PpaHMfMZDHQ0qfwBQGzaegbicWcspxaalNsO3WGvUTfNA84x7Y8HI1jBRbMJ9p98Ax3SkuHzHDgJa9ak3iNjyTfG9HwcJ929G61ZpubNZs7VVpnyvIQlNXVXFZr0yYTV0BXGBgGZQbqw6yjK1GEOGLIhhIpHf2R1Kq6zaUC7QSt8114EHU0dj9XcGRn9cH9ylIv6ommW17VtDEfWgjau7dHhouvXCpyt3k4OtrLfSxjlz01ngWbOl7kIwo8sHdzRbMrRr8xjyLrHkkh-J5j83PC3xrWxYmilvXN6um1gZz49WQS0sJ_ZKk0yZwSSagga0vv5BJyz-t76N8h3BJ1F1v53qscmooSOYvu9oMuxpXdZVLA7Y6GIUbBdq8vsrrsoU33Yw-xl8h0Lkv2o3JaYkBuKUlUjBrYkyE-47QJRGYQqnfawrBtVocFBoMV4Atsajxxk4Vg17JRdtBJINW1693-J4oDwOc7Gy3Xta0uuGTGWqDhIJ6V6Jij6eRDA63V-E0215ZxDt21fqu2Sl1uqeikJqsIGqcOLxMVR3zIF-eJMXHsi-VeKCH4JMdIVOuwlap2pixHvSTFlvLluMDHdQiRBT5w0BMPG3y7ZghNNOrvmqURwVFlM4sZmkz8fctEFVMGpIqj-uAc_hbXpYaoVFXq9eFsIRVK4pewJQi1Stx9pndHbpTO7fPWdL=w1920-h865';




if ( $('.mathrain_1p1').length > 0 ) {
    loop:
    for ( i = 0; i < quiz_nember; i++ ) {
        make_random_number_1p1()
    
        x = Math.floor(Math.random() * 330);
        y = 0-quiz_interval*random_number_list.length;
    
        random_number_list1 = [random_number1, random_number2, random_number, x, y]
        random_number_list.push(random_number_list1)
    }
} else {
    loop1:
    for (h = 0; h < quiz_nember*5; h++) {
        if ( $('.mathrain .mathrain_10p1').length > 0 ) {
            make_random_number_10p1()
        }
        
        if ( $('.mathrain .mathrain_10p10').length > 0  ) {
            make_random_number_10p10()
        }
    
        x = Math.floor(Math.random() * 330);
        y = 0-quiz_interval*random_number_list.length;
        // console.log(random_number_list.length)
        
        if ( random_number_list.length == 0 ) {
            random_number_list1 = [random_number1, random_number2, random_number, x, y]
            random_number_list.push(random_number_list1)
        } else {
            let match = 0;
            random_number_list.forEach(function(i) {
                // console.log(`😀${i}😀 : ${random_number}, ${i[2]*1}`)
                if ( random_number == i[2]*1 ) {
                    match = 1;
                    // console.log(`😀MATCH😀 : ${random_number}, ${i[2]*1}`)
                } 
            })
            if ( match == 0 ) {
                random_number_list1 = [random_number1, random_number2, random_number, x, y]
                random_number_list.push(random_number_list1)
            }
        }
        if ( random_number_list.length == quiz_nember ) {
            break loop1;
        }
    }
}


console.log(random_number_list)

window.onload = function() {

    // let img1_gra = document.getElementById("img1_gra");
    ctx.drawImage(img1_gra, 0,-100,canvasW,canvasH+100);

    ctx.font = "700 25px Arial";
    ctx.fillStyle = "Black";
    ctx.fillText(text_0, 0,40*1);
    ctx.font = "400 20px Arial";
    ctx.fillText(text_1, 0,text_start_line+text_line_interval*1);
    ctx.fillText(text_2, 0,text_start_line+text_line_interval*2);
    ctx.fillText(text_3, 0,text_start_line+text_line_interval*3);
    ctx.fillText(text_4, 0,text_start_line+text_line_interval*4);
    ctx.fillText(text_5, 0,text_start_line+text_line_interval*5);
    ctx.fillText(text_6, 0,text_start_line+text_line_interval*6);
    ctx.fillText(text_7, 0,text_start_line+text_line_interval*7);
    ctx.fillText(text_8, 0,text_start_line+text_line_interval*8);
    ctx.fillText(text_9, 0,text_start_line+text_line_interval*9);

};



function play() {
    // 배포용 속도
    difficulty_level = 100 - $('#difficulty_level').val()*10.1;
    // 테스트용 속도 (빠름)
    // difficulty_level = 100 - $('#difficulty_level').val()*12;

    setInterval(function() {
        $('#timer').val($('#timer').val()-0.1)
    }, 50)

    timer_degree = 0;
    setInterval(function() {
        timer_degree += 0.1;
        $('#reply').css("background", `linear-gradient( 90deg, rgb(255, 255, 255) ${timer_degree}%, rgb(0, 255, 34) ${timer_degree}% )`)
    }, 50)

    // console.log(difficulty_level)

    $('#btn_play').css("display", "none");
    $('#btn_reload').css("display", "");
    $('#difficulty_level').css("display", "none");

    // setInterval(rain, 2000);
    rain(difficulty_level);
}

function rain() {
    dy_count += dy;
    // console.log(dy_count);

    draw(dy_count)
    if ($('#timer').val() < 0) {
        $('#timer').val(100)
        game_over();
    }
    
    setTimeout(rain, difficulty_level);
}

function draw(dy_count) {

    random_number_list.forEach(function(i) {
        reply_number *= 1;
        i[2] *= 1;
        if ( reply_number == i[2] && dy_count + i[4] > 0 ) {
            i[0] = 9999;
            i[1] = 9999;
            i[2] = 9999;
            score = $("#score").val()*1 + 10;
            $("#score").val(score);
            reply_number = 0
        }

        if ( -i[4] + 400 < dy_count ) {
            i[0] = 9999;
            i[1] = 9999;
            i[2] = 9999;
        }
    });

    // 숫자를 그린다
    ctx.clearRect(0, -5500, canvas.width, 7500);
    
    ctx.drawImage(img1_gra, 0,-100,canvasW,canvasH+100);

    ctx.drawImage(img1, 0,-100,canvasW,canvasH+100);
    ctx.drawImage(img2, 0,-100-(canvasH+100)*1,canvasW,canvasH+100);
    ctx.drawImage(img3, 0,-100-(canvasH+100)*2,canvasW,canvasH+100);
    ctx.drawImage(img4, 0,-100-(canvasH+100)*3,canvasW,canvasH+100);
    ctx.drawImage(img5, 0,-100-(canvasH+100)*4,canvasW,canvasH+100);
    ctx.drawImage(img6, 0,-100-(canvasH+100)*5,canvasW,canvasH+100);
    ctx.drawImage(img7, 0,-100-(canvasH+100)*6,canvasW,canvasH+100);
    ctx.drawImage(img8, 0,-100-(canvasH+100)*7,canvasW,canvasH+100);
    ctx.drawImage(img9, 0,-100-(canvasH+100)*8,canvasW,canvasH+100);


    random_number_list.forEach(function(i) {
        if ( i[0]*1 != 9999 ) {
            // console.log(i[4]);
            ctx.font = "600 24px Arial";
            ctx.fillStyle = "Black";
            ctx.fillText(`${i[0]}+${i[1]}`, i[3], i[4]);
        }
    });

    ctx.translate(0, dy);
}

// function init() {
//     y = 0;
//     random_number1 = Math.floor(Math.random() * 100) + 1;
//     random_number2 = Math.floor(Math.random() * 100) + 1;
//     random_number1 *= 1;
//     random_number2 *= 1;
//     random_number = random_number1 + random_number2;
//     random_number *= 1;
// }


function make_random_number_1p1() {
    random_number1 = Math.floor(Math.random() * 10);
    random_number2 = Math.floor(Math.random() * 10);
    random_number = random_number1 + random_number2;
    return random_number1, random_number2, random_number
}

function make_random_number_10p1() {
    random_number1 = Math.floor(Math.random() * 100);
    random_number2 = Math.floor(Math.random() * 10);
    random_number = random_number1 + random_number2;
    return random_number1, random_number2, random_number
}

function make_random_number_10p10() {
    random_number1 = Math.floor(Math.random() * 100);
    random_number2 = Math.floor(Math.random() * 100);
    random_number = random_number1 + random_number2;
    return random_number1, random_number2, random_number
}

function number_display(n) {
    if ( $("#reply").val() == 0 ) {
        $("#reply").val('')
        $("#reply").val($("#reply").val() + n);
    } else {
        $("#reply").val($("#reply").val() + n);
    }
}

function number_display_clear() {
    $("#reply").val(0);
}

function number_display_reply() {
    reply_number = $("#reply").val();
    number_display_clear();
}

function game_over() {
    h1 = $(".mathrain h1").text();
    score = $("#score").val();
    alert(`

Game Over~🚀
${h1} 게임 결과~✨
당신의 점수는 ${ score }점 입니다.🎉
`)
location.reload();
}

