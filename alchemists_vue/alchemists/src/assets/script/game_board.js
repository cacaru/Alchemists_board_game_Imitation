import { socket } from '@/components/room_list.vue'
import { default as modal_area } from '@/components/modal_area.vue'
import { default as personal_game_data } from '@/components/personal_game_data.vue' 
import { default as adventurer_selling } from '@/components/adventurer_selling.vue'
import { default as favor_card_use } from '@/components/favor_card_use.vue'
import { default as show_ingredient_card } from '@/components/show_ingredient_card.vue'

export default {
    name : 'game_board',
    components : {
        Modal_component : modal_area,
        Personal_game_data : personal_game_data,
        Adventurer_selling : adventurer_selling,
        Favor_card_use : favor_card_use,
        Show_ingredient_card : show_ingredient_card,
    },
    async created() {
        await this.$nextTick();
        // 게임 초기화 & 시작 트리거
        socket.emit("created_data_announce", this.$route.params.room_name);
        this.my_key = socket.id;
        //console.log(this.my_key);

        if (this.$route.params.restart_counter != null){
            //console.log(this.$route.params.restart_counter);
            this.restart_counter = this.$route.params.restart_counter;
        }
    },
    mounted () {
        //최초 분배된 기본 재료 수령
        socket.on("get_ingame_data", (data) => {
            this.user_data = data;
            for(let i = 0; i < data.length; i++){
                if( data[i].is_master === 'true' ) {
                    this.room_owner = data[i].user_name;
                }
                if( data[i].user_key == this.my_key ){
                    this.my_data = data[i];
                    this.my_name = data[i].user_name;
                    this.my_color = data[i].user_color;
                    
                    // total_dicount_adventruer 갯수 설정
                    this.total_dicount_adventruer = 0;
                    if( data[i].user_ingame_data.discount_adventurer.ad_0 == true ){
                        this.total_dicount_adventruer += 1;
                    }
                    if( data[i].user_ingame_data.discount_adventurer.ad_1 == true ){
                        this.total_dicount_adventruer += 1;
                    }
                    if( data[i].user_ingame_data.discount_adventurer.ad_2 == true ){
                        this.total_dicount_adventruer += 1;
                    }
                    if( data[i].user_ingame_data.discount_adventurer.ad_3 == true ){
                        this.total_dicount_adventruer += 1;
                    }
                    break;
                }
            }

            // adventurer_selling_modal에 정보 전송
            Object.keys(this.$refs).forEach(el => {
                if (el.startsWith("adventurer_selling_modal") && this.$refs[el] != null){
                    //console.log(el);
                    //console.log(this.$refs[el]);
                    this.$refs[el].set_my_data(this.my_data);
                    this.$refs[el].set_my_key(this.my_key);
                    return false;
                }
            });            

            setTimeout(()=>{
                let msg = {
                    speaker : this.my_name,
                    msg     : "방에 입장하였습니다.",
                    type    : 'announce',
                    room_name : this.room_name,
                }
                socket.emit("chat", msg);    
            }, 500);
            
        });
        
        //채팅 on
        socket.on("chat", (data) => {
            // console.log("서버에서 받아온 데이터");
            // console.log(data);
            // console.log("받아오기 끝");
            switch ( data.type ) {
                case "announce" :
                    this.textarea += data.speaker + " 이(가) " + data.msg + "\n";
                    break;
                case "normal" :
                    this.textarea += data.speaker + " : " + data.msg + "\n";
                    break;
            }
            // 스크롤을 자동으로 내림
            //this.$refs.show_chat.scrollTop = this.$refs.show_chat.scrollHeight;
        });

        // 선택할 수 있는 카드 공개
        socket.on("ingredient_select_card_open", (data) => {
            //console.log(data);
            this.ingredient_card_selected = data;
        });

        // 호의 카드 사용 함수
        socket.on("show_favor_modal", (data) => {
            // 받는 정보
            // data :: 
            // card_kind : 어떤 호의카드를 사용했는지
            // temp_used_favor_card_list : 누가 해당 호의카드를 사용했는지
            console.log("show_favor_modal");
            console.log(data);
            this.card_kind = data.card_kind;
            this.temp_used_favor_card_list = data.temp_used_favor_card_list;

            // 내가 호의카드 썻는지 확인하기
            let im_used = false;
            // 호의카드 번호
            let card_num = 0;
            switch(this.card_kind) {
                case 'assistent':
                    card_num = 1;
                    break;
                case 'bar_owner':
                    card_num = 2;
                    break;
                case 'big_man':
                    card_num = 3;
                    break;
                case 'caretaker':
                    card_num = 4;
                    break;
                case 'herbalist':
                    card_num = 5;
                    break;
                case 'merchant':
                    card_num = 6;
                    break;
                case 'shopkeeper':
                    card_num = 7;
                    break;
                case 'wise_man':
                    card_num = 8;
                    break;
            }

            for( let i = 0; i < 4; i++ ){
                if( this.my_key == data.temp_used_favor_card_list[card_num][i].user_key ) {
                    im_used = true;
                    break;
                }
            }
            if( im_used == true ) {
                // 모달 on
                this.$refs.favor_card_use_check_modal.favor_modal_open(this.card_kind);
            }
        });

        // 호의 카드 사용 성공 알림
        socket.on("use_favor_card_alert", () => {
            this.favor_card_use_key_val += 1;
            // 모달 off
            this.$refs.favor_card_use_check_modal.favor_modal_close();
        });

        // 이번 게임에서 사용할 용병 리스트
        socket.on("adv_list_setting", (data) => {
            this.random_adv_list = data;
            //console.log(this.random_adv_list);
        });
        // 용병에게 판매 가능한 물약 리스트
        socket.on("adv_sell_potion_list", (data) => {
            this.adventurer_card_data = data;
            Object.keys(this.$refs).forEach(el => {
                if (el.startsWith("adventurer_selling_modal") && this.$refs[el] != null){
                    //console.log(this.$refs[el]);
                    this.$refs[el].adventurer_card_data_change(data);
                    return false;
                }
            });            
            // console.log(this.data);
        })
        socket.on("adv_sell_potion_ori_list", (data) => {
            this.adventurer_card_ori_data = data;
            //console.log(data);
        });

        // 용병에게 할인카드 제시가 끝났음을 받는 함수
        socket.on("end_adv_dis_step", (data) => {
            this.discount_coin_list = data;
            this.is_end_discount = true;
            // console.log(this.discount_coin_list);
        });

        // 용병에게 판매할 물약의 값을 결정되었음을 받는 함수
        socket.on("selling_start", (data) => {
            // 물약 생성 모달 on 
            Object.keys(this.$refs).forEach(el => {
                if (el.startsWith("adventurer_selling_modal")){
                    this.$refs[el].make_potion_modal_open(data);
                    return false;
                }
            });            
            //this.$refs.adventurer_selling_modal.make_potion_modal_open(data);
        });

        // 용병에게 물약 판매가 끝났음을 알리는 함수
        socket.on("selling_potion_end", (data) => {
            // 물약 판매 모달 닫기  
            //console.log("selling_potion_end");
            console.log(data);
            if (data.user_key == this.my_key){
                // 내 차례가 끝나면 잠시 대기해달라는 모달로 변경
                this.sell_potion_turn = false;
            }
            //다른 사람이 끝나면 아무 동작도 하지 않아야함

            this.selling_end_modal = true;
            this.selling_user_color = data.user_color;
            this.selling_success = data.selling_success;
            this.selling_potion_kind = data.potion;

            //console.log(this.my_key);
            
            // 3초 지연 후 자동 모달 끄기
            setTimeout(() => {
                Object.keys(this.$refs).forEach(el => {
                    if (el.startsWith("adventurer_selling_modal") && this.$refs[el] != null){
                        this.$refs[el].set_my_data(this.my_data);
                        this.$refs[el].set_my_key(this.my_key);
                        this.$refs[el].adventurer_card_data_change(this.adventurer_card_data);
                        return false;
                    }
                });            
                //this.$refs.adventurer_selling_modal.set_my_key(this.my_key);
                //this.$refs.adventurer_selling_modal.set_my_data(this.my_data);
                //this.$refs.adventurer_selling_modal.adventurer_card_data_change(this.adventurer_card_data);

                this.selling_end_modal = false;
                // 정보 초기화
                this.selling_success = false;
                this.selling_potion_kind = '';
            }, 3000);
        });

        // 용병에게 물약판매 순서를 받는 함수
        socket.on("change_selling_turn", (data) => {
            this.selling_turn = data;
        });

        // 변경된 게임 데이터를 받는 함수
        socket.on("change_user_data", (data) => {
            this.user_data = data;
            this.my_data = '';
            for( let i = 0; i < data.length; i++){
                if( data[i].user_key == this.my_key ){
                    this.my_data = data[i];
                    break;
                }
            }
            // adventurer_selling에 my_data 등록
            //this.$refs.adventurer_selling_modal.set_my_data(this.my_data);
            Object.keys(this.$refs).forEach(el => {
                if (el.startsWith("adventurer_selling_modal") && this.$refs[el] != null){
                    this.$refs[el].set_my_data(this.my_data);
                    return false;
                }
            });            
            //console.log(this.user_data);

            // total_dicount_adventruer 갯수 설정
            this.total_dicount_adventruer = 0;
            if( this.my_data.user_ingame_data.discount_adventurer.ad_0 == true ){
                this.total_dicount_adventruer += 1;
            }
            if( this.my_data.user_ingame_data.discount_adventurer.ad_1 == true ){
                this.total_dicount_adventruer += 1;
            }
            if( this.my_data.user_ingame_data.discount_adventurer.ad_2 == true ){
                this.total_dicount_adventruer += 1;
            }
            if( this.my_data.user_ingame_data.discount_adventurer.ad_3 == true ){
                this.total_dicount_adventruer += 1;
            }
        });
        
        // 변경된 final_round_order를 데이터를 받는 함수 
        socket.on("change_final_round_order", (data) => {
            this.final_round_order = data;
            //console.log(this.final_round_order);
        });

        // 변경된 result_table : 추리 테이블 데이터를 받는 함수
        socket.on("change_result_table", (data) => {
            this.result_table = data;
            //console.log(this.result_table);
        });

        // 몇 라운드 인지 받기
        socket.on("change_round", (data) => {
            this.round_cont = data;
            // console.log(this.round_cont);
        });

        // 논문 관련 데이터 받기
        socket.on("change_theory_data", (data) => {
            //console.log(data);
            this.theory_data = data;
            //console.log(this.theory_data);
            //console.log("논문 발표 완료");
        });

        // 물약 전시회 데이터 받기
        socket.on("change_exhibition_data", (data) => {
            this.exhibition_result = data;
            // console.log(this.exhibition_result);
        });

        // 게임 순서를 정하는 순서의 정보 받는 함수
        socket.on("round_order_setting_before" , (data) => {
            //console.log(data);
            let as = [];

            for( let i = 0; i < data.length; i++) {
                for( let j = 0; j < this.user_data.length; j++){
                    if( data[i] == this.user_data[j].user_key ){

                        let choice_data = {
                            user_key  : this.user_data[j].user_key,
                            user_name : this.user_data[j].user_name,
                        }
                        
                        as.push(choice_data);
                        
                        choice_data = '';
                    }
                }
            }
            this.round_setting_order = as;
            //console.log(this.round_setting_order[0].user_key);
            //console.log(this.round_order[this.round_order_selecter].user_key);
        });

        // 게임 순서를 정하는 순서를 정하는 변수를 받는 함수 
        socket.on("decide_round_setting_order_counter_send", (data) => {
            //console.log(data);
            this.round_setting_order_counter = data;
        });

        // 라운드 진행 순서를 고른 정보를 받는 함수
        socket.on("select_round_order_recive", (data) => {
            let btn_ele = '';
            let btn_before_ele = '';
            
            for( let i = 0; i < data.length; i++){
                 // 순서에 맞는 element를 선택
                switch(data[i].order) {
                    case 1 : 
                        btn_ele = this.$refs.btn_1;
                        break;
                    case 2 :
                        btn_ele = this.$refs.btn_2;
                        break;
                    case 3 :
                        btn_ele = this.$refs.btn_3;
                        break;
                    case 4 : 
                        btn_ele = this.$refs.btn_4;
                        break;
                    case 5 :
                        btn_ele = this.$refs.btn_5;
                        break;
                    case 6 :
                        btn_ele = this.$refs.btn_6;
                        break;
                    case 7 :
                        btn_ele = this.$refs.btn_7;
                        break;
                    case 8 :
                        btn_ele = this.$refs.btn_8;
                        break;
                }
                switch( data[i].before_order ){
                    case 1 : 
                        btn_before_ele = this.$refs.btn_1;
                        break;
                    case 2 :
                        btn_before_ele = this.$refs.btn_2;
                        break;
                    case 3 :
                        btn_before_ele = this.$refs.btn_3;
                        break;
                    case 4 : 
                        btn_before_ele = this.$refs.btn_4;
                        break;
                    case 5 :
                        btn_before_ele = this.$refs.btn_5;
                        break;
                    case 6 :
                        btn_before_ele = this.$refs.btn_6;
                        break;
                    case 7 :
                        btn_before_ele = this.$refs.btn_7;
                        break;
                    case 8 :
                        btn_before_ele = this.$refs.btn_8;
                        break;
                }
                //console.log(btn_ele[0].className);
                // 기존의 것이 있으면 기존 색 class 제거
                if( btn_before_ele != '' ){

                    let num = btn_before_ele[0].className.indexOf(data[i].user_color);
                    // 문구 검색 결과가 있으면 제거 
                    if ( num > 0 ){
                        let str = btn_before_ele[0].className.substring(0, num-1);
                        btn_before_ele[0].className = str;
                    }
                    // 결과가 음수면 없는 것이므로 넘김
                }

                // 기존의 것을 없앤 완전 초기화 상태이므로  기존의 것과 중복 되더라도 그냥 진행 가능
                // 기존 : 5 신규 5 일 경우 위에서 이미 검사하지 않고 지웠으므로
                // 신규 5를 다시 칠함
                // 두 함수에서 모두 if로 구분하여 ifif중복하기 귀찮아서 그냥 무조건 지우고 무조건 색칠
                if( btn_ele != '' ){
                    btn_ele[0].className += ' ' + data[i].user_color;
                    //console.log(btn_ele[0].className);
                }
                // 변수 초기화
                btn_before_ele = '';
                btn_ele = '';

            }
            // 내부 변수에도 값을 적용
            this.final_round_order = data;
            //console.log(data);
        });

        // 라운드 진행 순서를 고르는 것이 끝났다는 알림을 받는 함수 
        socket.on("decide_round_setting_order_end", (data) => {
            this.decide_order = !this.decide_order;
            // final_round_order을 고른 order에 맞게 재정렬한 값으로 재전송 받음
            this.final_round_order = data;
        });
        // 현재 라운드의 큐브 순서 받기
        socket.on("change_user_cube_data", (data) => {
            this.user_cube_data = data;
            //console.log(this.user_cube_data);
        });

        // 판매하는 아티펙트 정보를 받는 함수
        socket.on("can_buy_artifacts_list", (data) => {
            this.can_buy_artifacts = data;
            //console.log(this.can_buy_artifacts);
        });

        // 큐브 선택시 사용 가능 큐브가 없을 때
        socket.on("cant_use_cube", (data) => {
            alert(data.say);
        });

        // 논문 반박 성공시 모달 오픈 함수
        socket.on("open_stamp_modal", (data) => {
            //console.log(data);
            this.reciving_data = data;
            this.success_refute_modal_onoff = true;
        });

        // 전시회 물약 전시시 받을 함수
        socket.on("show_exhibition", (data) => {
            // 전시 재료 넣는 모달 끄기
            this.exhibition_check_modal = false;

            this.exhibition_result_user = data.user_name;
            this.exhibit_result = data.result;
            this.exhibit_on_cube_success = data.get_cube_success;

            this.exhibition_result_modal = true;

            // 3초 뒤 모달 off
            setTimeout(()=>{
                this.exhibition_result_modal = false;
            },5000);
        });

        // 보드 순서 시작
        socket.on("board_start", (start_board) => {
            this.board_start_checker = true;
            this.now_board_num = start_board;
            if (this.success_refute_modal_onoff != true ){
                this.board_start_event(this.now_board_num);
            }
        });
        
        // 호의 카드 <관리인> 사용 시 스스로 물약 마시기 모달 open
        socket.on("caretaker_board_start", (data) => {
            // 관리인 카드 사용!
            this.caretaker_used = true;
            this.using_user_num = data.using_user_num;
            // 모두 false로 만들고 선택된 보드만 true로 변경
            this.board_1_selected = false;
            this.board_2_selected = false;
            this.board_3_selected = false;
            this.board_4_selected = false;
            this.board_5_selected = false;
            this.board_6_selected = false;
            this.board_7_selected = false;
            this.board_8_selected = false;
            this.board_end_selected = false;
            // 다른 보드 의 모달 끄기
            this.sell_ingredient_modal_onoff = false;
            this.test_ingredient_modal_onoff = false;
            this.reasoning_table_onoff = false;
            this.presentation_of_theories_modal = false;
            this.ingre_presentation_modal_onoff = false;
            this.success_refute_modal_onoff = false;
            this.board_order = 0;
            // adventurer_selling_modal 끄기
            //this.$refs.adventurer_selling_modal.sell_potion_to_adv_modal_close();
            //this.$refs.adventurer_selling_modal.make_potion_modal_close();
            Object.keys(this.$refs).forEach(el => {
                if (el.startsWith("adventurer_selling_modal") && this.$refs[el] != null){
                    this.$refs[el].sell_potion_to_adv_modal_close();
                    this.$refs[el].make_potion_modal_close();
                    return false;
                }
            });            

            // 보드 순서 끄기
            this.my_turn = false;

            // 스스로 물약 마시기 보드 on
            this.board_8_selected = true;
            this.test_ingredient_modal_onoff = true;

            // 누가 사용 했는지 확인해 my_turn 넘기기
            if( data.temp_used_favor_card_list[4][data.using_user_num].user_key == this.my_key ){
                // 먼저 사용한 유저부터 사용 되게 될 꺼임
                this.my_trun = true;
            }
            else {
                this.my_turn = false;
            }

        });

        //보드 진행 시 순서 관련 변수 업데이트
        socket.on("change_board_order_val", (data) => {            
            this.board_order = data.board_order;
            this.board_cube_order = data.board_cube_order;

            // 보드 순서 일 때 변수 입력
            if( this.board_2_selected == true || // 재료 판매
                this.board_5_selected == true || // 논문 반박
                this.board_6_selected == true || // 논문 발표
                this.board_7_selected == true || // 학생에게 실험
                this.board_8_selected == true || // 본인에게 실험
                this.board_end_selected == true    // 물약 전시회
                ){
                if( this.final_round_order[this.board_order].user_key == this.my_key ){
                    this.my_turn = true;
                }else {
                    this.my_turn = false;
                }
            }
            //console.log(this.my_turn);
            // 물약 판매 순서일 경우 판매하는 모든 사람들이 동시에 진행할 수 있어야함
            if( this.board_3_selected == true ){
                for( let i = 0; i < this.final_round_order.length; i++){
                    // 내 차례가 있으면 그냥 보여주기
                    if( this.user_cube_data[3][i].user_key == this.my_key ){
                        if( this.user_cube_data[3][i].cube[1].is_select == true ) {
                            this.sell_potion_turn = true;
                            break;
                        }
                        break;
                    }
                }
            }
        });

        socket.on("round_end", () => {
            this.round_end_checker = true;

            if (this.success_refute_modal_onoff != true){
                // wait for modal off
                this.round_end_to_modal_event();
            }
            
        });

        socket.on("test_ingredient_result", (data) => {
            let user_num = -1;
            
            for( let i = 0; i < this.user_data.length; i++){
                if( this.user_data[i].user_key == data.user_key ){
                    user_num = i;
                }
            }

            this.open_result_user = this.user_data[user_num].user_name;
            this.open_result = data.test_result;

            this.open_result_modal = true;
            
            // 3초 지연 후 자동 모달 끄기
            setTimeout( () => {
                this.open_result_modal = false;
            }, 3000);
        });

        socket.on("move_room_page_check", () => {
            // 게임 강제 종료 확인 모달
            this.game_forced_termination_alert_modal = true;
        });

        // 게임 종료
        socket.on("game_end_event", (data) => {
            //console.log(data);
            // 게임이 종료된것을 알리는 모달을 호출
            // 모달에서 완료하기 를 누르면 end_page로 넘어가는 형식으로 변경
            /*
            
            this.$router.push({
                    name : 'end_page',
                    params : {
                        game_result : JSON.stringify(data),
                        name : this.my_name,
                        room_name : this.room_name,
                    }
                });
            */
            this.game_end_data = data;
            this.game_end_alert_modal = true;
        });

        socket.on("restart_counter_setting", (data) =>{
            this.$router.push({
                name : 'end_page',
                params : {
                    game_result : JSON.stringify(this.game_end_data),
                    name : this.my_name,
                    room_name : this.room_name,
                    restart_counter : data,
                }
            });
        });
    },
    data() {
        return  {
            // component forceUpdate를 위한 key변수
            favor_card_use_key_val : 0,

            restart_counter : 0,
            game_forced_termination_alert_modal : false,

            board_start_checker : false,
            now_board_num : 0,
            round_end_checker : false,

            user_data : '', 
            room_name : this.$route.params.room_name,
            my_key : '',
            my_data : '',
            my_name : '',
            my_color : '',
            room_owner : '',
            total_dicount_adventruer : 0,
            textarea : '',
            message : '',
            ingredient_card_selected : '',
            // overlay
            decide_order : false,
            // overlay 완료버튼으로 나오는 modal
            decide_order_modal : false,
            // 현재 라운드
            round_cont : 0,
            // 라운드 순서를 정하는 순서 정보
            round_setting_order : '',
            // 라운드 순서를 정하는 순서 
            round_setting_order_counter : 0,
            // 해당 라운드의 순서 결정완료 후 순서
            final_round_order : [],
            // 해당 라운드의 큐브 순서
            user_cube_data : [],
            // 라운드 준비 완료 확인 모달
            round_order_check : false,
            // 구매 가능한 아티펙트 변수
            can_buy_artifacts : '',
            // 재료조합 추리 및 결과 테이블
            result_table : [],
            // board 선택을 위한 변수 8개
            board_1_selected :false,
            board_2_selected :false,
            board_3_selected :false,
            board_4_selected :false,
            board_5_selected :false,
            board_6_selected :false,
            board_7_selected :false,
            board_8_selected :false,
            // 매 보드마다 순서를 정할 변수
            board_order : 0,
            // 큐브 순서를 정할 변수
            board_cube_order : 1,
            // 2번째 보드 modal
            sell_ingredient_modal_onoff : false,
            // 보드의 현재 순서가 자신인지 확인하는 변수
            my_turn : false,

            // 호의 카드 종류
            card_kind : '',
            // 호의 카드 사용 여부 통합 변수
            temp_used_favor_card_list : '',
            // 관리인 카드가 사용 되었다는 것을 알릴 변수
            caretaker_used : false,
            // 관리인 카드 사용 번호
            using_user_num : '',

            // 용병에게 물약 판매 차례를 진행할 사람들만 표기할 변수
            sell_potion_turn : false,
            // 용병 순서 변수
            random_adv_list : [],
            // 판매할 물약 변수 원형
            adventurer_card_ori_data : '',
            // 판매할 물약 변수 
            adventurer_card_data : '',
            // 할인 포인트 제시 여부
            get_discount_coin : false,
            // 선택한 할인 카드 저장 변수
            before_adv_discount_select_num : -1,
            // 제시한 할인 카드 를 저장할 변수
            discount_coin_list : [],
            // 할인제시 스텝이 끝났는지 안끝났는지 알려줄 변수
            is_end_discount : false,
            // 나의 할인 스텝이 끝났을 때 기다리기 위한 변수
            im_end_discount : false,
            // 내 판매가
            my_selling_price : 0,
            // 판매 차례
            selling_turn : 0,
            // 판매할 물약 종류 설정
            what_kind_sell_potion : '',
            // 판매 성공/실패 모달 
            selling_end_modal : false,
            // 판매 성공/실패 변수
            selling_success : false,
            // 판매 성공/실패한 유저의 색
            selling_user_color : '',
            // 판매한 물약
            selling_potion_kind : '',

            // 실험할 재료를 저장해둘 변수
            test_ingredient_list : [],
            // 실험 모달
            test_ingredient_modal_onoff : false,
            // 추리 테이블 모달
            my_reasoning_table_modal : false,
            // 추리 테이블 관련 변수
            icon_data : {
                0 : require('@/assets/img/material_card/ingredient_1.png'),
                1 : require('@/assets/img/material_card/ingredient_2.png'),
                2 : require('@/assets/img/material_card/ingredient_3.png'),
                3 : require('@/assets/img/material_card/ingredient_4.png'),
                4 : require('@/assets/img/material_card/ingredient_5.png'),
                5 : require('@/assets/img/material_card/ingredient_6.png'),
                6 : require('@/assets/img/material_card/ingredient_7.png'),
                7 : require('@/assets/img/material_card/ingredient_8.png'),
            },
            reasoning_data : {
                0 : require('@/assets/img/stamp/rgbl010.png'),
                1 : require('@/assets/img/stamp/rgbl101.png'),
                2 : require('@/assets/img/stamp/rglb100.png'),
                3 : require('@/assets/img/stamp/rglb011.png'),
                4 : require('@/assets/img/stamp/rlgb001.png'),
                5 : require('@/assets/img/stamp/rlgb110.png'),
                6 : require('@/assets/img/stamp/rlglbl000.png'),
                7 : require('@/assets/img/stamp/rlglbl111.png'),
            },
            black_stamp_data : {
                0 : require('@/assets/img/stamp/stamp_black_5.png'),
                1 : require('@/assets/img/stamp/stamp_black_5.png'),
                2 : require('@/assets/img/stamp/stamp_black_3.png'),
                3 : require('@/assets/img/stamp/stamp_black_3.png'),
                4 : require('@/assets/img/stamp/stamp_black_3.png'),
                5 : require('@/assets/img/stamp/stamp_black_red.png'),
                6 : require('@/assets/img/stamp/stamp_black_red.png'),
                7 : require('@/assets/img/stamp/stamp_black_green.png'),
                8 : require('@/assets/img/stamp/stamp_black_green.png'),
                9 : require('@/assets/img/stamp/stamp_black_blue.png'),
                10 : require('@/assets/img/stamp/stamp_black_blue.png'),
            },
            blue_stamp_data : {
                0 : require('@/assets/img/stamp/stamp_blue_5.png'),
                1 : require('@/assets/img/stamp/stamp_blue_5.png'),
                2 : require('@/assets/img/stamp/stamp_blue_3.png'),
                3 : require('@/assets/img/stamp/stamp_blue_3.png'),
                4 : require('@/assets/img/stamp/stamp_blue_3.png'),
                5 : require('@/assets/img/stamp/stamp_blue_red.png'),
                6 : require('@/assets/img/stamp/stamp_blue_red.png'),
                7 : require('@/assets/img/stamp/stamp_blue_green.png'),
                8 : require('@/assets/img/stamp/stamp_blue_green.png'),
                9 : require('@/assets/img/stamp/stamp_blue_blue.png'),
                10 : require('@/assets/img/stamp/stamp_blue_blue.png'),
            },
            red_stamp_data : {
                0 : require('@/assets/img/stamp/stamp_red_5.png'),
                1 : require('@/assets/img/stamp/stamp_red_5.png'),
                2 : require('@/assets/img/stamp/stamp_red_3.png'),
                3 : require('@/assets/img/stamp/stamp_red_3.png'),
                4 : require('@/assets/img/stamp/stamp_red_3.png'),
                5 : require('@/assets/img/stamp/stamp_red_red.png'),
                6 : require('@/assets/img/stamp/stamp_red_red.png'),
                7 : require('@/assets/img/stamp/stamp_red_green.png'),
                8 : require('@/assets/img/stamp/stamp_red_green.png'),
                9 : require('@/assets/img/stamp/stamp_red_blue.png'),
                10 : require('@/assets/img/stamp/stamp_red_blue.png'),
            },
            white_stamp_data : {
                0 : require('@/assets/img/stamp/stamp_white_5.png'),
                1 : require('@/assets/img/stamp/stamp_white_5.png'),
                2 : require('@/assets/img/stamp/stamp_white_3.png'),
                3 : require('@/assets/img/stamp/stamp_white_3.png'),
                4 : require('@/assets/img/stamp/stamp_white_3.png'),
                5 : require('@/assets/img/stamp/stamp_white_red.png'),
                6 : require('@/assets/img/stamp/stamp_white_red.png'),
                7 : require('@/assets/img/stamp/stamp_white_green.png'),
                8 : require('@/assets/img/stamp/stamp_white_green.png'),
                9 : require('@/assets/img/stamp/stamp_white_blue.png'),
                10 : require('@/assets/img/stamp/stamp_white_blue.png'),
            },
            ele_core_data : {
                0 : require('@/assets/img/game_icon/red_origin.png'),
                1 : require('@/assets/img/game_icon/green_origin.png'),
                2 : require('@/assets/img/game_icon/blue_origin.png'),
            },
            picked : -1,
            reasoning_table_onoff : false,
            // 실험 결과를 모두에게 보여주는 변수
            open_result : '',
            open_result_user : '',
            open_result_modal : false,
            // 논문 발표/ 반박 시 발표된 정보 저장
            theory_data : '',
            // 논문 발표/ 반박 모달
            presentation_of_theories_modal : false,
            ingre_presentation_modal_onoff : false,
            // 논문 발표/반박 원소 선택 관련 변소
            before_theory_ele_select_num : 0,
            before_theory_stm_select_num : 0,
            now_select_ingre : 0,
            before_refute_ori_select_num : 0,
            // 논문 반박 성공 시 열릴 모달 
            success_refute_modal_onoff : false,
            // 반박 성공시 보여줄 인장들
            reciving_data : '',

            // 마지막 라운드 모달
            last_round_modal_onoff : false,
            // 마자믹 라운드 보드 켜기
            board_end_selected : false,
            // 잘 발표된 물약 표기
            exhibition_result : '',
            // 발표하기 위한 모달
            exhibition_check_modal_onoff : false,
            exhibition_potion_num : -1,

            // 전시 결과 모달
            exhibition_result_modal : false,
            // 전시 결과창 - 전시자
            exhibition_result_user : '',
            // 전시 결과창 - 결과
            exhibit_result : '',
            // 전시 가능한 여부 변수
            exhibit_on_cube_success : false,

            // 게임 종료 알림 모달
            game_end_alert_modal : false,
            // 게임 종료 정보
            game_end_data : '',
        }
    },

    methods : {
        round_end_to_modal_event: function(){
            // 모든 보드 포인터 끄기
            this.board_1_selected = false;
            this.board_2_selected = false;
            this.board_3_selected = false;
            this.board_4_selected = false;
            this.board_5_selected = false;
            this.board_6_selected = false;
            this.board_7_selected = false;
            this.board_8_selected = false;
            // 모든 모달 끄기
            this.sell_ingredient_modal_onoff = false;
            this.test_ingredient_modal_onoff = false;
            this.reasoning_table_onoff = false;
            this.presentation_of_theories_modal = false;
            this.ingre_presentation_modal_onoff = false;
            this.success_refute_modal_onoff = false;
            this.last_round_modal_onoff = false;
            this.exhibition_check_modal_onoff = false;

            //this.$refs.adventurer_selling_modal.sell_potion_to_adv_modal_close();
            //this.$refs.adventurer_selling_modal.make_potion_modal_close();
            Object.keys(this.$refs).forEach(el => {
                if (el.startsWith("adventurer_selling_modal") && this.$refs[el] != null){
                    this.$refs[el].sell_potion_to_adv_modal_close();
                    this.$refs[el].make_potion_modal_close();
                    return false;
                }
            });        
            // 보드 칸 순서 끄기
            this.my_turn = false;
            // 용병에게 물약 판매 초기환
            this.my_selling_price = -1;
            this.selling_turn = 0;
            this.discount_coin_list = [];
            this.before_adv_discount_select_num = -1;
            // overlay 켜서 다시 순서 정하기 시작
            this.decide_order = !this.decide_order;
            this.final_round_order = '';
        },

        board_start_event : function(start_board){
            // 모두 false로 만들고 선택된 보드만 true로 변경
            this.board_1_selected = false;
            this.board_2_selected = false;
            this.board_3_selected = false;
            this.board_4_selected = false;
            this.board_5_selected = false;
            this.board_6_selected = false;
            this.board_7_selected = false;
            this.board_8_selected = false;
            // 다른 보드 의 모달 끄기
            this.sell_ingredient_modal_onoff = false;
            this.test_ingredient_modal_onoff = false;
            this.reasoning_table_onoff = false;
            this.presentation_of_theories_modal = false;
            this.ingre_presentation_modal_onoff = false;
            this.success_refute_modal_onoff = false;
            this.board_order = 0;

            //this.$refs.adventurer_selling_modal.sell_potion_to_adv_modal_close();
            //this.$refs.adventurer_selling_modal.make_potion_modal_close();
            Object.keys(this.$refs).forEach(el => {
                if (el.startsWith("adventurer_selling_modal") && this.$refs[el] != null){
                    this.$refs[el].sell_potion_to_adv_modal_close();
                    this.$refs[el].make_potion_modal_close();
                    return false;
                }
            });     
            // 보드 순서 끄기
            //this.my_turn = false;
            this.round_end_checker = false;
            switch(start_board) {
                case 1 : 
                    //재료 선택
                    this.board_1_selected = true;
                    break;
                case 2 : 
                    //재료 판매
                    this.board_2_selected = true;
                    this.sell_ingredient_modal_onoff = true;
                    break;
                case 3 :
                    // 용병에게 뭉약 판매
                    this.board_3_selected = true;
                    //this.$refs.adventurer_selling_modal.sell_potion_to_adv_modal_open();
                    Object.keys(this.$refs).forEach(el => {
                        if (el.startsWith("adventurer_selling_modal") && this.$refs[el] != null){
                            this.$refs[el].sell_potion_to_adv_modal_open();
                            this.$refs[el].make_potion_modal_close();
                            return false;
                        }
                    });        
                    break;
                case 4 : 
                    //아티펙트 구매
                    this.board_4_selected = true;
                    break;
                case 5 :
                    // 논문 반박
                    this.board_5_selected = true;
                    this.presentation_of_theories_modal = true;
                    break;
                case 6 :
                    //논문 발표
                    this.board_6_selected = true; 
                    this.presentation_of_theories_modal = true;
                    break;
                case 7 :
                    //학생에게 실험
                    this.board_7_selected = true;
                    this.test_ingredient_modal_onoff = true;
                    break;
                case 8 :
                    //교수에게 실험
                    this.board_8_selected = true;
                    this.test_ingredient_modal_onoff = true;
                    break;
                case 9 :
                    // 물약 발표회
                    this.board_end_selected = true;
                    this.last_round_modal_onoff = true;
                    break;
                default:
                    console.log("start_board_val_err " + start_board);
                    break;
            }
        },

        use_chat : function(){
            if( this.message !== '' ){
              let msg = {
                speaker : this.my_name,
                msg : this.message,
                type : "normal",
                room_name: this.room_name,
              }

            //   console.log("서버에 보낼 데이터");
            //   console.log(msg);
            //   console.log("보낼준비 끝");
              this.message = '';
              socket.emit("chat", msg);
            }
        },

        draw_ingredient_card : function(cnt) {
            let data = {
                cnt : cnt,
                my_key : this.my_key,
            }
            socket.emit("draw_ingredient_card", data);
        },

        // 인게임 라운드 진행 준비 완료를 알리는 함수
        round_ready : function() {
            this.round_order_check = true;
        },

        round_order_setting_end : function() {
            this.round_order_check = false;
            let send_data = {
                user_key : this.my_key,
                room_name : this.room_name,
            };
            socket.emit("round_ready_on", send_data);

            // 준비완료 chat
            let msg = {
                speaker : this.my_name,
                msg     : "준비 완료하였습니다.",
                type    : 'announce',
                room_name : this.room_name,
            }
            socket.emit("chat", msg);   
        },
        round_order_setting_close : function() {
            this.round_order_check = false;
        },

        // decide_order overlay의 완료 버튼
        decide_order_modal_open : function() {
            // 선택 순서에 따라 클릭 막기
            if( this.round_setting_order[this.round_setting_order_counter].user_key != this.my_key){
                alert("지금은 내 선택 순서가 아닙니다! 조금만 기다려주세용");
                return;
            }
            this.decide_order_modal = !this.decide_order_modal;
        },
        
        // decide_order_modal 의 결정 버튼
        decide_order_setting_end : function() {
            
            let is_select_order = false;

            for( let i = 0; i < this.final_round_order.length; i++){
                if( this.final_round_order[i].user_key == this.my_key ){
                    is_select_order = true;
                    break;
                }
            }
            if( !is_select_order ) {
                alert("순서를 고르고 결정을 눌러주세요");
                this.decide_order_modal = false;
                return;
            }
            let send_data = {
                room_name : this.room_name,
            };
            //위 if문에 안걸리면 선택한 순서가 있다는 것 이므로 진행
            // 게임 결정 순서를 공유해야 하므로 서버에 값을 증가시키라고 전송
            socket.emit("decide_round_setting_order_counter_incre", send_data);
            this.decide_order_modal = false;
        },

        select_order_btn : function(val){
            // 선택 순서에 따라 클릭 막기
            if( this.round_setting_order[this.round_setting_order_counter].user_key != this.my_key){
                alert("지금은 내 선택 순서가 아닙니다! 조금만 기다려주세용");
                return;
            }

            // 마지막 순서는 벌칙 존이므로 선택할 수 없다!
            if( val == 8 ){
                alert("벌칙 존이라서 선택할 수 없습니다. 다른 순서를 선택해주세요.");
                return;
            }
            
            if( this.final_round_order != '' ){
                for( let i = 0; i < this.final_round_order.length; i++ ){
                    if( this.final_round_order[i].user_key != this.my_key ){
                        if( val == this.final_round_order[i].order) {
                            alert("다른 사람이 선택한 순서는 선택 할 수 없습니다.");
                            return;
                        }
                    }
                }
            }
            let round_order_data = {
                user_key : this.my_key,
                order : val,
                room_name : this.room_name,
            };

            // 버튼 선택 결과 전송
            socket.emit("select_round_order", round_order_data);
        },

        // 큐브로 순서를 선택할 때 반응
        click_order_btn : function(n, u, user_key) {

            for( let i = 0; i < this.user_data.length; i++ ) {
                if( this.user_data[i].user_key == user_key ){
                    if( this.user_data[i].is_ready == true ){
                        alert("준비 완료 후에는 선택할 수 없습니다!");
                        return;
                    }
                }
            }

            if( user_key != this.my_key ){
                alert("본인의 색으로 구분된 버튼만 골라주세요!");
                return;
            }

            // 라운드가 진행중이면 클릭되선 안됨
            for( let i = 0; i < this.user_data.length; i++ ) {
                if( this.user_data[i].user_key == this.my_key ){
                    if( this.user_data[i].is_ingame == true ){
                        alert("라운드 진행 중에는 클릭 할 수 없습니다!");
                        return;
                    }
                }
            }

            // 1라운드에는 용병에게 물약 판매 / 논문 발표 / 반박을 할 수 없다
            if( this.round_cont == 1 && ( n == 3 || n == 5 || n == 6 ) ){
                alert("1라운드에는 해당 보드의 행동을 할 수 없습니다!\n 다른 보드를 선택해주세요");
                return;
            }

            // n :: user_cube_data[n] :: 보드 번호
            // u :: user_cube_data[n][유저번호].cube[u] :: 보드 의 큐브 번호
            let send_data = {
                user_key : user_key,
                board_num : n,
                button_order_num : u,
                room_name : this.room_name,
            }
            //console.log(send_data);
            //버튼 선택 결과 전송
            socket.emit("select_cube", send_data);
        },

        // 추리테이블 눌렀을 때 반응
        click_reasoning_ele : function(index , key) {

            let reasoning_data = {
                user_key : this.my_key,
                x : index,
                y : key,
                change_val : this.picked,
                room_name : this.room_name,
            }

            socket.emit("reasoning_table_change", reasoning_data);
            //console.log(reasoning_data);

            this.picked = -1;
            this.reasoning_table_onoff = false;
        },
        reasoning_table_open : function() {
            this.my_reasoning_table_modal = !this.my_reasoning_table_modal;
        },

        open_reasoning_table_modal : function( index, key ) {
            this.$refs.index_val.innerText = index;
            this.$refs.key_val.innerText = key;
            //console.log(this.$refs.index_val.innerText);
            //console.log(index + " " + key);
            this.reasoning_table_onoff = true;
        },
        reset_val : function () {
            this.picked = -1;
        },

        // 카드 클릭해서 모달 열기
        favor_card_modal_on : function(card_kind) {
            this.$refs.favor_card_use_check_modal.favor_modal_open(card_kind);
        },

        // 호의 카드 사용하기
        favor_card_use_check : function ( data ) {
            // 호의 카드를 사용한다고 서버에 알리고 효과 적용받기
            let send_data = '';
            if( data.card_name == 'big_man' ){
                send_data = {
                    user_key : this.my_key,
                    room_name : this.room_name,
                    favor_card : data.card_name,
                    select_board_num : data.select_board_num,
                } ;               
            }
            else if ( data.card_name == 'herbalist'){
                send_data = {
                    user_key : this.my_key,
                    room_name : this.room_name,
                    favor_card : data.card_name,
                    ingre_list : data.ingre_list,
                };
            }
            else {
                send_data = {
                    user_key : this.my_key,
                    room_name : this.room_name,
                    favor_card : data.card_name,
                };
            }
            console.log(send_data);
            socket.emit("favor_card_use_confirm", send_data);
        }, 

        // 재료카드 선택 이벤트
        pick_ingredient : function(data, index) {
            if( this.board_1_selected == false || this.user_cube_data[1][this.board_order].user_key != this.my_key){
                alert("아직 재료를 고를 수 없습니다.")
                return;
            }
            // 재료 카드 선택 이벤트는 무조건 0번에 존재하므로
            if( this.user_cube_data[1][this.board_order].cube[this.board_cube_order].is_select == true
                && this.user_cube_data[1][this.board_order].user_key == this.my_key )
            {
                switch(index){
                    case "card_1" : 
                        index = 0;
                        break;
                    case "card_2" : 
                        index = 1;
                        break;
                    case "card_3" : 
                        index = 2;
                        break;
                    case "card_4" : 
                        index = 3;
                        break;
                    case "card_5" :
                        index = 4; 
                        break;
                }
                // 위의 변수를 뚫고 내려오면 현재 내가 고를 상태가 맞는것
                let send_data = {
                    user_key : this.my_key,
                    pick_item : data,
                    cube_order : this.board_cube_order,
                    board_order : this.board_order,
                    ingredient_select_arr_order : index,
                    room_name : this.room_name,
                }

                //console.log(send_data);
                socket.emit("pick_ingredient", send_data);
                // 보드의 행동이 일시적으로 끝남
                this.board_start_checker = false;
            }
            
        },

        // 판매 재료카드 선택 이벤트
        click_sell_ingredient_check : function(name) {
            console.log(name);
            let num = -1;
            // 원래 선택되어 있는지 확인하고 전부 해제 
            for(let i = 0; i < this.user_data.length; i++){
                if( this.user_data[i].user_key == this.my_key ){
                    if( this.user_data[i].user_ingame_data.ingredient.card_1 > 0 ){
                        num = this.$refs.sell_card_1[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            let str = this.$refs.sell_card_1[0].className.substring(0, num-1);
                            this.$refs.sell_card_1[0].className = str;
                        }
                    }
                    if ( this.user_data[i].user_ingame_data.ingredient.card_2 > 0 ){
                        num = this.$refs.sell_card_2[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            let str = this.$refs.sell_card_2[0].className.substring(0, num-1);
                            this.$refs.sell_card_2[0].className = str;
                        }       
                    }
                    if ( this.user_data[i].user_ingame_data.ingredient.card_3 > 0 ){
                        num = this.$refs.sell_card_3[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            let str = this.$refs.sell_card_3[0].className.substring(0, num-1);
                            this.$refs.sell_card_3[0].className = str;
                        }
                    }
                    if ( this.user_data[i].user_ingame_data.ingredient.card_4 > 0 ){
                        num = this.$refs.sell_card_4[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            let str = this.$refs.sell_card_4[0].className.substring(0, num-1);
                            this.$refs.sell_card_4[0].className = str;
                        }
                    }
                    if ( this.user_data[i].user_ingame_data.ingredient.card_5 > 0 ){
                        num = this.$refs.sell_card_5[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            let str = this.$refs.sell_card_5[0].className.substring(0, num-1);
                            this.$refs.sell_card_5[0].className = str;
                        }
                    }
                    if ( this.user_data[i].user_ingame_data.ingredient.card_6 > 0 ){
                        num = this.$refs.sell_card_6[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            let str = this.$refs.sell_card_6[0].className.substring(0, num-1);
                            this.$refs.sell_card_6[0].className = str;
                        }
                    }
                    if ( this.user_data[i].user_ingame_data.ingredient.card_7 > 0 ){
                        num = this.$refs.sell_card_7[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            let str = this.$refs.sell_card_7[0].className.substring(0, num-1);
                            this.$refs.sell_card_7[0].className = str;
                        }
                    }
                    if ( this.user_data[i].user_ingame_data.ingredient.card_8 > 0 ){
                        num = this.$refs.sell_card_8[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            let str = this.$refs.sell_card_8[0].className.substring(0, num-1);
                            this.$refs.sell_card_8[0].className = str;
                        }
                    }
                    break;     
                }
            }
            
            // 전부 해제한 후 선택한 것을 선택표시
            let ref_str = '';
            switch(name){
                case 'card_1':
                    ref_str = this.$refs.sell_card_1;
                    break;
                case 'card_2':
                    ref_str = this.$refs.sell_card_2;
                    break;
                case 'card_3':
                    ref_str = this.$refs.sell_card_3;
                    break;
                case 'card_4':
                    ref_str = this.$refs.sell_card_4;
                    break;
                case 'card_5':
                    ref_str = this.$refs.sell_card_5;
                    break;
                case 'card_6':
                    ref_str = this.$refs.sell_card_6;
                    break;
                case 'card_7':
                    ref_str = this.$refs.sell_card_7;
                    break;
                case 'card_8':
                    ref_str = this.$refs.sell_card_8;
                    break;
            }
            console.log(ref_str);
            ref_str[0].className += " sell_border";
        },

        // 재료 판매 이벤트
        click_sell_ingredient : function() {
            let num = -1;
            let sell_item_num = 0;
            // 선택된 것을 확인하고 번호를 저장
            // 반드시 한 카드의 클래스에먼 sell_border가 있을것이므로 가능
            for(let i = 0; i < this.user_data.length; i++){
                if( this.user_data[i].user_key == this.my_key ){
                    if( this.user_data[i].user_ingame_data.ingredient.card_1 > 0 ){
                        num = this.$refs.sell_card_1[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            sell_item_num = 1;
                            this.$refs.sell_card_1[0].className = this.$refs.sell_card_1[0].className.substring(0, num-1);
                        }
                    }
                    num = 0;
                    if ( this.user_data[i].user_ingame_data.ingredient.card_2 > 0 ){
                        num = this.$refs.sell_card_2[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            sell_item_num = 2;
                            this.$refs.sell_card_2[0].className = this.$refs.sell_card_2[0].className.substring(0, num-1);
                        }
                    }
                    num = 0;
                    if ( this.user_data[i].user_ingame_data.ingredient.card_3 > 0 ){
                        num = this.$refs.sell_card_3[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            sell_item_num = 3;
                            this.$refs.sell_card_3[0].className = this.$refs.sell_card_3[0].className.substring(0, num-1);
                        }
                    }
                    num = 0;
                    if ( this.user_data[i].user_ingame_data.ingredient.card_4 > 0 ){
                        num = this.$refs.sell_card_4[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            sell_item_num = 4;
                            this.$refs.sell_card_4[0].className = this.$refs.sell_card_4[0].className.substring(0, num-1);
                        }
                    }
                    num = 0;
                    if ( this.user_data[i].user_ingame_data.ingredient.card_5 > 0 ){
                        num = this.$refs.sell_card_5[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            sell_item_num = 5;
                            this.$refs.sell_card_5[0].className = this.$refs.sell_card_5[0].className.substring(0, num-1);
                        }
                    }
                    num = 0;
                    if ( this.user_data[i].user_ingame_data.ingredient.card_6 > 0 ){
                        num = this.$refs.sell_card_6[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            sell_item_num = 6;
                            this.$refs.sell_card_6[0].className = this.$refs.sell_card_6[0].className.substring(0, num-1);
                        }
                    }
                    num = 0;
                    if ( this.user_data[i].user_ingame_data.ingredient.card_7 > 0 ){
                        num = this.$refs.sell_card_7[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            sell_item_num = 7;
                            this.$refs.sell_card_7[0].className = this.$refs.sell_card_7[0].className.substring(0, num-1);
                        }
                    }
                    num = 0;
                    if ( this.user_data[i].user_ingame_data.ingredient.card_8 > 0 ){
                        num = this.$refs.sell_card_8[0].className.indexOf('sell_border');
                        if( num > 0 ) {
                            sell_item_num = 8;
                            this.$refs.sell_card_8[0].className = this.$refs.sell_card_8[0].className.substring(0, num-1);
                        }
                    }
                    num = 0;
                    break;
                }
            }

            //판매할 데이터를 묶어 보냄
            let send_data = {
                user_key : this.my_key,
                sell_item_num : sell_item_num,
                board_order : this.board_order,
                cube_order : this.board_cube_order,
                room_name : this.room_name,
            }
            //console.log(send_data);
            socket.emit("sell_item_confirm", send_data);
        },

        // 재료 판매 포기 이벤트
        end_selling_ingredient : function() {
            let send_data = {
                user_key : this.my_key,
                board_num : this.board_num,
                cube_order : this.board_cube_order,
                room_name : this.room_name
            };
            socket.emit("cancel_selling_ingre", send_data);
            // 보드의 행동이 일시적으로 끝남
            this.board_start_checker = false;
        },

        // 아티팩트 구매 가능 여부 확인 및 구매
        buy_artifact_confirm : function( data ) {
            if( this.final_round_order[this.board_order].user_key != this.my_key ||
                this.board_4_selected == false 
                ){
                alert("자신의 순서에만 구매할 수 있습니다\n큐브를 두고 해당 보드의 순서가 되면 구매해 주세요");
                return;
            }
            let send_data = {
                board_order : this.board_order,
                cube_order : this.board_cube_order,
                rank : data.rank,
                arti_num : data.num,
                user_key : this.my_key,
                room_name : this.room_name,
            }

            socket.emit("buy_artifact_confirm", send_data);

            // 보드의 행동이 일시적으로 끝남을 알림
            this.board_start_checker = false;
        },

        // 실험 재료 2개 고르기
        click_test_ingredient_check : function(name){
            let same_name = false;
            // 이미 2가지가 선택되어 있으면 가장 먼저 들어온 1개를 제거하고 마지막에 들어온 한개를 진행
            if( this.test_ingredient_list.length == 2){
                // 공용 변수 - test_border를 제거할 변수 이름
                let before_name = '';
                let num = 0;
                // 같은 카드가 이미 있는지 검사
                
                same_name = this.test_ingredient_list.includes(name);
                //console.log(same_name);
                if( same_name == true ) {
                    // 같은걸 제거
                    let same_num = this.test_ingredient_list.indexOf(name);
                    this.test_ingredient_list.splice(same_num, 1);
                    before_name = name;
                }
                else {// 먼저 들어온 1개를 제거
                    before_name = this.test_ingredient_list.shift();
                }

                switch(before_name) {
                    case 'card_1' :
                        num = this.$refs.test_card_1[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.test_card_1[0].className = this.$refs.test_card_1[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_2' :
                        num = this.$refs.test_card_2[0].className.indexOf('test_border');
                        if( num > 0 ){
                            this.$refs.test_card_2[0].className = this.$refs.test_card_2[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_3' :
                        num = this.$refs.test_card_3[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.test_card_3[0].className = this.$refs.test_card_3[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_4' :
                        num = this.$refs.test_card_4[0].className.indexOf('test_border');
                        if (num > 0 ) {
                            this.$refs.test_card_4[0].className = this.$refs.test_card_4[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_5' :
                        num = this.$refs.test_card_5[0].className.indexOf('test_border');
                        if( num > 0 ){
                            this.$refs.test_card_5[0].className = this.$refs.test_card_5[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_6' :
                        num = this.$refs.test_card_6[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.test_card_6[0].className = this.$refs.test_card_6[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_7' :
                        num = this.$refs.test_card_7[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.test_card_7[0].className = this.$refs.test_card_7[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_8' :
                        num = this.$refs.test_card_8[0].className.indexOf('test_border');
                        if ( num > 0 ) {
                            this.$refs.test_card_8[0].className = this.$refs.test_card_8[0].className.substring(0, num-1);
                        }
                        break;
                }
                
            }
            
            if( same_name == false ) {
                // 변수에 추가
                this.test_ingredient_list.push(name);
                let ref_str = '';
                switch(name){
                    case 'card_1':
                        ref_str = this.$refs.test_card_1;
                        break;
                    case 'card_2':
                        ref_str = this.$refs.test_card_2;
                        break;
                    case 'card_3':
                        ref_str = this.$refs.test_card_3;
                        break;
                    case 'card_4':
                        ref_str = this.$refs.test_card_4;
                        break;
                    case 'card_5':
                        ref_str = this.$refs.test_card_5;
                        break;
                    case 'card_6':
                        ref_str = this.$refs.test_card_6;
                        break;
                    case 'card_7':
                        ref_str = this.$refs.test_card_7;
                        break;
                    case 'card_8':
                        ref_str = this.$refs.test_card_8;
                        break;
                }
                console.log(this.$refs);
                ref_str[0].className += " test_border";
            }
        },

        click_test_ingredient : function() {
            if( this.test_ingredient_list.length != 2 ){
                alert("재료카드를 2장 선택해주세요!!!");
                return;
            }
            // test_ingredient_list의 길이가 2이면 서버에게 보내고
            // 고른 재료 차감 / 변수 초기화 / 결과는 모두에게 전송
            // / -> 유물 카드 에 따른 효과 발동 할 부분 추가할 것 계산
            else {
                let board_is = 0;
                if( this.board_7_selected == true ){
                    board_is = 7;
                }else if ( this.board_8_selected == true ) {
                    board_is = 8;
                }
                // 호의카드 사용 여부에 따라 추가적인 정보 전송
                let send_data = '';
                if ( this.caretaker_used == true ) {
                    send_data = { 
                        user_key : this.my_key,
                        card_list : this.test_ingredient_list,
                        caretaker_used : true,
                        using_user_num : this.using_user_num,
                        board_is : board_is,
                        room_name : this.room_name,
                    }
                } else {
                    send_data = { 
                        user_key : this.my_key,
                        card_list : this.test_ingredient_list,
                        caretaker_used : false,
                        board_order : this.board_order,
                        cube_order : this.board_cube_order,
                        board_is : board_is,
                        room_name : this.room_name,
                    }
                }
                //console.log(send_data);
                socket.emit("test_ingredient_confirm", send_data);

                // 보드의 행동이 일시적으로 끝남을 알림
                this.board_start_checker = false;

                // 선택한 카드 test_border 지우기
                let num = 0;
                for(let i = 0; i < 2; i++ ){
                    switch(this.test_ingredient_list[i]) {
                        case 'card_1' :
                            num = this.$refs.test_card_1[0].className.indexOf('test_border');
                            if( num > 0 ) {
                                this.$refs.test_card_1[0].className = this.$refs.test_card_1[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_2' :
                            num = this.$refs.test_card_2[0].className.indexOf('test_border');
                            if( num > 0 ){
                                this.$refs.test_card_2[0].className = this.$refs.test_card_2[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_3' :
                            num = this.$refs.test_card_3[0].className.indexOf('test_border');
                            if( num > 0 ) {
                                this.$refs.test_card_3[0].className = this.$refs.test_card_3[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_4' :
                            num = this.$refs.test_card_4[0].className.indexOf('test_border');
                            if (num > 0 ) {
                                this.$refs.test_card_4[0].className = this.$refs.test_card_4[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_5' :
                            num = this.$refs.test_card_5[0].className.indexOf('test_border');
                            if( num > 0 ){
                                this.$refs.test_card_5[0].className = this.$refs.test_card_5[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_6' :
                            num = this.$refs.test_card_6[0].className.indexOf('test_border');
                            if( num > 0 ) {
                                this.$refs.test_card_6[0].className = this.$refs.test_card_6[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_7' :
                            num = this.$refs.test_card_7[0].className.indexOf('test_border');
                            if( num > 0 ) {
                                this.$refs.test_card_7[0].className = this.$refs.test_card_7[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_8' :
                            num = this.$refs.test_card_8[0].className.indexOf('test_border');
                            if ( num > 0 ) {
                                this.$refs.test_card_8[0].className = this.$refs.test_card_8[0].className.substring(0, num-1);
                            }
                            break;
                    }
                }
                this.test_ingredient_list = [];
            }
        },

        open_result_modal_close : function() {
            this.open_result_modal = false;
        },

        // 용병에게 물약 판매 전 코인 제시
        select_adv_dis_coin : function(n) { 
            // 선택된 다른 코인은 표기 해제
            let num = 0;
            if( this.before_adv_discount_select_num > 0 ) {
                switch(this.before_adv_discount_select_num){
                    case '0':
                        num = this.$refs.ad_0_ico[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ad_0_ico[0].className = this.$refs.ad_0_ico[0].className.substring(0, num-1);
                        }
                        break;
                    case '1':
                        num = this.$refs.ad_1_ico[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ad_1_ico[0].className = this.$refs.ad_1_ico[0].className.substring(0, num-1);
                        }
                        break;
                    case '2':
                        num = this.$refs.ad_2_ico[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ad_2_ico[0].className = this.$refs.ad_2_ico[0].className.substring(0, num-1);
                        }
                        break;
                    case '3':
                        num = this.$refs.ad_3_ico[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ad_3_ico[0].className = this.$refs.ad_3_ico[0].className.substring(0, num-1);
                        }
                        break;
                }
            }
            // 선택한 코인 표기
            let ref_str = '';
            switch(n){
                case '0':
                    ref_str = this.$refs.ad_0_ico;
                    break;
                case '1':
                    ref_str = this.$refs.ad_1_ico;
                    break;
                case '2':
                    ref_str = this.$refs.ad_2_ico;
                    break;
                case '3':
                    ref_str = this.$refs.ad_3_ico;
                    break;
            }
            ref_str[0].className += " selected";
            this.before_adv_discount_select_num = n;
        },

        // 용병에게 할인 카드 제시 결정 
        adv_dis_confirm : function() {
            // 보낼 데이터
            // 유저키 , 유저색
            // 내가 선택한 할인 제시 코인 번호 -> 유저데이터의 제시카드 false로 변경 -> 겜당 1회밖에 못쓰기 때문
            if( this.before_adv_discount_select_num < 0 ) {
                alert("제시카드가 선택되지 않았습니다!\n제시카드를 선택해야합니다.");
                return;
            }
            let send_data = {
                user_key : this.my_key,
                color : this.my_color,
                dis_coin_num : this.before_adv_discount_select_num,
                room_name : this.room_name,
            }

            socket.emit("adv_dis_confirm", send_data);
            // 할인카드를 제시했으므로 변수 변경
            this.im_end_discount = true;
        },

        // 용병에게 팔 물약의 값 선택하기
        sell_potion_set_price : function(n){
            this.my_selling_price = n;
        },

        // 용병에게 팔 금액 홀드하기
        decision_sell_potion_price : function(){
            //내 차례가 아니면 진행 불가
            if( this.my_key != this.discount_coin_list[this.selling_turn].user_key ){
                alert("내 차례가 아니면 확정할 수 없습니다\n 조금 더 기다려주세요");
                return;
            }
            let send_data = {
                user_key : this.my_key,
                sell_price : this.my_selling_price,
                room_name : this.room_name,
            };
            this.im_end_discount = false;
            this.is_end_discount = false;
            socket.emit("sell_price_confirm", send_data);
        },

        // 용병에게 팔 물약 고르기
        select_sell_potion_kind : function(kind) {
            this.what_kind_sell_potion = kind;
        },

        // 물약 조제하기 :: 
        make_potion_preparation : function(data) {
            
            // test_ingredient_list 의 2가지 재료를 사용 --> 서버에서 재료 제거
            // what_kind_sell_potion -> 보내서 정답과 비교(red_1,0/ green_1/0, blue_1/0)
            // user_key , 
            let send_data = {
                user_key : this.my_key,
                user_color : this.my_color,
                card_list : data,
                what_kind_sell_potion : this.what_kind_sell_potion, 
                selling_turn : this.selling_turn,
                room_name   : this.room_name,
            };

            this.what_kind_sell_potion = '';
            socket.emit("sell_to_adv_potion", send_data);

            // 보드의 행동이 일시적으로 끝남을 알림
            this.board_start_checker = false;
        },

        // 용병에게 물약을 판매하는 변수에 값 넣기
        hold_before_adv_discount_num : function(data) {
            this.before_adv_discount_select_num = data;
        },

        // 논문 발표에서 원소를 선택할 함수.
        theory_element_select : function(n) {
        
            // 이미 선택된 다른 것이 있다면 해제하고 새로 고른 것을 선택해야함
            let num = 0;
            if( this.before_theory_ele_select_num > 0){
                switch(this.before_theory_ele_select_num){
                    case 1:
                        num = this.$refs.ele_1[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_1[0].className = this.$refs.ele_1[0].className.substring(0, num-1);
                        }
                        break;
                    case 2:
                        num = this.$refs.ele_2[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_2[0].className = this.$refs.ele_2[0].className.substring(0, num-1);
                        }
                        break;
                    case 3:
                        num = this.$refs.ele_3[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_3[0].className = this.$refs.ele_3[0].className.substring(0, num-1);
                        }
                        break;
                    case 4:
                        num = this.$refs.ele_4[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_4[0].className = this.$refs.ele_4[0].className.substring(0, num-1);
                        }
                        break;
                    case 5:
                        num = this.$refs.ele_5[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_5[0].className = this.$refs.ele_5[0].className.substring(0, num-1);
                        }
                        break;
                    case 6:
                        num = this.$refs.ele_6[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_6[0].className = this.$refs.ele_6[0].className.substring(0, num-1);
                        }
                        break;
                    case 7:
                        num = this.$refs.ele_7[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_7[0].className = this.$refs.ele_7[0].className.substring(0, num-1);
                        }
                        break;
                    case 8:
                        num = this.$refs.ele_8[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_8[0].className = this.$refs.ele_8[0].className.substring(0, num-1);
                        }
                        break;
                }
            }

            // 현재 선택된 값을 수정
            let ref_str = '';
            switch(n){
                case 1:
                    ref_str = this.$refs.ele_1;
                    break;
                case 2:
                    ref_str = this.$refs.ele_2;
                    break;
                case 3:
                    ref_str = this.$refs.ele_3;
                    break;
                case 4:
                    ref_str = this.$refs.ele_4;
                    break;
                case 5:
                    ref_str = this.$refs.ele_5;
                    break;
                case 6:
                    ref_str = this.$refs.ele_6;
                    break;
                case 7:
                    ref_str = this.$refs.ele_7;
                    break;
                case 8:
                    ref_str = this.$refs.ele_8;
                    break;
            }
            
            // 현재 변수 저장
            this.before_theory_ele_select_num = n;
            ref_str[0].className += " selected";
        },

        //논문 발표에서 고를 인자를 선택하는 함수
        theory_stamp_select : function(n) {
            //console.log(this.$refs);
            // 이미 선택된 것이 있다면 해제하고 새로 고른 것을 선택해야 함
            // 나의 색에 따라 나와야하는 stamp의 모양이 달라야함...!
            let num = 0;
            if( this.before_theory_stm_select_num > 0){
                switch(this.before_theory_stm_select_num){
                    case 1:
                        num = this.$refs.stm_1.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_1.className = this.$refs.stm_1.className.substring(0, num-1);
                        }
                        break;
                    case 2:
                        num = this.$refs.stm_2.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_2.className = this.$refs.stm_2.className.substring(0, num-1);
                        }
                        break;
                    case 3:
                        num = this.$refs.stm_3.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_3.className = this.$refs.stm_3.className.substring(0, num-1);
                        }
                        break;
                    case 4:
                        num = this.$refs.stm_4.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_4.className = this.$refs.stm_4.className.substring(0, num-1);
                        }
                        break;
                    case 5:
                        num = this.$refs.stm_5.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_5.className = this.$refs.stm_5.className.substring(0, num-1);
                        }
                        break;
                    case 6:
                        num = this.$refs.stm_6.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_6.className = this.$refs.stm_6.className.substring(0, num-1);
                        }
                        break;
                    case 7:
                        num = this.$refs.stm_7.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_7.className = this.$refs.stm_7.className.substring(0, num-1);
                        }
                        break;
                    case 8:
                        num = this.$refs.stm_8.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_8.className = this.$refs.stm_8.className.substring(0, num-1);
                        }
                        break;
                    case 9:
                        num = this.$refs.stm_9.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_9.className = this.$refs.stm_9.className.substring(0, num-1);
                        }
                        break;
                    case 10:
                        num = this.$refs.stm_10.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_10.className = this.$refs.stm_10.className.substring(0, num-1);
                        }
                        break;
                    case 11:
                        num = this.$refs.stm_11.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_11.className = this.$refs.stm_11.className.substring(0, num-1);
                        }
                        break;
                }
            }

            // 현재 선택된 값을 수정
            let ref_str = '';
            switch(n){
                case 1:
                    ref_str = this.$refs.stm_1;
                    break;
                case 2:
                    ref_str = this.$refs.stm_2;
                    break;
                case 3:
                    ref_str = this.$refs.stm_3;
                    break;
                case 4:
                    ref_str = this.$refs.stm_4;
                    break;
                case 5:
                    ref_str = this.$refs.stm_5;
                    break;
                case 6:
                    ref_str = this.$refs.stm_6;
                    break;
                case 7:
                    ref_str = this.$refs.stm_7;
                    break;
                case 8:
                    ref_str = this.$refs.stm_8;
                    break;
                case 9:
                    ref_str = this.$refs.stm_9;
                    break;
                case 10:
                    ref_str = this.$refs.stm_10;
                    break;
                case 11:
                    ref_str = this.$refs.stm_11;
                    break;
            }
            
            // 현재 변수 저장
            this.before_theory_stm_select_num = n;
            ref_str.className += " selected";
        },

        // 논문 반박/발표 모달에서 발표/반박할 재료를 고르는 함수
        theory_presentation : function(n) {
            //console.log(this.theory_data);
            this.now_select_ingre = n;
            this.ingre_presentation_modal_onoff = true;
        },

        // 발표하기 버튼으로 실행될 함수
        theory_confirm : function(){
            let num = 0;
            //발표 
            if( this.board_6_selected == true ){
                // 넘겨야 하는 정보
                // 재료번호(ingre), 원소번호(ele), 스탬프 번호(stm), 유저키, 유저색 ,큐브순서, 보드순서
                let send_data = {
                    ele : this.before_theory_ele_select_num,
                    ingre : this.now_select_ingre,
                    user_key : this.my_key,
                    user_color : this.my_color,
                    stamp : this.before_theory_stm_select_num,
                    cube_order : this.board_cube_order,
                    board_order : this.board_order,
                    room_name : this.room_name,
                };
                
                socket.emit("presentation_theory", send_data);
                
                // 선택된 재료의 selected 초기화
                switch(this.before_theory_ele_select_num){
                    case 1:
                        num = this.$refs.ele_1[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_1[0].className = this.$refs.ele_1[0].className.substring(0, num-1);
                        }
                        break;
                    case 2:
                        num = this.$refs.ele_2[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_2[0].className = this.$refs.ele_2[0].className.substring(0, num-1);
                        }
                        break;
                    case 3:
                        num = this.$refs.ele_3[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_3[0].className = this.$refs.ele_3[0].className.substring(0, num-1);
                        }
                        break;
                    case 4:
                        num = this.$refs.ele_4[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_4[0].className = this.$refs.ele_4[0].className.substring(0, num-1);
                        }
                        break;
                    case 5:
                        num = this.$refs.ele_5[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_5[0].className = this.$refs.ele_5[0].className.substring(0, num-1);
                        }
                        break;
                    case 6:
                        num = this.$refs.ele_6[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_6[0].className = this.$refs.ele_6[0].className.substring(0, num-1);
                        }
                        break;
                    case 7:
                        num = this.$refs.ele_7[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_7[0].className = this.$refs.ele_7[0].className.substring(0, num-1);
                        }
                        break;
                    case 8:
                        num = this.$refs.ele_8[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_8[0].className = this.$refs.ele_8[0].className.substring(0, num-1);
                        }
                        break;
                }
                switch(this.before_theory_stm_select_num){
                    case 1:
                        num = this.$refs.stm_1.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_1.className = this.$refs.stm_1.className.substring(0, num-1);
                        }
                        break;
                    case 2:
                        num = this.$refs.stm_2.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_2.className = this.$refs.stm_2.className.substring(0, num-1);
                        }
                        break;
                    case 3:
                        num = this.$refs.stm_3.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_3.className = this.$refs.stm_3.className.substring(0, num-1);
                        }
                        break;
                    case 4:
                        num = this.$refs.stm_4.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_4.className = this.$refs.stm_4.className.substring(0, num-1);
                        }
                        break;
                    case 5:
                        num = this.$refs.stm_5.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_5.className = this.$refs.stm_5.className.substring(0, num-1);
                        }
                        break;
                    case 6:
                        num = this.$refs.stm_6.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_6.className = this.$refs.stm_6.className.substring(0, num-1);
                        }
                        break;
                    case 7:
                        num = this.$refs.stm_7.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_7.className = this.$refs.stm_7.className.substring(0, num-1);
                        }
                        break;
                    case 8:
                        num = this.$refs.stm_8.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_8.className = this.$refs.stm_8.className.substring(0, num-1);
                        }
                        break;
                    case 9:
                        num = this.$refs.stm_9.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_9.className = this.$refs.stm_9.className.substring(0, num-1);
                        }
                        break;
                    case 10:
                        num = this.$refs.stm_10.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_10.className = this.$refs.stm_10.className.substring(0, num-1);
                        }
                        break;
                    case 11:
                        num = this.$refs.stm_11.className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.stm_11.className = this.$refs.stm_11.className.substring(0, num-1);
                        }
                        break;
                }
                // 선택된 재료 초기화
                this.before_theory_ele_select_num = -1;
                this.before_theory_stm_select_num = -1;
            }
            //반박
            else if ( this.board_5_selected == true ){
                // 넘겨야 하는 정보
                // 재료번호(ingre), 틀리다고 주장할 원소 색(ori), 주장을 증명할 재료2개(arr), 유저키(user_key), 큐브순서, 보드순서
                let send_data = {
                    ingre : this.now_select_ingre,
                    ori : this.before_refute_ori_select_num,
                    arr : this.test_ingredient_list,
                    user_key : this.my_key,
                    cube_order : this.board_cube_order,
                    board_order : this.board_order,
                    room_name : this.room_name,
                };

                //console.log(send_data);
                socket.emit("refuting_theory_data", send_data);
                
                // 변수 초기화 영역
                switch(this.before_refute_ori_select_num){
                    case 1:
                        num = this.$refs.ele_core_1[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_core_1[0].className = this.$refs.ele_core_1[0].className.substring(0, num-1);
                        }
                        break;
                    case 2:
                        num = this.$refs.ele_core_2[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_core_2[0].className = this.$refs.ele_core_2[0].className.substring(0, num-1);
                        }
                        break;
                    case 3:
                        num = this.$refs.ele_core_3[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_core_3[0].className = this.$refs.ele_core_3[0].className.substring(0, num-1);
                        }
                        break;
                }

                // 초기화전 test_boarder 초기화
                for(let i = 0; i < 2; i++ ){
                    switch(this.test_ingredient_list[i]) {
                        case 'card_1' :
                            num = this.$refs.theory_ingre_[0].className.indexOf('test_border');
                            if( num > 0 ) {
                                this.$refs.theory_ingre_1[0].className = this.$refs.theory_ingre_1[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_2' :
                            num = this.$refs.theory_ingre_2[0].className.indexOf('test_border');
                            if( num > 0 ){
                                this.$refs.theory_ingre_2[0].className = this.$refs.theory_ingre_2[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_3' :
                            num = this.$refs.theory_ingre_3[0].className.indexOf('test_border');
                            if( num > 0 ) {
                                this.$refs.theory_ingre_3[0].className = this.$refs.theory_ingre_3[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_4' :
                            num = this.$refs.theory_ingre_4[0].className.indexOf('test_border');
                            if (num > 0 ) {
                                this.$refs.theory_ingre_4[0].className = this.$refs.theory_ingre_4[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_5' :
                            num = this.$refs.theory_ingre_5[0].className.indexOf('test_border');
                            if( num > 0 ){
                                this.$refs.theory_ingre_5[0].className = this.$refs.theory_ingre_5[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_6' :
                            num = this.$refs.theory_ingre_6[0].className.indexOf('test_border');
                            if( num > 0 ) {
                                this.$refs.theory_ingre_6[0].className = this.$refs.theory_ingre_6[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_7' :
                            num = this.$refs.theory_ingre_7[0].className.indexOf('test_border');
                            if( num > 0 ) {
                                this.$refs.theory_ingre_7[0].className = this.$refs.theory_ingre_7[0].className.substring(0, num-1);
                            }
                            break;
                        case 'card_8' :
                            num = this.$refs.theory_ingre_8[0].className.indexOf('test_border');
                            if ( num > 0 ) {
                                this.$refs.theory_ingre_8[0].className = this.$refs.theory_ingre_8[0].className.substring(0, num-1);
                            }
                            break;
                    }
                }

                // 선택한 재료카드 초기화
                this.test_ingredient_list = [];
                // 변수 초기화
                this.before_refute_ori_select_num = 0;

                // 보드의 행동이 일시적으로 끝남을 알림
                this.board_start_checker = false;
            }
            
            // 원소 발표/반박 모달 끄기!
            this.ingre_presentation_modal_onoff = false;
        },

        //논문 반박에서 어떤 원소를 반박할 건지 선택하는 함수
        theory_origin_select : function(n){
            // 이미 선택된 다른 것이 있다면 해제하고 새로 고른 것을 선택해야함
            let num = 0;
            if( this.before_refute_ori_select_num > 0){
                switch(this.before_refute_ori_select_num){
                    case 1:
                        num = this.$refs.ele_core_1[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_core_1[0].className = this.$refs.ele_core_1[0].className.substring(0, num-1);
                        }
                        break;
                    case 2:
                        num = this.$refs.ele_core_2[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_core_2[0].className = this.$refs.ele_core_2[0].className.substring(0, num-1);
                        }
                        break;
                    case 3:
                        num = this.$refs.ele_core_3[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ele_core_3[0].className = this.$refs.ele_core_3[0].className.substring(0, num-1);
                        }
                        break;
                }
            }

            // 현재 선택된 값을 수정
            let ref_str = '';
            switch(n){
                case 1:
                    ref_str = this.$refs.ele_core_1;
                    break;
                case 2:
                    ref_str = this.$refs.ele_core_2;
                    break;
                case 3:
                    ref_str = this.$refs.ele_core_3;
                    break;
            }
            
            // 현재 변수 저장
            this.before_refute_ori_select_num = n;
            ref_str[0].className += " selected";
        },

        // 논문 반박에서 반박한 원소를 증명할 재료를 고르는 함수
        theory_ingre_select : function(n){
            // 이전의 선택 제거 인데 배열이 2개
            // 이미 2가지가 선택되어 있으면 가장 먼저 들어온 1개를 제거하고 마지막에 들어온 한개를 진행
            if( this.test_ingredient_list.length == 2){
                // 먼저 들어온 1개를 제거
                let before_name = this.test_ingredient_list.shift();
                let num = 0;
                switch(before_name) {
                    case 1 :
                        num = this.$refs.theory_ingre_1[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.theory_ingre_1[0].className = this.$refs.theory_ingre_1[0].className.substring(0, num-1);
                        }
                        break;
                    case 2 :
                        num = this.$refs.theory_ingre_2[0].className.indexOf('test_border');
                        if( num > 0 ){
                            this.$refs.theory_ingre_2[0].className = this.$refs.theory_ingre_2[0].className.substring(0, num-1);
                        }
                        break;
                    case 3 :
                        num = this.$refs.theory_ingre_3[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.theory_ingre_3[0].className = this.$refs.theory_ingre_3[0].className.substring(0, num-1);
                        }
                        break;
                    case 4 :
                        num = this.$refs.theory_ingre_4[0].className.indexOf('test_border');
                        if (num > 0 ) {
                            this.$refs.theory_ingre_4[0].className = this.$refs.theory_ingre_4[0].className.substring(0, num-1);
                        }
                        break;
                    case 5 :
                        num = this.$refs.theory_ingre_5[0].className.indexOf('test_border');
                        if( num > 0 ){
                            this.$refs.theory_ingre_5[0].className = this.$refs.theory_ingre_5[0].className.substring(0, num-1);
                        }
                        break;
                    case 6 :
                        num = this.$refs.theory_ingre_6[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.theory_ingre_6[0].className = this.$refs.theory_ingre_6[0].className.substring(0, num-1);
                        }
                        break;
                    case 7 :
                        num = this.$refs.theory_ingre_7[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.theory_ingre_7[0].className = this.$refs.theory_ingre_7[0].className.substring(0, num-1);
                        }
                        break;
                    case 8 :
                        num = this.$refs.theory_ingre_8[0].className.indexOf('test_border');
                        if ( num > 0 ) {
                            this.$refs.theory_ingre_8[0].className = this.$refs.theory_ingre_8[0].className.substring(0, num-1);
                        }
                        break;                                  
                }
            }
            // 변수에 추가
            this.test_ingredient_list.push(n);
            let ref_str = '';
            switch(n){
                case 1:
                    ref_str = this.$refs.theory_ingre_1;
                    break;
                case 2:
                    ref_str = this.$refs.theory_ingre_2;
                    break;
                case 3:
                    ref_str = this.$refs.theory_ingre_3;
                    break;
                case 4:
                    ref_str = this.$refs.theory_ingre_4;
                    break;
                case 5:
                    ref_str = this.$refs.theory_ingre_5;
                    break;
                case 6:
                    ref_str = this.$refs.theory_ingre_6;
                    break;
                case 7:
                    ref_str = this.$refs.theory_ingre_7;
                    break;
                case 8:
                    ref_str = this.$refs.theory_ingre_8;
                    break;
            }
            ref_str[0].className += " test_border";
        },

        check_refute_info : function() {
            // 반박 성공확인창 끄기
            this.success_refute_modal_onoff = false;
            // 반박이 성공이었다면 서버에 체크 데이터 전송
            if (this.reciving_data != null || this.reciving_data != '' ){
                if (this.reciving_data.success == true){
                    socket.emit("check_refute_info", this.reciving_data);
                }
            }
            
            // 변수 초기화
            this.reciving_data = '';

            // round가 끝났다면 modal 초기화
            if (this.round_end_checker == true){
                this.round_end_to_modal_event();
            }
            // board_start 상태라면 보드 다시 그리기
            if (this.board_start_checker == true){
                this.board_start_event(this.now_board_num);
                console.log(this.now_board_num);
            }
        },

        click_exhibition_potion : function(potion_num) {          
            
            if( this.my_turn != true ) {
                alert("내 차례에 전시할 수 있습니다!\n차례를 기다려주세요!");
                return;
            }
            
            // 발표할 원소번호를 저장하고
            this.exhibition_potion_num = potion_num;
            // 발표할 모달을 켜기!
            this.exhibition_check_modal_onoff = true;

            //console.log(this.my_data);
        },

        test_ingredient_arr_getter : function(data) {
            this.test_ingredient_list = data;
        },

        // 물약 전시회에서 시연할 재료를 고르고 결정버튼을 눌렀을 떄 실행될 함수
        click_exhibit_ingre : function() {
            if( this.test_ingredient_list.length != 2 ){
                alert("재료카드 2장을 선택해주세요!");
                return;
            }
            let exhibit_potion = '';
            switch(this.exhibition_potion_num){
                case 1:
                  exhibit_potion = 'red_1';
                  break;
                case 2:
                  exhibit_potion = 'red_0';
                  break;
                case 3:
                  exhibit_potion = 'green_1';
                  break;
                case 4:
                  exhibit_potion = 'green_0';
                  break;
                case 5:
                  exhibit_potion = 'blue_1';
                  break;
                case 6:
                  exhibit_potion = 'blue_0';
                  break;
              }

            let send_data = {
                user_key : this.my_key,
                user_color : this.my_color,
                room_name  : this.room_name,
                card_list : this.test_ingredient_list,
                exhibit_potion : exhibit_potion,
                board_order : this.board_order,
                cube_order : this.board_cube_order,
            }
            
            // 발표!
            socket.emit("exhibit_ingre", send_data);

            // 보드의 행동이 일시적으로 끝남을 알림
            this.board_start_checker = false;

            // 선택한 재료카드 초기화
            this.test_ingredient_list = [];
            // 발표할 원소번호 초기화
            this.exhibition_potion_num = -1;
            // 모달 닫기
            this.exhibition_check_modal_onoff = false;
        },

        pass_board : function() {

            //보드 차례 넘기기
              /*
                user_key    : 유저 번호
                room_name   : 방 이름
                board_num   : 현재 보드 번호
                board_order : 현재 보드 순서
                cube_order : 현재 큐브 순서
            */
            let send_data = {
                user_key : this.my_key,
                room_name : this.room_name,
                board_num : this.now_board_num,
                board_order : this.board_order,
                cube_order : this.board_cube_order,
            }
            socket.emit("board_passing", send_data);
        },

        // 게임 종료 확인
        game_end_confirm : function() {
            // 종료 확인을 서버에 알림
            let send_data = {
                room_name : this.room_name,
            };
            socket.emit("game_end_confirm", send_data);
        },

        forced_termination_confirm : function() {
            // 현재 내가 게임 마스터면 
            if (this.my_data.is_master == 'true' || 
                this.my_data.is_master == true ){
                    this.$router.push({
                        name: 'game_lobby',
                        params: {
                            room_name: this.room_name,
                            room_pw: '',
                            count: this.$route.params.count,
                            name: this.my_name,
                            is_master : true,
                            re_page : this.restart_counter,
                            no_enter : true,
                        }
                    });
                }
            else{
                this.$router.push({
                    name: 'game_lobby',
                    params: {
                        room_name: this.room_name,
                        room_pw: '',
                        name: this.my_name,
                        is_master : false,
                        restart_counter : this.restart_counter,
                        no_enter : true,
                    }
                });

                }
            
        },

    },

    
}