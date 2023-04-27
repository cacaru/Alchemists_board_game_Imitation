<template>
    <b-container>
        <b-row v-if="this.my_data.user_key == this.my_key " >
            <b-col v-for="( card , name ) in this.my_data.user_ingame_data.ingredient" :key="name">
                <span class="card_and_count" v-if="name == 'card_1' && card > 0" >
                    <img class="exhibit_ingre_icon" ref="exhibit_card_1" src="@/assets/img/material_card/card_1.png" @click="exhibit_select_ingre(name)" />
                    <span> {{ card }} 장</span>
                </span>
                <span class="card_and_count" v-else-if="name == 'card_2' && card > 0" >
                    <img class="exhibit_ingre_icon" ref="exhibit_card_2" src="@/assets/img/material_card/card_2.png" @click="exhibit_select_ingre(name)" />
                    <span>{{ card }} 장</span>
                </span>
                <span class="card_and_count" v-else-if="name == 'card_3' && card > 0" >
                    <img class="exhibit_ingre_icon" ref="exhibit_card_3" src="@/assets/img/material_card/card_3.png" @click="exhibit_select_ingre(name)" />
                    <span>{{ card }} 장</span>
                </span>
                <span class="card_and_count" v-else-if="name == 'card_4' && card > 0" >
                    <img class="exhibit_ingre_icon" ref="exhibit_card_4" src="@/assets/img/material_card/card_4.png" @click="exhibit_select_ingre(name)" />
                    <span>{{ card }} 장</span>
                </span>
                <span class="card_and_count" v-else-if="name == 'card_5' && card > 0" >
                    <img class="exhibit_ingre_icon" ref="exhibit_card_5" src="@/assets/img/material_card/card_5.png" @click="exhibit_select_ingre(name)"  />
                    <span>{{ card }} 장</span>
                </span>
                <span class="card_and_count" v-else-if="name == 'card_6' && card > 0" >
                    <img class="exhibit_ingre_icon" ref="exhibit_card_6" src="@/assets/img/material_card/card_6.png" @click="exhibit_select_ingre(name)" />
                    <span>{{ card }} 장</span>
                </span>
                <span class="card_and_count" v-else-if="name == 'card_7' && card > 0" >
                    <img class="exhibit_ingre_icon" ref="exhibit_card_7" src="@/assets/img/material_card/card_7.png" @click="exhibit_select_ingre(name)" />
                    <span>{{ card }} 장</span>
                </span>
                <span class="card_and_count" v-else-if="name == 'card_8' && card > 0" >
                    <img class="exhibit_ingre_icon" ref="exhibit_card_8" src="@/assets/img/material_card/card_8.png" @click="exhibit_select_ingre(name)" />
                    <span>{{ card }} 장</span>
                </span>

            </b-col>
        </b-row>
    </b-container>
</template>

<script>

