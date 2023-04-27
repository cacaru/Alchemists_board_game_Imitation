<template>
    <div id="select_user_mount">
        <p> 몇 명이서 하나요?</p>
        <select v-model="selected">
            <option v-for="board in list" :value="board.val" :key="board">
                {{board.selected}}</option>
        </select>
        <div></div>
        <button class="start_game" v-on:click="create_lobby">게임 생성</button>
    </div>    
</template>

<script>
export default ({
    name : 'select_user_mount',

    data () {
        return {
            selected : '2',
            list : [
                {selected: '2명', val: 2},
                {selected: '3명', val: 3},
                {selected: '4명', val: 4}
            ]
        }
    },
    methods : {
        create_lobby : function(){
            this.$router.push({
                name: 'create_lobby',
                params : {
                    count : this.selected
                }
            })

            console.log("click " + this.selected);
        }
    }
})
</script>
<style scoped src="@/assets/css/first_page.css"></style>
