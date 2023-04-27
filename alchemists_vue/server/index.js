// 서버 관련 변수를 받을 readline
const rl = require("readline");
const r = rl.createInterface({
  input : process.stdin,
  output : process.stdout
});

// 로그 파일 을 남기기 위한 filesystem : fs
const fs = require('fs');
// log 폴더 생성
!fs.existsSync('./log') && fs.mkdirSync('./log');

let now = new Date();
let today_y = now.getFullYear();
let today_m = now.getMonth()+1;
let today_d = now.getDate();

let dir = "./log/" + today_y + today_m + today_d + ".txt";
let log = '';
let clog = '';

fs.open(dir, 'a', (err)=>{
	if (err) throw err;
 	console.log("파일 열기 성공 ! : 파일 이름 :: " + today_y + today_m + today_d + ".txt");
  // ip 받기 준비
  r.setPrompt("서버를 열 ip주소를 입력해주세요 >> ");
  r.prompt();

});


// 서버관련 import
const app = require("express")();
const server = require("http").createServer(app);

server.listen(3000);

// 로그에 시간을 남겨둘 변수 data 제작
const TIME_ZONE = 3240 * 10000;

// 서버 열 ip 번호
let ip = '';
let io = '';

// 매 순간의 시간이 지나갈 때마다 새 new Date를 부름
/*
new Date(+new Date()+TIME_ZONE).toISOString(). // 원형 :: YYYY-MM-DDThh:mm:ss.sssssss
            replace(/T/, ' ').      // replace T with a space
            replace(/\..+/, '')     // delete the dot and everything after
          :: YYYY-MM-DD hh:mm:ss 로 나옴
new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '')
*/


r.on("line", (answer) => {
  ip = answer;
  console_log = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > 입력받은 ip : " + ip;
  log = console_log + "\n";  
  fs.appendFile(dir, log, (err) => {
    if( err ) throw err;
    console.log(console_log);
  });

  r.close();
});
// 게임에서 사용하는 변수
// **************************************************************** //
// **************************************************************** //
// **************************************************************** //
// **************************************************************** //

// 방이름 리스트
// 리스트에는 
// room_name : '', count : 0, max_count:0
// 으로 들어감
let room_list = [];

// 각 방에서 사용될 전역 변수들 모음
// 게임에 필요한 모든 변수를 게임 이름 아래에 저장할 예정 -> 게임방 이름 중복이 되서는 안됨...
let room_data = [];

// 서버에 방을 생성해봅시다.
// namespace 생성하고
// 클라이언트에서 생성할 떄 사용한 room이름으로 namespace에 추가해서 
// 마지막에 방 나가기 할 때 방 나갈 수 있도록 합시다.
// 다시 시작하면 해당 방에 계속 머무른 상태로 페이지만 변경하는 식으로 바꾸면
// 현재 나오는 room_pw 에러를 잡을 수 있다.
// io.of('/') :: default  -> io.of('namespace_name'); :: custom
// socket.join('room_name');
// 그럼 방을 생성할 때 io를 키는게 아니라 바로 접속해야함...
// 그럼 게임을 생성할 떄 부터 이미 socket이 켜진 상태여야 하므로 
// 서버가 켜지지 않으면 접속조차 못함? -> 그렇게 해야겠다.
// 서버켜지고 -> 게임 페이지 접속부터 socket을 생성 -> 이러면 ... 게임 생성 방식이 변경되어야함
// room에 입장하는 방식으로 변경합시다

// 유물 정보 저장 변수
const artifacts_ori = {
    rank_1 : {
      0  : {
        name : "discount_card",
        gold : 3,
        point : 1,
        is_selled : false
      },
      1 : {
        name : "haste_boots",
        gold : 4,
        point : 2,
        is_selled : false
      },
      2 : {
        name : "magic_mortar",
        gold : 3,
        point : 1,
        is_selled : false
      },
      3 : {
        name : "night_vision",
        gold : 3,
        point : 1,
        is_selled : false
      },
      4 : {
        name : "printing_machine",
        gold : 4,
        point : 2,
        is_selled : false
      },
      5 : {
        name : "robe_of_respect" ,
        gold : 4,
        point : 0,
        is_selled : false
      },
    },
    rank_2 : {
      0 : {
        name : "chest_of_witch",
        gold : 3,
        point : 2,
        is_selled : false
      },
      1 : {
        name : "eloquent_necklace",
        gold : 4,
        point : 0,
        is_selled : false
      },
      2 : {
        name : "hypnotic_necklace",
        gold : 3,
        point : 1,
        is_selled : false
      },
      3 : {
        name : "seal_of_authority",
        gold : 4,
        point : 0,
        is_selled : false
      },
      4 : {
        name : "silver_glass",
        gold : 4,
        point : 6,
        is_selled : false
      },
      5 : {
        name : "thinking_hat",
        gold : 4,
        point : 1,
        is_selled : false
      },
    },
    rank_3 : {
      0 : {
        name : "bronze_cup",
        gold : 4,
        point : 4,
        is_selled : false
      },
      1 : {
        name : "feather_hat",
        gold : 3,
        point : 0,
        is_selled : false
      },
      2 : {
        name : "glass_cabinet",
        gold : 5,
        point : 0,
        is_selled : false
      },
      3 : {
        name : "golden_alter",
        gold : 1,
        point : 0,
        is_selled : false
      },
      4 : {
        name : "magic_mirror",
        gold : 4,
        point : 0,
        is_selled : false
      },
      5 : {
        name : "statue_of_wisdom",
        gold : 4,
        point : 0,
        is_selled : false
      }
    }
}

// 현재 라운드에 실험으로 생긴 결과를 저장할 변수
let round_test_result = {
  0 : { red_0 : false },
  1 : { red_1 : false },
  2 : { blue_0 : false },
  3 : { blue_1 : false },
  4 : { green_0 : false },
  5 : { green_1 : false },
  6 : { blank : false },
}

// 현재 라운드에 사용한 favor_card를 임시 저장할 변수
/*   1  assistent
 *   2  bar_owner
 *   3  big_man
 *   4  caretaker
 *   5  herbalist
 *   6  merchant
 *   7  shopkeeper
 *   8  wise_man
 *   최대 4명이므로 
 *    1 : {
 *      0 : {
 *          user_key : '',
 *        }
 *      1~3
 *      }
 */
const temp_used_favor_card_list_ori = {
  1 : {
    0 : {user_key : '', count : 0, },
    1 : {user_key : '', count : 0, },
    2 : {user_key : '', count : 0, },
    3 : {user_key : '', count : 0, },
  },
  2 : {
    0 : {user_key : '', count : 0, },
    1 : {user_key : '', count : 0, },
    2 : {user_key : '', count : 0, },
    3 : {user_key : '', count : 0, },
  },
  3 : {
    0 : {user_key : '', count : 0, },
    1 : {user_key : '', count : 0, },
    2 : {user_key : '', count : 0, },
    3 : {user_key : '', count : 0, },
  },
  4 : {
    0 : {user_key : '', count : 0, },
    1 : {user_key : '', count : 0, },
    2 : {user_key : '', count : 0, },
    3 : {user_key : '', count : 0, },
  },
  5 : {
    0 : {user_key : '', count : 0, },
    1 : {user_key : '', count : 0, },
    2 : {user_key : '', count : 0, },
    3 : {user_key : '', count : 0, },
  },
  6 : {
    0 : {user_key : '', count : 0, },
    1 : {user_key : '', count : 0, },
    2 : {user_key : '', count : 0, },
    3 : {user_key : '', count : 0, },
  },
  7 : {
    0 : {user_key : '', count : 0, },
    1 : {user_key : '', count : 0, },
    2 : {user_key : '', count : 0, },
    3 : {user_key : '', count : 0, },
  },
  8 : {
    0 : {user_key : '', count : 0, },
    1 : {user_key : '', count : 0, },
    2 : {user_key : '', count : 0, },
    3 : {user_key : '', count : 0, },
  },
};
let temp_used_favor_card_list = '';

// 현재 입장중인 사용자의 데이터를 저장할 array
let user_data_array = [];
/*
 * user_name    :: 유저 닉네임                  string
 * user_color   :: 유저 색깔 저장               string
 * is_master    :: 방장 여부                    true/false
 * is_ready     :: 준비완료 여부                true/false
 * is_ingame    :: 라운드 시작 중을 알림         true/false
 * user_key     :: 소켓 키를 유저 고유키로 구분   string
 * user_ingame_data ::  인 게임에서 필요한 데이터 배열로 저장 -- 또 배열 추가
 *  cube_count                                 int
 *  point                                      int    // 기본 점수 10점
 *  ingredient                                 int
 *     card_1 to 8 개인 사용자의 카드 갯수 저장 
 *     total
 *  favor_card                                 int
 *     assistent
 *     bar_owner
 *     big_man
 *     caretaker
 *     herbalist
 *     merchant
 *     shopkeeper
 *     wise_man
 *     total
 *  my_gold
 *  artifacts  :: 구매한 아이템                 true/false
 *     discount_card      0
 *     haste_boots        1
 *     magic_mortar       2
 *     night_vision       3
 *     printing_machine   4
 *     robe_of_respect    5
 *      
 *     chest_of_witch     0
 *     eloquent_necklace  1
 *     hypnotic_necklace  2
 *     seal_of_authority  3
 *     silver_glass       4
 *     thinking_hat       5
 *     
 *     bronze_cup         0
 *     feather_hat        1
 *     glass_cabinet      2
 *     golden_alter       3
 *     magic_mirror       4
 *     statue_of_wisdom   5
 *
 *   discount_adventurer :: 물약판매 할인권      true/false
 *     ad_0
 *     ad_1
 *     ad_2
 *     ad_3
 *   is_check_potion :: 실험을 통해 획득한 물약  true/false
 *     red_1                                    + :: 1
 *     red_0                                    - :: 0
 *     blue_1
 *     blue_0
 *     green_1
 *     green_0
 *     blank
 *     this_round_red_0
 *     this_round_blue_0
 *     this_round_green_0
 *  have_stamp       :: 소지한 인장             true/false
 *     point_5_1/2
 *     point_3_1/2/3
 *     question_red_1/2
 *     question_blue_1/2
 *     question_green_1/2
 *     total          :: int
 *  get_extra_point   :: 물약 발표회에서 한 색의 두 경우 모두 발표했을 때 추가점수 2점 획득 여부
 *     red                                     true/false
 *     green
 *     blue
 */

// 게임 마스터가 설정할 방 옵션들
let room_pw = '0';                      // 비밀번호
let room_member_count = 0;              // 방 인원제한
let room_name = '';                     // ~의 게임방 이름 지정

// 인게임 관련 변수
let round_cont = 0;                     // 라운드 카운터
let decide_round_order_cont = 0;        // 라운드의 순서를 결정하는 순서를 결정하는 변수
let ingredient_select_arr = [0,0,0,0,0];// 재료 선택 칸의 5장 카드 번호 저장변수
let order = [];                         // 방 순서
let show_artifacts = [];                // 구매할 수 있는 아티펙트 저장변수
const adventurer_card_ori_data = {        // 용병 카드 종류에 따른 정답 
  1 : {
    red_1 : false,
    red_0 : true,
    green_1 : true,
    green_0 : false,
    blue_1 : true,
    blue_0 : false,
  },
  2 : {
    red_1 : true,
    red_0 : false,
    green_1 : true,
    green_0 : false,
    blue_1 : false,
    blue_0 : true,
  },
  3 : {
    red_1 : true,
    red_0 : false,
    green_1 : true,
    green_0 : false,
    blue_1 : true,
    blue_0 : false,
  },
  4 : {
    red_1 : false,
    red_0 : true,
    green_1 : false,
    green_0 : true,
    blue_1 : false,
    blue_0 : true,
  },
  5 : {
    red_1 : false,
    red_0 : true,
    green_1 : false,
    green_0 : true,
    blue_1 : true,
    blue_0 : false,
  },
  6 : {
    red_1 : true,
    red_0 : false,
    green_1 : false,
    green_0 : true,
    blue_1 : false,
    blue_0 : true,
  },
};
let adventurer_card_data = '';          // 용병 카드 종류에 따른 정답 
let random_adv_list = [];               // 이번 게임에 사용할 용병들을 저장할 변수
let discount_coin_list = [];            // 한 라운드에 제시된 할인 카드 목록 : user_key, dis_coin(0 ~ 3)
let save_selling_potion_price = [];     // 한 라운드에서 선택한 판매 금액

// 라운드 순서에 따른 값
let final_round_order = [];
/* 1 : gold-1
 * 2 : none
 * 3 : ingredient + 1
 * 4 : ingredient + 2
 * 5 : ingredient + 1 / favor_card + 1
 * 6 : favor_card + 2
 * 7 : ingredient + 2 / favor_card + 1
 * 8 : ingredient + 1 / favor_card + 1
 */
// final_round_order
// user_key :: 사용자 고유키
// order    :: 현재 고른 순서
// before_order :: 이전에 고른 순서 :: 프론트 클래스에서 색을 뺴기 위해 필요
// user_color :: 클라이언트에서 불필요한 연산을 줄이기 위해

// 라운드 보드의 순서
let user_cube_data = ['',[],[],[],[],[],[],[],[],[]];
// user_cube_data       n, cnt, is_select - false
//   ingredient_select    - 1 3 111
//                          - 0 : user_key 
//                              : user_color
//                              : order
//                              : length
//                              : cube : 0 3 111
//   ingredient_sell      - 2 2 12
//   adventurer           - 3 1 2
//   buy_artifacts        - 4 2 12
//   refuting_theory      - 5 2 11
//   presentation_theory  - 6 2 12
//   test_student         - 7 2 11
//   test_i               - 8 2 11
//   exhibition           - 9 3(4) 111(1)
// 

// 재료 조합 결과 테이블
let result_table = [];
/* 8 * 8 table :: n,n기준 대칭 
 * ingredient_result
 *   red_1, 0
 *   blue_1, 0
 *   green_1, 0
 *   blank
 * 8 * 8 table
 * ingredient_reasoning
 *   0 : blank
 *   1 : false
 *   2 : true
 * user_key
 */

// 정답 테이블
let ingredient_answer = [];
/* 배열의 번호에 따라 정답을 매칭시킴 
 * 따라서 ingredient번호 - 1 에 해당하는 번호가 답인것
 * ex) 3번 재료의 정답은 ingredient_answer[2]
 *   1 : rgbl010
 *   2 : rgbl101
 *   3 : rglb011
 *   4 : rglb100
 *   5 : rlgb001
 *   6 : rlgb110
 *   7 : rlglbl000
 *   8 : rlglbl111
 */

// 논문 정보
let theory_data = {
  1 : {
    element : '',
    stamp : {
      1 : {
        user_key : '',
        color : '',
        point : ''  
      },
      2 : {
        user_key : '',
        color : '',
        point : ''  
      },
      3 : {
        user_key : '',
        color : '',
        point : ''  
      },
    }
  },
  2 : {
    element : '',
    stamp : {
      1 : {
        user_key : '',
        color : '',
        point : ''  
      },
      2 : {
        user_key : '',
        color : '',
        point : ''  
      },
      3 : {
        user_key : '',
        color : '',
        point : ''  
      },
    }
  },
  3 : {
    element : '',
    stamp : {
      1 : {
        user_key : '',
        color : '',
        point : ''  
      },
      2 : {
        user_key : '',
        color : '',
        point : ''  
      },
      3 : {
        user_key : '',
        color : '',
        point : ''  
      },
    }
  },
  4 : {
    element : '',
    stamp : {
      1 : {
        user_key : '',
        color : '',
        point : ''  
      },
      2 : {
        user_key : '',
        color : '',
        point : ''  
      },
      3 : {
        user_key : '',
        color : '',
        point : ''  
      },
    }
  },
  5 : {
    element : '',
    stamp : {
      1 : {
        user_key : '',
        color : '',
        point : ''  
      },
      2 : {
        user_key : '',
        color : '',
        point : ''  
      },
      3 : {
        user_key : '',
        color : '',
        point : ''  
      },
    }
  },
  6 : {
    element : '',
    stamp : {
      1 : {
        user_key : '',
        color : '',
        point : ''  
      },
      2 : {
        user_key : '',
        color : '',
        point : ''  
      },
      3 : {
        user_key : '',
        color : '',
        point : ''  
      },
    }
  },
  7 : {
    element : '',
    stamp : {
      1 : {
        user_key : '',
        color : '',
        point : ''  
      },
      2 : {
        user_key : '',
        color : '',
        point : ''  
      },
      3 : {
        user_key : '',
        color : '',
        point : ''  
      },
    }
  },
  8 : {
    element : '',
    stamp : {
      1 : {
        user_key : '',
        color : '',
        point : ''  
      },
      2 : {
        user_key : '',
        color : '',
        point : ''  
      },
      3 : {
        user_key : '',
        color : '',
        point : ''  
      },
    }
  }
};
/* 재료 번호에 맞게 데이터를 넣음
 * 1 : 1번 재료에 발표된 논문의 정보 
 *   : element : 매칭될 원소 번호
 *   : stamp : user_key
 *           : color
 *           : point_5_1/2 , 3_1/2 , question~~
 */

let checking_refute_info_num = 0;

// 물약 발표회 관련 정보를 저장할 변수
// 1~12
// 1~6 :: 각 물약의 첫번째 발표 순서
// 7~12 :: 각 물약의 두번째 발표 순서  
// 1,7 : red+ / 2,8 : red-
// 3,9 : green+ / 4,10 : green-
// 5,11 : blue+ / 6,12 : blue-
// 해당 공간에 발표한 유저의 키, 색 이 들어가야함
let exhibition_result = {
  first : {
    1 : {
      user_key : '',
      color : '',
    },
    2 : {
      user_key : '',
      color : '',
    },
    3 : {
      user_key : '',
      color : '',
    },
    4 : {
      user_key : '',
      color : '',
    },
    5 : {
      user_key : '',
      color : '',
    },
    6 : {
      user_key : '',
      color : '',
    },
  },
  second : {
    7 : {
      user_key : '',
      color : '',
    },
    8 : {
      user_key : '',
      color : '',
    },
    9 : {
      user_key : '',
      color : '',
    },
    10 : {
      user_key : '',
      color : '',
    },
    11 : {
      user_key : '',
      color : '',
    },
    12 : {
      user_key : '',
      color : '',
    },
  }
};

// 최종 순위 
let game_result = {
  1 : {
    name : '',
    score : '',
    grade : '',
  },
  2 : {
    name : '',
    score : '',
    grade : '',
  },
  3 : {
    name : '',
    score : '',
    grade : '',
  },
  4 : {
    name : '',
    score : '',
    grade : '',
  },
};

// 게임이 끝나고 남아서 계속 할지 여부 를 저장하는 변수
// user_key 만 저장
let left_game = [];

// 게임이 끝나고 다시 할때 필요한 정보 
let left_game_result_data = {
  room_pw : '',
  count : 0,
};

// ************************************************************************************************************************************************************************* //
// ************************************************************************************************************************************************************************* //
// ************************************************************************************************************************************************************************* //
// ************************************************************************************************************************************************************************* //

