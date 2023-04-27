<template>
    <div id="end_page">
        <p id="main_title">알케미스트</p>
        <div id="game_result_section">
            <div id="area_top">
                <b-container id="background_text_area">
                    <b-row class="data_row">
                        <b-col>
                            이름
                        </b-col>
                        <b-col>
                            점수
                        </b-col>
                    </b-row>
                    <b-row class="data_row" v-for="data in this.game_result" :key="data">
                        <b-col class="data_col" col="1">
                            {{ data.name }}
                        </b-col>
                        <b-col class="data_col" col="1">
                            {{ data.score }}
                        </b-col>
                    </b-row>
                </b-container>
            </div>

            <div id="end_page_btn_extra_area">
                <div id="end_page_btn_extra_area_bot">
                    <div id="end_page_btn_extra_area_inner">
                        <span class="option_show_span">{{ this.restart_member_count }}명이 다시하기를 원합니다!</span>
                    </div>
                </div>
            </div>

            <div id="end_page_btn_area">
                <button class="main_button restart_btn" v-if="this.cancel_restart_val == false" v-on:click="want_restart" >다시 하기</button>
                <button class="main_button re_cancel_btn" v-if="this.cancel_restart_val == true" v-on:click="cancel_restart" >취소 하기</button>
                <button class="main_button close_btn" v-if="this.cancel_restart_val == false" v-on:click="close_game">게임 나가기</button>
            </div>

        </div>
    </div>
</template>

<script src="@/assets/script/end_page.js">

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style src="@/assets/css/end_page.css" scoped></style>
