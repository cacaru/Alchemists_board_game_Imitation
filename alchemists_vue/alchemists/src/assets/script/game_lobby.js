import { socket } from '@/components/room_list.vue'
import { default as Chat_area } from '@/components/chat_area.vue'

export default {
  name : 'game_lobby',
  components : {
    Chat_area : Chat_area,
  },
  async mounted() {
    //this.$route.params.room_address:this.$route.params.port
    // console.log(params.nick_name);
    //console.log("server in");
    //console.log(this.$route.params);
    let msg;
    let no_enter;
    if (this.$route.params.no_enter === 'true'){
      no_enter = true;
    }
    else{
      no_enter = false;
    }
    //마스터 인 입장만 count를 가지고 있으므로
    if ( this.$route.params.is_master === 'true' ) {
      msg = {
        user_name : this.$route.params.name,
        room_name : this.$route.params.room_name,
        room_pw   : this.$route.params.room_pw,
        count     : this.$route.params.count,
        is_master : this.$route.params.is_master,
        is_ready  : false,
        msg       : this.$route.params.name + " 가 방에 참가하였습니다.",
        no_enter  : no_enter
      };
      //console.log("master");
    } else {
      msg = {
        user_name : this.$route.params.name,
        room_name : this.$route.params.room_name,
        is_master : this.$route.params.is_master,
        is_ready  : false,
        msg       : this.$route.params.name + " 가 방에 참가하였습니다."  ,
        no_enter  : no_enter,
      }

      if (this.$route.params.restart_counter != null ){
        this.timer = this.$route.params.restart_counter;
      }

      //console.log("entras");
    }

    setTimeout(()=>{
      //console.log(this.$route.params);
      // 입장했음을 알림
      socket.emit("enter", msg);
    }, 1000);

      // 방 정보를 받음 - 입장 했을 때 자신에게만 한 번 전송
    socket.on("announce_room_name", (room_data) => {
      this.room_name = room_data.room_name;
      this.my_key = room_data.my_key;

      this.show_page = true;
      //console.log(room_data);
    });

    //다른 채팅이 있는지 확인
    socket.on("chat", (data) => {
      switch ( data.type ) {
        case "server" :
          this.textarea += "[" + data.speaker + "]   " + data.msg + "\n";
          break; 
        case "normal" :
          this.textarea += data.speaker + " : " + data.msg + "\n";
          break;
      }
      //console.log(this.textarea);
      // console.log("chat :: ");
      // console.log(this.$refs);
      // console.log(data);
      
      // 스크롤을 자동으로 내림
      /*
      if( this.$refs.show_chat != null && this.$refs.show_chat != undefined ){
        this.$refs.show_chat.scrollTop = this.$refs.show_chat.scrollHeight;
      }
      */
    });

    // 다른 플레이어가 입장시 / 색깔 선택 시 정보를 받음
    socket.on("all_player", (data) => {
      //console.log(data);
      this.users = data;
      var num = -1;
      for(var i = 0 ; i < this.users.length; i++){
        if( this.users[i].user_key == this.my_key ){
          num = i ;
          //console.log(this.users[i].is_ready);
          //console.log(typeof(this.users[i].is_ready));
          break;
        }
      }
      this.is_master = this.users[num].is_master;
    });

    // 색을 고를 떄 같은 색이면 못고르게 하기
    socket.on("same_color", () => {
      alert("다른 플레이어와 같은 색을 고를 수 없습니다!\n다른 색을 골라주세요.");
      this.color_button = this.color_button === 'inline-block' ? 'none' : 'inline-block';
      this.back_button = this.back_button === 'none' ? 'inline-block' : 'none';
    });
/*
    //입장시 방이 가득 찼는지 확인
    socket.on("pull_room", () => {
        //console.log(res + "방이 가득 찼습니다.");
        alert("방이 가득 찼습니다. 뒤로 돌아갑니다");
        // master 면 create 로
        if( this.$route.params.is_master === 'true' ){
          this.$router.push({
            name: 'create_lobby',
            params : {
                // 이름 돌려줘야함
                count : this.$route.params.count
            }
          });
        }
        // entrans 면 enter 로
        else {
          this.$router.push({
            // 이름 돌려줘야함
            name: 'enter_lobby',
          });
        }
        
    });

    // 마스터가 아닌데 방이 없는 곳에 들어갈 경우
    socket.on("no_room", () => {
      alert("방이 아직 생성되지 않았거나 잘못된 주소입니다. 주소를 확인해주세요");
      alert("뒤로 돌아갑니다.");
      this.$router.push({
          name: 'enter_lobby'
      });
    })

    // 비밀번호가 잘못 된 경우
    socket.on("wrong_pw", () => {
      alert("비밀번호가 잘못되었습니다. 비밀번호를 확인해주세요");
      this.$router.push({
        name: 'enter_lobby'
      });
    });
*/
    // 게임 시작으로 인해 모두 방으로 이동
    socket.on("everyone_move_to_board", () => {
      this.textarea = '';
      this.$router.push({
              name: 'game_board',
              params : {
                room_name : this.room_name,
                count : this.$route.params.count,
              },
        });
    });

    // 방 나가기
    socket.on("move_room_list", ()=>{ 
      this.$router.push({
        name: 'room_list',
        params : {
          name : this.name,
          re_page : true,
          restart_counter : this.timer,
        },
      });
    });

  },
  unmounted() {
    //console.log("파괴확인");
    //console.log(this.$refs);
  },
  data() {
    return  {
      users : '',
      textarea : '',
      message : '',
      room_name : this.$route.params.room_name,
      my_key : '',
      my_name : this.$route.params.name,
      color_button : 'inline-block',
      back_button : 'none',
      toggle_game_start : "준비완료",
      is_master : "false",
      show_page : false,
      timer : 0,
    }
  },
  methods : {
    // 고른 색을 서버에 알림
    choose_color : function(color) {

        this.color_button = this.color_button === 'inline-block' ? 'none' : 'inline-block';
        this.back_button = this.back_button === 'none' ? 'inline-block' : 'none';

        let data = {
          user_name   : this.my_name,
          user_color  : color,
          user_key    : this.my_key,
          room_name   : this.room_name,
        };
        //선택한 값을 서버에 전송
        socket.emit("edit_color", data);

    },

    sending_message : function(data){
      let msg = {
        speaker : this.my_name,
        msg : data,
        type : "normal",
        room_name : this.room_name,
        }
      socket.emit("chat", msg);
    },

    back_choose_color : function() {
      this.color_button = this.color_button === 'inline-block' ? 'none' : 'inline-block';
      this.back_button = this.back_button === 'none' ? 'inline-block' : 'none';
      let data = {
          user_name   : this.my_name,
          user_color  : '',
          user_key    : this.my_key,
          room_name   : this.room_name
        }
      //선택한 값을 서버에 전송
      socket.emit("edit_color", data);
    },

    game_start : function() {

      // 색을 선택해야 시작 가능하게 변경
      for(let i = 0; i < this.users.length; i++){
        if( this.users[i].user_key == this.my_key ){
          if (this.users[i].user_color == '' || this.users[i].user_color == undefined){
            alert("색을 먼저 골라주세요!");
            return;
          }
          break;
        }
      }
      var ready_player = 0;
      // 준비 완료 시 데이터를 갱신하므로 is_ready 값이 true 인 수를 셈
      for( var i = 0 ; i < this.users.length; i++){
        if ( this.users[i].is_ready === true || this.users[i].is_ready == "true") {
          ready_player++;
        }
      }
      // master는 is_ready 값이 false 이므로 +1 
      // 방최대 인원까지 모두 모여야 게임 시작 가능하게 함
      //console.log(ready_player);
      //console.log(this.users.length);

      if( ready_player + 1 == this.users.length && this.users.length == this.$route.params.count ){
         //console.log("게임 시작 준비 완료");
        // console.log(socket);
        
        socket.emit("move_to_board_everyone", this.room_name);

      }
      else {
        alert("모든 플레이어가 준비 완료상태가 되어야 합니다.");
      }

    },

    game_ready : function(){
      var is_select_color = false;
      for( var i = 0 ; i < this.users.length; i++){
        if( this.users[i].user_key === this.my_key ){
          if( this.users[i].user_color != '' ){
            is_select_color = true;
          }
        }
      }

      if( !is_select_color ) {
        alert("색을 먼저 선택해주세요!");
        return;
      }

      if ( this.toggle_game_start === '준비완료' ){
        ready_var = true;
      }else {
        ready_var = false;
      }

      this.toggle_game_start = this.toggle_game_start === '준비완료' ? '시작대기' : '준비완료';
      var ready_var;
      
      let data = {
        is_ready : ready_var,
        user_key : this.my_key,
        room_name : this.room_name
      };

      socket.emit("lobby_ready", data);
    },

    room_out : function() {
      // 서버에 방 나감을 알리고
      let send_data = {
        room_name : this.room_name,
        user_key : this.my_key,
      };
      socket.emit("quit_room", send_data);
      // room_list로 돌아감.
      this.$router.push({
        name : 'room_list',
        params : {
          re_page : true,
          name : this.my_name,
        },
      });
    },
      

  },
}