// 서버 열기
r.on("close", () =>{
  // http server를 socket.io server로 upgrade
  io = require("socket.io")(server
      , {
        pingTimeout: 1000,
        cors: {
          origin : "*", //"http://" + ip + ":8080",
          methods: ["GET", "POST"],
          credentials: true,
        },
  });
  
  clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > 서버 열림!";
  log = clog + "\n";
  fs.appendFile(dir, log, (err) => {
    if ( err ) throw err;
  });
  console.log(clog);

  clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > 서버 주소 :: http://" + ip + ":3000";
  log = clog + "\n";
  fs.appendFile(dir, log, (err) => {
    if( err ) throw err;
  });
  console.log(clog);
  //r.resume();

  // localhost:3000 서버에 접속하면 client로 메세지 전송
  app.get("/", (req, res) => {
    res.send("서버 열림");
  });


  
//connection event handler
io.on("connection", (socket) => {
  //console.log(socket.id);
  // 유니티와 접속할 때 사용될 emit -- vue에서는 받는 함수를 만들지 않음
  socket.emit("Connect_unity", "왜됨");
  
  // 서버에 접속한것이 성공하면 성공했다고 보내줄 함수
  socket.on("enter_room_gate", (data) => {
    // data === nick_name
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+data+"님이 서버에 접속하셨습니다.";
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    // 방 실시간 동기화를 위해 로비 방을 만들어 그곳에 join 시킴
    socket.join("lobby");
    io.to("lobby").emit("enter_room_success", room_list);
  });


  // client 가 방에 입장하면
   // data :: user_name
    //         room_name
    //         room_pw
    //         is_master
    //         is_ready
    //         count
    //         msg
  socket.on("enter", (data) => {
    // 다른 사용자에게 보여줄 데이터로 가공
    let user_key = socket.id;
    if (data.no_enter == 'false' || data.no_enter == undefined || data.no_enter == false){
      //master가 입장하면 비밀번호 새로 설정
      //console.log(data);
      socket.leave('lobby');
      socket.join(data.room_name);

      if( data.is_master === 'true' ){
        // 방에 접속하면 room_list 에 이름을 추가하고
        // room_data에 기본 골자(방에서 사용될 필요 변수 이름들) 집어넣어두기
        let temp = {
          name: data.room_name,
          count: 1,
          max_count : data.count,
        }
        room_list.push(temp);
        //room_member_count = data.count;
        //  게임 끝나고도 필요한 정보 초기화 되지 않도록 저장해둠
        // room_data로 남겨둘 거라 필요 없는 과정이 됨
        /*
        left_game_result_data.room_pw = data.room_pw;
        left_game_result_data.count = data.count;
        */
      } else {
        for( let i = 0; i < room_list.length; i++ ){
          if( data.room_name == room_list[i].name ){
            room_list[i].count++;
            break;
          }
        }
      }
    }

    // 서버 관리 방법 변경에 의한 불필요 코드
    /*
    // 마스터가 아닌데 방이 없으면 방이 없다고 안내해야함
    if( data.is_master != 'true' && room_pw == '0' ){
      console.log("마스터가 아닌데 들어오려해서 방이 없어요");
      console.log(data);
      socket.emit("no_room");
    }*/


    // 인 게임 데이터 초기화
    // 재료카드
    let ingredient = {
      card_1 : 0,
      card_2 : 0,
      card_3 : 0,
      card_4 : 0,
      card_5 : 0,
      card_6 : 0,
      card_7 : 0,
      card_8 : 0,
      total : 0,
    };
    //호의카드
    let favor_card = {
      assistent : 0,
      bar_owner : 0,
      big_man   : 0,
      caretaker : 0,
      herbalist : 0,
      merchant  : 0,
      shopkeeper: 0,
      wise_man  : 0,
      total : 0,
    };
    // 유물카드
    let artifacts = {
        // rank1
        discount_card : false,
        haste_boots : false,
        magic_mortar : false,
        night_vision : false,
        printing_machine : false,
        robe_of_respect : false,

        //rank2
        chest_of_witch : false,
        eloquent_necklace : false,
        hypnotic_necklace : false,
        seal_of_authority : false,
        silver_glass : false,
        thinking_hat : false,

        //rank3
        bronze_cup : false,
        feather_hat : false,
        glass_cabinet : false,
        golden_alter : false,
        magic_mirror : false,
        statue_of_wisdom : false,
    };
    // 용병 할인 카드
    let discount_adventurer = {
      ad_0 : true,
      ad_1 : true,
      ad_2 : true,
      ad_3 : true
    };
    // 내가 확인한 포션 분기
    let is_check_potion = {
      red_1 : false,
      red_0 : false,
      blue_1 : false,
      blue_0 : false,
      green_1 : false,
      green_0 : false,
      blank  : false,
      this_round_red_0 : false,
      this_round_blue_0 : false,
      this_round_green_0 : false,
    };
    //내가 소지한 인장
    let have_stamp = {
      point_5_1 : true,
      point_5_2 : true,
      point_3_1 : true,
      point_3_2 : true,
      point_3_3 : true,
      question_red_1 : true,
      question_red_2 : true,
      question_blue_1 : true,
      question_blue_2 : true,
      question_green_1 : true,
      question_green_2 : true,
      total : 11
    };

    let user_ingame_data = {
      my_gold : 0,
      cube_count : 3,
      point : 10,
      ingredient : ingredient,
      favor_card : favor_card,
      artifacts : artifacts,
      discount_adventurer : discount_adventurer,
      is_check_potion : is_check_potion,  
      have_stamp : have_stamp,
      get_extra_point : {
        red : false,
        green : false,
        blue : false,
      }
    };
    
    let user_data = {
      user_name : data.user_name,
      user_color : '',
      is_master : data.is_master,
      is_ready : data.is_ready,
      is_ingame : false,
      user_key : user_key,
      user_ingame_data : user_ingame_data,
    }

    // 재료 추리 테이블
    let ingredient_reasoning = [
      [0,0,0,0,0,0,0,0],
      [0,0,0,0,0,0,0,0],
      [0,0,0,0,0,0,0,0],
      [0,0,0,0,0,0,0,0],
      [0,0,0,0,0,0,0,0],
      [0,0,0,0,0,0,0,0],
      [0,0,0,0,0,0,0,0],
      [0,0,0,0,0,0,0,0]
    ];
    
    // 재료 조합 결과 테이블 
    let ingredient_result = [
      ['','','','','','','',''],
      ['','','','','','','',''],
      ['','','','','','','',''],
      ['','','','','','','',''],
      ['','','','','','','',''],
      ['','','','','','','',''],
      ['','','','','','','',''],
      ['','','','','','','','']
    ];

    let result_table_data = {
      user_key : user_key,
      ingredient_reasoning : ingredient_reasoning,
      ingredient_result : ingredient_result
    };

    var exhibit = exhibition_result;
    // room_data에 정보 저장을 시작할 것
    // 필요한 것
    // user_data_array, result_table, user_cube_data, artifacts, round_test_result
    // temp_used_favor_card_list, final_round_order
    // room_pw                       // 비밀번호
    // room_member_count = 0;              // 방 인원제한
    // room_name = '';                     // ~의 게임방 이름 지정
    // round_cont = 0;                     // 라운드 카운터
    // decide_round_order_cont = 0;        // 라운드의 순서를 결정하는 순서를 결정하는 변수
    // ingredient_select_arr = [0,0,0,0,0];// 재료 선택 칸의 5장 카드 번호 저장변수
    // order = [];                         // 방 순서
    // show_artifacts = [];                // 구매할 수 있는 아티펙트 저장변수
    // adventurer_card_data = '';          // 용병 카드 종류에 따른 정답 
    // random_adv_list = [];               // 이번 게임에 사용할 용병들을 저장할 변수
    // discount_coin_list = [];            // 한 라운드에 제시된 할인 카드 목록 : user_key, dis_coin(0 ~ 3)
    // save_selling_potion_price = [];     // 한 라운드에서 선택한 판매 금액
    if( room_data[data.room_name] == undefined || room_data[data.room_name].room_member_count == 0) {
      let temp = {
        user_data_array: [],
        user_cube_data : ['',[],[],[],[],[],[],[],[],[]],
        result_table: [],
        artifacts: artifacts_ori,
        round_test_result: round_test_result,
        temp_used_favor_card_list: '',
        final_round_order: [],
        room_pw: data.room_pw,
        room_member_count: data.count,
        now_member_count : 1,
        round_cont : 0,
        decide_round_order_cont: 0,
        ingredient_select_arr: [0,0,0,0,0],
        order: [],
        show_artifacts : [],
        adventurer_card_data: '',
        random_adv_list: [],
        discount_coin_list: [],
        save_selling_potion_price: [],
        ingredient_answer : [],
        theory_data : theory_data,
        checking_refute_info_num : 0,
        exhibition_result : exhibit,
        game_result : game_result,
        left_game: [],
        left_game_result_data: left_game_result_data,
        restart_counter : 0,
      };
      room_data[data.room_name] = temp;
    }
    else {
      room_data[data.room_name].now_member_count++;
    }

    room_data[data.room_name].user_data_array.push(user_data);
    room_data[data.room_name].result_table.push(result_table_data);
    room_data[data.room_name].order.push(user_key);
    
    //console.log(room_data);
    /*  서버 관리 방법 변경으로 쓸모없어짐
    // 가공 저장
    user_data_array.push(user_data);
    result_table.push(result_table_data);

    //인 게임 순서는 들어온 순서대로 진행
    order.push(user_key);
    */

    // 로비의 채팅창에 보낼 서버 메세지
    let msg = {
      speaker: "서버",
      msg: data.msg,
      type: "server"
    };

    // 방정보
    let personal_room_data = {
      room_name : data.room_name,
      my_key : user_key
    };
  
    // lobby에 해당 방이 열렸다는 정보를 알림
    io.to("lobby").emit("enter_room_success", room_list);
    // 메시지를 모든 client에게 메시지를 전송한다
    io.to(data.room_name).emit("chat", msg);
    // 방금 접속한 사용자에게 방 이름과 자신의 키 전송
    socket.emit("announce_room_name", personal_room_data);
    // 현재 접속한 모든 사용자를 모두에게 전송
    io.to(data.room_name).emit("all_player", room_data[data.room_name].user_data_array);

    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방의 메세지 : " + data.user_name + "/ 내용 : " + data.msg;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
  });

  // client가 방에 입장하기 위해 pw를 검사
  // data :: room_name, room_pw
  socket.on("check_pw", (data) => {
    //console.log(data);
    //비밀번호 맞는지 검사 후 입장/퇴장 조치
    if( room_data[data.room_name].room_pw != data.room_pw ) {
      //퇴장
      socket.emit("wrong_pw");
    }
    else {
      socket.emit("ok_pw");
    }
  });

  // 채팅
  // data :: speaker, msg, type(normal, server) , room_name
  socket.on("chat", (data) => {
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name+ "방의 메세지 >> "+ data.speaker + " : " + data.msg;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);

    let msg = {
      speaker : data.speaker,
      msg     : data.msg,
      type    : data.type
    }
    
    io.to(data.room_name).emit("chat", msg);
  });

  //색 선택을 통한 데이터 갱신
  // data :: user_name, user_color, user_key, room_name
  socket.on("edit_color", (data) => {
    //console.log(data);
    let msg;
    if( data.user_color != ''){
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> "+ data.user_name + "이(가) " + data.user_color + "를 선택했습니다.";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
      });
      console.log(clog);

      // 중첩되는 색이 있는지 확인
      // 있으면 선택불가 하다고 띄워주기
      // 없으면 pass
      for(var i = 0; i < room_data[data.room_name].now_member_count; i++ ){
        //console.log(i);
        if( room_data[data.room_name].user_data_array[i].user_color == data.user_color ){
          // 같은 것이 있으므로 emit 후 return
          socket.emit("same_color");
          return;
        }
      }
      // 위에서 통과 된 것 == 같은 색이 없는것 이므로 진행
      var i = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);
      room_data[data.room_name].user_data_array[i].user_color = data.user_color;

      msg = {
        speaker : "서버",
        msg     : data.user_name + "이(가) " +data.user_color + "를 선택했습니다.",
        type    : "server"
      }
    }
    else {
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.user_name + "이(가) 선택을 취소했습니다.";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
      });
      console.log(clog);
      var i = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);
      room_data[data.room_name].user_data_array[i].user_color = data.user_color;

      msg = {
        speaker : "서버",
        msg     : data.user_name + "이(가) 선택을 취소 했습니다.",
        type    : "server"
      }
    }
    
    // 재전송 시켜줌
    io.to(data.room_name).emit("chat", msg);
    
    io.to(data.room_name).emit("all_player", room_data[data.room_name].user_data_array);
  });

  // 준비 완료 버튼을 눌렀을 때 데이터 갱신
  // data :: is_ready, user_key, room_name
  socket.on("lobby_ready", (data) => {
    
    //데이터 갱신
    var i = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);
    room_data[data.room_name].user_data_array[i].is_ready = data.is_ready;
    var say;
    if( data.is_ready === true || data.is_ready == "true" ){
      say = room_data[data.room_name].user_data_array[i].user_name + " 준비 완료";
    }else {
      say = room_data[data.room_name].user_data_array[i].user_name + " 준비 해제";
    }
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> "+  room_data[data.room_name].user_data_array[i].user_name + say;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    let msg = {
      speaker   : "서버",
      msg       : say,
      type      : "server"
    };

    // 정보 갱신
    io.to(data.room_name).emit("chat", msg);
    io.to(data.room_name).emit("all_player", room_data[data.room_name].user_data_array);
  });

  // 게임 보드로 다같이 이동
  // data :: room_name
  socket.on("move_to_board_everyone", (room_name) => {
    //console.log(room_name);
    console.log(room_data[room_name].user_data_array.length);
    for(let i = 0; i < room_data[room_name].user_data_array.length; i++){
      // 최초 시작시 재료카드 3장을 분배
      room_data[room_name].user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
      room_data[room_name].user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
      room_data[room_name].user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
      // 총 수량 표기
      room_data[room_name].user_data_array[i].user_ingame_data.ingredient.total += 3;
      // 금화는 2원씩 가지고 시작
      room_data[room_name].user_data_array[i].user_ingame_data.my_gold += 2;
      // 게임 준비는 전부 해제 ( 인 게임에서도 준비 완료 변수 사용 )
      room_data[room_name].user_data_array[i].is_ready = false;
    }
    // 재료카드 선택칸의 5장의 카드 번호 배열
    suffle_ingredient_card_select(room_name);
    // 용병 5명 등록
    suffle_adv_card_list(room_name);
    // 정답 생성
    make_answer(room_name);
    console.log("정상작동");
    io.to(room_name).emit("everyone_move_to_board");
  });

  /***************************************************************************/
  /*              인 게임에서 필요한 함수        */ 
  /***************************************************************************/

  socket.on("created_data_announce", (room_name) => {
    //console.log(room_name);
    room_data[room_name].temp_used_favor_card_list = temp_used_favor_card_list_ori;
    // 초기화된 인게임 데이터 전송
    socket.emit("get_ingame_data", room_data[room_name].user_data_array);
    // 게임 순서를 결정하는 순서 전송
    socket.emit("round_order_setting_before", room_data[room_name].order);
    // 선택할 수 있는 카드 공개
    socket.emit("ingredient_select_card_open", room_data[room_name].ingredient_select_arr);
    // 용병 정보 공개
    socket.emit("adv_list_setting", room_data[room_name].random_adv_list);
    // 용병에게 판매할 수 있는 물약 정보 전송
    room_data[room_name].adventurer_card_data = adventurer_card_ori_data;
    socket.emit("adv_sell_potion_list", room_data[room_name].adventurer_card_data);
    socket.emit("adv_sell_potion_ori_list", adventurer_card_ori_data);
    // 라운드 순서를 결정하는 순서를 결정하는 변수 공유를 위한 전송
    socket.emit("decide_round_setting_order_counter_send", room_data[room_name].decide_round_order_cont);
    // 게임 추리 및 결과 테이블 전송
    socket.emit("change_result_table", room_data[room_name].result_table);
    // 초기 논문 데이터 전송
    socket.emit("change_theory_data", room_data[room_name].theory_data);
    // 현재 라운드 전송
    socket.emit("change_round", room_data[room_name].round_cont);
    // 물약 전시회 기본 변수 전송
    socket.emit("change_exhibition_data", room_data[room_name].exhibition_result);

    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + room_name + "방의 게임이 시작되었습니다.";
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
  });

  // overlay에서 인게임 말 놓는 순서 결정을 위한 함수
  // 인게임에서 정한 순서에 큐브 를 놓기 위한 함수
  // data[ 
    //    user_key, 
    //    select_order  
    //    room_name
    //  ]
  socket.on("select_round_order", (data) => {
    
    let u_data = '';
    let change_data = false;
    let temp_data = '';

    let msg = '';
    let speaker = '';
    let user_color = '';
    // 초록색 물약 부작용이 있는지 확인하기 위한 변수
    let have_side_effects = false;
    // user_key
    // ingredient_select    - 0 3
    // ingredient_sell      - 1 2
    // adventurer           - 2 1
    // buy_artifacts        - 3 2
    // refuting_theory      - 4 2
    // presentation_theory  - 5 2
    // test_student         - 6 2
    // test_i               - 7 2
    // exhibition           - 8 3(4) 111(1)
      
    // 말하는 이 및 색 설정
    for( let i = 0; i < room_data[data.room_name].user_data_array.length; i++){
      if( room_data[data.room_name].user_data_array[i].user_key == data.user_key ){
        speaker = room_data[data.room_name].user_data_array[i].user_name;
        user_color = room_data[data.room_name].user_data_array[i].user_color;
        // 2라운드 이후 초록 물약 디버프가 있는지 확인
        if( room_data[data.room_name].user_data_array[i].user_ingame_data.is_check_potion.this_round_green_0 == true ){
          have_side_effects = true;
        }
        break;
      }
    }

    //  cube_data를 따로 분리해야 호의카드를 편히 쓸 수 있음.
    // 처음 넣는 것이라면 0번으로 시작
    // have_side_effect가 true 면 order : 8, 아니면 data.order
    // 이미 있으면 추가하면 안됨...
    if( room_data[data.room_name].user_cube_data[1].length <= 0 ) {
      temp_data = {
        user_key : data.user_key,
        user_color : user_color,
        order : '',
        length : 3,
        cube : {
          1 : {
            num : 1,
            cnt : 1,
            is_select : false,
          },
          2 : {
            num : 2,
            cnt : 1,
            is_select : false,
          },
          3 : {
            num : 3,
            cnt : 1,
            is_select : false,
          },
        }
      };
      if( have_side_effects == true ) {
        temp_data.order = 8;
      }
      else {
        temp_data.order = data.order;
      }
  
      room_data[data.room_name].user_cube_data[1].push(temp_data);
      
      temp_data = {
        user_key : data.user_key,
        user_color : user_color,
        order : '',
        length : 2,
        cube : {
          1 : {
            num : 1,
            cnt : 1,
            is_select : false,
          },
          2 : {
            num : 2,
            cnt : 2 ,
            is_select : false,
          }
        }
      };
      if( have_side_effects == true ) {
        temp_data.order = 8;
      }
      else {
        temp_data.order = data.order;
      }
      room_data[data.room_name].user_cube_data[2].push(temp_data);
  
      temp_data = {
        user_key : data.user_key,
        user_color : user_color,
        order : '',
        length : 1,
        cube : {
          1 : {
            num : 1,
            cnt : 2,
            is_select : false,
          }
        }
      };
      if( have_side_effects == true ) {
        temp_data.order = 8;
      }
      else {
        temp_data.order = data.order;
      }
      room_data[data.room_name].user_cube_data[3].push(temp_data);
  
      temp_data = {
        user_key : data.user_key,
        user_color : user_color,
        order : '',
        length : 2,
        cube : {
          1 : {
            num : 1,
            cnt : 1,
            is_select : false,
          },
          2 : {
            num : 2,
            cnt : 2,
            is_select : false,
          }
        }
      };
      if( have_side_effects == true ) {
        temp_data.order = 8;
      }
      else {
        temp_data.order = data.order;
      }
      room_data[data.room_name].user_cube_data[4].push(temp_data);
  
      temp_data = {
        user_key : data.user_key,
        user_color : user_color,
        order : '',
        length : 2,
        cube : {
          1 : {
            num : 1,
            cnt : 1,
            is_select : false,
          },
          2 : {
            num : 2,
            cnt : 1,
            is_select : false,
          },
        }
      };
      if( have_side_effects == true ) {
        temp_data.order = 8;
      }
      else {
        temp_data.order = data.order;
      }
      room_data[data.room_name].user_cube_data[5].push(temp_data);
  
      temp_data = {
        user_key : data.user_key,
        user_color : user_color,
        order : '',
        length : 2,
        cube : {
          1 : {
            num : 1,
            cnt : 1,
            is_select : false,
          },
          2 : {
            num : 2,
            cnt : 2,
            is_select : false,
          }
        }
      };
      if( have_side_effects == true ) {
        temp_data.order = 8;
      }
      else {
        temp_data.order = data.order;
      }
      room_data[data.room_name].user_cube_data[6].push(temp_data);
  
      temp_data = {
        user_key : data.user_key,
        user_color : user_color,
        order : '',
        length : 2,
        cube : {
          1 : {
            num : 1,
            cnt : 1,
            is_select : false,
          },
          2 : {
            num : 2,
            cnt : 1,
            is_select : false,
          }
        },
      };
      if( have_side_effects == true ) {
        temp_data.order = 8;
      }
      else {
        temp_data.order = data.order;
      }
      room_data[data.room_name].user_cube_data[7].push(temp_data);
  
      temp_data = {
        user_key : data.user_key,
        user_color : user_color,
        order : '',
        length : 2,
        cube : {
          1 : {
            num : 1,
            cnt : 1,
            is_select : false,
          },
          2 : {
            num : 2,
            cnt : 1,
            is_select : false,
          }
        }
      };
      if( have_side_effects == true ) {
        temp_data.order = 8;
      }
      else {
        temp_data.order = data.order;
      }
      room_data[data.room_name].user_cube_data[8].push(temp_data);
  
      if( room_data[data.room_name].room_member_count == 4 ) {
        temp_data = {
          user_key : data.user_key,
          user_color : user_color,
          order : '',
          length : 4,
          cube : {
            1 : {
              num : 1,
              cnt : 1,
              is_select : false,
            },
            2 : {
              num : 2,
              cnt : 1,
              is_select : false,
            },
            3 : {
              num : 3,
              cnt : 1,
              is_select : false,
            },
          },
        };
      } 
      else {
        temp_data = {
            user_key : data.user_key,
            user_color : user_color,
            order : '',
            length : 4,
            cube : {
              1 : {
                num : 1,
                cnt : 1,
                is_select : false,
              },
              2 : {
                num : 2,
                cnt : 1,
                is_select : false,
              },
              3 : {
                num : 3,
                cnt : 1,
                is_select : false,
              },
              4 : {
                num : 4,
                cnt : 1,
                is_select : false,
              },
            },
        };
      }
      if( have_side_effects == true ) {
        temp_data.order = 8;
      }
      else {
        temp_data.order = data.order;
      }
      room_data[data.room_name].user_cube_data[9].push(temp_data);
    }
    else if ( room_data[data.room_name].user_cube_data[1].length > 0) {
      let already_on = false;
      let my_number = -1;
      for ( let i = 0 ; i < room_data[data.room_name].user_cube_data[1].length; i++ ){
        if( room_data[data.room_name].user_cube_data[1][i].user_key == data.user_key ){
          already_on = true;
          my_number = i;
          break;
        }
      }

      if( already_on == true ) {
        for( let i = 1; i < 10; i++ ){
          room_data[data.room_name].user_cube_data[i][my_number].order = data.order;
        }
      }
      else {
        temp_data = {
          user_key : data.user_key,
          user_color : user_color,
          order : '',
          length : 3,
          cube : {
            1 : {
              num : 1,
              cnt : 1,
              is_select : false,
            },
            2 : {
              num : 2,
              cnt : 1,
              is_select : false,
            },
            3 : {
              num : 3,
              cnt : 1,
              is_select : false,
            },
          }
        };
        if( have_side_effects == true ) {
          temp_data.order = 8;
        }
        else {
          temp_data.order = data.order;
        }
    
        room_data[data.room_name].user_cube_data[1].push(temp_data);
        
        temp_data = {
          user_key : data.user_key,
          user_color : user_color,
          order : '',
          length : 2,
          cube : {
            1 : {
              num : 1,
              cnt : 1,
              is_select : false,
            },
            2 : {
              num : 2,
              cnt : 2 ,
              is_select : false,
            }
          }
        };
        if( have_side_effects == true ) {
          temp_data.order = 8;
        }
        else {
          temp_data.order = data.order;
        }
        room_data[data.room_name].user_cube_data[2].push(temp_data);
    
        temp_data = {
          user_key : data.user_key,
          user_color : user_color,
          order : '',
          length : 1,
          cube : {
            1 : {
              num : 1,
              cnt : 2,
              is_select : false,
            }
          }
        };
        if( have_side_effects == true ) {
          temp_data.order = 8;
        }
        else {
          temp_data.order = data.order;
        }
        room_data[data.room_name].user_cube_data[3].push(temp_data);
    
        temp_data = {
          user_key : data.user_key,
          user_color : user_color,
          order : '',
          length : 2,
          cube : {
            1 : {
              num : 1,
              cnt : 1,
              is_select : false,
            },
            2 : {
              num : 2,
              cnt : 2,
              is_select : false,
            }
          }
        };
        if( have_side_effects == true ) {
          temp_data.order = 8;
        }
        else {
          temp_data.order = data.order;
        }
        room_data[data.room_name].user_cube_data[4].push(temp_data);
    
        temp_data = {
          user_key : data.user_key,
          user_color : user_color,
          order : '',
          length : 2,
          cube : {
            1 : {
              num : 1,
              cnt : 1,
              is_select : false,
            },
            2 : {
              num : 2,
              cnt : 1,
              is_select : false,
            },
          }
        };
        if( have_side_effects == true ) {
          temp_data.order = 8;
        }
        else {
          temp_data.order = data.order;
        }
        room_data[data.room_name].user_cube_data[5].push(temp_data);
    
        temp_data = {
          user_key : data.user_key,
          user_color : user_color,
          order : '',
          length : 2,
          cube : {
            1 : {
              num : 1,
              cnt : 1,
              is_select : false,
            },
            2 : {
              num : 2,
              cnt : 2,
              is_select : false,
            }
          }
        };
        if( have_side_effects == true ) {
          temp_data.order = 8;
        }
        else {
          temp_data.order = data.order;
        }
        room_data[data.room_name].user_cube_data[6].push(temp_data);
    
        temp_data = {
          user_key : data.user_key,
          user_color : user_color,
          order : '',
          length : 2,
          cube : {
            1 : {
              num : 1,
              cnt : 1,
              is_select : false,
            },
            2 : {
              num : 2,
              cnt : 1,
              is_select : false,
            }
          },
        };
        if( have_side_effects == true ) {
          temp_data.order = 8;
        }
        else {
          temp_data.order = data.order;
        }
        room_data[data.room_name].user_cube_data[7].push(temp_data);
    
        temp_data = {
          user_key : data.user_key,
          user_color : user_color,
          order : '',
          length : 2,
          cube : {
            1 : {
              num : 1,
              cnt : 1,
              is_select : false,
            },
            2 : {
              num : 2,
              cnt : 1,
              is_select : false,
            }
          }
        };
        if( have_side_effects == true ) {
          temp_data.order = 8;
        }
        else {
          temp_data.order = data.order;
        }
        room_data[data.room_name].user_cube_data[8].push(temp_data);
    
        if( room_data[data.room_name].room_member_count == 4 ) {
          temp_data = {
            user_key : data.user_key,
            user_color : user_color,
            order : '',
            length : 4,
            cube : {
              1 : {
                num : 1,
                cnt : 1,
                is_select : false,
              },
              2 : {
                num : 2,
                cnt : 1,
                is_select : false,
              },
              3 : {
                num : 3,
                cnt : 1,
                is_select : false,
              },
            },
          };
        } 
        else {
          temp_data = {
              user_key : data.user_key,
              user_color : user_color,
              order : '',
              length : 4,
              cube : {
                1 : {
                  num : 1,
                  cnt : 1,
                  is_select : false,
                },
                2 : {
                  num : 2,
                  cnt : 1,
                  is_select : false,
                },
                3 : {
                  num : 3,
                  cnt : 1,
                  is_select : false,
                },
                4 : {
                  num : 4,
                  cnt : 1,
                  is_select : false,
                },
              },
          };
        }
        if( have_side_effects == true ) {
          temp_data.order = 8;
        }
        else {
          temp_data.order = data.order;
        }
        room_data[data.room_name].user_cube_data[9].push(temp_data);
      }
    }

    // 새로운 데이터인지 판단
    // round_order가 비어있으면 완전히 새로운 선택
    if( room_data[data.room_name].final_round_order == '' ){
      if( have_side_effects == true ){
        u_data = {
          user_key : data.user_key,
          before_order : 0,
          order : 8,
          user_color : user_color,
        }
      } else {
        u_data = {
          user_key : data.user_key,
          before_order : 0,
          order : data.order,
          user_color : user_color,
        }
      }
      
    }
    else {
      // 이미 선택된 것이 있으니 본인이 다시 선택한 것인지 확인
      for( let i = 0; i < room_data[data.room_name].final_round_order.length; i++ ){
        // 본인 것이 있는 경우 수정
        if ( room_data[data.room_name].final_round_order[i].user_key == data.user_key ){
            if( have_side_effects == true ){
              // 부작용이 있으면 고정되어있어야함
              change_data = true;
            }else {
              room_data[data.room_name].final_round_order[i].before_order = room_data[data.room_name].final_round_order[i].order;
              room_data[data.room_name].final_round_order[i].order = data.order;
              change_data = true;
            }
          }
      }
      // final_round_order는 있으나 현재 데이터가 새것일 경우 // 본인 선택이 없을 경우
      if( u_data == '' && !change_data ){
        if( have_side_effects == true ){
          u_data = {
            user_key : data.user_key,
            before_order : 0,
            order : 8,
            user_color : user_color,
          }
        } else {
          u_data = {
            user_key : data.user_key,
            before_order : 0,
            order : data.order,
            user_color : user_color,
          }
        }
      }
    }
    
    if( u_data != '' ){
      room_data[data.room_name].final_round_order.push(u_data);
    }
  
    room_data[data.room_name].user_cube_data[0] = room_data[data.room_name].final_round_order.length;

    // 클라이언트에서 지우는 행위를 안하기 때문에 취소 문구가 필요 없음
    // 부작용이 있는 상태라면 고를 수 없어야함 msg 대신 alert이 가야함
    
    if( have_side_effects == true ) {
      msg = {
        say : "전 라운드 녹색 음성물약의 부작용으로 순서를 고를 수 없습니다. \n 완료 버튼을 눌러주세요"
      }
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " : 초록 부작용으로 순서 고정!";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
      socket.emit("cant_use_cube", msg);
    }
    else {
      msg = {
        speaker : speaker,
        msg : data.order + '번째 순서를 선택하였습니다.',
        type : 'announce'
      }
      // chat으로 모두에게 알림
      io.to(data.room_name).emit("chat", msg);
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " : " + data.order + "번째 순서 선택";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
    }
    
    // 모두에게 해당 사용자가 선택한 값을 알림
    //console.log(room_data[data.room_name].user_cube_data);
    io.to(data.room_name).emit("select_round_order_recive", room_data[data.room_name].final_round_order);
    io.to(data.room_name).emit("change_user_cube_data", room_data[data.room_name].user_cube_data);
  });

  // overlay에서 인게임 말 놓는 순서를 결정하는 순서를 증가시키는 함수
   // data :: room_name
  socket.on("decide_round_setting_order_counter_incre", (data)=> {
    //console.log(data);
    room_data[data.room_name].decide_round_order_cont += 1;

    // 모든 사람이 결정 했을 때
    if( room_data[data.room_name].room_member_count == room_data[data.room_name].decide_round_order_cont ){

      // 녹색 물약 부작용 끝내기
      for( let i = 0 ; i < room_data[data.room_name].user_data_array.length ; i++ ){
        room_data[data.room_name].user_data_array[i].user_ingame_data.is_check_potion.this_round_green_0 = false;
      }

      // 라운드 시작 준비
      room_data[data.room_name].round_cont += 1;
      io.to(data.room_name).emit("change_round", room_data[data.room_name].round_cont);

      // 1 3 5 라운드 일 때 아티펙트 등급에 따라 아티펙트 변경
      // 3 5 라운드 일때 모험가 타일의 메리트/디메리트 진행
      if( room_data[data.room_name].round_cont == 1 ){
        // 아티펙트 뽑기
        rand_artifacts(data.room_name);

        let send_data = {
          show_artifacts : room_data[data.room_name].show_artifacts,
          rank : 1,
        }
        io.to(data.room_name).emit("can_buy_artifacts_list", send_data);
      }else if ( room_data[data.room_name].round_cont == 3 ) {
        // 아티펙트 뽑기
        rand_artifacts(data.room_name);
        
        let send_data = {
          show_artifacts : room_data[data.room_name].show_artifacts,
          rank : 2,
        }
        io.to(data.room_name).emit("can_buy_artifacts_list", send_data);
      }else if ( room_data[data.room_name].round_cont == 5 ) {
        // 아티펙트 뽑기
        rand_artifacts(data.room_name);
        
        let send_data = {
          show_artifacts : room_data[data.room_name].show_artifacts,
          rank : 3,
        }
        io.to(data.room_name).emit("can_buy_artifacts_list", send_data);
      }
    
      // 고른 order에 따른 금화, 호의, 재료카드를 랜덤하게 추가함
      for( let i = 0; i < room_data[data.room_name].user_data_array.length; i++ ){
        for( let j = 0 ; j < room_data[data.room_name].final_round_order.length; j++){
          if( room_data[data.room_name].user_data_array[i].user_key == room_data[data.room_name].final_round_order[j].user_key){
            switch(room_data[data.room_name].final_round_order[j].order){
              case 1 :
                room_data[data.room_name].user_data_array[i].user_ingame_data.my_gold += 1; 
                break;
              case 2 :
                break;
              case 3 :
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.total += 1;
                break;
              case 4 :
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.total += 2;
                break;
              case 5 :
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.total += 1;
                rand_favor_card(i, data.room_name);
                room_data[data.room_name].user_data_array[i].user_ingame_data.favor_card.total += 1;
                break;
              case 6 :
                rand_favor_card(i, data.room_name);
                rand_favor_card(i, data.room_name);
                room_data[data.room_name].user_data_array[i].user_ingame_data.favor_card.total += 2;
                break;
              case 7 :
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.total += 2;
                rand_favor_card(i, data.room_name);
                room_data[data.room_name].user_data_array[i].user_ingame_data.favor_card.total += 1;
                break;
              case 8 :
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
                room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.total += 1;
                rand_favor_card(i, data.room_name);
                room_data[data.room_name].user_data_array[i].user_ingame_data.favor_card.total += 1;
                break;
            }
            break;
          }
        }
      }

      // 업데이트 된 데이터 중 
      //favor_card가 약초학자라면 바로 재료 3개를 추가하고 
      //호의카드 사용 모달을 띄우도록 클라에 전송
      // 막 전송되면 안되니까 한번이라도 if 안을 타면 전송되게 변경
      /*
      let send_check_bool = false;
      for( let i = 0; i < user_data_array.length; i++ ) {
        if( user_data_array[i].user_ingame_data.ingredient['herbalist'] > 0 ) {
          // 카드 3장을 추가
          user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
          user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
          user_data_array[i].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;

          user_data_array[i].user_ingame_data.ingredient.total += 3;
          // 약초학자 카드 숫자 제거
          user_data_array[i].user_ingame_data.favor_card.herbalist -= 1;

          // 약초학자 카드를 썻다는 카운트 증가
          let first_in_me = true;
          for( let j = 0; j < 4; j++ ){
            if( temp_used_favor_card_list[5][j].user_key == user_data_array[i].user_key ){
              temp_used_favor_card_list[5][j].count++;
              first_in_me = false;
              break;
            }
          }
          if( first_in_me == true ) {
            //첫번째이므로 넣는데 빈공간에 넣어야함
            for(let j = 0; j < 4; j++ ) {
              if( temp_used_favor_card_list[5][j].user_key == '' ){
                temp_used_favor_card_list[5][j].user_key = user_data_array[i].user_key;
                temp_used_favor_card_list[5][j].count++;
                break;
              }
            }
          }

          // 약초학자 카드가 복수 일 수 있으므로 
          i--;

          // 약초학 카드가 한번이라도 사용되었으므로 true로 변경
          send_check_bool = true;
        }
      }

      // 약초학 카드가 사용 되었다면 모달을 호출
      // 보내야 하는 것 :: temp_used_favor_card_list
      //               :: 카드명(card_kind)
      console.log(send_check_bool);
      if( send_check_bool == true ) {
        let send_data = {
          temp_used_favor_card_list : temp_used_favor_card_list,
          card_kind : 'herbalist',
        }
        io.emit("show_favor_modal", send_data);
      }
      
      */
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방의 " + room_data[data.room_name].round_cont  +" 라운드가 시작되었습니다.";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
      // 추가한 카드 데이터를 다시 보냄
      io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);

      // final_round_order를 order 순서에 따라 정렬하여 선택이 끝났음과 함께 알림
      room_data[data.room_name].final_round_order = re_sort_order(room_data[data.room_name].final_round_order);
      room_data[data.room_name].user_cube_data[1] = re_sort_order(room_data[data.room_name].user_cube_data[1]);
      room_data[data.room_name].user_cube_data[2] = re_sort_order(room_data[data.room_name].user_cube_data[2]);
      room_data[data.room_name].user_cube_data[3] = re_sort_order(room_data[data.room_name].user_cube_data[3]);
      room_data[data.room_name].user_cube_data[4] = re_sort_order(room_data[data.room_name].user_cube_data[4]);
      room_data[data.room_name].user_cube_data[5] = re_sort_order(room_data[data.room_name].user_cube_data[5]);
      room_data[data.room_name].user_cube_data[6] = re_sort_order(room_data[data.room_name].user_cube_data[6]);
      room_data[data.room_name].user_cube_data[7] = re_sort_order(room_data[data.room_name].user_cube_data[7]);
      room_data[data.room_name].user_cube_data[8] = re_sort_order(room_data[data.room_name].user_cube_data[8]);
      room_data[data.room_name].user_cube_data[9] = re_sort_order(room_data[data.room_name].user_cube_data[9]);

      io.to(data.room_name).emit("decide_round_setting_order_end", room_data[data.room_name].final_round_order);
      io.to(data.room_name).emit("change_user_cube_data", room_data[data.room_name].user_cube_data);
      room_data[data.room_name].decide_round_order_cont = 0;
      
      for(let i = 0 ; i < room_data[data.room_name].room_member_count; i++){
        clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방의 순서 " + i + ". : " + room_data[data.room_name].final_round_order[i].user_key;
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
        });
        console.log(clog);
      }

    }else {
      // 아직 모두 끝나지 않았을 때
      io.to(data.room_name).emit("decide_round_setting_order_counter_send", room_data[data.room_name].decide_round_order_cont);
    }
  });

  // 큐브 선택
  /*
      user_key,
      board_num : 보드 번호,
      button_order_num : 버튼 순서,
      room_name : 방 이름
  */
  socket.on("select_cube", (data) => {
    let user_data_num = 0;          // user_data_array에서의 유저번호
    let final_round_order_num = 0;  // final_round_order에서의 유저번호
    
    // user_data_array 유저번호 추출
    user_data_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);

    // final_round_order 유저번호 추출
    final_round_order_num = room_data[data.room_name].final_round_order.findIndex(v => v.user_key === data.user_key);
    
    // u 값이 2 이상이면, == 선택한 큐브의 위치가 2번째 이상의 위치라면
    if (data.button_order_num >= 2 ){
      //그 이하의 값이 모두 true인지 확인하고 맞으면 이하로 틀리면 오류출력
      for( let i = data.button_order_num - 1 ; i >= 1; i--){
        // true가 아니라는 것 이므로 오류
        if( !room_data[data.room_name].user_cube_data[data.board_num][final_round_order_num].cube[i].is_select ){
          let err = {
            say : "이전 위치부터 큐브를 두어야 합니다.",
          }
          socket.emit("cant_use_cube", err );
          return;  
        }
      }
    }
    // 통과 했으면 모두 ture라는 이야기 이므로 진행 
    // 선택이 안되어 있다면 - 큐브의 갯수를 검사하여 사용 가능 여부를 판단하고
    //                       사용 가능 하다면 큐브 갯수를 줄이고 선택
    //                       사용 불가라면    오류를 출력하고 반환
    // 선택이 되어 있다면  - 큐브 갯수를 하나 증가하고 선택 취소

    if( !room_data[data.room_name].user_cube_data[data.board_num][final_round_order_num].cube[data.button_order_num].is_select ){
      if( room_data[data.room_name].user_data_array[user_data_num].user_ingame_data.cube_count >= room_data[data.room_name].user_cube_data[data.board_num][final_round_order_num].cube[data.button_order_num].cnt ) {
        room_data[data.room_name].user_cube_data[data.board_num][final_round_order_num].cube[data.button_order_num].is_select = true;
        room_data[data.room_name].user_data_array[user_data_num].user_ingame_data.cube_count -= room_data[data.room_name].user_cube_data[data.board_num][final_round_order_num].cube[data.button_order_num].cnt;
      }
      else {
        let err = {
          say : "사용 가능한 큐브가 없습니다."
        }
        socket.emit("cant_use_cube", err );
        return;
      }
    }
    else {
      // 선택한 번호를 선택 취소하고 
      room_data[data.room_name].user_cube_data[data.board_num][final_round_order_num].cube[data.button_order_num].is_select = false;
      room_data[data.room_name].user_data_array[user_data_num].user_ingame_data.cube_count += room_data[data.room_name].user_cube_data[data.board_num][final_round_order_num].cube[data.button_order_num].cnt;

      //  다음 번호도 선택이 되어있다면 다음 번호도 해제
      for( let i = data.button_order_num ; i <= room_data[data.room_name].user_cube_data[data.board_num][final_round_order_num].length; i++){
        if( room_data[data.room_name].user_cube_data[data.board_num][final_round_order_num].cube[i].is_select ){
          room_data[data.room_name].user_cube_data[data.board_num][final_round_order_num].cube[i].is_select = false;
          room_data[data.room_name].user_data_array[user_data_num].user_ingame_data.cube_count += room_data[data.room_name].user_cube_data[data.board_num][final_round_order_num].cube[i].cnt;
        }
      }
      
    }

    // 수정된 데이터 전송
    io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);

    //io.to(data.room_name).emit("change_final_round_order", room_data[data.room_name].final_round_order);
    io.to(data.room_name).emit("change_user_cube_data", room_data[data.room_name].user_cube_data);
  });

  // 재료 카드 선택 
  /*
    user_key : this.my_key,
    pick_item : 선택한 카드
    cube_order : 큐브 순서
    board_order : this.board_order,
    ingredient_select_arr_order : index,
    room_name : 방 이름
  */
  socket.on("pick_ingredient", (data) =>{
    //console.log(data);
    // 랜덤 카드를 뽑았을 경우
    var num = Math.ceil(Math.random() * 8);
    if( data.pick_item == 0 ) {
      for( var i = 0; i < room_data[data.room_name].user_data_array.length; i++){
        if( room_data[data.room_name].user_data_array[i].user_key === data.user_key ){
          room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient['card_' + num] += 1;
          room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.total += 1;     
          break;
        }
      }
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " : " + num + "번 재료 카드 선택";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
    }
    // 랜덤이 아닌 지정카드를 뽑았을 경우
    else {
      for( let i = 0 ; i < room_data[data.room_name].user_data_array.length; i++){
        if( room_data[data.room_name].user_data_array[i].user_key == data.user_key ) {

          room_data[data.room_name].ingredient_select_arr[data.ingredient_select_arr_order] = 0;

          switch(data.pick_item){
            case 1 :
              room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.card_1 += 1;
              break;
            case 2 :
              room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.card_2 += 1;
              break;
            case 3 :
              room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.card_3 += 1;
              break;
            case 4 :
              room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.card_4 += 1;
              break;
            case 5 :
              room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.card_5 += 1;
              break;
            case 6 :
              room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.card_6 += 1;
              break;
            case 7 :
              room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.card_7 += 1;
              break;
            case 8 :
              room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.card_8 += 1;
              break;
          }
          room_data[data.room_name].user_data_array[i].user_ingame_data.ingredient.total += 1;
          break;
        }
      }
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " : " + data.pick_item + "번 재료 선택";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
    }
    // 재료카드 장수 업데이트
    io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);

    //보드 기능 이후 큐브 및 보드 찾는 함수
    // 재료 선택 보드 이므로 board_num = 1
    board_end_event(data, 1);

  });

  // 추리 테이블 blank x o 선택
  /*
    user_key : this.my_key,
    x : index,
    y : key,
    change_val : this.picked,
    room_name : this.room_name,
  */
  socket.on("reasoning_table_change", (data) => {
    for( let i = 0; i < room_data[data.room_name].result_table.length; i++ ){
      if( data.user_key == room_data[data.room_name].result_table[i].user_key){
        room_data[data.room_name].result_table[i].ingredient_reasoning[data.x][data.y] = data.change_val;
        break;
      }
    }

    socket.emit("change_result_table", room_data[data.room_name].result_table);    
  });

  // 재료 판매 함수
  /*
    user_key : this.my_key,
    sell_item_num : sell_item_num,
    board_order : this.board_order,
    cube_order : this.board_cube_order,
    room_name : this.room_name,
  */
  socket.on("sell_item_confirm", (data) => {
    // 유저 번호 받기
    let user_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);

    //일단 바로 판매
    switch(data.sell_item_num){
      case 1 :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_1 -= 1;
        break;
      case 2 :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_2 -= 1;
        break;
      case 3 :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_3 -= 1;
        break;
      case 4 :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_4 -= 1;
        break;
      case 5 :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_5 -= 1;
        break;
      case 6 :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_6 -= 1;
        break;
      case 7 :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_7 -= 1;
        break;
      case 8 :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_8 -= 1;
        break;
    }

    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " : " + data.sell_item_num + "번 재료 판매";
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);

    // 재료카드를 제거 했으므로 total에서도 제거
    room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.total -= 1;
    // 우선 골드 증가
    room_data[data.room_name].user_data_array[user_num].user_ingame_data.my_gold += 1;

    // 호의 카드 사용 여부 확인
    for(let i = 0 ; i < room_data[data.room_name].room_member_count; i++){
      if( room_data[data.room_name].temp_used_favor_card_list[8][i].user_key == data.user_key ){
        //호의카드를 사용한 것이므로 추가 골드 +1
        // 추가는 카운트 만큼 함
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.my_gold += temp_used_favor_card_list[8][i].count;

        // 모두 사용 했으므로 user_key와 count를 제거
        room_data[data.room_name].temp_used_favor_card_list[8][i].user_key = '';
        room_data[data.room_name].temp_used_favor_card_list[8][i].count = 0;
        break;
      }
    }

    // 재료카드 장수 업데이트
    io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);
    // 보드 기능 이후 다음 보드 / 큐브 찾는 함수
    // 재료 판매 보드 이므로 board_num = 2
    board_end_event(data, 2);
    
  });

  // 재료 판매 취소 함수
  /*  data
     *    user_key      :: 유저 키
     *    board_num     :: 현재 보드에서의 순서 번호
     *    cube_order    :: 큐브 순서 번호( 1, 2, 3 )
     *    room_name     :: 방 이름
     */
  socket.on("cancel_selling_ingre", (data) => {

    board_end_event(data, 2);
  });

  // 용병에게 할인카드 제시 함수
  // 제시한 할인카드는 이번 게임에서 사용 불가
  /*
     data :
      user_key      :: 유저키
      color         :: 유저색
      dis_coin_num  :: 선택한 할인 제시 카드
      room_name     :: 방 이름
  */
  socket.on("adv_dis_confirm", (data) => {
    // 유저 데이터에서 유저 번호 저장해두기
    let user_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);

    // 제시한 할인 카드가 이미 사용되었으면 오류로 메세지 전송
    // 오류가 없으면 제시한 할인 카드 사용 불가 표시
    switch(data.dis_coin_num){
      case '0':
        if ( room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_0 == false ){
          var send_data = {
            say : "이미 제시에 사용한 카드를 고르셨습니다.\n다른 할인 카드를 골라주세요"
          }
          socket.emit("cant_use_cube", send_data);
          return;
        }
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_0 = false;
        break;
      case '1':
        if ( room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_1 == false ){
          var send_data = {
            say : "이미 제시에 사용한 카드를 고르셨습니다.\n다른 할인 카드를 골라주세요"
          }
          socket.emit("cant_use_cube", send_data);
          return;
        }
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_1 = false;
        break;
      case '2':
        if ( room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_2 == false ){
          var send_data = {
            say : "이미 제시에 사용한 카드를 고르셨습니다.\n다른 할인 카드를 골라주세요"
          }
          socket.emit("cant_use_cube", send_data);
          return;
        }
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_2 = false;
        break;
      case '3':
        if ( room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_3 == false ){
          var send_data = {
            say : "이미 제시에 사용한 카드를 고르셨습니다.\n다른 할인 카드를 골라주세요"
          }
          socket.emit("cant_use_cube", send_data);
          return;
        }
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_3 = false;
        break;
    }
    
    // discount_coin_list에 데이터 넣기
    room_data[data.room_name].discount_coin_list.push(data);

    // 해당 보드에 큐브를 올려둔 사람 수 만큼 제시 되면 제시 종료 후 판매로 넘어가야함
    // 보드에 큐브를 올려둔 사람 수 계산
    let adv_sel_round_user_num = 0;
    for( let i = 0; i < room_data[data.room_name].final_round_order.length; i++ ) {
      if( room_data[data.room_name].user_cube_data[3][i].cube[1].is_select == true ){
        adv_sel_round_user_num++;
      }
    }

    // 큐브를 올려둔 사람 수 == 할인제시 유저수 이면 할인제시 변수의 dis_coin_num이 큰 순서대로 정렬해서 넘겨야함
    if( room_data[data.room_name].discount_coin_list.length == adv_sel_round_user_num ){
      room_data[data.room_name].discount_coin_list.sort(function(a,b){
        // 정렬된 오더에서 제시한 카드가 같으면 큐브 순서가 먼저인 쪽으로 변경해야함
        if( a.dis_coin_num > b.dis_coin_num ) return -1;
        else if ( a.dis_coin_num < b.dis_coin_num ) return 1;
        else if( a.dis_coin_num == b.dis_coin_num ) {
          let a_final_num = -1;
          let b_final_num = -1;
          for( let i = 0; i < final_round_order.length; i++ ){
            if( a.user_key == room_data[data.room_name].user_cube_data[3][i].user_key ) {
              a_final_num = i;
            }
            if( b.user_key == room_data[data.room_name].user_cube_data[3][i].user_key ){
              b_final_num = i;
            }
          }
          if( a_final_num > b_final_num ) {
            // a가 더 크면 a가 뒤쪽 순서이므로
            return -1;
          }
          else {
            return 1;
          }
        }
      });
      
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방의 할인 제시 정보";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
      clog = room_data[data.room_name].discount_coin_list;
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
      // 정렬이 완료되면 모든 유저에게 알려야함
      io.to(data.room_name).emit("end_adv_dis_step", room_data[data.room_name].discount_coin_list);
    }
    
  });

  // 용병에게 제시한 판매 금액 홀드
  /*
    data
      user_key  :: 유저키
      sell_price :: 판매가
      room_name  :: 방 이름
  */
  socket.on("sell_price_confirm", (data) => {
    // 우선 데이터를 저장
    room_data[data.room_name].save_selling_potion_price.push(data);

    // 다음 스텝으로 넘어가게 클라에 신호를 보냄
    // 클라에 신호를 보낼 때 호의카드를 사용했는지 여부를 포함해 보냄
    let merchant_use_check = false;
    for( let i = 0 ; i < room_data[data.room_name].room_member_count; i++ ){
      if( room_data[data.room_name].temp_used_favor_card_list[6][i].user_key == data.user_key ){
        merchant_use_check = true;
        break;
      }
    }
    socket.emit("selling_start", merchant_use_check);
  });

  // 용병에게 물약 판매
  /*
    data:
      user_key  :: 유저키
      user_color:: 유저색
      card_list :: 물약을 만들 2가지 재료 (card_1~8)
      what_kind_sell_potion :: 만들 것을 선택한 물약의 종류(red_1,0/ green_1,0 / blue_1,0)\
      selling_turn :: 현재판매 순서
      room_name :: 방 이름
  */
  socket.on("sell_to_adv_potion", (data) => {
    // adventurer_card_data - 판매하는 물약 종류
    // random_adv_list      - 물약 종류를 알 변수 추출
    // discount_coin_list   - 제시 할인가
    // save_selling_potion_price - 유저가 얼마에 판매할지 고른 값 ( 4: 완전일치 / 3: 부호완전일치 / 2: 맞는부호+중성(명성-1) / 1:아무거나(명성-1))
    // 유저 번호 받기
    let user_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);

    // 상인 호의카드 사용 여부 확인
    for( let i = 0 ; i < room_data[data.room_name].room_member_count; i++ ){
      if( room_data[data.room_name].temp_used_favor_card_list[6][i].user_key == data.user_key ){
        // 내 순서가 첫번쨰라면 금화 +1
        if( room_data[data.room_name].discount_coin_list[0].user_key == data.user_key ){
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.my_gold += 1;
          break;
        }
      }
    }

    // 1. 받은 재료로 실험을 진행해 결괏값을 받아옴
    // 1-1 사용한 재료는 차감을 진행
    let card_number_data = [];
    //재료 차감
    for(let i = 0; i < 2; i++){
      switch(data.card_list[i]){
        case 'card_1':
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_1 -= 1;
            card_number_data.push(1);
            break;
        case 'card_2':
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_2 -= 1;
            card_number_data.push(2);
            break;
        case 'card_3':
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_3 -= 1;
            card_number_data.push(3);
            break;
        case 'card_4':
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_4 -= 1;
            card_number_data.push(4);
            break;
        case 'card_5':
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_5 -= 1;
            card_number_data.push(5);
            break;
        case 'card_6':
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_6 -= 1;
            card_number_data.push(6);
            break;
        case 'card_7':
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_7 -= 1;
            card_number_data.push(7);
            break;
        case 'card_8':
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_8 -= 1;
            card_number_data.push(8);
            break;
      }
    };
    // total -2
    room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.total -= 2;
    let ingre_list = {
      x : card_number_data[0],
      y : card_number_data[1],
      user_key : data.user_key,
      room_name : data.room_name,
    };

    let preparate_potion = compare_ingredient(ingre_list);
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " 의 실험 결과 " + preparate_potion;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " 가 만드려는 물약 " + data.what_kind_sell_potion;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    // 2. 결괏값과 만들기로 한 물약의 종류를 비교
    // 2-1. 판매가에따라 다른 결과를 도출
    // 2-1-1. 4골드 판매 :: 완벽히 일치해야함
    // 2-1-2. 3골드 판매 :: 부호만 일치해도 됨
    // 2-1-3. 2골드 판매 :: 중성을 포함한 부호일치 / 명성 -1점
    // 2-1-4. 1골드 판매 :: 아무거나 상관없음 / 명성 -1점
    // 2-1-5. 모든 판매에서 틀리면 명성 -1점 골드x
    // 내 판매가
    let my_selling_price = -1;
    for( let i = 0; i < room_data[data.room_name].save_selling_potion_price.length; i++ ){
      if( room_data[data.room_name].save_selling_potion_price[i].user_key == data.user_key ){
        my_selling_price = room_data[data.room_name].save_selling_potion_price[i].sell_price;
      }
    }
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name +"방 >> " + data.user_key + " 의 판매가 :: " + my_selling_price;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    // 내 할인가
    let my_discount = -1;
    for( let i = 0; i < room_data[data.room_name].discount_coin_list.length; i++ ){
      if( room_data[data.room_name].discount_coin_list[i].user_key == data.user_key ) {
        my_discount = room_data[data.room_name].discount_coin_list[i].dis_coin_num;
      }
    }
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " 의 할인가 :: " + my_discount;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    // 판매 결과 변수 :: 성공적 :true/ 실패 : false
    let selling_success = false;
    // 부호를 비교할 변수
    let result_sign = -1;
    let pre_sign = -1;
    switch(my_selling_price){
      case 4:
        if( preparate_potion == data.what_kind_sell_potion ){
          selling_success = true;
        }
        break;
      case 3:
        // 부호 설정
        if( preparate_potion == 'red_1' || preparate_potion == 'green_1' || preparate_potion == 'blue_1' ){
          pre_sign = 1;
        } else if ( preparate_potion == 'blank' ){
          pre_sign = 2;
        }
        else {
          pre_sign = 0;
        }

        // 정답은 공백이 있을 수 없으므로 추가 예외처리 필요 x
        if( data.what_kind_sell_potion == 'red_1' || data.what_kind_sell_potion == 'green_1' || data.what_kind_sell_potion == 'blue_1' ){
          result_sign = 1;
        } else {
          result_sign = 0;
        }

        // 두 부호 비교
        if( result_sign == pre_sign ){
          // 같으면 성공
          selling_success = true;
        }
        break;
      case 2:
        // 부호 설정
        if( preparate_potion == 'red_1' || preparate_potion == 'green_1' || preparate_potion == 'blue_1' ){
          pre_sign = 1;
        } else if ( preparate_potion == 'blank' ){
          pre_sign = 2;
        } else {
          pre_sign = 0;
        }

        // 정답은 공백이 있을 수 없으므로 추가 예외처리 필요 x
        if( data.what_kind_sell_potion == 'red_1' || data.what_kind_sell_potion == 'green_1' || data.what_kind_sell_potion == 'blue_1' ){
          result_sign = 1;
        } else {
          result_sign = 0;
        }

        // 두 부호를 비교하는데 공백 포함
        if( result_sign == pre_sign || pre_sign == 2 ){
          selling_success = true;
        }
        break;
      case 1:
        selling_success = true;
        break;
    }

    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " 의 물약 판매 결과 :: " + selling_success;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);

    // 3. 판매 가능한 물약 리스트에서 판매된 (판매에 성공한)물약을 false로 변경 - 다음사람은 해당 물약을 못팜
    if( selling_success == true ){
      // 판매가 성공하였으므로 제시한 할인가를 제한 골드를 증여
      // 술집 점원 호의카드 사용 여부 확인
      for( let i = 0 ; i < room_data[data.room_name].room_member_count; i++ ){
        if( room_data[data.room_name].temp_used_favor_card_list[2][i].user_key == data.user_key ){
          // 술집 점원 카드를 사용한 것이므로 정확하게 일치하는 물약이었다면 명성 +1
          if ( preparate_potion == data.what_kind_sell_potion ){
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += room_data[data.room_name].temp_used_favor_card_list[2][i].count;
          }
          else {
            // 정확하게 일치하지 않으면 전보다 한단계 좋은 물약으로 침 == 골드 +1
            // 사용한 갯수만큼 증가하므로 count만큼 증가
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.my_gold += room_data[data.room_name].temp_used_favor_card_list[2][i].count;
          }

          // 사용이 종료되었으므로 호의카드 사용 리스트에서 제거
          room_data[data.room_name].temp_used_favor_card_list[2][i].user_key = '';
          room_data[data.room_name].temp_used_favor_card_list[2][i].count = 0;
          break;
        }
      }
      room_data[data.room_name].user_data_array[user_num].user_ingame_data.my_gold += ( my_selling_price - my_discount );
      // 1,2 골드에 판매하면 명성 1점 감점
      if( my_selling_price == 2 || my_selling_price == 1 ){
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.point -= 1;
      }

      // 판매에 성공한 what_kind_sell_potion에 맞게 false로 변경
      room_data[data.room_name].adventurer_card_data[room_data[data.room_name].round_cont][data.what_kind_sell_potion] = false;

      // 판매 완료를 채팅으로 알림
      let msg = {
        speaker : room_data[data.room_name].user_data_array[user_num].user_name,
        msg     : "용병에게 물약 판매를 성공하였습니다. 판매 금액은 " + my_selling_price + "원 입니다.",
        type    : "announce"
      }
      io.to(data.room_name).emit("chat", msg);
    }
    // 판매가 실패하면 명성 -1점
    else {
      room_data[data.room_name].user_data_array[user_num].user_ingame_data.point -= 1;
      // 채팅으로 알림
      let msg = {
        speaker : room_data[data.room_name].user_data_array[user_num].user_name,
        msg     : "용병에게 물약 판매를 실패하였습니다.",
        type    : "announce"
      }
      io.to(data.room_name).emit("chat", msg);
    }
    
    // 3.1 제시한 할인 카드도 false로 변경
    switch(my_discount){
      case 0:
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_0 = false;
        break;
      case 1:
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_1 = false;
        break;
      case 2:
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_2 = false;
        break;
      case 3:
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.discount_adventurer.ad_3 = false;
        break;
    }
    

    // 4. 다음 순서로 넘겨야함
    // 4-1. 이번 사용자의 큐브를 제거
    for( let i = 0 ; i < room_data[data.room_name].room_member_count ; i++ ){
      if( room_data[data.room_name].user_cube_data[3][i].user_key == data.user_key ){
        room_data[data.room_name].user_cube_data[3][i].cube[1].is_select = false;
        break;
      }
    }
    // 4-1-1 마지막일 경우 완전히 끝남을 알려야함 == 다음 큐브 유무가 있는지 확인함
    let left_user_num = 0;
    let perfect_end = false;
    for( let i = 0; i < room_data[data.room_name].room_member_count; i++ ){
      if( room_data[data.room_name].user_cube_data[3][i].cube[1].is_select == true ){
        left_user_num++;
      }
    }
    if( left_user_num <= 0 ){
      perfect_end = true;
    }

    // 4-2. selling_turn ++
    // 바로 변경데이터 전송 -- selling_turn은 모든 사용자가 공유해야하므로 io
    // 성공 실패 관계 없이 종료 되었으므로 끝
    io.to(data.room_name).emit("change_selling_turn", data.selling_turn+1);
    // 수정된 정답 전송
    io.to(data.room_name).emit("adv_sell_potion_list", room_data[data.room_name].adventurer_card_data);
    
    // 다음 순서를 알려주는 함수 진행
    // 성공 / 실패 여부를 모두에게 알리고
    let send_data = {
      selling_success : selling_success,
      potion : data.what_kind_sell_potion,
      user_key : data.user_key,
      user_color : data.user_color,
    }
    io.to(data.room_name).emit("selling_potion_end", send_data);
    // 명성과 골드 정보 전송
    io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);

    // perfect_end가 true 면 보드 종료를 알림
    if( perfect_end == true ){
      let end_data = {
        board_order : 0,
        cube_order : 1,
        room_name : data.room_name,
      }
      
      // 보드가 완전히 종료되고 다른 보드로 넘어가야함
      board_end_event(end_data, 3);
    }
    
  });

  // 아티펙트 구매 함수
  /*
    board_order : this.board_order,
    cube_order : this.board_cube_order,
    rank : data.rank,
    arti_num : data.num,
    user_key : this.my_key,
    room_name : this.room_name,
  */
  socket.on("buy_artifact_confirm", (data) => {
    console.log(data);
    // 유저 정보 저장
    let user_num = -1;
    let pre_gold = 0;
    for(let i = 0; i < room_data[data.room_name].user_data_array.length; i++){
      if( room_data[data.room_name].user_data_array[i].user_key == data.user_key ){
        user_num = i;
        console.log(user_num);
        pre_gold = room_data[data.room_name].user_data_array[i].user_ingame_data.my_gold;
        break;
      }
    }
    
    //아티팩트 구매
    var arti_rank = data.rank;
    // shopkeeper 호의카드가 사용되었다면 필요 골드를 -1 함
    var need_gold = room_data[data.room_name].artifacts[arti_rank][data.arti_num].gold;
    // 가게주인 호의카드가 있을 경우 필요골드 -1;
    for ( let i = 0; i < room_data[data.room_name].room_member_count; i++ ){
      if( room_data[data.room_name].temp_used_favor_card_list[7][i].user_key == data.user_key ){
        // 호의 카드 사용 수 만큼 골드 차액
        need_gold -= room_data[data.room_name].temp_used_favor_card_list[7][i].count;

        // 호의 카드 사용 중인 것 해제
        room_data[data.room_name].temp_used_favor_card_list[7][i].user_key = '';
        room_data[data.room_name].temp_used_favor_card_list[7][i].count = 0;
        break;
      }
    }

    if( pre_gold < need_gold ){
      var send_data = {
        say : "골드가 모자랍니다!\n 다른 아티펙트를 구매해 주세요."
      }
      socket.emit("cant_use_cube", send_data);
      return;
    }
    // 골드가 구매할 아티펙트의 가격과 같거나 크다면
    else {
      // 오류로 인해 구매할 수 없는 아이템에 대해 구매를 진행하였을 경우
      if(room_data[data.room_name].artifacts[arti_rank][data.arti_num].is_selled == true) {
        console.log(arti_rank);
        console.log(data.arti_num);
        var send_data = {
          say : "해당 아이템은 누군가 가지고 있습니다. \n 구매할 수 없습니다."
        }
        socket.emit("cant_use_cube",send_data);
        return;
      }
      // 구매 개시
      // 서버 정보에서 더이상 구매하지 못하게 막음
      room_data[data.room_name].artifacts[arti_rank][data.arti_num].is_selled = true;
      var arti_name = room_data[data.room_name].artifacts[arti_rank][data.arti_num].name;
      console.log("구매할 유물 이름 >> " + arti_name);
      // 구매 확정
      room_data[data.room_name].user_data_array[user_num].user_ingame_data.artifacts[arti_name] = true;
      console.log(room_data[data.room_name].user_data_array[user_num].user_ingame_data.artifacts[arti_name]);

      room_data[data.room_name].user_data_array[user_num].user_ingame_data.my_gold -= need_gold;
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') + " > " + data.room_name + "방 >> " + data.user_key + " 의 아티팩트 구매!  :: " + arti_name;      // 구매한 아티펙트를 뒤집어야함 == 구매하지 못하게!
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
      //console.log(room_data[data.room_name].show_artifacts.length);
      for( let i = 0 ; i < room_data[data.room_name].show_artifacts.length; i++){
        if( room_data[data.room_name].show_artifacts[i] == data.arti_num ){
          room_data[data.room_name].show_artifacts[i] = -1;
          break;
        }
      }
      let send_arti_rank = 0;
      switch(data.rank){
        case "rank_1" : send_arti_rank = 1; break;
        case "rank_2" : send_arti_rank = 2; break;
        case "rank_3" : send_arti_rank = 3; break;
      }
      let artifacts_data = {
        show_artifacts : room_data[data.room_name].show_artifacts,
        rank : send_arti_rank,
      }
      // 아티팩트 구매 완료를 chat으로 알림
      let msg = {
        speaker : room_data[data.room_name].user_data_array[user_num].user_name,
        msg     : arti_name + " 을(를) 구매했습니다." ,
        type    : "announce"
      }
      io.to(data.room_name).emit("chat", msg);
      
      //console.log(artifacts_data);
      //구매한 정보 업데이트
      io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);
      //표시될 아티펙트 정보 업데이트
      io.to(data.room_name).emit("can_buy_artifacts_list", artifacts_data);
    }

    // return 되지 않고 여기에 오면 구매가 진행되었다고 확신
    // 아티팩트 구매 이므로 4
    board_end_event(data, 4);
  });

  // 실험 함수
  /*
    user_key : this.my_key,
    card_list : this.test_ingredient_list,
    caretaker_used : false,
    board_order : this.board_order,
    cube_order : this.board_cube_order,
    board_is : board_is,
    room_name : this.room_name,
  */
  socket.on("test_ingredient_confirm", (data) => {
    console.log(data);
    //유저 번호 찾기
    let user_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);
    
    // 이전 실험결과에 따른 디버프 유무
    // 학생에게 실험하기 칸이였을 경우
    if( data.board_is == 7 ){
      //실험결과가 색 관계 없이 - 였었다면( 이전실험이 / 당장 실험은 디버프가 없음) 금화가 있어야 실험 가능
      // 금화가 없으면 실험 중지 // 있으면 1개 차감
      if( room_data[data.room_name].round_test_result[0].red_0 || room_data[data.room_name].round_test_result[2].blue_0 || room_data[data.room_name].round_test_result[4].green_0 ){
        if( room_data[data.room_name].user_data_array[user_num].user_ingame_data.my_gold > 0 ) {
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.my_gold -= 1;
        }
        else {
          let send_data = {
            say : "골드가 모자랍니다. 실험을 하실 수 없습니다."
          }
          socket.emit("cant_use_cube", send_data);
          return;
        }
      }
    }

    let card_number_data = [];
    //재료 차감
    for(let i = 0; i < 2; i++){
      switch(data.card_list[i]){
        case "card_1":
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_1 -= 1;
            card_number_data.push(1);
            break;
        case "card_2":
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_2 -= 1;
            card_number_data.push(2);
            break;
        case "card_3":
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_3 -= 1;
            card_number_data.push(3);
            break;
        case "card_4":
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_4 -= 1;
            card_number_data.push(4);
            break;
        case "card_5":
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_5 -= 1;
            card_number_data.push(5);
            break;
        case "card_6":
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_6 -= 1;
            card_number_data.push(6);
            break;
        case "card_7":
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_7 -= 1;
            card_number_data.push(7);
            break;
        case "card_8":
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_8 -= 1;
            card_number_data.push(8);
            break;
      }
    }
    room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.total -= 2;
    
    // 실험 진행
    let test_ingredient = {
      x : card_number_data[0],
      y : card_number_data[1],
      user_key : data.user_key,
      room_name : data.room_name,
    }
    let result_test = compare_ingredient(test_ingredient);
      
    // 재료 비교 결과 전송
    socket.emit("change_result_table", room_data[data.room_name].result_table);
    // 결과 에 따라 분기
    // 자신이 실험했을 때 결과가 - 고 자신의 첫 번째라면 디버프 받음
    switch(result_test){
      case "red_0" :
        if( data.board_is == 8 ){
          // 지금이 이번 라운드 첫 red_0 인 경우
          // 다음 턴 큐브 카운트 -1 // 한 큐브는 병원으로 
          // 변수로 조절
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.is_check_potion.this_round_red_0 = true;
        }
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.is_check_potion.red_0 = true;
        room_data[data.room_name].round_test_result[0].red_0 = true;
        break;
      case "red_1" :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.is_check_potion.red_1 = true;
        room_data[data.room_name].round_test_result[1].red_1 = true;
        break;
      case "blue_0" :
        if( data.board_is == 8 ){
          // 지금이 이번 라운드 첫 blue_0 인 경우
          // 명성 점수 -1
          // 변수로 조절
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.is_check_potion.this_round_blue_0 = true;
        }
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.is_check_potion.blue_0 = true;
        room_data[data.room_name].round_test_result[2].blue_0 = true;
        break;
      case "blue_1" :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.is_check_potion.blue_1 = true;
        room_data[data.room_name].round_test_result[3].blue_1 = true;
        break;
      case "green_0" :
        if( data.board_is == 8 ){
          // 지금이 이번 라운드 첫 green_0 인 경우
          // 다음 라운드 순서가 무조건 8번
          // 변수로 조절
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.is_check_potion.this_round_green_0 = true;
        }
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.is_check_potion.green_0 = true;
        room_data[data.room_name].round_test_result[4].green_0 = true;
        break;
      case "green_1" :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.is_check_potion.green_1 = true;
        room_data[data.room_name].round_test_result[5].green_1 = true;
        break;
      case "blank" :
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.is_check_potion.blank = true;
        room_data[data.room_name].round_test_result[6].blank = true;
        break;
    }

    let send_data = {
      user_key : data.user_key,
      test_result : result_test,
    };
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " 의 " + data.board_is + "번 보드 실험 결과 : " + result_test;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);

    // 결과를 챗으로도 전송
    var result_test_str;
    switch(result_test){
      case "red_0" :
        result_test_str = "붉은색 -"
        break;
      case "red_1" :
        result_test_str = "붉은색 +"
        break;
      case "blue_0" :
        result_test_str = "푸른색 -"
        break;
      case "blue_1" :
        result_test_str = "푸른색 +"
        break;
      case "green_0" :
        result_test_str = "녹색 -"
        break;
      case "green_1" :
        result_test_str = "녹색 +"
        break;
      case "blank" :
        result_test_str = "투명색"
        break;
    }
    let msg = {
      speaker : room_data[data.room_name].user_data_array[user_num].user_name,
      msg     : "실험 결과는 " + result_test_str + " 물약 입니다.",
      type    : "announce"
    }
    io.to(data.room_name).emit("chat", msg);

    //결과 전송
    io.to(data.room_name).emit("test_ingredient_result", send_data);
    io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);
    //보드 함수 진행
    // 실험 인데 학생 실험이면 7, 자기 실험이면 8
    if( data.caretaker_used == false ){
      board_end_event(data, data.board_is);
    }
    else {
      caretaker_board_end_event(data);
    }
    
  });

  // 논문 발표 함수
  // 발표한 재료와 원소, 발표자를 받아서 저장함 - 금화 한개 차감
  // data ::
  //  ele :: element 번호
  //  ingre :: 현재 선택된 재료 번호
  //  stamp :: 발표자가 사용한 stamp
  //  user_key , user_color
  //  cube_order : 큐브 순서 
  //  board_order: 보드 순서
  //  room_name :: 방 이름
  socket.on("presentation_theory", (data) => {

    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + ">> 방 " + data.user_key + "의 논문 발표 시작";
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    //console.log(data);
    // 발표된 재료에 맞게 theory_data를 수정해야함
    // 발표자의 stamp를 false로 변경하고 total -= 1
    // 해당 원소의 첫번째 순서라면 바로 적용
    // 원소의 발표가 아니라 단순히 stamp 만을 추가한 것이라면 element 수정이 아닌 stamp의 다음 번호에 추가한다.
    // ele가 0 이상이면 발표/ 0이면 stamp 추가!

    // 
    for(var i = 0; i < room_data[data.room_name].user_data_array.length; i++){
      if( room_data[data.room_name].user_data_array[i].user_key == data.user_key ){
        if( room_data[data.room_name].user_data_array[i].user_ingame_data.my_gold > 0 ){
          room_data[data.room_name].user_data_array[i].user_ingame_data.my_gold -= 1;
          break;
        }
        else {
          var send_data = {
            say : "골드가 부족합니다. 발표가 불가능하니 차례를 넘겨주세요"
          }
          clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " : 골드 부족으로 발표 불가";
          log = clog + "\n";
          fs.appendFile(dir, log, (err) => {
            if( err ) throw err;
            console.log(clog);
          });
          socket.emit("cant_use_cube", send_data);
          return;
        }
      }
    }

    let stamp_order_num = 0;
    // 데이터가 아니라 발표된 원소를 살피는게 맞다
    if (room_data[data.room_name].theory_data[data.ingre].element < 1 || 
        room_data[data.room_name].theory_data[data.ingre].element > 8   )  
    {
      room_data[data.room_name].theory_data[data.ingre].element = data.ele;
      // 발표이므로 반드시 1번 stamp에 저장
      room_data[data.room_name].theory_data[data.ingre].stamp[1].user_key = data.user_key;
      room_data[data.room_name].theory_data[data.ingre].stamp[1].color = data.user_color;
      // stamp 추가 : 번호에 따라 string으로 넣어야함
      // 따라서 아래에서 stamp와 total 수정할 때 한 번에 넣기
      stamp_order_num = 1;
      //room_data[data.room_name].theory_data[data.ingre].stamp[1].point = data.stamp;
      
    }
    // 발표가 아니라 의견 추가
    else if ( data.ele == 0 ||
             (room_data[data.room_name].theory_data[data.ingre].element > 0 &&
              room_data[data.room_name].theory_data[data.ingre].element < 9)) 
    {
      // 2번 검사 후 user_key가 공란이면 저장 아니면 3번으로 저장
      // 3번까지 저장되어 있다면 오류 메세지 전송
      if( room_data[data.room_name].theory_data[data.ingre].stamp[2].user_key == '' ){
        room_data[data.room_name].theory_data[data.ingre].stamp[2].user_key = data.user_key;
        room_data[data.room_name].theory_data[data.ingre].stamp[2].color = data.user_color;
        stamp_order_num = 2;
        //room_data[data.room_name].theory_data[data.ingre].stamp[2].point = data.stamp;
      } else if ( room_data[data.room_name].theory_data[data.ingre].stamp[3].user_key == '' ){
        room_data[data.room_name].theory_data[data.ingre].stamp[3].user_key = data.user_key;
        room_data[data.room_name].theory_data[data.ingre].stamp[3].color = data.user_color;
        stamp_order_num = 3;
        //room_data[data.room_name].theory_data[data.ingre].stamp[3].point = data.stamp;
      }
      // 두 조건 모두 막히면 모든 stamp가 저장되어 있는 상태이므로 선택 불가!
      else{
        var send_data = {
          say : "모든 도장이 찍혀있습니다.\n 다른 재료에 도장을 찍어주세요!"
        }
        socket.emit("cant_use_cube", send_data);
        // 다시 시작을 알리기
        var send_data = {
          user_key : data.user_key,
          restart : true,
          success : false,
        }
        socket.emit("theory_presentation_success", send_data);
        return;
      }
    }

    // 사용한 유저 데이터의 stmap 와 total 제거
    for(var i = 0; i < room_data[data.room_name].user_data_array.length; i++){
      if( room_data[data.room_name].user_data_array[i].user_key == data.user_key ){
        switch(data.stamp) {
          case 1:
            room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.point_5_1 = false;
            room_data[data.room_name].theory_data[data.ingre].stamp[stamp_order_num].point = "point_5_1";
            break;
          case 2:
            room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.point_5_2 = false;
            room_data[data.room_name].theory_data[data.ingre].stamp[stamp_order_num].point = "point_5_2";
            break;
          case 3:
            room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.point_3_1 = false;
            room_data[data.room_name].theory_data[data.ingre].stamp[stamp_order_num].point = "point_3_1";
            break;
          case 4:
            room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.point_3_2 = false;
            room_data[data.room_name].theory_data[data.ingre].stamp[stamp_order_num].point = "point_3_2";
            break;
          case 5:
            room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.point_3_3 = false;
            room_data[data.room_name].theory_data[data.ingre].stamp[stamp_order_num].point = "point_3_3";
            break;
          case 6:
            room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.question_red_1 = false;
            room_data[data.room_name].theory_data[data.ingre].stamp[stamp_order_num].point = "question_red_1";
            break;
          case 7:
            room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.question_red_2 = false;
            room_data[data.room_name].theory_data[data.ingre].stamp[stamp_order_num].point = "question_red_2";
            break;
          case 8:
            room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.question_green_1 = false;
            room_data[data.room_name].theory_data[data.ingre].stamp[stamp_order_num].point = "question_green_1";
            break;
          case 9:
            room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.question_green_2 = false;
            room_data[data.room_name].theory_data[data.ingre].stamp[stamp_order_num].point = "question_green_2";
            break;
          case 10:
            room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.question_blue_1 = false;
            room_data[data.room_name].theory_data[data.ingre].stamp[stamp_order_num].point = "question_blue_1";
            break;
          case 11:
            room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.question_blue_2 = false;
            room_data[data.room_name].theory_data[data.ingre].stamp[stamp_order_num].point = "question_blue_2";
            break;
        }
        room_data[data.room_name].user_data_array[i].user_ingame_data.have_stamp.total -= 1;
        // 명성점수 += 1
        room_data[data.room_name].user_data_array[i].user_ingame_data.point += 1;
        break;
      }
    }

    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> "+ data.user_key + "가 " + data.ingre + "번 원소에 " + data.ele + "로 논문 발표";
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    // 정보 업데이트 
    // user_data_array / theory_data 
    io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);
    io.to(data.room_name).emit("change_theory_data", room_data[data.room_name].theory_data);

    // 성공 했다고 알리기
    var send_data = {
      user_key : data.user_key,
      restart : false,
      success : true,
    }
    io.to(data.room_name).emit("theory_presentation_success", send_data);

    let user_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);
    // 성공을 챗으로도 알림
    let msg = {
      speaker : room_data[data.room_name].user_data_array[user_num].user_name,
      msg     : "논문 발표에 성공하였습니다.",
      type    : "announce"
    }
    
    io.to(data.room_name).emit("chat", msg);

    // 보드 함수 진행
    // 논문 발표이므로 6
    board_end_event(data, 6);
  });

  // 논문 반박 함수
  // 받을 data
  /*
    ingre    : 선택된 원소          1~8 num
    ori      : 반박할 원소의 색깔    1 red  2 green 3 blue num
    user_key : 반박한 사람          string
    arr      : 원소의 색깔이 틀렸다는 것을 주장하기 위한 2가지 재료 1~8 *2
    cube_order : 큐브 순서 
    board_order: 보드 순서
    room_name : 방이름
  */
  socket.on("refuting_theory_data",(data) => {
    //console.log(data);
    // 선택한 원소에 반박할 주장이 있어야 가능함!
    // 선택한 원소에 ele가 있으면 발표된 자료가 있는 것으로 간주
    if( room_data[data.room_name].theory_data[data.ingre].element == '' ){
      var send_data = {
        say : "발표된 자료가 없습니다. 발표된 적이 있는 원소만 반박 가능합니다."
      }
      socket.emit("cant_use_cube", send_data);
      send_data = {
        user_key : data.user_key,
        restart : true,
        success : false,
      }
      socket.emit("theory_refute_success", send_data);
      return;
    }

    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + "가 " + data.ingre + "번 원소에 " + data.ori + "가 틀리다고 논문 반박";
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    // 반박한 원소와 원소의 색깔에 따라 결과가 달라짐
    // 반박한 원소의 정답 을 불러옴
    // ori :: 1 - red / 2 - green / 3 - blue

    // 현재 발표되어 있는 원소의 선택된 색의 번호를 저장
    // 0 : -, 1 : +
    let pre_ele_num = -1;
    switch(room_data[data.room_name].theory_data[data.ingre].element){
      case 1:
        if( data.ori == '1' || data.ori == '3' ){
          pre_ele_num = 0;
        }
        else if ( data.ori == '2' ){
          pre_ele_num = 1;
        }
        break;
      case 2:
        if( data.ori == '1' || data.ori == '3' ){
          pre_ele_num = 1;
        }
        else if ( data.ori == '2' ){
          pre_ele_num = 0;
        }
        break;
      case 3:
        if( data.ori == '2' || data.ori == '3' ){
          pre_ele_num = 1;
        }
        else if ( data.ori == '1' ){
          pre_ele_num = 0;
        }
        break;
      case 4:
        if( data.ori == '2' || data.ori == '3' ){
          pre_ele_num = 0;
        }
        else if ( data.ori == '1' ){
          pre_ele_num = 1;
        }
        break;
      case 5:
        if( data.ori == '1' || data.ori == '2' ){
          pre_ele_num = 0;
        }
        else if ( data.ori == '3' ){
          pre_ele_num = 1;
        }
        break;
      case 6:
        if( data.ori == '1' || data.ori == '2' ){
          pre_ele_num = 1;
        }
        else if ( data.ori == '3' ){
          pre_ele_num = 0;
        }
        break;
      case 7:
        pre_ele_num = 0;
        break;
      case 8:
        pre_ele_num = 1;
        break;
    }
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + "가 반박한 원소 색의 발표되어 있는 값 0 1 : " + pre_ele_num;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    // 정답의 원소 색 번호를 저장
    // 0 : -, 1 : +
    let test_ingredient = {
      x : data.arr[0],
      y : data.arr[1],
      user_key : data.user_key,
      room_name : data.room_name,
    }
    let answer = compare_ingredient(test_ingredient);
    let ref_ele_num = -1;
    switch(answer){
        case "red_1": 
        case "green_1": 
        case "blue_1":
          ref_ele_num = 1;
          break;
        case "red_0":
        case "green_0":
        case "blue_0":
          ref_ele_num = 0;
          break;
        case "blank":
          ref_ele_num = -1;
          break;
    }
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> "+ data.user_key + " : 반박한 원소의 증명 색의 0 1 : " + ref_ele_num;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);

    // 정답 색 찾기
    let real_answer = room_data[data.room_name].ingredient_answer[data.ingre - 1];
    let real_ele_num = -1;
    switch(real_answer){
      case 1:
        if( data.ori == '1' || data.ori == '3' ){
          real_ele_num = 0;
        }
        else if ( data.ori == '2' ){
          real_ele_num = 1;
        }
        break;
      case 2:
        if( data.ori == '1' || data.ori == '3' ){
          real_ele_num = 1;
        }
        else if ( data.ori == '2' ){
          real_ele_num = 0;
        }
        break;
      case 3:
        if( data.ori == '2' || data.ori == '3' ){
          real_ele_num = 1;
        }
        else if ( data.ori == '1' ){
          real_ele_num = 0;
        }
        break;
      case 4:
        if( data.ori == '2' || data.ori == '3' ){
          real_ele_num = 0;
        }
        else if ( data.ori == '1' ){
          real_ele_num = 1;
        }
        break;
      case 5:
        if( data.ori == '1' || data.ori == '2' ){
          real_ele_num = 0;
        }
        else if ( data.ori == '3' ){
          real_ele_num = 1;
        }
        break;
      case 6:
        if( data.ori == '1' || data.ori == '2' ){
          real_ele_num = 1;
        }
        else if ( data.ori == '3' ){
          real_ele_num = 0;
        }
        break;
      case 7:
        real_ele_num = 0;
        break;
      case 8:
        real_ele_num = 1;
        break;
    }
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> "+ data.user_key + " : 반박한 원소의 정답 색의 0 1 : " + real_ele_num;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);


    // 증명 원소와 정답 부호가 같으면 반박 실패
    if ( real_ele_num == ref_ele_num && pre_ele_num != ref_ele_num ){
      // 반박에 성공 시 
      // 반박 주인은 명성 2점 +
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> "+ data.user_key + " : 논문 반박 성공!";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
      for(let i = 0; i < room_data[data.room_name].user_data_array.length; i++){
        if( room_data[data.room_name].user_data_array[i].user_key == data.user_key ){
          room_data[data.room_name].user_data_array[i].user_ingame_data.point += 2;
        }
      }

      // 반박된 요소와 일치한 색의 인장은 단순제거
      // 요소과 겹치지 않는 색은 -5점
      // 금/ 은 인장도 -5점
      // -> 변수를 확인하고 인장을 올린 유저들의 명성을 -5점 깎음
      let question_1 = '';
      let question_2 = '';
      if( data.ori == '1' ){ // red
        question_1 = 'question_red_1';
        question_2 = 'question_red_2';
      }else if ( data.ori == '2' ){
        question_1 = 'question_green_1';
        question_2 = 'question_green_2';
      }else if ( data.ori == '3' ){
        question_1 = 'question_blue_1';
        question_2 = 'question_blue_2';
      }
      for(let i = 1; i <= 3; i++){
        if( room_data[data.room_name].theory_data[data.ingre].stamp[i].user_key != '' ){
          // 스탬프 확인
          // 반박된 색과 다르면 무조건 -5점 감점이므로 반박된 색과 question_color 가 다르면 감점
          // question_1이나 2가 하나라도 있으면 패스, 
          if( room_data[data.room_name].theory_data[data.ingre].stamp[i].point != question_1 && 
              room_data[data.room_name].theory_data[data.ingre].stamp[i].point != question_2 ){
            // user_key 번호 저장
            let user_i = -1;
            for(let j = 0; i < room_data[data.room_name].user_data_array.length; j++){
              if( room_data[data.room_name].user_data_array[j].user_key == room_data[data.room_name].theory_data[data.ingre].stamp[i].user_key ){
                user_i = j;
                break;
              }
            }
            
            // 5점 감점
            room_data[data.room_name].user_data_array[user_i].user_ingame_data.point -= 5;
          }
          // 위 조건에 통과 되지 않음 == question_1 || _2 가 true 인 것이므로 
          // 감점 없이 사라짐  따라서 할거 없음...
          // else 생략
        }
      }

      // 반박에 성공한 원소에 올려져 있는 인장을 전부 공개하고
      // 클라이언트에서 모달을 통해 모두에게 알리는 방식 사용
      // 제거된 인장은 돌려주지 않음 == 이번 게임에서 더이상 사용할 수 없음 -> 변수 안건드리면 끝
      //  ㄴ 안내멘트 나오도록 클라이언트에서 처리
      // send_data :: theory_data의 반박된 원소의 정보들 중 유의미한 정보들
      //    ingre_num : 재료번호
      //    ori       : 반박된 원소색
      //    stamp : user_key
      //            color
      //            point
      //  -> stamp는 for문으로 user_key가 있는 경우에 넣고 아니면 break -> 순서대로 넣으므로
      let stamp_data = [];
      for( let i = 1 ; i <= 3; i++){
        if( room_data[data.room_name].theory_data[data.ingre].stamp[i].user_key != '' ){
          var tmp = {
            user_key : room_data[data.room_name].theory_data[data.ingre].stamp[i].user_key,
            color : room_data[data.room_name].theory_data[data.ingre].stamp[i].color,
            point : room_data[data.room_name].theory_data[data.ingre].stamp[i].point,
          }
          stamp_data.push(tmp);
          tmp = '';
        }
      }
      let send_data = {
        user_key : data.user_key,
        room_name : data.room_name,
        ingre_num : data.ingre,
        ori : data.ori,
        success : true,
        stamp : stamp_data,
      }
      io.to(data.room_name).emit("open_stamp_modal", send_data);

      // 반박한 사람이 발표에 큐브를 두었을 경우 해당 큐브를 즉시 사용하고 발표 가능
      // 반박 성공을 채팅으로 알림
      var user_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);
      let msg = {
        speaker : room_data[data.room_name].user_data_array[user_num].user_name,
        msg     : "논문 반박에 성공하였습니다!",
        type    : "announce"
      }
      io.to(data.room_name).emit("chat", msg);
    }
    // 반박 실패 
    else {
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> "+ data.user_key + " : 반박 실패!";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
      console.log(clog);
      // 반박 실패 :: 명성 -1 점
      for(let i = 0; i < room_data[data.room_name].user_data_array.length; i++){
        if( room_data[data.room_name].user_data_array[i].user_key == data.user_key ){
          room_data[data.room_name].user_data_array[i].user_ingame_data.point -= 1;
        }
      }

      let send_data = {
        user_key : data.user_key,
        room_name : data.room_name,
        ingre_num : data.ingre,
        ori : data.ori,
        success : false,
        stamp : '',
      }
      io.to(data.room_name).emit("open_stamp_modal", send_data);

      // 반박 실패를 채팅으로 알림
      var user_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);
      let msg = {
        speaker : room_data[data.room_name].user_data_array[user_num].user_name,
        msg     : "안타깝게도 논문 반박에 실패하였습니다.",
        type    : "announce"
      }
      io.to(data.room_name).emit("chat", msg);
    }
    
    // 업데이트 데이터 전송
    // user_data_array / theory_data 
    io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);

    // 보드 종료 이벤트 발생
    // 논문 반박이므로 5
    board_end_event(data, 5);
  });

  // 반박 성공 모달 확인창을 받는 함수
  // data :
  //    ingre_num : 재료 번호
  //    room_name : 방 이름
  //    stamp :
  //         user_key : 유저 번호
  //         color    : 스탬프 색
  //         point    : 인장 종류
  // 우선 반박 확인인원 증가 후
  socket.on("check_refute_info", (data) => {
    console.log(data);
    checking_refute_info_num += 1;
    console.log("확인 인원 :: " + checking_refute_info_num);
    // 모두 확인한 것이므로 반박된 원소의 인장괴 ele를 초기화해야함.
    if( checking_refute_info_num == room_data[data.room_name].room_member_count ){
      // 초기화
      room_data[data.room_name].theory_data[data.ingre_num] = {
        element : '',
        stamp : {
          1 : {
            user_key : '',
            color : '',
            point : ''  
          },
          2 : {
            user_key : '',
            color : '',
            point : ''  
          },
          3 : {
            user_key : '',
            color : '',
            point : ''  
          },
        }
      }

      // 확인 인원 초기화
      checking_refute_info_num = 0;
      // theory_data 전송하기
      io.to(data.room_name).emit("change_theory_data", room_data[data.room_name].theory_data);
      console.log("init_refute_ele");
    }

  });

  // 물약 발표회에서 물약 전시 결과 확인 함수
  /* data
   *    user_key : 유저번호
   *    user_color : 발표한 유저 색
   *    card_list : 선택한 재료가 저장된 배열 :: card_1~8
   *    exhibit_potion : 발표할 물약 
   *          red_1,0 / green_1,0 / blue_1,0
   *    board_order : 보드 순서
   *    cube_order  : 큐브 순서
   *    room_name   : 방 이름
   */
  socket.on("exhibit_ingre", (data) => {
    //유저 번호 찾기
    let user_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key == data.user_key);

    // 재료 2개로 발표할 물약과 같은지 비교 해야함
    // 재료 2개의 변수는 번호로 바꿔야함
    // 사용한 2개의 재료는 차감해야함
    let temp_ingre_list = [];
    for(let i = 0 ; i < 2; i++){
      switch(data.card_list[i]){
        case 'card_1':
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_1 -= 1;
          temp_ingre_list.push(1);
          break;
        case 'card_2':
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_2 -= 1;
          temp_ingre_list.push(2);
          break;
        case 'card_3':
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_3 -= 1;
          temp_ingre_list.push(3);
          break;
        case 'card_4':
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_4 -= 1;
          temp_ingre_list.push(4);
          break;
        case 'card_5':
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_5 -= 1;
          temp_ingre_list.push(5);
          break;
        case 'card_6':
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_6 -= 1;
          temp_ingre_list.push(6);
          break;
        case 'card_7':
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_7 -= 1;
          temp_ingre_list.push(7);
          break;
        case 'card_8':
          room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_8 -= 1;
          temp_ingre_list.push(8);
          break;
      }
    }
    // 총 합에서 차감
    room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.total -= 2;

    let ingre_list = {
      x : temp_ingre_list[0],
      y : temp_ingre_list[1],
      room_name : data.room_name,
      user_key : data.user_key,
    };

    // 재료 비교
    let ingre_test_result = compare_ingredient(ingre_list);
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> "+ data.user_key + " : 전시하기로한 물약 : " + data.exhibit_potion;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> "+ data.user_key + " : 전시 물약 생성 결과 : " + ingre_test_result;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
    
    // 발표하기로 한 물약과 재료비교의 결과가 다르면 실패
    if( data.exhibit_potion != ingre_test_result ) {
        // 발표자의 명성을 -1점 감점한다
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.point -= 1;

        var send_data = {
          user_name : room_data[data.room_name].user_data_array[user_num].user_name,
          user_key : data.user_key,
          get_cube_success : false,
          result : false,
        };

    }
    else {
      // 발표할 물약과 결과가 같으면 발표 성공!
      // 발표에 성공하면 exhibhiton_result에 반영해야함
      /*  red_1    :: 1,7 중 하나
          red_0    :: 2,8
          green_1  :: 3,9
          green_0  :: 4,10
          blue_1   :: 5,11
          blue_0   :: 6,12
      */
      // 첫째 줄에 큐브를 두는 것에 성공했을 경우 명성 +1점
      // 같은 색에 +- 둘다 성공했을 경우 명성 +2점 추가
      let get_cube_success = false;
      switch( data.exhibit_potion ){
        case 'red_1':
          if( room_data[data.room_name].exhibition_result.first[1].user_key == ''){
            room_data[data.room_name].exhibition_result.first[1].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.first[1].color = data.user_color;
            // 첫번쨰 칸이므로 명성 +1
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 1;
            get_cube_success = true;
          } 
          else if ( room_data[data.room_name].exhibition_result.second[7].user_key == '' ) {
            room_data[data.room_name].exhibition_result.second[7].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.second[7].color = data.user_color;
            get_cube_success = true;
          }
          else {
            // 모든 칸에 큐브가 있으므로 발표에 성공해도 큐브를 놓을 수 없음
            // do nothing
          }
          // red_0 에 자신의 유저키가 존재하고 ...ingame_data.get_extra_point.red == false 면 2점 추가
          if( (room_data[data.room_name].exhibition_result.first[2].user_key == data.user_key || room_data[data.room_name].exhibition_result.second[8].user_key == data.user_key) &&
               room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.red == false
              ){
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 2;
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.red = true;
              }
          break;
        case 'red_0':
          if( room_data[data.room_name].exhibition_result.first[2].user_key == ''){
            room_data[data.room_name].exhibition_result.first[2].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.first[2].color = data.user_color;
            // 첫번쨰 칸이므로 명성 +1
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 1;
            get_cube_success = true;
          } 
          else if ( room_data[data.room_name].exhibition_result.second[8].user_key == '' ) {
            room_data[data.room_name].exhibition_result.second[8].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.second[8].color = data.user_color;
            get_cube_success = true;
          }
          else {
            // 모든 칸에 큐브가 있으므로 발표에 성공해도 큐브를 놓을 수 없음
          }
          // red_1 에 자신의 유저키가 존재하고 ...ingame_data.get_extra_point.red == false 면 2점 추가
          if( (room_data[data.room_name].exhibition_result.first[1].user_key == data.user_key || room_data[data.room_name].exhibition_result.second[7].user_key == data.user_key) &&
               room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.red == false 
              ){
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 2;
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.red = true;
              }
          break;
        case 'green_1':
          if( room_data[data.room_name].exhibition_result.first[3].user_key == ''){
            room_data[data.room_name].exhibition_result.first[3].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.first[3].color = data.user_color;
            // 첫번쨰 칸이므로 명성 +1
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 1;
            get_cube_success = true;
          }
          else if ( room_data[data.room_name].exhibition_result.second[9].user_key == '' ) {
            room_data[data.room_name].exhibition_result.second[9].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.second[9].color = data.user_color;
            get_cube_success = true;
          }
          else {
            // 모든 칸에 큐브가 있으므로 발표에 성공해도 큐브를 놓을 수 없음
          }
          // green_0 에 자신의 유저키가 존재하고 ...ingame_data.get_extra_point.green == false 면 2점 추가
          if( (room_data[data.room_name].exhibition_result.first[4].user_key == data.user_key || room_data[data.room_name].exhibition_result.second[10].user_key == data.user_key) &&
               room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.green == false
              ){
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 2;
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.green = true;
              }
          break;
        case 'green_0':
          if( room_data[data.room_name].exhibition_result.first[4].user_key == ''){
            room_data[data.room_name].exhibition_result.first[4].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.first[4].color = data.user_color;
            // 첫번쨰 칸이므로 명성 +1
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 1;
            get_cube_success = true;
          }
          else if ( room_data[data.room_name].exhibition_result.second[10].user_key == '' ) {
            room_data[data.room_name].exhibition_result.second[10].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.second[10].color = data.user_color;
            get_cube_success = true;
          }
          else {
            // 모든 칸에 큐브가 있으므로 발표에 성공해도 큐브를 놓을 수 없음
          }
          // green_1 에 자신의 유저키가 존재하고 ...ingame_data.get_extra_point.green == false 면 2점 추가
          if( (room_data[data.room_name].exhibition_result.first[3].user_key == data.user_key || room_data[data.room_name].exhibition_result.second[9].user_key == data.user_key) &&
               room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.green == false
              ){
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 2;
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.green = true;
              }
          break;
        case 'blue_1':
          if( room_data[data.room_name].exhibition_result.first[5].user_key == ''){
            room_data[data.room_name].exhibition_result.first[5].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.first[5].color = data.user_color;
            // 첫번쨰 칸이므로 명성 +1
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 1;
            get_cube_success = true;
          }
          else if ( room_data[data.room_name].exhibition_result.second[11].user_key == '' ) {
            room_data[data.room_name].exhibition_result.second[11].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.second[11].color = data.user_color;
            get_cube_success = true;
          }
          else {
            // 모든 칸에 큐브가 있으므로 발표에 성공해도 큐브를 놓을 수 없음
          }
          // blue_0 에 자신의 유저키가 존재하고 ...ingame_data.get_extra_point.blue == false 면 2점 추가
          if( (room_data[data.room_name].exhibition_result.first[6].user_key == data.user_key || room_data[data.room_name].exhibition_result.second[12].user_key == data.user_key) &&
              room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.blue == false
              ){
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 2;
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.blue = true;
              }
          break;
        case 'blue_0':
          if( room_data[data.room_name].exhibition_result.first[6].user_key == ''){
            room_data[data.room_name].exhibition_result.first[6].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.first[6].color = data.user_color;
            // 첫번쨰 칸이므로 명성 +1
            room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 1;
            get_cube_success = true;
          }
          else if ( room_data[data.room_name].exhibition_result.second[12].user_key == '' ) {
            room_data[data.room_name].exhibition_result.second[12].user_key = data.user_key;
            room_data[data.room_name].exhibition_result.second[12].color = data.user_color;
            get_cube_success = true;
          }
          else {
            // 모든 칸에 큐브가 있으므로 발표에 성공해도 큐브를 놓을 수 없음
          }
          // blue_1 에 자신의 유저키가 존재하고 ...ingame_data.get_extra_point.blue == false 면 2점 추가
          if( (room_data[data.room_name].exhibition_result.first[5].user_key == data.user_key || room_data[data.room_name].exhibition_result.second[11].user_key == data.user_key) &&
               room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.blue == false
              ){
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.point += 2;
                room_data[data.room_name].user_data_array[user_num].user_ingame_data.get_extra_point.blue = true;
              }
          break;
      }

      var send_data = {
        user_name : room_data[data.room_name].user_data_array[user_num].user_name,
        user_key : data.user_key,
        get_cube_success : get_cube_success,
        result : true,
      };
      clog =  new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + data.user_key + " : 발표 성공 > 큐브는 둘 수 있나? " + get_cube_success;
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
    }
    // 안내
    io.to(data.room_name).emit("show_exhibition", send_data);
    // 공통 전송 구간
    // 발표회 정보 재전송
    io.to(data.room_name).emit("change_exhibition_data", room_data[data.room_name].exhibition_result);
    // 명성 관련 데이터가 변경 되었을 수 있으므로 재전송
    io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);
    // 다음 순서로 진행
    // 물약 발표회 순서이므로 9번
    board_end_event(data, 9);
  });

  // 호의 카드 사용하기 함수
  // data :
  //    user_key : 유저 번호
  //    room_name : 방 이름
  //    favor_card : 사용하는 호의카드
  //    select_board_num : 힘센친구 카드가 사용될 때 필요한 변수
  //    ingre_list : 약초학자 카드가 사용될 떄 필요한 변수
  socket.on("favor_card_use_confirm", (data) => {
    // 1.사용할 호의카드 가 무엇이냐에 따라 행동이 다름
    // 1-1 switch 로 분기를 갈라 실행...
    // 1-2 사용은 반드시 됨 -> 카드가 없으면 실행이 되지 않으므로

    // 유저 번호 받기
    let user_num = -1;
    for( let i = 0; i < room_data[data.room_name].user_data_array.length; i++ ){
      if( room_data[data.room_name].user_data_array[i].user_key == data.user_key ){
        user_num = i;
        break;
      }
    }

    // 카드 실행
    switch(data.favor_card){
      case 'assistent':
        // 행동큐브 한개 늘리기
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.cube_count += 1;
        // 호의 카드 제거
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.favor_card['assistent'] -= 1;
        // 사용한 티 내야함
        break;
      case 'bar_owner':
        // 물약판매가 완벽하면 명성+1
        // 그 아랫단계면 골드+1
        // 0~3 을 돌면서 user_key가 없은 곳에 넣기
        for( let i = 0; i < room_data[data.room_name].room_member_count; i++ ) {
          if( room_data[data.room_name].temp_used_favor_card_list[2][i].user_key == '' ) {
            room_data[data.room_name].temp_used_favor_card_list[2][i].user_key = data.user_key;
            room_data[data.room_name].temp_used_favor_card_list[2][i].count += 1;
            break;
          }
          else if ( room_data[data.room_name].temp_used_favor_card_list[2][i].user_key == data.user_key ) {
            room_data[data.room_name].temp_used_favor_card_list[2][i].count += 1;
            break;
          }
        }
        // 호의카드 제거
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.favor_card.bar_owner -= 1;
        break;
      case 'big_man':
        // 선택된 보드의 큐브 순서를 가장 위로 올림
        // 사용한 순서에서 자신의 큐브를 맨 위로 올리는 것

        // user_cube_data에서 해당 보드의 큐브 순서중 자신의 user_key를 가진 번호를 0으로 바꿈
        let my_num = -1;
        for( let i = 0; i < room_data[data.room_name].user_cube_data[0]+1; i++ ){
          if( room_data[data.room_name].user_cube_data[data.select_board_num][i].user_key == data.user_key) {
            my_num = i;
            break;
          }
        }
        let temp = room_data[data.room_name].user_cube_data[data.select_board_num].splice(my_num, 1);
        room_data[data.room_name].user_cube_data[data.select_board_num].unshift(temp);

        clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> " +  data.user_key + " 유저의 호의카드 사용으로 인한 큐브 정보 변경";
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
          console.log(clog);
        });
        clog = room_data[data.room_name].user_cube_data;
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
          console.log(clog);
        });

        room_data[data.room_name].user_data_array[user_num].user_ingame_data.favor_card.big_man -= 1;
        break;
      case 'caretaker':
        // 용병에게 물약 판매 전 스스로 물약 먹기 가능해짐
        // 0~3 을 돌면서 user_key가 없은 곳에 넣기
        // 큐브 한개를 사용하므로 큐브 카운터를 제거해야함
        for( let i = 0; i < room_data[data.room_name].room_member_count; i++ ) {
          if( room_data[data.room_name].temp_used_favor_card_list[4][i].user_key == '' ) {
            room_data[data.room_name].temp_used_favor_card_list[4][i].user_key = data.user_key;
            room_data[data.room_name].temp_used_favor_card_list[4][i].count += 1;
            break;
          }
          else if ( room_data[data.room_name].temp_used_favor_card_list[4][i].user_key == data.user_key ) {
            room_data[data.room_name].temp_used_favor_card_list[4][i].count += 1;
            break;
          }
        }
        // 큐브 1개 제거
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.cube_count -= 1;
        // 호의카드 제거
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.favor_card.caretaker -= 1;
        break;
      case 'herbalist':
        // 전송 받은 재료 2개를 제거
        // user_num 에 있은 user_ingame_data에서 ingredient 제거
        for( let i = 0; i < 2; i++ ){
          switch(data.ingre_list[i]){
            case 'card_1':
              room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_1 -= 1;
              break;
            case 'card_2':
              room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_2 -= 1;
              break;
            case 'card_3':
              room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_3 -= 1;
              break;
            case 'card_4':
              room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_4 -= 1;
              break;
            case 'card_5':
              room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_5 -= 1;
              break;
            case 'card_6':
              room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_6 -= 1;
              break;
            case 'card_7':
              room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_7 -= 1;
              break;
            case 'card_8':
              room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.card_8 -= 1;
              break;
          }
        }

        // 랜덤한 3개의 재료 추가
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient['card_' + Math.ceil(Math.random() * 8 )] += 1;

        room_data[data.room_name].user_data_array[user_num].user_ingame_data.ingredient.total += 1;
        break;
      case 'merchant':
        // 첫번쨰로 물약을 팔면 골드+1
        // 첫 번째가 아니면 물약 3가지 중 하나 고를 수 있게됨
        // 0~3 을 돌면서 user_key가 없은 곳에 넣기
        for( let i = 0; i < room_data[data.room_name].room_member_count; i++ ) {
          if( room_data[data.room_name].temp_used_favor_card_list[6][i].user_key == '' ) {
            room_data[data.room_name].temp_used_favor_card_list[6][i].user_key = data.user_key;
            room_data[data.room_name].temp_used_favor_card_list[6][i].count += 1;
            break;
          }
          else if ( room_data[data.room_name].temp_used_favor_card_list[6][i].user_key == data.user_key ) {
            room_data[data.room_name].temp_used_favor_card_list[6][i].count += 1;
            break;
          }
        }
        // 호의카드 제거
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.favor_card.merchant -= 1;
        break;
      case 'shopkeeper':
        // 아이템을 살 때 아이템의 가격 -1
        // 0~3 을 돌면서 user_key가 없은 곳에 넣기
        for( let i = 0; i < room_data[data.room_name].room_member_count; i++ ) {
          if( room_data[data.room_name].temp_used_favor_card_list[7][i].user_key == '' ) {
            room_data[data.room_name].temp_used_favor_card_list[7][i].user_key = data.user_key;
            room_data[data.room_name].temp_used_favor_card_list[7][i].count += 1;
            break;
          }
          else if ( room_data[data.room_name].temp_used_favor_card_list[7][i].user_key == data.user_key ) {
            room_data[data.room_name].temp_used_favor_card_list[7][i].count += 1;
            break;
          }
        }
        // 호의카드 제거
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.favor_card.shopkeeper -= 1;
        break;
      case 'wise_man':
        // 재료 판매시 골드 +1
        // 0~3 을 돌면서 user_key가 없은 곳에 넣기
        for( let i = 0; i < room_data[data.room_name].room_member_count; i++ ) {
          if( room_data[data.room_name].temp_used_favor_card_list[8][i].user_key == '' ) {
            room_data[data.room_name].temp_used_favor_card_list[8][i].user_key = data.user_key;
            room_data[data.room_name].temp_used_favor_card_list[8][i].count += 1;
            break;
          }
          else if ( room_data[data.room_name].temp_used_favor_card_list[8][i].user_key == data.user_key ) {
            room_data[data.room_name].temp_used_favor_card_list[8][i].count += 1;
            break;
          }
        }
        // 호의카드 제거
        room_data[data.room_name].user_data_array[user_num].user_ingame_data.favor_card.wise_man -= 1;
        break;
    }

    // 2 결과 전송
    io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);
    // 사용 성공 확인 알림
    socket.emit("use_favor_card_alert");
  });

  // 라운드 준비 완료 함수 
  /*
    user_key : 실행 유저 키
    room_name : 방 이름
  */
  socket.on("round_ready_on", (data) => {
    let cnt = 0;
    for( let i = 0; i < room_data[data.room_name].user_data_array.length; i++){
      if( room_data[data.room_name].user_data_array[i].user_key == data.user_key){
        room_data[data.room_name].user_data_array[i].is_ready = true;
        cnt += 1;
      }
      else {
        if( room_data[data.room_name].user_data_array[i].is_ready == true ){
          cnt += 1;
        }
      }
    }
    // 모두가 준비되면 라운드 시작해야함
    if( cnt == room_data[data.room_name].room_member_count ){
      // 모두의 준비 완료를 해제하고 라운드 시작 변수를 true 하고 라운드를 시작해야함
      for( let i = 0; i < room_data[data.room_name].user_data_array.length; i++ ){
        room_data[data.room_name].user_data_array[i].is_ready = false;
        room_data[data.room_name].user_data_array[i].is_ingame = true;
      }
/*
      // 호의카드를 사용 할 수 있다면 사용 여부를 물어야함
      // 시작하자마자 사용할 수 있는 건 조수 카드 뿐
      for(let i = 0; i < user_data_array.length; i++){
        if( user_data_array[i].user_ingame_data.favor_card.assistent > 0 ){


        }
      }
*/
      // final_round_order에서 라운드 찾기 
      // -- 한 명이라도 큐브로 선택한 라운드가 있다면 해당 라운드 시작
      let first_start_board = next_board(data.room_name);

      if ( first_start_board <= 0 ){
        clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> 선택한 보드 없음";
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
          console.log(clog);
        });
        let send_data = {
          board_order : 1,
          cube_order : 1,
          room_name : data.room_name,
        };
        // 선택 된 것이 없는 것 이므로 라운드 종료
        board_end_event(send_data, 1);
      }
      else if ( first_start_board > 0 ) {
        // 준비 완료 상태 갱신
        io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);
        clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> " + first_start_board + "번째 보드 시작";
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
          console.log(clog);
        });
        // 시작할 보드칸의 순서 설정
        let start_board_order = find_first_board_cube(first_start_board, data.room_name);
        let send_data = {
          board_order : start_board_order,
          board_cube_order : 1,
        };

        // 해당 보드칸의 시작을 알림
        io.to(data.room_name).emit("board_start",first_start_board);

        io.to(data.room_name).emit("change_board_order_val", send_data);
      }
      
    }
    else {
      // 모두에게 전송해야 모두의 데이터가 갱신됨
      io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);
    }
  });

  // 행동 넘기기 함수
  /*
    user_key    : 유저 번호
    room_name   : 방 이름
    board_num   : 현재 보드 번호
    board_order : 현재 보드 순서
    cube_order : 현재 큐브 순서
  */
  socket.on("board_passing", (data) => {
    board_end_event(data, data.board_num);
  });

  /*********************************************/
  /*           방 나가기 관련 함수들             */
  /*********************************************/


  // 서버 나갔을 때
  socket.on("disconnecting", () => {
   
    let room = [...socket.rooms]; // set의 array화
    //console.log(room[1]);
    let user_name = '';
    // 있던 룸이 lobby가 아니라면
    if( room[1] != 'lobby' && ( room[1] != null || room[1] != undefined )){
      console.log("로비가 아닌 곳에서 나감");
      if (room[1].includes('_waiting_room')){
        console.log("종료 후 대기실에서 나감");
        var num = room[1].indexof("_waiting_room");
        var room_name = room[1][0..num];
        room_data[room_name].left_game_result_data.count--;
         // 방의 최대 인원수를 줄이고
        var room_num = room_list.findIndex(v => v.name === room_name);
        room_list[room_num].count--;
        // 방의 모든 인원이 나가면 room_list를 제거
        if (room_list[room_num].count < 1){
          room_list.splice(room_num, 1);
          // room_data 터트리기
          room_data[room_name] = undefined; // 이래야 이름이 같은 방이 생기면 다시 사용 가능
        }
      }
      else{
        let user_num = room_data[room[1]].user_data_array.findIndex(v => v.user_key === room[0]);
        user_name = room_data[room[1]].user_data_array[user_num].user_name;
        console.log(user_name);
        // 로그를 남기고
        clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + room[1] + "방 >> " + user_name + "이(가) 방에서 나갔습니다.";
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
          console.log(clog);
        });
        // 룸에 남아있는 마지막 인원이었다면 방 제거
        if( room_data[room[1]].now_member_count <= 1 ){
          // 마지막 남은 사람이 나가는 것이므로 list에서 방 삭제

          room_list.splice(room_list.findIndex(v => v.name === room[1]), 1);
    
          // room_data에서 room_name으로 된 부분 삭제
          delete room_data[room[1]];
          //console.log(room_data);
          // 로비에 방 리스트 정보 업데이트
          io.to("lobby").emit("enter_room_success", room_list);
        }
        else {
          //아니면 방 퇴장
          let master_out = room_data[room[1]].user_data_array[user_num].is_master;
  
          room_data[room[1]].user_data_array.splice(user_num, 1);
          user_num = room_data[room[1]].result_table.findIndex(v => v.user_key === room[0]);
          room_data[room[1]].result_table.splice(user_num, 1);
          user_num = room_data[room[1]].order.findIndex(v => v.user_key === room[0]);
          room_data[room[1]].order.splice(user_num, 1);
  
          room_data[room[1]].now_member_count--;
  
          // room_list에서 count 제거
          for( let i = 0; i < room_list.length; i++ ){
            if( room[1] == room_list[i].name ){
              room_list[i].count--;
              break;
            }
          }
  
          // 마스터가 나갔다면 다음 사람에게 방장을 넘겨줌
          if( master_out == "true" || master_out == true || master_out == 'true' ) {
            room_data[room[1]].user_data_array[0].is_master = "true";
            user_name = room_data[room[1]].user_data_array[0].user_name;
          }
  
          // 로그를 남기고
          clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + room[1] + "방 >> " + user_name + "이(가) 방장이 되었습니다.";
          log = clog + "\n";
          fs.appendFile(dir, log, (err) => {
            if( err ) throw err;
            console.log(clog);
          });
  
          // 방 내부에 정보 업데이트
          // 메시지를 모든 client에게 메시지를 전송한다
          let msg = {
            speaker: "서버",
            msg: user_name+"님이 방에서 나갔습니다.",
            type: "server"
          };
            
          io.to(room[1]).emit("chat", msg);
          // 현재 접속한 모든 사용자를 모두에게 전송
          io.to(room[1]).emit("all_player", room_data[room[1]].user_data_array);

          // 남은 인원이 2명 미만일 경우 room_page로 이동
          if (room_data[room[1]].now_member_count < 2){
            // 게임을 진행했떤 정보를 초기화함
            init_everything(room[1]);
            io.to(room[1]).emit("move_room_page_check");
          }

  
      }
      
      
      }
    }

    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + socket.id + " 유저가 서버에서 접속을 해제하였습니다!";
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);
  });


  // 방 나갔을 떄
  /*
    data : room_name, user_key
  */
  socket.on("quit_room", (data) => {
    // quit_room은  게임 시작 전 로비에서만 가능하므로 로비에 들어갈 때 입력된 값만 제거
    // room_data의 내용에서 한 자리를 지운다
    // 지우기 전에 room_list에서  count를 확인하고 현재 1인데 이것이면 
    // 마지막 남은 한명이 나가는 것이므로 방.폭.
    var room_num = room_list.findIndex(v => v.name === data.room_name);
    //console.log(room_list[room_num]);
    if( room_list[room_num].count <= 1 ){
      // 마지막 남은 사람이 나가는 것이므로 list에서 방 삭제
      room_list.splice(room_num, 1);

      // room_data에서 room_name으로 된 부분 삭제
      delete room_data[data.room_name];
      //console.log(room_data);

      io.to("lobby").emit("enter_room_success", room_list);
    }
    else {
      let master_out = false;
      let user_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);
      let name = room_data[data.room_name].user_data_array[user_num].user_name;
      if( room_data[data.room_name].user_data_array[user_num].is_master == "true" || 
          room_data[data.room_name].user_data_array[user_num].is_master == true ){
            master_out = true;
          }

      room_data[data.room_name].user_data_array.splice(user_num, 1);
      user_num = room_data[data.room_name].result_table.findIndex(v => v.user_key === data.user_key);
      room_data[data.room_name].result_table.splice(user_num, 1);
      user_num = room_data[data.room_name].order.findIndex(v => v.user_key === data.user_key);
      room_data[data.room_name].order.splice(user_num, 1);
      
      room_data[data.room_name].now_member_count--;

      room_list[room_num].count--;

      // 로그를 남기고
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + name + "이(가) 방에서 나갔습니다.";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });

      // 마스터가 나갔다면 남은 인원중 가장 처음 들어온 인원에게 방장 권한을 넘겨줌
      if( master_out ) {
        room_data[data.room_name].user_data_array[0].is_master = "true";
        name = room_data[data.room_name].user_data_array[0].user_name;
        // 로그를 남기고
        clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + name + "이(가) 방장이 되었습니다.";
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
          console.log(clog);
        });
      }

      // 남은 방 인원에게 유저 정보 업데이트해줌
      io.to(data.room_name).emit("all_player", room_data[data.room_name].user_data_array);
    }
    
    //console.log(room_list);

    // lobby에서 room_list 페이지로 이동하게 
    socket.emit("move_room_list");

    // 방에서 나오고 
    socket.leave(data.room_name);
    // 로비로 들어간다
    socket.join('lobby');

  });

  // 게임 중에 방에서 나갔을 때
  // data : room_name , user_key
  socket.on("quit_room_in_board", (data) => {
    var room_num = room_list.findIndex(v => v.name === data.room_name);
    let master_out = false;
    let user_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key === data.user_key);
      let name = room_data[data.room_name].user_data_array[user_num].user_name;
    if( room_data[data.room_name].user_data_array[user_num].is_master == "true" || 
        room_data[data.room_name].user_data_array[user_num].is_master == true ){
          master_out = true;
        }

    room_data[data.room_name].user_data_array.splice(user_num, 1);
    user_num = room_data[data.room_name].result_table.findIndex(v => v.user_key === data.user_key);
    room_data[data.room_name].result_table.splice(user_num, 1);
    user_num = room_data[data.room_name].order.findIndex(v => v.user_key === data.user_key);
    room_data[data.room_name].order.splice(user_num, 1);
    
    room_data[data.room_name].now_member_count--;
    // 최대 인원 수도 수정해야 돌아감
    if (room_data[data.room_name].room_member_count > 2){
      room_data[data.room_name].room_member_count--;
    }
    room_list[room_num].count--;

    //console.log(room_data[data.room_name].now_member_count);
    //console.log(room_data[data.room_name].room_member_count);
    //console.log(room_list[room_num].count);
    // 로그를 남기고
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + name + "이(가) 방에서 나갔습니다.";
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
      console.log(clog);
    });

    // 마스터가 나갔다면 남은 인원중 가장 처음 들어온 인원에게 방장 권한을 넘겨줌
    if( master_out ) {
      room_data[data.room_name].user_data_array[0].is_master = "true";
      name = room_data[data.room_name].user_data_array[0].user_name;
      // 로그를 남기고
      clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + data.room_name + "방 >> " + name + "이(가) 방장이 되었습니다.";
      log = clog + "\n";
      fs.appendFile(dir, log, (err) => {
        if( err ) throw err;
        console.log(clog);
      });
    }

    // lobby에서 room_list 페이지로 이동하게 
    socket.emit("move_room_list");

    // 방에서 나오고 
    socket.leave(data.room_name);
    // 로비로 들어간다
    socket.join('lobby');

    // 방장 권한 변경 으로 인해 무조건 전송해야함
    io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);

    // 남은 인원이 2명 미만일 경우 room_page로 이동
    if (room_data[data.room_name].now_member_count < 2){
      // 게임을 진행했던 정보를 초기화
      init_everything(data.room_name);
      io.to(data.room_name).emit("move_room_page_check");
    }

  });


  /*************************************************/
  /*           마지막 페이지                        */
  /*************************************************/

  socket.on("game_end_confirm", (data) => {
    // 방에서 나와서 대기실로 이동됨
    // 대기실은 room_name_waiting_room로 결정
    // data - room_name, user_key
    socket.emit("restart_counter_setting", ++room_data[data.room_name].restart_counter);
    var room_name = data.room_name + "_waiting_room";
    socket.leave(data.room_name);
    socket.join(room_name);
  });
  
  socket.on("out_game", (data) => {
    // data :: room_name, user_key,
    room_data[data.room_name].left_game_result_data.count--;

    // 방의 최대 인원수를 줄이고
    var room_num = room_list.findIndex(v => v.name === data.room_name);
    room_list[room_num].count--;
    // 방의 모든 인원이 나가면 room_list를 제거
    if (room_list[room_num].count < 1){
      room_list.splice(room_num, 1);
      // room_data 터트리기
      room_data[data.room_name] = undefined; // 이래야 이름이 같은 방이 생기면 다시 사용 가능
    }
      
    // 로비로 이동
    socket.leave(data.room_name+"_waiting_room");
    socket.join('lobby');

  });

  socket.on("restart_checking", (data) => {
    var room_name = data.room_name + "_waiting_room";
    // data :: send_data - user_key, room_name
    room_data[data.room_name].left_game.push(data.user_key);

    // 게임에 남는 사람이 현재 접속된 사람수와 같으면 바로 로비로 이동
    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > left_game.legnth:" + room_data[data.room_name].left_game.length + "  ::  " + "left_game_result_data.count:" + room_data[data.room_name].left_game_result_data.count;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);

    if( room_data[data.room_name].left_game.length == room_data[data.room_name].left_game_result_data.count ) {
      // 남는 사람이 전부 이므로 로비로 이동 하는데 master는 처음 다시하자는 사람에게 주자
      // 전송해주어야 하는 데이터 
      // user_name, room_pw, is_master, is_ready, count
      let send_data = {
        room_pw : room_data[data.room_name].left_game_result_data.room_pw,
        count : room_data[data.room_name].left_game_result_data.count,
        master_key : room_data[data.room_name].left_game[0],
      };

      io.to(room_name).emit("restart", send_data);
    }
    // 다시시작할 수 있는 상황이 아니면 게임 로비로 이동시킴
    else if (room_data[data.room_name].left_game_result_data.count < 2){
      // 다시 시작할 수 없으므로 로비로 이동
      io.to(room_name).emit("cant_restart");
    }
    else {
      io.to(room_name).emit("restart_check", room_data[data.room_name].left_game.length);
    }

  });

  socket.on("lobby_move", (data) => {
    var room_name = data.room_name + "_waiting_room";
    socket.leave(room_name);
    socket.join('lobby');
  });

  socket.on("restart_cancel", (data) => {
    var room_name = data.room_name + "_waiting_room";

    // data :: send_data - user_key, room_name
    let num = room_data[data.room_name].left_game.indexOf(data.user_key);
    room_data[data.room_name].left_game.splice(num, 1);

    io.to(room_name).emit("restart_cancel_check", room_data[data.room_name].left_game.length);

    clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > left_game.legnth:" + room_data[data.room_name].left_game.length + "  ::  " + "left_game_result_data.count:" + room_data[data.room_name].left_game_result_data.count;
    log = clog + "\n";
    fs.appendFile(dir, log, (err) => {
      if( err ) throw err;
    });
    console.log(clog);

    // 취소하므로써 다른 이들이 다시 시작 가능한 인원수라면 -> 가능 자체가 불가능할것같은데
    if( room_data[data.room_name].left_game.length === room_data[data.room_name].left_game_result_data.count ) {
      // 남는 사람이 전부 이므로 로비로 이동 하는데 master는 game_result[1]에게 주자
      // 전송해주어야 하는 데이터 
      // user_name, room_pw, is_master, is_ready, count
      let send_data = {
        room_pw : room_data[data.room_name].left_game_result_data.room_pw,
        count : room_data[data.room_name].left_game_result_data.count,
        master_key : room_data[data.room_name].left_game[0],
      };

      io.to(room_name).emit("restart", send_data);
    }
    else {
      io.to(room_name).emit("restart_check", room_data[data.room_name].left_game.length);
    }
  });


});

});



