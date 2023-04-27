import { socket } from '@/components/room_list.vue'

export default {
    name : 'end_page',
    created() {
        this.my_key = socket.id;
    },
    mounted () {
        // 다시하기 확인
        socket.on("restart_check", (data) => {
            this.restart_member_count = data;
        });
        // 다시하기 취소!
        socket.on("restart_cancel_check", (data) => {
            this.restart_member_count = data;
            this.cancel_restart_val = false;
        });

        socket.on("cant_restart", () => {
            let send_data = {
                room_name : this.room_name,
            };
            socket.emit("lobby_move", send_data);
            this.$router.push({
                name : 'room_list',
                params : {
                    re_page : true,
                    name : this.my_name,
                    restart_counter : this.restart_counter,
                }
            });
        });

        socket.on("restart", (data) => {
            this.room_pw = data.room_pw;
            this.count = data.count;
            let is_master = false;
            if( data.master_key == this.my_key ){
                is_master = true;
            }
            let send_data = {
                room_name : this.room_name,
            };
            socket.emit("lobby_move", send_data);
            this.$router.push({
                name: 'game_lobby',
                params : {
                    name      : this.my_name,
                    room_name : this.room_name,
                    room_pw   : this.room_pw,
                    count     : this.count,
                    is_master : is_master,
                    is_restart : true,
                    restart_counter : this.restart_counter,
                    // ip port 정보도 보내주는게 원칙인데 서버에 방 형식으로 구현방식을 변경해서 진행해야할듯 일단 ㄱ
                }
            });
        });

    },
    data () {
        return {
            game_result : JSON.parse(this.$route.params.game_result),
            my_key : '',
            restart_member_count : 0,
            cancel_restart_val : false,
            my_name : this.$route.params.name,
            room_pw : '',
            count : '',
            room_name : this.$route.params.room_name,
            restart_counter : this.$route.params.restart_counter,
        }
    },
    methods : {
        want_restart : function() {
            // 다시 하고 싶다는 의사 전달 
            // 추후에 인원이 다 안차더라도 게임 로비로 이동됨
            // 로비로 이동될 떄 게임방 주인은 순위 가장 높은 사람으로 선정
            let send_data = {
                user_key : this.my_key,
                room_name : this.room_name,
            };
            this.cancel_restart_val = true;
            socket.emit("restart_checking", send_data);
        },

        close_game : function() {
            // 서버에서 나가지고 room_list로 이동됨
            let send_data = {
                room_name : this.room_name,
                user_key : this.my_key,
            };
            socket.emit("out_game", send_data);
            // 모든 컴포넌트 새로그리기?
            this.$router.push({
                name : 'room_list',
                params : {
                    re_page : true,
                    name : this.$route.params.name,
                    restart_counter : this.$route.params.restart_counter,
                }
            });
        },

        cancel_restart : function() {
            let send_data = {
                user_key : this.my_key,
                room_name : this.room_name,
            };
            socket.emit("restart_cancel", send_data);
        },
    }
}