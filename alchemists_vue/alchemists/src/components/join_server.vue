<template>
    <div id="join_server_div">
        <div id="form_div">
            <div id="form_group_top_div">
                <div id="form_group_bottom_div">     
                    <div id="form_group_div">
                        <b-form-group
                            id="server_info_input_group_1"
                            class="input_group"
                            label="IP address:"
                            label-for="ip_input"
                            prepend="ip"
                            description="서버를 연 사람의 ip를 입력하세요"
                        >
                            <b-form-input
                                id="input-1"
                                v-model="this.form.ip"
                                placeholder="접속할 서버 ip를 입력해주세요"
                                required
                            ></b-form-input>
                        </b-form-group>

                        <b-form-group
                            id="server_info_input_group_2"
                            class="input_group"
                            label="닉네임:"
                            label-for="name_input"
                            prepend="닉네임"
                        >
                            <b-form-input
                                id="name_input"
                                v-model="this.form.name"
                                placeholder="사용할 닉네임을 입력해주세요"
                                required
                            >
                            </b-form-input>
                        </b-form-group>
                    </div>
                </div>
            </div>
        </div>
        <div id="join_btn_area">
            <b-button type="button" id="btn_join_server" v-on:click="join_to_server">접속</b-button>
        </div>
    </div>
</template>

<script>

export default ({
    name : 'join_server',

    data () {
        return {
            form: {
                ip: '',
                name: '',
            },
        }
    },
    methods : {
        join_to_server : function() {
            if (this.form.name.length < 1){
                alert("이름을 입력해주세요!");
                return;
            }
            this.$emit('loading_start', this.form);
        }
    }
})
</script>
<style scoped src="@/assets/css/first_page.css"></style>