/*********************************************/
/*           함수                            */
/*********************************************/



// 선택될 5장의 카드 셔플
function suffle_ingredient_card_select(room_name){
  for( let i = 0; i < 5; i++ ){
    room_data[room_name].ingredient_select_arr[i] = Math.ceil(Math.random() * 8);
  }
}

// 6명의 용병중 사용할 5명 랜덤 선택
// 중복 불가
function suffle_adv_card_list(room_name) {
  let i = 0;
  while (i < 5) {
    let rand_num = Math.ceil(Math.random() * 6);
    if( room_data[room_name].random_adv_list.indexOf(rand_num) < 0 ){
      room_data[room_name].random_adv_list[i] = rand_num;
      i++;
    }
  }
}

// 정답 설정
function make_answer(room_name) {
  // 정답 순서를 저장할 임시 변수에 정답 순서를 담아둔 뒤
  // ingredient_answer에 ingredient와 대칭시켜 저장함
  
  // 재료는 무조건 8개
  for( let i = 0; i < 8; i++ ){
    if (room_data[room_name].ingredient_answer.length >= 8){
      break;
    }
    let n = Math.ceil(Math.random() * 8 );
    if( room_data[room_name].ingredient_answer.length < 1 ) {
      room_data[room_name].ingredient_answer.push(n);
    }
    else{
      let is_correct = true;
      for(let j = 0; j < room_data[room_name].ingredient_answer.length; j++){
        if( room_data[room_name].ingredient_answer[j] == n ){
          i -= 1;
          is_correct = false;
          break;
        }
      }
      if( is_correct ){
        // is_correct 값이 그대로라면 중복이 없는 것이므로 push!
        room_data[room_name].ingredient_answer.push(n);  
      }
    }
  }

  clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > " + room_name + "방의 정답 >> "+ room_data[room_name].ingredient_answer;
  log = clog + "\n";
  fs.appendFile(dir, log, (err) => {
    if( err ) throw err;
    console.log(clog);
  });
}

