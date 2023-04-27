<template>
  <div v-if="this.show_page">
    <b-container id="game_lobby">
      <b-row>
        <b-col>
          <div id="room_title">
              {{ room_name }}
          </div>
        </b-col>
      </b-row>
      <b-row id="room_back">
        <b-col>
          <b-container id="room_back">
            <b-row align-v="stretch" v-for="i in Math.ceil(users.length / 2 )" :key="i">
              <b-col sm="6" v-for="entrans in users.slice((i - 1) * 2, i * 2)" :key="entrans">
                
                <div>
                  <b-overlay v-bind:show="entrans.is_ready == true || entrans.is_ready == 'true'" class="entrans" rounded="lg" >
                    <p class="inline name" v-if="entrans.is_master == 'true'">방장</p>
                    
                    <p class="inline user_name"> {{ entrans.user_name }} </p>
                    
                    <div class="choose_color_section">
                      <p class="user_color_announce" v-if="entrans.user_color == '' "> 색 선택 중 </p>
                      <p class="user_color_announce" v-else > 고른 색 </p>
                      <div class="button_area" v-if="my_key === entrans.user_key">
                        <b-icon-funnel-fill v-on:click="choose_color('red')" v-bind:style="{display : color_button }" style="width : 45px; height: 45px; color:red; transform: rotate(180deg);"></b-icon-funnel-fill>
                        <b-icon-funnel-fill v-on:click="choose_color('blue')" v-bind:style="{display : color_button }" style="width : 45px; height: 45px; color:blue; transform: rotate(180deg);"></b-icon-funnel-fill>
                        <b-icon-funnel-fill v-on:click="choose_color('black')" v-bind:style="{display : color_button }" style="width : 45px; height: 45px; color:black; transform: rotate(180deg);"></b-icon-funnel-fill>
                        <b-icon-funnel-fill v-on:click="choose_color('white')" v-bind:style="{display : color_button }" style="width : 45px; height: 45px; color:white; transform: rotate(180deg);"></b-icon-funnel-fill>
                        <b-icon-funnel-fill v-on:click="back_choose_color" v-bind:style="{display : back_button , color: entrans.user_color }" style="width : 45px; height: 45px; transform: rotate(180deg);"></b-icon-funnel-fill>
                        <p class="announce" v-bind:style="{display : back_button }"> 한 번 더 누르면 고른 색이 취소됩니다. </p>
                      </div>
                      <div v-else>
                        <b-icon-funnel-fill v-bind:style="{ color : entrans.user_color }" style="width : 45px; height: 45px; transform: rotate(180deg);"/>
                      </div>
                    </div>

                    <template v-slot:overlay>
                      <div class="overlay_ready">
                          <b-icon-funnel style="width:100px; height:100px;" />
                          <p id="check_label">준비 완료</p>
                      </div>
                    </template>
                  </b-overlay>
                </div>
              </b-col>
            </b-row>
          </b-container>
        </b-col>
      </b-row>

      <b-row id="end_content">
        <b-col sm="8">
            <Chat_area
              :textarea_val="this.textarea"
              :timer="this.timer"
              @sending_message="sending_message"
            >
            </Chat_area>
        </b-col>
        <b-col>
          <b-button pill class="game_start" v-on:click="game_start" v-if="this.is_master === 'true' "> 게임 시작 </b-button>
          <b-button pill class="game_start" v-on:click="game_ready" v-else> {{ this.toggle_game_start }} </b-button>
          <b-button pill class="room_out_btn" v-on:click="room_out">방 나가기</b-button>
        </b-col>
      </b-row>
    </b-container>
  </div>

  <div v-else>
    <div id="loading_div">
      <div id="loading_title">
        입장중입니다!
      </div>
      <div id="loading_spin_div">
        <b-spinner style="width: 100px; height: 100px;" label="Spinning"></b-spinner>
      </div>
    </div>
  </div>
</template>

<script src="@/assets/script/game_lobby.js">
</script>

<style scoped src="@/assets/css/game_lobby.css"></style>