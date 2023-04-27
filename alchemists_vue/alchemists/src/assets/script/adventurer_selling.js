export default {
    name : "adventurer_selling",
    props: {
        // 라운드 순서
        round_cont : {
            type : Number,
        },
        // 포션 판매하는 사람 표시
        sell_potion_turn : {
            type : Boolean,
        },
        adventurer_card_ori_data : {
            type : Object,
        },
        // 이번 라운드에서 사용할 용병 순서
        random_adv_list : {
            type : Object,
        },
        // 타인까지 포함해 저장될 할인 제시가
        discount_coin_list : {
            type : Object,
        },
        
        // 할인 제시 스텝이 끝났는가
        is_end_discount : {
            type : Boolean,
        },
        // 내 할인 제시가 끝났는가
        im_end_discount : {
            type : Boolean,
        },
        // 내가 선택한 물약판매가
        my_selling_price : {
            type : Number,
        },
        // 판매 차례
        selling_turn : {
            type : Number,
        },
        // 할인 포인트 제시 여부
        get_discount_coin : {
            type : Boolean,
        },
        // 판매할 물약 종류 설정
        what_kind_sell_potion : {
            type : String,
        },

    },
    data() {
        return { 
            my_data : '',
            my_key : '',

            // 모달 onoff 변수
            make_potion_to_sell_onoff : false,
            sell_potion_to_adv_modal_onoff : false,

            // merchant 호의카드 사용 여부
            merchant_use : false,

            // 용병에게 판매할 물약을 만들 때 선택할 재료의 test_boarder 를 저장해둘 변수
            ingre_test_boarder : {
                1 : false,
                2 : false,
                3 : false,
                4 : false,
                5 : false,
                6 : false,
                7 : false,
                8 : false,
            },
            // 용병에게 판매 가능한 물약리스트
            adventurer_card_data : '',

            // 선택한 할인 카드 저장 변수
            before_adv_discount_select_num : -1,
            // 최종 판매 금액
            final_selling_price : -1,
            // 선택한 카드 리스트
            test_ingredient_list : [],
            adv_icon_data : {
                0 : require('@/assets/img/adventurer/ad_0.png'),
                1 : require('@/assets/img/adventurer/ad_1.png'),
                2 : require('@/assets/img/adventurer/ad_2.png'),
                3 : require('@/assets/img/adventurer/ad_3.png'),
            },

        }
    },
    methods : {
        set_my_data : function(data) {
            this.my_data = '';
            this.my_data = data;
        },
        set_my_key : function(data) {
            this.my_key = '';
            this.my_key = data;
        },

        // 모달 onoff 함수들
        sell_potion_to_adv_modal_open : function() {
            this.sell_potion_to_adv_modal_onoff = true;
        },
        sell_potion_to_adv_modal_close : function() {
            this.sell_potion_to_adv_modal_onoff = false;
        },
        make_potion_modal_open : function(merchant_use) {
            this.make_potion_to_sell_onoff = true;
            this.merchant_use = merchant_use;
        },
        make_potion_modal_close : function() {
            this.make_potion_to_sell_onoff = false;
        },

        // 용병 판매 변수 변경 함수
        adventurer_card_data_change : function(data) {
            this.adventurer_card_data = data;
        },

        reasoning_table_open : function() {
            // console.log("click_reasoning_table");
            this.$emit('open_reasoning_table_from_adv');
        },

        // 실험 재료 2개 고르기
        click_test_ingredient_check : function(name){
            
            // 같은 카드가 이미 있는지 검사
            let same_name = false;
            // 이미 2가지가 선택되어 있으면 가장 먼저 들어온 1개를 제거하고 마지막에 들어온 한개를 진행
            if( this.test_ingredient_list.length == 2){
                // 공용 변수 - test_border를 제거할 변수 이름
                let before_name = '';
                
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
                        this.ingre_test_boarder[1] = false;
                        break;
                    case 'card_2' :
                        this.ingre_test_boarder[2] = false;
                        break;
                    case 'card_3' :
                        this.ingre_test_boarder[3] = false;
                        break;
                    case 'card_4' :
                        this.ingre_test_boarder[4] = false;
                        break;
                    case 'card_5' :
                        this.ingre_test_boarder[5] = false;
                        break;
                    case 'card_6' :
                        this.ingre_test_boarder[6] = false;
                        break;
                    case 'card_7' :
                        this.ingre_test_boarder[7] = false;
                        break;
                    case 'card_8' :
                        this.ingre_test_boarder[8] = false;
                        break;
                }
                
            }
            
            if( same_name == false ){
                this.test_ingredient_list.push(name);

                switch(name) {
                    case 'card_1' :
                        this.ingre_test_boarder[1] = true;
                        break;
                    case 'card_2' :
                        this.ingre_test_boarder[2] = true;
                        break;
                    case 'card_3' :
                        this.ingre_test_boarder[3] = true;
                        break;
                    case 'card_4' :
                        this.ingre_test_boarder[4] = true;
                        break;
                    case 'card_5' :
                        this.ingre_test_boarder[5] = true;
                        break;
                    case 'card_6' :
                        this.ingre_test_boarder[6] = true;
                        break;
                    case 'card_7' :
                        this.ingre_test_boarder[7] = true;
                        break;
                    case 'card_8' :
                        this.ingre_test_boarder[8] = true;
                        break;
                }
            }
        
        },

        // 용병에게 물약 판매 전 코인 제시
        select_adv_dis_coin : function(n) { 
            // 선택된 다른 코인은 표기 해제
            let num = 0;
            if( this.before_adv_discount_select_num + 1  > 0 ) {
                switch(this.before_adv_discount_select_num + 1 ){
                    case 1:
                        num = this.$refs.ad_0_ico[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ad_0_ico[0].className = this.$refs.ad_0_ico[0].className.substring(0, num-1);
                        }
                        break;
                    case 2:
                        num = this.$refs.ad_1_ico[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ad_1_ico[0].className = this.$refs.ad_1_ico[0].className.substring(0, num-1);
                        }
                        break;
                    case 3:
                        num = this.$refs.ad_2_ico[0].className.indexOf('selected');
                        if( num > 0 ) {
                            this.$refs.ad_2_ico[0].className = this.$refs.ad_2_ico[0].className.substring(0, num-1);
                        }
                        break;
                    case 4:
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
                case 1:
                    ref_str = this.$refs.ad_0_ico;
                    break;
                case 2:
                    ref_str = this.$refs.ad_1_ico;
                    break;
                case 3:
                    ref_str = this.$refs.ad_2_ico;
                    break;
                case 4:
                    ref_str = this.$refs.ad_3_ico;
                    break;
            }
            //console.log(ref_str + "  " + n);
            ref_str[0].className += " selected";
            this.before_adv_discount_select_num = n-1;

            // 변수를 socket단에 전송
            this.$emit('hold_before_adv_discount_num', this.before_adv_discount_select_num);
        },

        // 용병에게 할인 카드 제시 결정 
        adv_dis_confirm : function() {
            console.log(this.my_data);
            console.log(this.im_end_discount);
            console.log(this.my_key);
            console.log(this.is_end_discount);
            this.before_adv_discount_select_num = -1;
            this.$emit('adv_dis_confirm');
        },

        // 용병에게 팔 물약의 값 선택하기
        sell_potion_set_price : function(n){
            let num = -1;

            for( let i = 0 ; i < this.discount_coin_list.length; i++ ){
                if( this.discount_coin_list[i].user_key == this.my_key ){
                    num = n - this.discount_coin_list[i].dis_coin_num;
                    this.final_selling_price = num;
                    break;
                }
            }
            
            if( num > 0 ){
                this.$emit('sell_potion_set_price', n);
            }
            // num이 0보다 작으면 선택 불가 알람 진행
            else {
                alert("제시한 할인 카드로 인해 선택이 불가능합니다.\n더 높은 금액을 설정해주세요");
                return;
            }
            
        },

        // 용병에게 팔 금액 홀드하기
        decision_sell_potion_price : function(){
            this.final_selling_price = '';
            //console.log(this.my_data);
            // 값 홀드 해달라는 함수 전송
            this.$emit('hold_sell_potion_price');
        },

        // 용병에게 팔 물약 고르기
        select_sell_potion_kind : function(kind) {
            this.$emit('kind_sell_potion_check', kind);
        },

        // 물약 조제하기 :: 
        make_potion_preparation : function() {
            console.log(this.my_data);
            if( this.test_ingredient_list.length != 2 ){
                // 2장을 고르지 않으면 데이터가 넘어가면 안됨
                alert("재료 2장을 고르고 조제를 시작해주세요!");
                return;
            }
            // 조제할 물약이 선택되어 있지 않으면 조제 불가
            if( this.what_kind_sell_potion == '' ){
                alert("조제할 물약을 선택해 주세요!");
                return;
            }

            this.$emit('make_potion_check', this.test_ingredient_list);

            // 조제하기 끄기
            this.make_potion_to_sell_onoff = false;
            // test_boarder 클래스 제거
            let num = 0;
            for( let i = 0; i < 2; i++ ){
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
        },
    },
    
}