// 재료 비교 함수
function compare_ingredient(data) {
  // 비교할 2개의 값을 작은 순서대로 저장한 후
  // 작은 순서의 값을 switch 에 넣어 비교 : 제일 편함 
  // 값은 반드시 다른 것이 보장됨
  //console.log(data);
  let tmp_s_val = room_data[data.room_name].ingredient_answer[data.x - 1];
  let tmp_l_val = room_data[data.room_name].ingredient_answer[data.y - 1];

  // 정답에 대조해서 카드의 번호를 원소의 번호로 대입해야함...
  let s_val = tmp_s_val > tmp_l_val ? tmp_l_val : tmp_s_val;
  let l_val = tmp_s_val < tmp_l_val ? tmp_l_val : tmp_s_val;
  clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> 재료비교 중 " + data.x + " :: " + data.y;
  log = clog + "\n";
  fs.appendFile(dir, log, (err) => {
    if( err ) throw err;
    console.log(clog);
  });
  clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> 원소 비교 번호 : " + s_val + " :: " + l_val;
  log = clog + "\n";
  fs.appendFile(dir, log, (err) => {
    if( err ) throw err;
    console.log(clog);
  });
  // 유저 번호 찾기
  let user_num = room_data[data.room_name].user_data_array.findIndex(v => v.user_key == data.user_key);
  
  // 모두에게 알려질 결과 변수(return 값)
  let announce_result = '';
  switch( s_val ){
    case 1 :
      if( l_val == 2 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blank';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blank';
        announce_result = 'blank';
      }
      else if ( l_val == 3 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'green_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'green_1';
        announce_result = 'green_1';
      }
      else if ( l_val == 4 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blue_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blue_0';
        announce_result = 'blue_0';
      }
      else if ( l_val == 5 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'red_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'red_0';
        announce_result = 'red_0';
      }
      else if ( l_val == 6 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blue_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blue_0';
        announce_result = 'blue_0';
      }
      else if ( l_val == 7 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'red_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'red_0';
        announce_result = 'red_0';
      }
      else if ( l_val == 8 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'green_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'green_1';
        announce_result = 'green_1';
      }
      break;
    case 2 :
      if ( l_val == 3 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blue_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blue_1';
        announce_result = 'blue_1';
      }
      else if ( l_val == 4 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'green_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'green_0';
        announce_result = 'green_0';
      }
      else if ( l_val == 5 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blue_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blue_1';
        announce_result = 'blue_1';
      }
      else if ( l_val == 6 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'red_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'red_1';
        announce_result = 'red_1';
      }
      else if ( l_val == 7 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'green_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'green_0';
        announce_result = 'green_0';
      }
      else if ( l_val == 8 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'red_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'red_1';
        announce_result = 'red_1';
      }
      break;
    case 3 :
      if ( l_val == 4 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blank';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blank';
        announce_result = 'blank';
      }
      else if ( l_val == 5 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'red_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'red_0';
        announce_result = 'red_0';
      }
      else if ( l_val == 6 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'green_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'green_1';
        announce_result = 'green_1';
      }
      else if ( l_val == 7 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'red_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'red_0';
        announce_result = 'red_0';
      }
      else if ( l_val == 8 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blue_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blue_1';
        announce_result = 'blue_1';
      }
      break;
    case 4 :
      if ( l_val == 5 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'green_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'green_0';
        announce_result = 'green_0';
      }
      else if ( l_val == 6 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'red_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'red_1';
        announce_result = 'red_1';
      }
      else if ( l_val == 7 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blue_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blue_0';
        announce_result = 'blue_0';
      }
      else if ( l_val == 8 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'red_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'red_1';
        announce_result = 'red_1';
      }
      break;
    case 5 :
      if ( l_val == 6 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blank';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blank';
        announce_result = 'blank';
      }
      else if ( l_val == 7 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'green_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'green_0';
        announce_result = 'green_0';
      }
      else if ( l_val == 8 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blue_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blue_1';
        announce_result = 'blue_1';
      }
      break;
    case 6 :
      if ( l_val == 7 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blue_0';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blue_0';
        announce_result = 'blue_0';
      }
      else if ( l_val == 8 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'green_1';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'green_1';
        announce_result = 'green_1';
      }
      break;
    case 7 :
      if ( l_val == 8 ){
        room_data[data.room_name].result_table[user_num].ingredient_result[data.x-1][data.y-1] = 'blank';
        room_data[data.room_name].result_table[user_num].ingredient_result[data.y-1][data.x-1] = 'blank';
        announce_result = 'blank';
      }
      break;
  }

  return announce_result;
}

