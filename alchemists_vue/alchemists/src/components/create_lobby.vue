<template>
    <div id="create_lobby" class="lobby">
        <p id="title"> 게임 생성 </p>
        <div id="create_info">

            <p class="help"> 방의 이름을 작성해주세요 </p>
            <input class="input_box" type="text" v-model="room_name" placeholder="방 이름 작성"/>
            <div class="margin_area"></div>
            <p class="help"> 방의 비밀번호를 작성해주세요 </p>
            <input class="input_box" type="text" v-model="room_pw" placeholder="비밀번호 작성"/>
            <div class="margin_area"></div>
            <p class="help"> 게임 인원 </p>
            <p class="sm_help"> 게임인원이 모두 차야 게임을 시작 할 수 있습니다. </p>
            <select v-model="selected" id="room_max_count_select">
                <option v-for="board in list" :value="board.val" :key="board">
                        {{board.selected}}</option>
            </select>

            <div class="margin_area"></div>
            <button class="create_game" v-on:click="create_game" > 로비 생성 </button>
            <br>
            <button class="back_main" v-on:click="back_first_page"> 돌아가기 </button>
            <div class="end_area"></div>
        </div>
    </div>
</template>

<script>

export default {
    el : '#create_lobby',
    name: 'create_lobby',
     props: {
        // 이미 있는 room_list
        room_list : {
            type : Array,
            required : true
        },
        name : {
            type : String,
        },
        re_page : {
            type : Boolean,
        }
    },
    created() {
        //console.log(this.$route.params);
    },
    data() {
        return {
            room_name : '',
            room_pw : '',
            count : '',
            selected : '2',
            list : [
                {selected: '2인', val: 2},
                {selected: '3인', val: 3},
                {selected: '4인', val: 4}
            ]
        }
    },
    methods: {
        create_game : function() {
            // 방이 이미 있는지 검사해야함.
            for( let i = 0; i < this.room_list.length; i++ ){
                if( this.room_list[i].name == this.room_name ){
                    //방이름이 같으므로 생성 불가
                    alert("이미 같은 이름의 방이 존재합니다.\n방 이름을 변경해주세요!");
                    return;
                }
            }

            this.$router.push({
                name: 'game_lobby',
                params: {
                    room_name: this.room_name,
                    room_pw: this.room_pw,
                    count: this.selected,
                    name: this.name,
                    is_master : true,
                    restart_counter : this.re_page,
                }
            });
        },
        back_first_page : function() {
            this.$emit("back_page");
        },
        change_count : function(cnt) {
            console.log(cnt);
            this.selected = cnt;
        }
    },

}

</script>

<style scoped src="@/assets/css/create_lobby.css"></style>