export default {
    name : "show_ingredient_card",
    props : {
        my_data : {
            type : Object,
        },
        my_key : {
            type : String,
        }
    },
    data() {
        return {
            test_ingredient_list : [],
            init_checker : 0,
        }
    },
    watch : {
        init_checker() {
            this.init_boarder();
        }
    },

    methods : {
        // 물약 발표회에서 발표할 물약의 재료 2가지를 선택할 함수
        // name :: card_1~8
        exhibit_select_ingre : function(name) {
            // 이미 2가지가 선택되어 있으면 가장 먼저 들어온 1개를 제거하고 마지막에 들어온 한개를 진행
            if( this.test_ingredient_list.length == 2){
                // 먼저 들어온 1개를 제거
                let before_name = this.test_ingredient_list.shift();
                let num = 0;
                switch(before_name) {
                    case 'card_1' :
                        num = this.$refs.exhibit_card_1[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.exhibit_card_1[0].className = this.$refs.exhibit_card_1[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_2' :
                        num = this.$refs.exhibit_card_2[0].className.indexOf('test_border');
                        if( num > 0 ){
                            this.$refs.exhibit_card_2[0].className = this.$refs.exhibit_card_2[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_3' :
                        num = this.$refs.exhibit_card_3[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.exhibit_card_3[0].className = this.$refs.exhibit_card_3[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_4' :
                        num = this.$refs.exhibit_card_4[0].className.indexOf('test_border');
                        if (num > 0 ) {
                            this.$refs.exhibit_card_4[0].className = this.$refs.exhibit_card_4[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_5' :
                        num = this.$refs.exhibit_card_5[0].className.indexOf('test_border');
                        if( num > 0 ){
                            this.$refs.exhibit_card_5[0].className = this.$refs.exhibit_card_5[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_6' :
                        num = this.$refs.exhibit_card_6[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.exhibit_card_6[0].className = this.$refs.exhibit_card_6[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_7' :
                        num = this.$refs.exhibit_card_7[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.exhibit_card_7[0].className = this.$refs.exhibit_card_7[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_8' :
                        num = this.$refs.exhibit_card_8[0].className.indexOf('test_border');
                        if ( num > 0 ) {
                            this.$refs.exhibit_card_8[0].className = this.$refs.exhibit_card_8[0].className.substring(0, num-1);
                        }
                        break;                                  
                }
            }
            // 변수에 추가
            this.test_ingredient_list.push(name);
            let ref_str = '';
            switch(name){
                case 'card_1':
                    ref_str = this.$refs.exhibit_card_1;
                    break;
                case 'card_2':
                    ref_str = this.$refs.exhibit_card_2;
                    break;
                case 'card_3':
                    ref_str = this.$refs.exhibit_card_3;
                    break;
                case 'card_4':
                    ref_str = this.$refs.exhibit_card_4;
                    break;
                case 'card_5':
                    ref_str = this.$refs.exhibit_card_5;
                    break;
                case 'card_6':
                    ref_str = this.$refs.exhibit_card_6;
                    break;
                case 'card_7':
                    ref_str = this.$refs.exhibit_card_7;
                    break;
                case 'card_8':
                    ref_str = this.$refs.exhibit_card_8;
                    break;
            }
            ref_str[0].className += " test_border";

            this.$emit("test_ingredient_arr", this.test_ingredient_list);
        },

        init_boarder : function(){
            let num = 0;
            // 초기화전 test_boarder 초기화
            for(let i = 0; i < 2; i++ ){
                switch(this.test_ingredient_list[i]) {
                    case 'card_1' :
                        num = this.$refs.test_card_1[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.test_card_1[0].className = this.$refs.test_card_1[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_2' :
                        num = this.$refs.test_card_2[0].className.indexOf('test_border');
                        if( num > 0 ){
                            this.$refs.test_card_2[0].className = this.$refs.test_card_2[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_3' :
                        num = this.$refs.test_card_3[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.test_card_3[0].className = this.$refs.test_card_3[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_4' :
                        num = this.$refs.test_card_4[0].className.indexOf('test_border');
                        if (num > 0 ) {
                            this.$refs.test_card_4[0].className = this.$refs.test_card_4[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_5' :
                        num = this.$refs.test_card_5[0].className.indexOf('test_border');
                        if( num > 0 ){
                            this.$refs.test_card_5[0].className = this.$refs.test_card_5[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_6' :
                        num = this.$refs.test_card_6[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.test_card_6[0].className = this.$refs.test_card_6[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_7' :
                        num = this.$refs.test_card_7[0].className.indexOf('test_border');
                        if( num > 0 ) {
                            this.$refs.test_card_7[0].className = this.$refs.test_card_7[0].className.substring(0, num-1);
                        }
                        break;
                    case 'card_8' :
                        num = this.$refs.test_card_8[0].className.indexOf('test_border');
                        if ( num > 0 ) {
                            this.$refs.test_card_8[0].className = this.$refs.test_card_8[0].className.substring(0, num-1);
                        }
                        break;
                }
            }
        }
    }
}

</script>

<style scoped>

img.exhibit_ingre_icon {
    width: 80px;
}

img.exhibit_ingre_icon.test_border, img.ingre_pre_ele_icon.selected, img.ingre_pre_ingre_icon.test_border {
    border: 3px solid skyblue;
    border-radius: 12px;
}

</style>