// 아티펙트 구매 가능 여부 초기화 함수
function init_artifacts(room_name) {
  for(let i = 0 ; i < 6; i ++){
    room_data[room_name].artifacts.rank_1[i].is_selled = false;
  }
  for(let i = 0 ; i < 6; i ++){
    room_data[room_name].artifacts.rank_2[i].is_selled = false;
  }
  for(let i = 0 ; i < 6; i ++){
    room_data[room_name].artifacts.rank_3[i].is_selled = false;
  }
}

// 모든 가용 가능 변수를 초기화하는 함수
function init_everything(room_name) {
  init_artifacts(room_name);
  room_data[room_name].user_data_array = [];
  room_data[room_name].room_pw = '';
  room_data[room_name].room_name = '';
  room_data[room_name].round_cont = 0;
  room_data[room_name].room_member_count = 0;
  room_data[room_name].now_member_count = 0;
  room_data[room_name].order = [];
  room_data[room_name].decide_round_order_cont = 0;
  room_data[room_name].restart_counter = 0;
  room_data[room_name].ingredient_select_arr = [0,0,0,0,0];
  room_data[room_name].show_artifacts = [];
  room_data[room_name].adventurer_card_data= '';
  room_data[room_name].random_adv_list = [];
  room_data[room_name].random_adv_list = [];
  room_data[room_name].discount_coin_list = [];
  room_data[room_name].save_selling_potion_price = [];

  room_data[room_name].final_round_order = [];
  room_data[room_name].user_cube_data = ['',[],[],[],[],[],[],[],[],[]];
  room_data[room_name].result_table = [];
  room_data[room_name].temp_used_favor_card_list = '';

  room_data[room_name].ingredient_answer = [];
  room_data[room_name].round_test_result = {
    0 : { red_0 : false },
    1 : { red_1 : false },
    2 : { blue_0 : false },
    3 : { blue_1 : false },
    4 : { green_0 : false },
    5 : { green_1 : false },
    6 : { blank : false },
  };
  room_data[room_name].exhibition_result = {
    first : {
      1 : {
        user_key : '',
        color : '',
      },
      2 : {
        user_key : '',
        color : '',
      },
      3 : {
        user_key : '',
        color : '',
      },
      4 : {
        user_key : '',
        color : '',
      },
      5 : {
        user_key : '',
        color : '',
      },
      6 : {
        user_key : '',
        color : '',
      },
    },
    second : {
      7 : {
        user_key : '',
        color : '',
      },
      8 : {
        user_key : '',
        color : '',
      },
      9 : {
        user_key : '',
        color : '',
      },
      10 : {
        user_key : '',
        color : '',
      },
      11 : {
        user_key : '',
        color : '',
      },
      12 : {
        user_key : '',
        color : '',
      },
    }
  };
  room_data[room_name].checking_refute_info_num = 0;
  room_data[room_name].theory_data = {
    1 : {
      element : '',
      stamp : {
        1 : {
          user_key : '',
          color : '',
          point : ''  
        },
        2 : {
          user_key : '',
          color : '',
          point : ''  
        },
        3 : {
          user_key : '',
          color : '',
          point : ''  
        },
      }
    },
    2 : {
      element : '',
      stamp : {
        1 : {
          user_key : '',
          color : '',
          point : ''  
        },
        2 : {
          user_key : '',
          color : '',
          point : ''  
        },
        3 : {
          user_key : '',
          color : '',
          point : ''  
        },
      }
    },
    3 : {
      element : '',
      stamp : {
        1 : {
          user_key : '',
          color : '',
          point : ''  
        },
        2 : {
          user_key : '',
          color : '',
          point : ''  
        },
        3 : {
          user_key : '',
          color : '',
          point : ''  
        },
      }
    },
    4 : {
      element : '',
      stamp : {
        1 : {
          user_key : '',
          color : '',
          point : ''  
        },
        2 : {
          user_key : '',
          color : '',
          point : ''  
        },
        3 : {
          user_key : '',
          color : '',
          point : ''  
        },
      }
    },
    5 : {
      element : '',
      stamp : {
        1 : {
          user_key : '',
          color : '',
          point : ''  
        },
        2 : {
          user_key : '',
          color : '',
          point : ''  
        },
        3 : {
          user_key : '',
          color : '',
          point : ''  
        },
      }
    },
    6 : {
      element : '',
      stamp : {
        1 : {
          user_key : '',
          color : '',
          point : ''  
        },
        2 : {
          user_key : '',
          color : '',
          point : ''  
        },
        3 : {
          user_key : '',
          color : '',
          point : ''  
        },
      }
    },
    7 : {
      element : '',
      stamp : {
        1 : {
          user_key : '',
          color : '',
          point : ''  
        },
        2 : {
          user_key : '',
          color : '',
          point : ''  
        },
        3 : {
          user_key : '',
          color : '',
          point : ''  
        },
      }
    },
    8 : {
      element : '',
      stamp : {
        1 : {
          user_key : '',
          color : '',
          point : ''  
        },
        2 : {
          user_key : '',
          color : '',
          point : ''  
        },
        3 : {
          user_key : '',
          color : '',
          point : ''  
        },
      }
    }
  };
}

