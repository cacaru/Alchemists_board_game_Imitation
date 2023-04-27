<template>
    <div id="first_page">
        <p id="main_title">알케미스트</p>
        <div id="press_start" >
            <div class="is_main_button_group" v-if="this.is_join_server == false">
                <button class="main_button" @click="join_server">접속하기</button>
            </div>
            <div class="is_join_server_div" v-if="this.is_join_server">
                <join_server
                    @loading_start="loading_start"
                ></join_server>
            </div>
            <div class="is_join_server_loading" v-if="this.loading">
                <div id="loading_div">
                    <div id="spin">
                        <b-spinner type="grow" variant="success"></b-spinner>
                    </div>
                    <div id="loading_ment">
                        접속중
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script src="@/assets/script/first_page.js">
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style src="@/assets/css/first_page.css" scoped></style>
