<template>
    <div id="connect_success" v-if="this.loading == false">
        <div id="room_list" v-if="this.listing == true">
            <div id="title_area">
                방 목록
            </div>
            <div class="room_list_area" v-if="this.room_list != ''">
                
                <b-card
                    v-for="room in this.room_list"
                    :key="room"
                    class="room_card"
                    :footer="room.count + '/' + room.max_count"
                    footer-tag="footer"
                    :title="room.name"
                    v-on:click="pw_modal_on(room.name)"
                >                    
                </b-card>
                
            </div>
            <div class="room_list_area" v-else>
                <b-card
                    class="room_card"
                    title="현재 서버에 열린 방이 없습니다."
                    body-class="back_color"
                >                    
                </b-card>
            </div>
            <div id="room_create_area">
                <b-button class="room_create_btn" v-on:click="create_room">
                    방만들기
                </b-button>
            </div>
        </div>

        <div v-else-if="this.listing == false"> 
            <Create_lobby
                v-bind:re_page="this.re_page"
                v-bind:name="this.name"
                v-bind:room_list="this.room_list"
                @back_page="back_page"
            >
            </Create_lobby>
        </div>

        <b-modal
            id="check_pw_modal" 
            centered 
            v-model="check_pw_modal"
            ref="check_pw_modal" 
            header-text-variant="light"
            header-bg-variant="success"
            hide-footer
            hide-header-close
            :title="this.want_room + ' 참가하기'"
            >
            <div id="check_pw_modal_div">
                <b-form-group
                    id="check_pw_input"
                    class="input_group"
                    label="비밀번호"
                    label-for="pw_input"
                    prepend="닉네임"
                >
                    <b-form-input
                        id="pw_input"
                        v-model="this.room_pw"
                        placeholder="올바른 비밀번호를 입력해주세요"
                        required
                    >
                    </b-form-input>
                </b-form-group>
                <div class="btn_area">
                    <b-button class="join_room_btn" variant="primary" v-on:click="enter_room">들어가기</b-button>
                </div>
            </div>
        </b-modal>

    </div>
    <div id="loading_div" v-else>
        <p id="loading_title">
            접속중입니다.
        </p>
        <b-spinner style="width: 100px; height: 100px;" label="Spinning"></b-spinner>
    </div>
</template>

<script src="@/assets/script/room_list.js">
</script>

<style scoped src="@/assets/css/room_list.css">
</style>