// 아티펙트 새로 뽑는 함수
function rand_artifacts(room_name) {
  // 아티펙트 변수를 초기화 하고
  room_data[room_name].show_artifacts = [];
  // 3개의 아티펙트를 집어넣음
  // 이때 값이 중복되선 안됨
  for ( let i = 0; i < 3 ; i++ ) {
    let rand = Math.ceil(Math.random() * 6 - 1 );
    if( room_data[room_name].show_artifacts.length < 1 ){
      room_data[room_name].show_artifacts.push(rand);
    }
    else {
      let clean = true;
      for( let j = 0; j < room_data[room_name].show_artifacts.length; j++ ) {
        // 같은 수가 이미 있으면 i 줄여서 한 번 더 돌려야 함
        if( room_data[room_name].show_artifacts[j] == rand){
          clean = false;
          i -= 1;
          break;
        }
      }

      if( clean ) {
        room_data[room_name].show_artifacts.push(rand);
      }
    }
  }
}

// final_round_order를 order 순서에 따라 정렬하는 함수
// 사용 :: merge
const merge = function (left, right) { // 정렬된 왼쪽과 오른쪽 배열을 받아서 하나로 합치는 순수한 함수
	// left, right already sorted
	const result = [];
	while (left.length !== 0 && right.length !== 0) {
		left[0].order <= right[0].order ? result.push(left.shift()) : result.push(right.shift());	
	}

	return [...result, ...left, ...right]; // 아래 세줄과 같은 역할을 하는 코드
    // if(left.length === 0) results.push(...right);
    // if(right.length === 0) results.push(...left);
    // return results;
}

