import { io } from 'socket.io-client';
import { default as Create_lobby } from '@/components/create_lobby.vue';

// 다른 곳에서도 쓸 수 있게 전역 선언
var socket;

export default {
    name : 'room_list',
    components : {
        Create_lobby : Create_lobby,
    },
    created() {
        
    },
    mounted() {
        if( this.$route.params.re_page == "false" || this.$route.query.re_page == "false"){
            //console.log("start");
            //console.log(this.$route.params.ip);
            socket = io('http://' + this.$route.params.ip + ":3000");
            //console.log(this.$route.params);
        }
        else if ( this.$route.params.re_page == "true"){
            //console.log(this.$route.params);
        }

        this.enter_server();

        socket.on("enter_room_success", (data)=> {
            //console.log(data);
            this.room_list = data;
        });

        socket.on("wrong_pw", () => {
            alert("비밀번호가 틀렸습니다. \n다시 시도해주세요");
        });

        socket.on("ok_pw", () => {
            let count;
            for(let i = 0 ; i < this.room_list.length; i++ ){
                if( this.room_list[i].name == this.want_room ){
                    count = this.room_list[i].max_count;
                    break;
                }
            }
            this.$router.push({
                name: 'game_lobby',
                params: {
                    room_name: this.want_room,
                    room_pw: this.room_pw,
                    count : count,
                    name: this.name,
                    is_master : false,
                    restart_counter : this.restart_counter,
                }
            });
        });
    },
    data () {
        return {
            name : this.$route.params.name,
            room_list : '',
            room_pw : '',
            want_room : '',
            listing : true,
            loading : true,
            check_pw_modal : false,
            restart_counter : this.$route.params.restart_counter,
        }
    },
    methods : {
        enter_server : async function() {
            setTimeout(()=>{
                if( socket.id == undefined || socket.id == '' ){
                    // 접속 실패로 이전화면으로 돌림
                    alert("접속에 실패하였습니다.\n 주소를 다시 확인해주세요");
                    this.$router.push({
                        name: 'first_page',
                        params : {
                            refresh : true,
                        }
                    });
                }
                this.loading = false;
                socket.emit("enter_room_gate", this.name);
            }, 3000);
            
        },

        create_room : function() {
            this.listing = false;
        },
        back_page : function() {
            this.listing = true;
        },

        pw_modal_on : function(room_name) {
            // console.log(room_name);
            // console.log(this.room_pw);
            this.want_room = room_name;
            // 방이 가득찼는지 room_list의 count와 max_count를 검사
            let left_sit = 0;
            for(let i = 0 ; i < this.room_list.length; i++ ){
                if( this.room_list[i].name == room_name ){
                    left_sit = this.room_list[i].max_count - this.room_list[i].count;
                    break;
                }
            }
            //console.log(left_sit);
            if( left_sit > 0 ){
                // pw입력창 띄우기
                this.check_pw_modal = !this.check_pw_modal;
            }
            else {
                alert("해당 방은 인원이 가득 찼습니다!.\n다른 방을 찾아주세요.");
            }
        },

        enter_room : function() {
            // console.log(this.room_pw);
            // console.log(this.want_room);
            let send_data = {
                room_pw : this.room_pw,
                room_name : this.want_room,
            };
            //console.log(send_data);
            this.check_pw_modal = !this.check_pw_modal;
            socket.emit("check_pw", send_data);
        }
    },
}

export { socket };