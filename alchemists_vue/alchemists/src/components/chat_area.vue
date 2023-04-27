<template>
    <div>
        <textarea class="rounded" id="chat_area" :ref="'show_chat' + this.timer" v-model="show_textarea" no-resize disabled></textarea>
        <input id="message_area" class="rounded shadow " v-model="message" v-on:keyup.enter="sending_message"/>
        <b-button pill id="sending" v-on:click="sending_message"> 보내기 </b-button>
    </div>
</template>


<script>

export default {
    name : 'chat_area',
    props: ['textarea_val', 'timer'],

    data () {
        return {
            message : "",
            show_textarea : this.textarea_val,
        }
    },
    
    watch : {
        textarea_val() {
            //console.log(this.textarea_val);
            this.show_textarea = this.textarea_val;
            Object.keys(this.$refs).forEach(el => {
                if (el.startsWith("show")){
                    this.$refs[el].scrollTop = this.$refs[el].scrollHeight;
                    return false;
                }
            });
        }
    },
    methods : {
        sending_message : function(){
            if( this.message !== '' ){
                var msg = this.message;
                //console.log(msg);
                this.message = '';
                this.$emit('sending_message', msg);
            }
        },

    }
};

</script>

<style scoped src="@/assets/css/game_lobby.css"></style>