const re_sort_order = function (array) {
	// ending condition: length === 1 인 배열이 들어올 때, 정렬이 끝난 것. 
	if (array.length === 1) return array;

	// 2로 나누고 내림을 해야
	// length 가 2일 때도 안전하게 배열을 slice 할 수 있다.
	const middle_index = Math.floor(array.length / 2); 
	const left = array.slice(0, middle_index);
	const right = array.slice(middle_index);

	// 재귀로 계속해서 반으로 나누면서 length 가 1이 될때까지 쪼개고, 
	// 거꾸로 올라오면서 순수한 함수인 merge에 인자로 넣어서 다시 병합되어서 최종값을 리턴한다.
	return merge(re_sort_order(left), re_sort_order(right));
}

// 랜덤한 호의카드를 뽑는 카드
function rand_favor_card(i, room_name) {
  let num = Math.ceil(Math.random() * 8);

  switch(num) {
    case 1 :
      room_data[room_name].user_data_array[i].user_ingame_data.favor_card.assistent += 1;
      break;
    case 2 :
      room_data[room_name].user_data_array[i].user_ingame_data.favor_card.bar_owner += 1;
      break;
    case 3 :
      room_data[room_name].user_data_array[i].user_ingame_data.favor_card.big_man += 1;
      break;
    case 4 :
      room_data[room_name].user_data_array[i].user_ingame_data.favor_card.caretaker += 1;
      break;
    case 5 :
      room_data[room_name].user_data_array[i].user_ingame_data.favor_card.herbalist += 1;
      break;
    case 6 :
      room_data[room_name].user_data_array[i].user_ingame_data.favor_card.merchant += 1;
      break;
    case 7 :
      room_data[room_name].user_data_array[i].user_ingame_data.favor_card.shopkeeper += 1;
      break;
    case 8 :
      room_data[room_name].user_data_array[i].user_ingame_data.favor_card.wise_man += 1;
      break;
  }

  room_data[room_name].user_data_array[i].user_ingame_data.favor_card.total += 1;
}

