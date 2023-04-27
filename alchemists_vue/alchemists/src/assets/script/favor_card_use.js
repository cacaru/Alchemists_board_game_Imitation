export default {
    name : "favor_card_use",
    props: {
        my_key : {
            type : String,
            require : true,
        },
        // 재료를 버리기 위한 변수
        my_data : {
            type : Object,
            require : true,
        },
        temp_used_favor_card_list : {
            type : Object,
        }
    },
    data() {
        return {
            // 모달 onoff
            favor_card_use_check_modal : false,
            select_board_num : -1,
            ingre_list : [],
            
            //사용될 호의카드
            card_kind : '',

            // 약초학자 카드 사용 갯수
            herbalist_card_num : 0,
            // 버릴 카드를 선택할 갯수 
            dumping_ingre_num : 0,

            // 호의카드 사용 가능 여부 확인
            can_use : false,
        }
    },

    methods : {
        favor_modal_open : function(card_kind) {
            this.favor_card_use_check_modal = true;
            this.card_kind = card_kind;
            this.what_kind_favor_card_used();
        },
        favor_modal_close : function() {
            this.favor_card_use_check_modal = false;
            this.can_use = false;
        },
        
        // 호의카드 종류에 따라 실행시킬 함수
        what_kind_favor_card_used : function() {
            //console.log(this.card_kind);
            switch(this.card_kind) {
                case 'assistent':
                    if( this.my_data.user_ingame_data.favor_card.assistent > 0 ){
                        this.can_use = true;
                    }
                    break;
                case 'bar_owner':
                    if( this.my_data.user_ingame_data.favor_card.bar_owner > 0 ){
                        this.can_use = true;
                    }
                    break;
                case 'big_man':
                    if( this.my_data.user_ingame_data.favor_card.big_man > 0 ){
                        this.can_use = true;
                    }
                    break;
                case 'caretaker':
                    if( this.my_data.user_ingame_data.favor_card.caretaker > 0 ){
                        this.can_use = true;
                    }
                    break;
                case 'herbalist':
                    if( this.my_data.user_ingame_data.favor_card.herbalist > 0 ){
                        this.can_use = true;
                    }
                    /*
                    for( let i = 0 ; i < 4 ; i++ ){
                        if( this.temp_used_favor_card_list[5][i].user_key == this.my_key ){
                            this.can_use = true;
                            this.herbalist_card_num = this.temp_used_favor_card_list[5][i].count;
                            this.dumping_ingre_num = this.herbalist_card_num * 2;
                            break;
                        }
                    }
                    */
                    break;
                case 'merchant':
                    if( this.my_data.user_ingame_data.favor_card.merchant > 0 ){
                        this.can_use = true;
                    }
                    break;
                case 'shopkeeper':
                    if( this.my_data.user_ingame_data.favor_card.shopkeeper > 0 ){
                        this.can_use = true;
                    }
                    break;
                case 'wise_man':
                    if( this.my_data.user_ingame_data.favor_card.wise_man > 0 ){
                        this.can_use = true;
                    }
                    break;
            }
            
        },

        // 호의 카드 사용
        use_favor_card : function( card_name ) {
            let send_data = '';
            if( card_name == 'big_man'){
                if( this.select_board_num < 0 ){
                    alert("사용하시려면 보드를 선택해주세요!");
                    return;
                }
                send_data = {
                    card_name,
                    select_board_num : this.select_board_num,
                }
            }
            else if ( card_name == 'herbalist' ){
                if( this.ingre_list.length != this.dumping_ingre_num ){
                    alert("버려야할 재료의 갯수를 채워서 골라주세요! ");
                    return;
                }
                send_data = {
                    card_name : card_name,
                    ingre_list : this.ingre_list,
                }
            }
            else {
                send_data = {
                    card_name,
                }
            }

            this.$emit('favor_card_use_check', send_data);
        },

        // 힘센 친구 보드 선택
        click_select_board : function(board_num) {
            let num = -1;
            // 같은 보드가 선택되면 취소 !
            if( board_num == this.select_board_num ) {
                this.select_board_num = -1;

                switch(board_num) {
                    case 1:
                        num = this.$refs.board_1[0].className.indexOf('select_border');
                        if( num > 0 ) {
                            this.$refs.board_1[0].className = this.$refs.board_1[0].className.substring(0, num-1);
                        }
                        break;
                    case 2:
                        num = this.$refs.board_2[0].className.indexOf('select_border');
                        if( num > 0 ) {
                            this.$refs.board_2[0].className = this.$refs.board_2[0].className.substring(0, num-1);
                        }
                        break;
                    case 3:
                        num = this.$refs.board_3[0].className.indexOf('select_border');
                        if( num > 0 ) {
                            this.$refs.board_3[0].className = this.$refs.board_3[0].className.substring(0, num-1);
                        }
                        break;
                    case 4:
                        num = this.$refs.board_4[0].className.indexOf('select_border');
                        if( num > 0 ) {
                            this.$refs.board_4[0].className = this.$refs.board_4[0].className.substring(0, num-1);
                        }
                        break;
                    case 5:
                        num = this.$refs.board_5[0].className.indexOf('select_border');
                        if( num > 0 ) {
                            this.$refs.board_5[0].className = this.$refs.board_5[0].className.substring(0, num-1);
                        }
                        break;
                    case 6:
                        num = this.$refs.board_6[0].className.indexOf('select_border');
                        if( num > 0 ) {
                            this.$refs.board_6[0].className = this.$refs.board_6[0].className.substring(0, num-1);
                        }
                        break;
                    case 7:
                        num = this.$refs.board_7[0].className.indexOf('select_border');
                        if( num > 0 ) {
                            this.$refs.board_7[0].className = this.$refs.board_7[0].className.substring(0, num-1);
                        }
                        break;
                    case 8:
                        num = this.$refs.board_8[0].className.indexOf('select_border');
                        if( num > 0 ) {
                            this.$refs.board_8[0].className = this.$refs.board_8[0].className.substring(0, num-1);
                        }
                        break;
                }
            }
            else {
                // 같은 보드가 아니면 기존 번호의 보드 select_border를 제거하고 선택된 보드에 select_border를 추가
                if( this.select_board_num > 0 ){
                    // 기존에 선택된 것이 있으면 제거
                    switch(this.select_board_num) {
                        case 1:
                            num = this.$refs.board_1[0].className.indexOf('select_border');
                            if( num > 0 ) {
                                this.$refs.board_1[0].className = this.$refs.board_1[0].className.substring(0, num-1);
                            }
                            break;
                        case 2:
                            num = this.$refs.board_2[0].className.indexOf('select_border');
                            if( num > 0 ) {
                                this.$refs.board_2[0].className = this.$refs.board_2[0].className.substring(0, num-1);
                            }
                            break;
                        case 3:
                            num = this.$refs.board_3[0].className.indexOf('select_border');
                            if( num > 0 ) {
                                this.$refs.board_3[0].className = this.$refs.board_3[0].className.substring(0, num-1);
                            }
                            break;
                        case 4:
                            num = this.$refs.board_4[0].className.indexOf('select_border');
                            if( num > 0 ) {
                                this.$refs.board_4[0].className = this.$refs.board_4[0].className.substring(0, num-1);
                            }
                            break;
                        case 5:
                            num = this.$refs.board_5[0].className.indexOf('select_border');
                            if( num > 0 ) {
                                this.$refs.board_5[0].className = this.$refs.board_5[0].className.substring(0, num-1);
                            }
                            break;
                        case 6:
                            num = this.$refs.board_6[0].className.indexOf('select_border');
                            if( num > 0 ) {
                                this.$refs.board_6[0].className = this.$refs.board_6[0].className.substring(0, num-1);
                            }
                            break;
                        case 7:
                            num = this.$refs.board_7[0].className.indexOf('select_border');
                            if( num > 0 ) {
                                this.$refs.board_7[0].className = this.$refs.board_7[0].className.substring(0, num-1);
                            }
                            break;
                        case 8:
                            num = this.$refs.board_8[0].className.indexOf('select_border');
                            if( num > 0 ) {
                                this.$refs.board_8[0].className = this.$refs.board_8[0].className.substring(0, num-1);
                            }
                            break;
                    }
                }
                // select_border 추가
                let ref_str = '';
                switch(board_num) {
                    case 1:
                        ref_str = this.$refs.board_1;
                        break;
                    case 2:
                        ref_str = this.$refs.board_2;
                        break;
                    case 3:
                        ref_str = this.$refs.board_3;
                        break;
                    case 4:
                        ref_str = this.$refs.board_4;
                        break;
                    case 5:
                        ref_str = this.$refs.board_5;
                        break;
                    case 6:
                        ref_str = this.$refs.board_6;
                        break;
                    case 7:
                        ref_str = this.$refs.board_7;
                        break;
                    case 8:
                        ref_str = this.$refs.board_8;
                        break;
                }
                console.log(this.$refs);
                ref_str[0].className += ' select_border';
                this.select_board_num = board_num;
            }
        },

        // 약초학자 판매 재료 선택
        click_ingre_for_sell : function(name) {
            // 이미 2가지가 선택되어 있으면 가장 먼저 들어온 1개를 제거하고 마지막에 들어온 한개를 진행
            if( this.ingre_list.length == 2) {
                // 공용 변수 - test_border를 제거할 변수 이름
                let before_name = '';
                let num = 0;
                // 같은 카드가 이미 있는지 검사

                before_name = this.ingre_list.shift();

                switch(before_name) {
                    case 'card_1' :
                        num = this.$refs.ingre_1[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.ingre_1[0].className = this.$refs.ingre_1[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_2' :
                        num = this.$refs.ingre_2[0].className.indexOf('test_border');
                        if( num > 0 ){
                            this.$refs.ingre_2[0].className = this.$refs.ingre_2[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_3' :
                        num = this.$refs.ingre_3[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.ingre_3[0].className = this.$refs.ingre_3[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_4' :
                        num = this.$refs.ingre_4[0].className.indexOf('test_border');
                        if (num > 0 ) {
                            this.$refs.ingre_4[0].className = this.$refs.ingre_4[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_5' :
                        num = this.$refs.ingre_5[0].className.indexOf('test_border');
                        if( num > 0 ){
                            this.$refs.ingre_5[0].className = this.$refs.ingre_5[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_6' :
                        num = this.$refs.ingre_6[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.ingre_6[0].className = this.$refs.ingre_6[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_7' :
                        num = this.$refs.ingre_7[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.ingre_7[0].className = this.$refs.ingre_7[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_8' :
                        num = this.$refs.ingre_8[0].className.indexOf('test_border');
                        if ( num > 0 ) {
                            this.$refs.ingre_8[0].className = this.$refs.ingre_8[0].className.substring(0, num-1);
                        }
                        break;
                }
                
            }
            // 변수에 추가
            this.ingre_list.push(name);
            let ref_str = '';
            switch(name){
                case 'card_1':
                    ref_str = this.$refs.ingre_1;
                    break;
                case 'card_2':
                    ref_str = this.$refs.ingre_2;
                    break;
                case 'card_3':
                    ref_str = this.$refs.ingre_3;
                    break;
                case 'card_4':
                    ref_str = this.$refs.ingre_4;
                    break;
                case 'card_5':
                    ref_str = this.$refs.ingre_5;
                    break;
                case 'card_6':
                    ref_str = this.$refs.ingre_6;
                    break;
                case 'card_7':
                    ref_str = this.$refs.ingre_7;
                    break;
                case 'card_8':
                    ref_str = this.$refs.ingre_8;
                    break;
            }
            ref_str[0].className += " test_border";
            
        },

    }
}