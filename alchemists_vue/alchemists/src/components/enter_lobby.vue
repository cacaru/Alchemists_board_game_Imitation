<template>
    <div id="enter_lobby" class="lobby">
        <p id="title"> 게임 참가 </p>
            <div id="create_info">
                <p class="help"> 접속할 아이피를 작성해주세요 </p>
                <input class="input_box long" type="text" v-model="ip_number" /> 
                <div class="margin_area"></div>
                <p class="help"> 접속할 포트번호를 작성해주세요 </p>
                <input class="input_box" type="text" v-model="port_number" placeholder="열 포트번호를 써주세요"/>
                <div class="margin_area"></div>
                <p class="help"> 방의 비밀번호를 작성해주세요 </p>
                <input class="input_box" type="password" v-model="room_pw" placeholder="비밀번호 작성"/>
                <div class="margin_area"></div>
                <p class="help"> 게임 인원 </p>
                <p id="menber_count"> {{ this.$route.params.count }} </p>
                <div class="margin_area"/>
                <span class="help"> 사용할 닉네임 </span><input class="input_box" type="text" v-model="nick_name" placeholder="닉네임" />
                <div class="margin_area"></div>
                <button class="create_game" v-on:click="enter_lobby" > 로비 참가 </button>
                <div class="margin_area"></div>
                <button class="back_main" v-on:click="back_first_page"> 돌아가기 </button>
                <div class="end_area"></div>
            </div>
    </div>
</template>

<script>

export default {
    el : '#enter_lobby',
    name: 'enter_lobby',
    created() {

    },
    data() {
        return {
            port_number : '8080',
            ip_number : '',
            nick_name : '',
            room_pw : ''
        }
    },
    methods: {
        enter_lobby : function() {
            // console.log(this.port_number);
            // console.log(this.ip_address);
            // console.log(this.room_pw);
            // console.log(this.nick_name);
            this.$router.push({
                name: 'game_lobby',
                params: {
                    room_address : this.ip_address,
                    room_pw: this.room_pw,
                    port: this.port_number,
                    is_master : false,
                    nick_name: this.nick_name,
                }
            });
        },
        back_first_page : function() {
            this.$router.push({
                name: 'first_page'
            })
        }
    },
    watch : {
        port_number()    {
            return this.port_number = this.port_number.replace(/[^0-9]/g, '');
        }
    }

}

</script>

<style scoped src="@/assets/css/create_lobby.css"></style>