// 다음 보드 라운드 찾는 함수
function next_board(room_name) {
  // final_round_order에서 라운드 찾기 
  // -- 한 명이라도 큐브로 선택한 라운드가 있다면 해당 라운드 시작
  let board = 0;
  for( let i = 0 ; i < room_data[room_name].final_round_order.length; i++ ){
    for( let j = 1; j < 10; j++ ){

      if( room_data[room_name].user_cube_data[j][i].cube[1].is_select == true ){
        if( board == 0) {
          // 최초인데 현재 i 에서 j가 최소일 것이므로 저장하고 j loop break
          board = j;
          break;
        }
        else if( board > j ){
          // 최소를 구하는 것이므로 j가 더 작다면 j를 저장하고
          // j 는 이후 계속 커지므로 더 볼 필요 없으니 break;
          board = j;
          break;
        }
        else{
          // 최소를 구하는 것이므로 선택된 순서가 first_start_board가 크다면 
          //그 이후도 볼 필요 없음
          break;
        }            
      }
    }
  }
  return board;
}

// 해당 보드에서 가장 가까운 1번 큐브를 찾는 함수
function find_first_board_cube(board_num, room_name) {
  // final_round_order에서 주어진 숫자의 보드에서 첫번째 큐브 찾기
  let first_cube_owner = -1;
  for( let i = 0 ; i < room_data[room_name].final_round_order.length; i++){
    if( room_data[room_name].user_cube_data[board_num][i].cube[1].is_select == true){
      first_cube_owner = i;
      // 가장 먼저 만나는 값이 필요하므로 바로 break
      break;
    }
  }
  return first_cube_owner;
}

// 보드 진행 이후 다음 큐브 / 보드를 찾아 가는 함수
function board_end_event(data, board_num) {
  
  //user_cube_data 에서 is_select 제거
  room_data[data.room_name].user_cube_data[board_num][data.board_order].cube[data.cube_order].is_select = false;

  let cube_max = 0;
  switch( board_num ){
    case 1 :
      cube_max = 3;
      break;
    case 3 :
      cube_max = 1;
      break;
    case 2 :
    case 4 :
    case 5 :
    case 6 :
    case 7 :
    case 8 :
      cube_max = 2;
      break;
    case 9 : 
      cube_max = 4;
      break;
  }
  
  // 다음 큐브가 있는지 검사
  let have_another_ingredient_cube = false;
  let next_board_order_num = 0;
  let next_board_cube_order_num = 1;
  for( let j = data.cube_order; j <= cube_max; j++ ){
    // 다음 순서 이거나 내 큐브 다음 것이 있을 경우를 건져아햠
    for( let i = 0; i < room_data[data.room_name].final_round_order.length; i++ ){
      // 재료 선택에 다음 순서가 있다는 이야기 이므로 원래대로 진행
      if( room_data[data.room_name].user_cube_data[board_num][i].cube[j].is_select ){
        have_another_ingredient_cube = true;
        next_board_order_num = i;
        next_board_cube_order_num = j;
        break;
      }
    }

    // 위 if문 에 걸려서 j 값이 변경 되었다면 탈출해야함.
    if( have_another_ingredient_cube ){
      break;
    }
  }
  clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> 다음 큐브가 있는지? : " + have_another_ingredient_cube + " / 보드 순서 번호 : " + next_board_order_num + " / 큐브순서번호 : " + next_board_cube_order_num;
  log = clog + "\n";
  fs.appendFile(dir, log, (err) => {
    if( err ) throw err;
    console.log(clog);
  });
  let send_data; 

  // false라면 더이상 큐브는 진행 하지 않으므로 다음 보드로 이동 여부를 확인해야함
  if( !have_another_ingredient_cube ){
    let next_board_num = next_board(data.room_name);
    // 더이상 보드 넘버가 없으면 이번 라운드 종료!
    if( next_board_num == 0 ) {
      // round_cont 가 완전 엔딩 == 6이라면 게임 종료 후 점수 순서대로 유저 표기 / game_end / 다시 시작 할 지 물어봄
      if( room_data[data.room_name].round_cont == 6 ) {
        // 남은 골드와 호의카드로 명성치 추가 계산
        // 골드 3원 -> 명성 1점
        // 호의 1장 -> 금화 2장
        // 논문에 작성된 원소와 인장 값을 계산해서 명성에 추가
        // 명성 점수에 따라서 1 2 3 4 등을 순차 나열
        // 점수가 같으면 공동 등위로 올림
        for( let i = 0; i < room_data[data.room_name].room_member_count; i++){
          // 호의카드 1장당 금화 2점
          room_data[data.room_name].user_data_array[i].user_ingame_data.my_gold += room_data[data.room_name].user_data_array[i].user_ingame_data.favor_card.total * 2;
          // 골드 3원당 명성 1점 +
          room_data[data.room_name].user_data_array[i].user_ingame_data.point += room_data[data.room_name].user_data_array[i].user_ingame_data.my_gold / 3;
          
        }
        // 발표된 논문의 원소와 정답을 확인함
        for( let i = 1; i < 9; i++ ){  
          if( room_data[data.room_name].theory_data[i].element != '' ){
            let answer_tf = ( room_data[data.room_name].theory_data[i].element == ingredient_answer[i-1] );

            if( answer_tf == true ) {
              // 정답이면 인장의 별표들에 맞는 점수 추가
              for( let j = 1; j < 4; j++ ){
                if( room_data[data.room_name].theory_data[i].stamp[j].user_key != '' ) {
                  switch(room_data[data.room_name].theory_data[i].stamp[j].point){
                    case 'point_5_1':
                    case 'point_5_2':
                      for(let k = 0; k < room_data[data.room_name].room_member_count; k++){
                        if( room_data[data.room_name].user_data_array[k].user_key == theory_data[i].stamp[j].user_key ){
                          room_data[data.room_name].user_data_array[k].user_ingame_data.point += 5;
                          break;
                        }
                      }
                      break;
                    case 'point_3_1':
                    case 'point_3_2':
                    case 'point_3_3':
                      for(let k = 0; k < room_data[data.room_name].room_member_count; k++){
                        if( room_data[data.room_name].user_data_array[k].user_key == theory_data[i].stamp[j].user_key ){
                          room_data[data.room_name].user_data_array[k].user_ingame_data.point += 3;
                          break;
                        }
                      }
                      break;
                    default:
                      // 다른 인장은 색깔뿐이므로 점수 없음
                      break;
                  }
                }
              }
            }
            else if ( answer_tf == false ) {
              // 실패하면 별표 인장( 5, 3점짜리 ) 당 -4점
              for( let j = 1; j < 4; j++ ){
                if( room_data[data.room_name].theory_data[i].stamp[j].user_key != '' ) {
                  let evade_fail = false;
                  switch(room_data[data.room_name].theory_data[i].stamp[j].point){
                    case 'point_5_1':
                    case 'point_5_2':
                    case 'point_3_1':
                    case 'point_3_2':
                    case 'point_3_3':
                      for(let k = 0; k < room_data[data.room_name].room_member_count; k++){
                        if( room_data[data.room_name].user_data_array[k].user_key == room_data[data.room_name].theory_data[i].stamp[j].user_key ){
                          room_data[data.room_name].user_data_array[k].user_ingame_data.point -= 4;
                          evade_fail = true;
                          break;
                        }
                      }
                      break;
                      // 색상을 알맞게 회피하면 0점
                      // 색상을 알맞게 회피하지 못하면 -4점
                      // 정답과 인장의 element를 비교해서 다른 원소를 찾아야함
                    case 'question_red_1':
                      evade_fail = compare_answer_to_theory_for_ending(i-1, i, 'r');
                      break;
                    case 'question_red_2':
                      evade_fail = compare_answer_to_theory_for_ending(i-1, i, 'r');
                      break;
                    case 'question_blue_1':
                      evade_fail = compare_answer_to_theory_for_ending(i-1, i, 'b');
                      break;
                    case 'question_blue_2':
                      evade_fail = compare_answer_to_theory_for_ending(i-1, i, 'b');
                      break;
                    case 'question_green_1':
                      evade_fail = compare_answer_to_theory_for_ending(i-1, i, 'g');
                      break;
                    case 'question_green_2':
                      evade_fail = compare_answer_to_theory_for_ending(i-1, i, 'g');
                      break;
                    default:
                      // 다른 인장은 색깔뿐이므로 점수 없음
                      break;
                  }

                  if( evade_fail == false ) {
                    for(let k = 0; k < room_data[data.room_name].room_member_count; k++){
                      if( room_data[data.room_name].user_data_array[k].user_key == room_data[data.room_name].theory_data[i].stamp[j].user_key ){
                        room_data[data.room_name].user_data_array[k].user_ingame_data.point -= 4;
                        break;
                      }
                    }
                  }

                }
              }
              
            }
          }
        }

        // 점수 집계가 끝났으므로 최종 점수에 맞게 1등부터 순서대로 list에 삽입
        // 점수가 동일하면 순서 상관 없이 막 넣음        
        // game_result { 1 : {name, score, grade}}
        // 우선 변수 하나에 넣고
        // score 순으로 정렬한 뒤에
        // grade를 작성하면 되겠구만
        let temp_result = [];
        for( let i = 0; i < room_data[data.room_name].room_member_count; i++ ){
          let temp = {
            name  : room_data[data.room_name].user_data_array[i].user_name,
            order : room_data[data.room_name].user_data_array[i].user_ingame_data.point,
          };
          temp_result.push(temp);
        }

        // temp_result에서 score 순으로 정렬 -> 작은 순으로 정렬됨
        temp_result = re_sort_order(temp_result);

        // 현재 정렬 상태의 역순으로 넣으면 끗
        let cnt = room_data[data.room_name].room_member_count-1;
        let grade = Number(1);
        for ( let i = 1; i <= room_data[data.room_name].room_member_count; i++){
          game_result[i].name = temp_result[cnt].name;
          game_result[i].score = Number(temp_result[cnt].order);
          if( i > 1 ) {
            if( game_result[i].score != game_result[i-1].score ){
              grade++;
            }
          }
          game_result[i].grade = grade;
          cnt--;
        }
        clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> ------------------게임 끝-----------------------";
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
          console.log(clog);
        });
        clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> "+ game_result;
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
          console.log(clog);
        });
        // 게임 완료 자료를 전송함
        io.to(data.room_name).emit("game_end_event", game_result);

        room_data[data.room_name].left_game_result_data.count = room_data[data.room_name].room_member_count;
        room_data[data.room_name].left_game_result_data.room_pw = room_data[data.room_name].room_pw;

        // 다른 모든 변수를 초기화하고 종료
        init_everything(data.room_name);
        return;
      }
      else {
        // board_order 초기화
        send_data = {
          board_order : 0,
          board_cube_order : 1,
        }
        // ingredient_select_arr 새로 전송
        suffle_ingredient_card_select(data.room_name);
        io.to(data.room_name).emit("ingredient_select_card_open", room_data[data.room_name].ingredient_select_arr);
        // 라운드 종료 알림
        // 1라운드가 아니면 사용 가능 큐브를 인원에 따라 배분
        // 라운드 종료 시점은 반드시 1라운드에 가장 처음 오므로 굳이 현재 라운드를 알 필요가 없음
        let left_cube_half = 0;
        let tmp_cube_cnt = 0;
        if( room_data[data.room_name].room_member_count == 2 ){
          tmp_cube_cnt = 6;
        } else if ( room_data[data.room_name].room_member_count == 3 ){
          tmp_cube_cnt = 5;
        } else if ( room_data[data.room_name].room_member_count == 4 ){
          tmp_cube_cnt = 4;
        }
        // 개인에게 적용하는데 자신의 물약 실험으로 인한 부작용이 있다면 적용
        for( let i = 0; i < room_data[data.room_name].user_data_array.length; i++){
          // 큐브를 새로 배분하기 전 - 이전 라운드에 사용하지 않은 큐브 /2 만큼 호의카드 추가
          left_cube_half = room_data[data.room_name].user_data_array[i].user_ingame_data.cube_count / 2;
          for( let j = 0; j < left_cube_half; j++ ) {
            rand_favor_card(i, data.room_name);
          }
          // 큐브 갯수 새로 적용
          room_data[data.room_name].user_data_array[i].user_ingame_data.cube_count = tmp_cube_cnt;

          // 붉은 물약 부작용
          if( room_data[data.room_name].user_data_array[i].user_ingame_data.is_check_potion.this_round_red_0 == true ) {
            room_data[data.room_name].user_data_array[i].user_ingame_data.cube_count -= 1;
            // 부작용 먹었으니 해제
            room_data[data.room_name].user_data_array[i].user_ingame_data.is_check_potion.this_round_red_0 = false;
          }
          // 푸른 물약 부작용
          if( room_data[data.room_name].user_data_array[i].user_ingame_data.is_check_potion.this_round_blue_0 == true ){
            room_data[data.room_name].user_data_array[i].user_ingame_data.point -= 1;
            // 부작용 먹었으니 해제
            room_data[data.room_name].user_data_array[i].user_ingame_data.is_check_potion.this_round_blue_0 = false;
          }
        }

        // 라운드를 새로 시작 하기 전 final_order를 초기화
        room_data[data.room_name].final_round_order.splice(0, room_data[data.room_name].final_round_order.length);
        // user_cube_data 초기화
        room_data[data.room_name].user_cube_data = [,[],[],[],[],[],[],[],[],[]];

        // 라운드를 고를 순서를 세팅하고 보냄
        // 고를 순서는 1234 -> 2341 > 3412 > 4123 > 1234 
        let tmp = room_data[data.room_name].order[0];
        
        for( let i = 0; i < room_data[data.room_name].order.length; i++){
          if( i != room_data[data.room_name].order.length-1 ){
            room_data[data.room_name].order[i] = room_data[data.room_name].order[i+1];
          } else {
            room_data[data.room_name].order[i] = tmp;
          }
        }
        
        // 순서 번호 0으로 초기화
        room_data[data.room_name].decide_round_order_cont = 0;

        //라운드 진행 중을 알리는 변수 초기화
        for( let i = 0; i < room_data[data.room_name].user_data_array.length; i++ ){
          room_data[data.room_name].user_data_array[i].is_ingame = false;
        }

        // 용병에게 물약 판매관련 변수 초기화
        room_data[data.room_name].discount_coin_list = [];
        room_data[data.room_name].save_selling_potion_price = [];
        room_data[data.room_name].adventurer_card_data = adventurer_card_ori_data;
        room_data[data.room_name].temp_used_favor_card_list = temp_used_favor_card_list_ori;

        // 변경한 변수 적용
        io.to(data.room_name).emit("change_user_data", room_data[data.room_name].user_data_array);
        // 라운드 순서를 결정하는 순서를 결정하는 변수 공유를 위한 전송
        io.to(data.room_name).emit("decide_round_setting_order_counter_send", room_data[data.room_name].decide_round_order_cont);
        // 클라 내부 데이터를 초기화 하기 위해 빈 값을 보냄
        // 모두에게 해당 사용자가 선택한 값을 알림
        // io.emit("change_final_round_order", final_round_order);
        // -> 아래에서 final_round_order 를 전송하기때문에 위에서 초기화하면 끝임

        // 게임 순서를 결정하는 순서 전송
        io.to(data.room_name).emit("round_order_setting_before", room_data[data.room_name].order);
        io.to(data.room_name).emit("round_end");
        clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> 라운드를 종료합니다.";
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
          console.log(clog);
        });
      }
      
    }
    else{
      //번호가 있다면 해당 보드로 이동
      //해당 번호의 보드에서 고른 큐브의 순서에 맞게 초기화해야함
      let o_num = find_first_board_cube(next_board_num, data.room_name);
      send_data = {
        board_order : o_num,
        board_cube_order : 1,
      }

      // 호의 카드 caretaker가 사용되었을 때 3번 보드( 용병에게 물약판매 )전에 스스로 물약 마시기가 가능함
      let caretaker_use = false;
      let caretaker_using_user = -1;
      if( next_board_num == 3 ){
        for(let i = 0; i < room_data[data.room_name].room_member_count; i++ ){
          // 한 명이라도 썻으면 스스로 물약마시기 보드 띄우기... -> 다시 3번 보드로 돌리기가 어떻게되지
          if( room_data[data.room_name].temp_used_favor_card_list[4][i].user_key != '' ){
            caretaker_use = true;
            caretaker_using_user = i;
            break;
          }
        }
      }
      
      
      // 호의카드가 사용되지 않았을 때 or 다음 보드가 3번이 아닐때
      if( caretaker_use == false ) {
        clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> 다음 순서인 " + next_board_num + "번째 보드 시작 -- board_end_event";
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
          console.log(clog);
        });
        io.to(data.room_name).emit("board_start", next_board_num);
      }
      else {
        clog = new Date(+new Date()+TIME_ZONE).toISOString().replace(/T/, ' ').replace(/\..+/, '') +" > "+ data.room_name + "방 >> 관리인 사용 :: 스스로 물약마시기 보드 시작";
        log = clog + "\n";
        fs.appendFile(dir, log, (err) => {
          if( err ) throw err;
          console.log(clog);
        });
        let other_send_data = {
          temp_used_favor_card_list : room_data[data.room_name].temp_used_favor_card_list,
          using_user_num : caretaker_using_user,
        }
        io.to(data.room_name).emit("caretaker_board_start", other_send_data);
        return;
      }
    }
  }
  // 큐브가 있어서 해당 보드 계속 진행
  else {
    // 재전송할 데이터를 계산
    let board_order = next_board_order_num;
    let board_cube_order = next_board_cube_order_num;

    send_data = {
      board_order : board_order,
      board_cube_order : board_cube_order,
    }
  }

  // 화면에 표기된 재료카드 업데이트
  io.to(data.room_name).emit("ingredient_select_card_open", room_data[data.room_name].ingredient_select_arr);
  // 변수 업데이트를 모두에게 알림 -- 모두가 공유하는 변수이므로
  io.to(data.room_name).emit("change_board_order_val", send_data);
  // 화면에 표기된 큐브 업데이트
  io.to(data.room_name).emit("change_final_round_order", room_data[data.room_name].final_round_order);
  io.to(data.room_name).emit("change_user_cube_data", room_data[data.room_name].user_cube_data);
}

// 관리인 카드를 통해 스스로 물약 마시기 행동을 하였을 때 다음보드 찾기
function caretaker_board_end_event(data) {
/*
data : user_key
     : card_list
     : caretaker_used
     : board_is
     : using_user_num
     : room_name
  봐야할 정보 
    temp_favor_card_use_list
*/
  // 사용한 번호의 temp_favor_card_use_list 제거
  room_data[data.room_name].temp_used_favor_card_list[4][data.using_user_num].count = 0;
  room_data[data.room_name].temp_used_favor_card_list[4][data.using_user_num].user_key = '';

  // temp_favor_card_use_list에 4번 caretaker가 전부 사용 되었는지 확인
  let use_end = true;
  let next_use_num = -1;
  for(let i = 0; i < room_data[data.room_name].room_member_count; i++){
    if( room_data[data.room_name].temp_used_favor_card_list[4][i].user_key != '' ){
      use_end = false;
      next_use_num = i;
      break;
    }
  }

  // 전부 사용 되었으면 일반 board_end_event로 넘겨서 실행
  let send_data = '';
  if( use_end == true ) {
    // board_end_event에서 보드번호와 오더를 받고 해당 번호를 false 처리하는 과정이 필수이므로 
    // 반드시 false 인 0,1 값을 넣어줌
    send_data = {
      board_order : 0,
      cube_order : 1,
      room_name : data.room_name,
    };
    board_end_event(send_data, 1);
  } 
  // 사용 되지 않았으면 다음 유저에게 턴을 넘겨야함
  else {
    send_data = {
      caretaker_use : true,
      temp_used_favor_card_list : room_data[data.room_name].temp_used_favor_card_list,
      using_user_num : next_use_num,
    };

    io.to(data.room_name).emit("caretaker_board_start", send_data);
  }    

}

function compare_answer_to_theory_for_ending(answer_num, theory_num, compare_color){
  // answer_num :: ingredient_answer의 번호
  // theory_num :: 논문이 발표된 재료의 번호
  // compare_color :: 인장의 색 :: r, g, b
  let return_value = false;

  // 정답 원소의 r g b 색을 저장할 각 변수 ( 하나로 해도 할 수 있는데 이게 편하지 않나....)
  let answer_r = '';
  let answer_g = '';
  let answer_b = '';

  switch( answer_num ) {
    case 1:
      answer_r = 0; answer_g = 1; answer_b = 0;
      break;
    case 2:
      answer_r = 1; answer_g = 0; answer_b = 1;
      break;
    case 3:
      answer_r = 0; answer_g = 1; answer_b = 1;
      break;
    case 4:
      answer_r = 1; answer_g = 0; answer_b = 0;
      break;
    case 5:
      answer_r = 1; answer_g = 0; answer_b = 0;
      break;
    case 6:
      answer_r = 0; answer_g = 1; answer_b = 1;
      break;
    case 7:
      answer_r = 0; answer_g = 0; answer_b = 0;
      break;
    case 8:
      answer_r = 1; answer_g = 1; answer_b = 1;
      break;
  }

  // 논문에 발표된 원소의 rgb를 저장할 각 변수
  let theory_r = '';
  let theory_g = '';
  let thoery_b = '';

  switch( theory_num ) {
    case 1:
      theory_r = 0; theory_g = 1; thoery_b = 0;
      break;
    case 2:
      theory_r = 1; theory_g = 0; thoery_b = 1;
      break;
    case 3:
      theory_r = 0; theory_g = 1; thoery_b = 1;
      break;
    case 4:
      theory_r = 1; theory_g = 0; thoery_b = 0;
      break;
    case 5:
      theory_r = 1; theory_g = 0; thoery_b = 0;
      break;
    case 6:
      theory_r = 0; theory_g = 1; thoery_b = 1;
      break;
    case 7:
      theory_r = 0; theory_g = 0; thoery_b = 0;
      break;
    case 8:
      theory_r = 1; theory_g = 1; thoery_b = 1;
      break;
  }

  // 정답과 원소를 비교해서 틀린 원소에 false를 입힘
  let compare_r = true;
  let compare_g = true;
  let compare_b = true;

  if( answer_r != theory_r ) compare_r = false;
  if( answer_g != theory_g ) compare_g = false;
  if( answer_b != thoery_b ) compare_b = false;

  // compare_color가 false 라면 true로 변경 ==> 원소 발표에 감점이 없음!
  if( compare_color == 'r' ) compare_r = true;
  else if( compare_color == 'g' ) compare_g = true;
  else if( compare_color == 'b' ) compare_b = true;

  // 위에서 회피한 값을 true로 변경하였으므로 모두 true가 아니면 false
  // == false가 1개 이상이면 제대로 회피 못한것
  let false_cnt = 0;

  if( compare_r == false ) false_cnt++;
  if( compare_g == false ) false_cnt++;
  if( compare_b == false ) false_cnt++;

  if( false_cnt >= 1 ) {
    return_value = false;
  }
  else {
    // false 갯수가 0개로 잘 회피됨 
    return_value = true;    
  }
  return return_value;
}